using System.ComponentModel;
using Newtonsoft.Json;
using System.Configuration;
using System.Data;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WorkdayCalculator
{
    /// <summary>
    /// The input form.
    /// </summary>
    public partial class InputForm : Form
    {

        #region Private Variables

        private BindingList<Holiday> _holidayList;
        private DateTimePicker _cellDateTimePicker;
        private TimeOnly _startTime = new(8, 00);
        private TimeOnly _endTime = new(16, 00);
        private DateTime _inputDate = DateTime.Now;
        private Direction _inputDirection = Direction.Add;
        private double _inputDays;
        private List<DateTime> _recurringHolidays = new();
        private List<DateTime> _nonRecurringHolidays = new();
        private string _holidayFilePath = ConfigurationManager.AppSettings["HolidayFilePath"];
        private string _workingHourFilePath = ConfigurationManager.AppSettings["WorkingHourFilePath"];
        private string _logFilePath = ConfigurationManager.AppSettings["LogFilePath"];

        #endregion

        public InputForm()
        {
            InitializeComponent();

            //Set up the calendar UI control for the holiday list grid
            this._cellDateTimePicker = new DateTimePicker();
            this._cellDateTimePicker.ValueChanged += new EventHandler(cellDateTimePickerValueChanged);
            this._cellDateTimePicker.Visible = false;
            this._cellDateTimePicker.Format = DateTimePickerFormat.Custom;
            this._cellDateTimePicker.CustomFormat = "dd/MM/yyyy";
            this.holidayDataGridView.Controls.Add(_cellDateTimePicker);

            //Set the default start values
            inputDateTimePicker.Value = _inputDate;
            addRadioButton.Checked = true;
            dayTextBox.Text = _inputDays.ToString();

            //Load the pre-configured holiday list if exists
            LoadDataFromJson();
        }

        /// <summary>
        /// Loads the pre-configured holiday details amd working hours from json.
        /// Will be blank first time, but retains the values saved by the user after that
        /// </summary>
        private void LoadDataFromJson()
        {
            if (File.Exists(_holidayFilePath))
            {
                var jsonData = File.ReadAllText(_holidayFilePath);
                _holidayList = JsonConvert.DeserializeObject<BindingList<Holiday>>(jsonData);
            }
            else
            {
                _holidayList = new BindingList<Holiday>();
            }

            if (File.Exists(_workingHourFilePath))
            {
                var jsonData = File.ReadAllText(_workingHourFilePath);
                var times = JsonConvert.DeserializeObject<dynamic>(jsonData);
                _startTime = times.dayStart;
                _endTime = times.dayEnd;
            }

            // Set the value of the start date time picker to today's date with the start time.
            startDateTimePicker.Value = DateTime.Now.Date + _startTime.ToTimeSpan();

            // Set the value of the end date time picker to today's date with the end time.
            endDateTimePicker.Value = DateTime.Now.Date + _endTime.ToTimeSpan();

            // Set the data source of the holiday data grid view to the loaded holiday list.
            holidayDataGridView.DataSource = _holidayList;
        }

        /// <summary>
        /// Event handler for the click event of the calculate button.
        /// Calculates workdays based on input values and updates the result accordingly.
        /// </summary>
        private void btnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                //Validate holiday setup for avoiding manual input errors
                if (InvalidHolidaySetup()) return;

                if (_inputDays <= 0)
                {
                    MessageBox.Show("Please enter a positive numeric value greater than 0 for calculation");
                }
                else
                {
                    // Adjust input date to remove seconds and milliseconds for accuracy.
                    _inputDate = _inputDate.AddSeconds(-1 * _inputDate.Second);
                    _inputDate = _inputDate.AddMilliseconds(-1 * _inputDate.Millisecond);

                    // Calculate workdays based on input date, days, and direction, then update the result label.
                    lblResult.Text = CalculateWorkdays(_inputDate, _inputDays, _inputDirection);
                }
            }
            catch(Exception ex)
            {
                File.AppendAllText(_logFilePath+ DateTime.Now.ToString("yyyyMMddHHmmss")+".txt", ex.ToString());
                MessageBox.Show("Unexpected error. Please check log files for details");
            }
        }


        /// <summary>
        /// Calculates workdays based on the given start date, number of days to modify, and direction.
        /// </summary>
        /// <param name="startDate">The starting date for calculation.</param>
        /// <param name="daysToModify">The number of days to modify, which may include fractional days.</param>
        /// <param name="direction">The direction of modification, either addition or subtraction.</param>
        /// <returns>A string representing the calculated workday.</returns>
        public string CalculateWorkdays(DateTime startDate, double daysToModify, Direction direction)
        {
            // Prepare lists of holidays for comparison
            _nonRecurringHolidays= _holidayList.Where(p => !p.IsRecurring).Select(p => p.Date).ToList();
            _recurringHolidays = _holidayList.Where(p => p.IsRecurring).Select(p => p.Date).ToList();


            var daysToCalculate = Math.Floor(daysToModify); // Ensure positive number of days
            var hoursToCalculate = daysToModify - daysToCalculate; // Handle fractional day inputs
            var step = direction == Direction.Add ? 1 : -1;
            var dayStart = _startTime.ToTimeSpan();
            var dayEnd = _endTime.ToTimeSpan();
            var hoursPerDay = dayEnd - dayStart;

            DateTime resultDate = new();

            // If the input start date falls on a holiday or weekend,
            // pick the next/previous workday to start the calculation
            resultDate = GetNextWorkday(startDate, direction == Direction.Add ? -1 : 1);
            if (resultDate != startDate)
            {
                resultDate = ResetToWorkingHours(0, direction == Direction.Add ? dayEnd : dayStart, TimeSpan.Zero, resultDate);
            }

            // Once we are on the correct day to start the calculation,
            // Reset to working hours if we are outside the working timeframe
            int dayChange = 0;
            TimeSpan resetToTime = TimeSpan.Zero;

            if (resultDate.TimeOfDay <= dayStart)
            {
                dayChange = (direction == Direction.Add) ? -1 : 0;
                resetToTime = (direction == Direction.Add) ? dayEnd : dayStart;
                resultDate = ResetToWorkingHours(dayChange, resetToTime, TimeSpan.Zero, resultDate);
            }

            if (resultDate.TimeOfDay >= dayEnd)
            {
                dayChange = (direction == Direction.Add) ? 0 : 1;
                resetToTime = (direction == Direction.Add) ? dayEnd : dayStart;
                resultDate = ResetToWorkingHours(dayChange, resetToTime, TimeSpan.Zero, resultDate);
            }

            // Iterate the number of days to be calculated
            while (daysToCalculate > 0)
            {
                resultDate = resultDate.AddDays(step); // Increment or decrement date by one day
                resultDate = GetNextWorkday(resultDate, step);
                daysToCalculate--;
            }

            //Logic for handling partial days if any, if input is a decimal
            if (hoursToCalculate > 0)
            {
                resultDate = resultDate.AddHours(step * hoursToCalculate * hoursPerDay.TotalHours);

                // If the results go outside the working hours, reset to the next working hours
                if (resultDate.TimeOfDay > dayEnd && direction == Direction.Add)
                {
                    var offset = resultDate.TimeOfDay - dayEnd;
                    resultDate = ResetToWorkingHours(step, dayStart, offset, resultDate);
                }

                if (resultDate.TimeOfDay < dayStart && direction == Direction.Subtract)
                {
                    var offset = resultDate.TimeOfDay - dayStart;
                    resultDate = ResetToWorkingHours(step, dayEnd, offset, resultDate);
                }
            }

            // Return the calculated workday as a formatted string
            return resultDate.ToString("dd/MM/yyyy HH:mm");
        }


        /// <summary>
        /// Gets the next/previous workday based on the given date and step. Returns the same date if the passed date is a working day itself.
        /// </summary>
        /// <param name="date">The starting date.</param>
        /// <param name="step">The step size for finding the next workday based on addition or subtraction</param>
        /// <returns>The next/previous workday after the given date.</returns>
        private DateTime GetNextWorkday(DateTime date, int step)
        {
            // Initialize the return date with the given date.
            var returnDate = date;

            // Loop until the return date falls on a workday or is not a holiday.
            while (returnDate.DayOfWeek == DayOfWeek.Saturday || returnDate.DayOfWeek == DayOfWeek.Sunday 
                                                              || _nonRecurringHolidays.Contains(returnDate.Date) 
                                                              || _recurringHolidays.Any(p=>p.Day==returnDate.Day && p.Month==returnDate.Month))
            {
                // Add the step to the return date.
                returnDate = returnDate.AddDays(step);
            }
            return returnDate;
        }


        /// <summary>
        /// Resets the given result date to working hours based on the specified parameters.
        /// </summary>
        /// <param name="step">The step size for adjusting the result date.</param>
        /// <param name="resetTime">The day start or end time to reset the result date to, within the working hours </param>
        /// <param name="offset">The offset to apply to the reset time.</param>
        /// <param name="resultDate">The date to be reset.</param>
        /// <returns>The result date reset to working hours.</returns>
        private DateTime ResetToWorkingHours(int step, TimeSpan resetTime, TimeSpan offset, DateTime resultDate)
        {
            // Increment the result date by the specified step.
            resultDate = resultDate.AddDays(step);

            // Get the next workday for the incremented result date.
            resultDate = GetNextWorkday(resultDate.Date, step);

            // Reset the result date to the specified reset time within working hours and apply the offset.
            resultDate = resultDate.Date.Add(resetTime).Add(offset);

            // Return the result date reset to working hours.
            return resultDate;
        }

        #region HolidayListActions

        /// <summary>
        /// Event handler for the click event of the delete button.
        /// Deletes the selected holiday from the list and updates the DataGridView.
        /// </summary>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Check if any row is selected in the DataGridView.
            if (holidayDataGridView.SelectedRows.Count > 0 && _holidayList.Count>0)
            {
                // Get the index of the selected row.
                int selectedIndex = holidayDataGridView.SelectedRows[0].Index;

                // Remove the holiday at the selected index from the list.
                _holidayList.RemoveAt(selectedIndex);

                // Refresh the DataGridView to reflect the changes.
                holidayDataGridView.DataSource = null; // Unbind the DataGridView temporarily
                holidayDataGridView.DataSource = _holidayList; // Rebind the DataGridView
                // SaveDataToJson(); // Optionally save data to JSON here.
            }
            else
            {
                // If no row is selected, prompt the user to select a row for deletion.
                MessageBox.Show("Select a row to delete.");
            }
        }


        /// <summary>
        /// Event handler for the click event of the save button.
        /// Validates the holiday list and saves data to JSON if valid.
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (InvalidHolidaySetup()) return;
            // Save the data to JSON.
            SaveDataToJson();

            // Display a message box indicating successful data saving.
            MessageBox.Show("Data saved successfully.");
        }

        private bool InvalidHolidaySetup()
        {
            // Check if any holiday in the list has null date or empty name.
            if (_holidayList.Any(day => day.Date == null || string.IsNullOrWhiteSpace(day.Name)))
            {
                // Display a message box to prompt the user to provide mandatory details.
                MessageBox.Show("Please provide the mandatory details for the holiday list");
                return true;
            }

            if (_endTime <= _startTime)
            {
                MessageBox.Show("Please review the working hour start and end");
                return true;
            }

            return false;
        }


        /// <summary>
        /// Saves holiday list data and working hours to JSON files.
        /// </summary>
        private void SaveDataToJson()
        {
            var holidayJsonData = JsonConvert.SerializeObject(_holidayList, Formatting.Indented);
            File.WriteAllText(_holidayFilePath, holidayJsonData);

            // Serialize the start time and end time to JSON format.
            var workingHoursJson = JsonConvert.SerializeObject(new
            {
                dayStart = _startTime,
                dayEnd = _endTime
            });
            File.WriteAllText(_workingHourFilePath, workingHoursJson);
        }
        #endregion

        #region EventHandlers

        private void dayTextBox_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(dayTextBox.Text)) return;
            if (!Double.TryParse(dayTextBox.Text, out var val))
            {
                MessageBox.Show("Please enter a numeric value");
            }
            //Make sure that the final calculation will result in a valid date
            else if(val>500000)
            {
                MessageBox.Show("Your input will result in an invalid date. Please enter a value less than 500000");
            }
            else
            {
                _inputDays = val;
            }
        }
        private void inputDateTimePicker_TextChanged(object sender, EventArgs e)
        {
            _inputDate = inputDateTimePicker.Value; // Update the variable with the text from the TextBox
        }
        private void startDateTimePicker_TextChanged(object sender, EventArgs e)
        {
            _startTime = TimeOnly.FromDateTime(startDateTimePicker.Value); // Update the variable with the text from the TextBox
        }
        private void endDateTimePicker_TextChanged(object sender, EventArgs e)
        {
            _endTime = TimeOnly.FromDateTime(endDateTimePicker.Value); // Update the variable with the text from the TextBox
        }
        private void addRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (addRadioButton.Checked)
            {
                _inputDirection = Direction.Add;
            }
        }
        private void subRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (subRadioButton.Checked)
            {
                _inputDirection = Direction.Subtract;
            }
        }
        void cellDateTimePickerValueChanged(object sender, EventArgs e)
        {
            holidayDataGridView.CurrentCell.Value = _cellDateTimePicker.Value.ToString("dd/MM/yyyy");
            _cellDateTimePicker.Visible = false;
        }
        private void holidayGrid_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            var index = holidayDataGridView.CurrentCell.ColumnIndex;
            if (index == 1)
            {
                Rectangle tempRect = this.holidayDataGridView.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                _cellDateTimePicker.Location = tempRect.Location;
                _cellDateTimePicker.Width = tempRect.Width;
                //cellDateTimePicker.Format = DateTimePickerFormat.Short;
                try
                {
                    if (holidayDataGridView.CurrentCell.Value != null)
                    {
                        _cellDateTimePicker.Value = DateTime.Parse(holidayDataGridView.CurrentCell.Value.ToString());
                    }
                    else
                    {
                        _cellDateTimePicker.Value = DateTime.Now.Date;
                    }
                }
                catch
                {
                    _cellDateTimePicker.Value = DateTime.Now.Date;
                }
                _cellDateTimePicker.Visible = true;
            }
        }

        #endregion
    }
}

