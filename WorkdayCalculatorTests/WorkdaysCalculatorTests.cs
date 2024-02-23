using Microsoft.VisualStudio.TestTools.UnitTesting;
//using NUnit.Framework;
using System;
using System.Configuration;
using WorkdayCalculator;

/// <summary>
/// The workdays calculator tests.
/// </summary>

[TestClass]
public class WorkdaysCalculatorTests
{
    //Scenarios given in the case
    [DataRow("24/05/2004 15:07", 0.25, Direction.Add, "25/05/2004 09:07")] //Example scenario 1:Basic Working Day Calculation
    [DataRow("24/05/2004 04:00", 0.5, Direction.Add, "24/05/2004 12:00")] //Example scenario 2:Midnight Boundary and Fractional Working Day
    [DataRow("24/05/2004 18:05", 5.5, Direction.Subtract, "14/05/2004 12:00")] //Example scenario 3:Working Day Calculation with Negative Days
    [DataRow("24/05/2004 19:03", 44.723656, Direction.Add, "27/07/2004 13:47")] //Additional scenario 1
    [DataRow("24/05/2004 18:03", 6.7470217, Direction.Subtract, "13/05/2004 10:01")] //Additional scenario 2
    [DataRow("24/05/2004 08:03", 12.782709, Direction.Add, "10/06/2004 14:18")] //Additional scenario 3
    [DataRow("24/05/2004 07:03", 8.276628, Direction.Add, "04/06/2004 10:12")] //Additional scenario 4

    //Additional test cases - Addition
    [DataRow("21/02/2024 07:30", 2, Direction.Add, "22/02/2024 16:00")] //Start before working hours
    [DataRow("21/02/2024 17:30", 2, Direction.Add, "23/02/2024 16:00")] //Start after working hours
    [DataRow("20/02/2024 16:00", 3, Direction.Add, "23/02/2024 16:00")] //Edge case: start date at working hours end
    [DataRow("21/02/2024 08:00", 2, Direction.Add, "22/02/2024 16:00")] //Edge case: start date at working hours start
    [DataRow("21/02/2024 10:03", 3, Direction.Add, "26/02/2024 10:03")] //Skip Weekends in calculation
    [DataRow("22/12/2023 09:03", 3, Direction.Add, "28/12/2023 09:03")] //Skip Weekend + Recurring holiday in calculation
    [DataRow("24/02/2024 09:03", 2, Direction.Add, "27/02/2024 16:00")] //Start date falling on a weekend
    [DataRow("20/02/2024 15:00", 2.25, Direction.Add, "23/02/2024 09:00")] //Partial days
    [DataRow("21/02/2024 07:30", 1.25, Direction.Add, "22/02/2024 10:00")] //Start before working hours + Partial days
    [DataRow("20/02/2024 17:30", 2.3, Direction.Add, "23/02/2024 10:23")] //Start after working hours + Partial days
    [DataRow("21/02/2024 15:02", 2.3215, Direction.Add, "26/02/2024 09:36")] //Weekend + Partial days

    //Additional test cases - Subtraction
    [DataRow("22/02/2024 10:00", 2, Direction.Subtract, "20/02/2024 10:00")] //Normal working hours within the week
    [DataRow("22/02/2024 07:30", 2, Direction.Subtract, "20/02/2024 08:00")] //Start before working hours
    [DataRow("22/02/2024 17:30", 2, Direction.Subtract, "21/02/2024 08:00")] //Start after working hours
    [DataRow("23/02/2024 16:00", 3, Direction.Subtract, "21/02/2024 08:00")] //Edge case: start date at working hours end
    [DataRow("23/02/2024 08:00", 2, Direction.Subtract, "21/02/2024 08:00")] //Edge case: start date at working hours start
    [DataRow("26/02/2024 10:03", 3, Direction.Subtract, "21/02/2024 10:03")] //Skip Weekends in calculation
    [DataRow("26/12/2023 09:03", 3, Direction.Subtract, "20/12/2023 09:03")] //Skip Weekend + Recurring holiday in calculation
    [DataRow("24/02/2024 09:03", 2, Direction.Subtract, "22/02/2024 08:00")] //Start date falling on a weekend
    [DataRow("23/02/2024 09:00", 2.25, Direction.Subtract, "20/02/2024 15:00")] //Partial days
    [DataRow("22/02/2024 07:30", 1.25, Direction.Subtract, "20/02/2024 14:00")] //Start before working hours + Partial days
    [DataRow("23/02/2024 17:30", 2.3, Direction.Subtract, "21/02/2024 13:36")] //Start after working hours + Partial days
    [DataRow("27/02/2024 15:02", 2.3215, Direction.Subtract, "23/02/2024 12:27")] //Weekend + Partial days

    //Test cases for year overlap
    [DataRow("29/12/2023 10:00", 2, Direction.Add, "03/01/2024 10:00")] //New year + weekend
    [DataRow("02/01/2024 10:00", 2, Direction.Subtract, "28/12/2023 10:00")] //New year + weekend
    [DataTestMethod]
    public void TestCalculateWorkdays(string startDateStr, double daysToModify, Direction direction, string expectedEndDateStr)
    {
        // Arrange
        DateTime startDate = DateTime.Parse(startDateStr);

        // Create an instance of the form class containing the CalculateWorkdays method
        InputForm calculator = new InputForm();

        // Act
        string actualEndDate = calculator.CalculateWorkdays(startDate, daysToModify, direction);

        // Assert
        Assert.AreEqual(expectedEndDateStr, actualEndDate);
    }
}