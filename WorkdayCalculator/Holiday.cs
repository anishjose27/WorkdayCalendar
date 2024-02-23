namespace WorkdayCalculator
{
    // Class representing a holiday with properties for its name, date, and recurrence status.
    public class Holiday
    {
        // Gets or sets the name of the holiday.
        public string? Name { get; set; }

        // Gets or sets the date of the holiday.
        public DateTime Date { get; set; }

        // Gets or sets a boolean indicating whether the holiday is recurring.
        public bool IsRecurring { get; set; }
    }

    // Enumeration representing directions, specifically for addition and subtraction.
    public enum Direction
    {
        // Represents addition direction.
        Add,

        // Represents subtraction direction.
        Subtract
    }
}
