using System.Windows.Forms;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WorkdayCalculator
{
    partial class InputForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            holidayDataGridView = new DataGridView();
            btnDelete = new Button();
            btnSave = new Button();
            lblStart = new Label();
            lblEnd = new Label();
            lblStartDate = new Label();
            inputDateTimePicker = new DateTimePicker();
            addRadioButton = new RadioButton();
            subRadioButton = new RadioButton();
            operationGroupBox = new GroupBox();
            lblDays = new Label();
            dayTextBox = new TextBox();
            lblResultDateTime = new Label();
            lblResult = new Label();
            btnCalculate = new Button();
            startDateTimePicker = new DateTimePicker();
            endDateTimePicker = new DateTimePicker();
            workHourGroupBox = new GroupBox();
            holidayGroupBox = new GroupBox();
            calcGroupBox = new GroupBox();
            ((System.ComponentModel.ISupportInitialize)holidayDataGridView).BeginInit();
            operationGroupBox.SuspendLayout();
            workHourGroupBox.SuspendLayout();
            holidayGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // holidayDataGridView
            // 
            holidayDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            holidayDataGridView.Location = new Point(12, 179);
            holidayDataGridView.Name = "holidayDataGridView";
            holidayDataGridView.Size = new Size(360, 150);
            holidayDataGridView.TabIndex = 0;
            holidayDataGridView.CellBeginEdit += holidayGrid_CellBeginEdit;
            // 
            // btnDelete
            // 
            btnDelete.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnDelete.Location = new Point(292, 188);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(75, 23);
            btnDelete.TabIndex = 2;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnSave
            // 
            btnSave.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
            btnSave.Location = new Point(295, 362);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 23);
            btnSave.TabIndex = 3;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // lblStart
            // 
            lblStart.AutoSize = true;
            lblStart.Location = new Point(6, 27);
            lblStart.Name = "lblStart";
            lblStart.Size = new Size(37, 17);
            lblStart.TabIndex = 6;
            lblStart.Text = "Start";
            // 
            // lblEnd
            // 
            lblEnd.AutoSize = true;
            lblEnd.Location = new Point(119, 27);
            lblEnd.Name = "lblEnd";
            lblEnd.Size = new Size(30, 17);
            lblEnd.TabIndex = 7;
            lblEnd.Text = "End";
            // 
            // lblStartDate
            // 
            lblStartDate.AutoSize = true;
            lblStartDate.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblStartDate.Location = new Point(12, 38);
            lblStartDate.Name = "lblStartDate";
            lblStartDate.Size = new Size(128, 21);
            lblStartDate.TabIndex = 8;
            lblStartDate.Text = "Start DateTime:";
            // 
            // inputDateTimePicker
            // 
            inputDateTimePicker.CustomFormat = "dd/MM/yyyy HH:mm";
            inputDateTimePicker.Format = DateTimePickerFormat.Custom;
            inputDateTimePicker.Location = new Point(142, 36);
            inputDateTimePicker.Name = "inputDateTimePicker";
            inputDateTimePicker.Size = new Size(132, 23);
            inputDateTimePicker.TabIndex = 9;
            inputDateTimePicker.ValueChanged += inputDateTimePicker_TextChanged;
            // 
            // addRadioButton
            // 
            addRadioButton.AutoSize = true;
            addRadioButton.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            addRadioButton.Location = new Point(6, 22);
            addRadioButton.Name = "addRadioButton";
            addRadioButton.Size = new Size(51, 21);
            addRadioButton.TabIndex = 10;
            addRadioButton.TabStop = true;
            addRadioButton.Text = "Add";
            addRadioButton.UseVisualStyleBackColor = true;
            addRadioButton.CheckedChanged += addRadioButton_CheckedChanged;
            // 
            // subRadioButton
            // 
            subRadioButton.AutoSize = true;
            subRadioButton.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            subRadioButton.Location = new Point(71, 22);
            subRadioButton.Name = "subRadioButton";
            subRadioButton.Size = new Size(77, 21);
            subRadioButton.TabIndex = 11;
            subRadioButton.TabStop = true;
            subRadioButton.Text = "Subtract";
            subRadioButton.UseVisualStyleBackColor = true;
            subRadioButton.CheckedChanged += subRadioButton_CheckedChanged;
            // 
            // operationGroupBox
            // 
            operationGroupBox.Controls.Add(addRadioButton);
            operationGroupBox.Controls.Add(subRadioButton);
            operationGroupBox.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            operationGroupBox.Location = new Point(295, 12);
            operationGroupBox.Name = "operationGroupBox";
            operationGroupBox.Size = new Size(169, 52);
            operationGroupBox.TabIndex = 12;
            operationGroupBox.TabStop = false;
            operationGroupBox.Text = "Operation";
            // 
            // lblDays
            // 
            lblDays.AutoSize = true;
            lblDays.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDays.Location = new Point(470, 38);
            lblDays.Name = "lblDays";
            lblDays.Size = new Size(138, 21);
            lblDays.TabIndex = 13;
            lblDays.Text = "Number of Days:";
            // 
            // dayTextBox
            // 
            dayTextBox.Location = new Point(610, 36);
            dayTextBox.MaxLength = 25;
            dayTextBox.Name = "dayTextBox";
            dayTextBox.Size = new Size(100, 23);
            dayTextBox.TabIndex = 14;
            dayTextBox.TextChanged += dayTextBox_TextChanged;
            // 
            // lblResultDateTime
            // 
            lblResultDateTime.AutoSize = true;
            lblResultDateTime.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblResultDateTime.Location = new Point(12, 85);
            lblResultDateTime.Name = "lblResultDateTime";
            lblResultDateTime.Size = new Size(139, 21);
            lblResultDateTime.TabIndex = 15;
            lblResultDateTime.Text = "Result DateTime:";
            // 
            // lblResult
            // 
            lblResult.AutoSize = true;
            lblResult.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblResult.ForeColor = Color.FromArgb(0, 192, 0);
            lblResult.Location = new Point(153, 82);
            lblResult.Name = "lblResult";
            lblResult.Size = new Size(0, 25);
            lblResult.TabIndex = 16;
            // 
            // btnCalculate
            // 
            btnCalculate.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCalculate.Location = new Point(743, 34);
            btnCalculate.Name = "btnCalculate";
            btnCalculate.Size = new Size(102, 28);
            btnCalculate.TabIndex = 17;
            btnCalculate.Text = "Calculate";
            btnCalculate.UseVisualStyleBackColor = true;
            btnCalculate.Click += btnCalculate_Click;
            // 
            // startDateTimePicker
            // 
            startDateTimePicker.CalendarFont = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            startDateTimePicker.CustomFormat = "HH:mm";
            startDateTimePicker.Format = DateTimePickerFormat.Custom;
            startDateTimePicker.Location = new Point(49, 21);
            startDateTimePicker.Name = "startDateTimePicker";
            startDateTimePicker.ShowUpDown = true;
            startDateTimePicker.Size = new Size(64, 25);
            startDateTimePicker.TabIndex = 18;
            startDateTimePicker.ValueChanged += startDateTimePicker_TextChanged;
            // 
            // endDateTimePicker
            // 
            endDateTimePicker.CalendarFont = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            endDateTimePicker.CustomFormat = "HH:mm";
            endDateTimePicker.Format = DateTimePickerFormat.Custom;
            endDateTimePicker.Location = new Point(155, 19);
            endDateTimePicker.Name = "endDateTimePicker";
            endDateTimePicker.ShowUpDown = true;
            endDateTimePicker.Size = new Size(65, 25);
            endDateTimePicker.TabIndex = 19;
            endDateTimePicker.ValueChanged += endDateTimePicker_TextChanged;
            // 
            // workHourGroupBox
            // 
            workHourGroupBox.Controls.Add(lblStart);
            workHourGroupBox.Controls.Add(endDateTimePicker);
            workHourGroupBox.Controls.Add(startDateTimePicker);
            workHourGroupBox.Controls.Add(lblEnd);
            workHourGroupBox.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            workHourGroupBox.Location = new Point(12, 335);
            workHourGroupBox.Name = "workHourGroupBox";
            workHourGroupBox.Size = new Size(233, 64);
            workHourGroupBox.TabIndex = 20;
            workHourGroupBox.TabStop = false;
            workHourGroupBox.Text = "Working Hours";
            // 
            // holidayGroupBox
            // 
            holidayGroupBox.Controls.Add(btnDelete);
            holidayGroupBox.Location = new Point(3, 147);
            holidayGroupBox.Name = "holidayGroupBox";
            holidayGroupBox.Size = new Size(483, 280);
            holidayGroupBox.TabIndex = 21;
            holidayGroupBox.TabStop = false;
            holidayGroupBox.Text = "Manage Holidays";
            // 
            // calcGroupBox
            // 
            calcGroupBox.Location = new Point(3, 0);
            calcGroupBox.Name = "calcGroupBox";
            calcGroupBox.Size = new Size(861, 139);
            calcGroupBox.TabIndex = 22;
            calcGroupBox.TabStop = false;
            // 
            // InputForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(917, 456);
            Controls.Add(workHourGroupBox);
            Controls.Add(btnCalculate);
            Controls.Add(lblResult);
            Controls.Add(lblResultDateTime);
            Controls.Add(dayTextBox);
            Controls.Add(lblDays);
            Controls.Add(operationGroupBox);
            Controls.Add(inputDateTimePicker);
            Controls.Add(lblStartDate);
            Controls.Add(btnSave);
            Controls.Add(holidayDataGridView);
            Controls.Add(holidayGroupBox);
            Controls.Add(calcGroupBox);
            Name = "InputForm";
            Text = "Workday Calculator";
            ((System.ComponentModel.ISupportInitialize)holidayDataGridView).EndInit();
            operationGroupBox.ResumeLayout(false);
            operationGroupBox.PerformLayout();
            workHourGroupBox.ResumeLayout(false);
            workHourGroupBox.PerformLayout();
            holidayGroupBox.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView holidayDataGridView;
        private Button btnDelete;
        private Button btnSave;
        private Label lblStart;
        private Label lblEnd;
        private Label lblStartDate;
        private DateTimePicker inputDateTimePicker;
        private RadioButton addRadioButton;
        private RadioButton subRadioButton;
        private GroupBox operationGroupBox;
        private Label lblDays;
        private TextBox dayTextBox;
        private Label lblResultDateTime;
        private Label lblResult;
        private Button btnCalculate;
        private DateTimePicker startDateTimePicker;
        private DateTimePicker endDateTimePicker;
        private GroupBox workHourGroupBox;
        private GroupBox holidayGroupBox;
        private GroupBox calcGroupBox;
    }
}