namespace BinaryOcean.Library.Tests;

public class DateTimeExtensionsTests
{
    [Fact]
    public void AddWeeks_ShouldAddCorrectNumberOfWeeks()
    {
        // Arrange
        var dateTime = new DateTime(2025, 2, 5);
        var weeksToAdd = 2;

        // Act
        var result = dateTime.AddWeeks(weeksToAdd);

        // Assert
        Assert.Equal(new DateTime(2025, 2, 19), result);
    }

    [Fact]
    public void ToShortDateTimeString_ShouldReturnCorrectFormat()
    {
        // Arrange
        var dateTime = new DateTime(2025, 2, 5, 14, 30, 0);

        // Act
        var result = dateTime.ToShortDateTimeString();

        // Assert
        Assert.Equal("2/5/2025 2:30 PM", result);
    }

    [Fact]
    public void ToLongDateTimeString_ShouldReturnCorrectFormat()
    {
        // Arrange
        var dateTime = new DateTime(2025, 2, 5, 14, 30, 0);

        // Act
        var result = dateTime.ToLongDateTimeString();

        // Assert
        Assert.Equal("Wednesday, February 5, 2025 2:30:00 PM", result);
    }

    [Fact]
    public void ToShortDateString_Nullable_ShouldReturnEmptyStringForNull()
    {
        // Arrange
        DateTime? dateTime = null;

        // Act
        var result = dateTime.ToShortDateString();

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void ToShortDateString_Nullable_ShouldReturnCorrectFormat()
    {
        // Arrange
        DateTime? dateTime = new DateTime(2025, 2, 5);

        // Act
        var result = dateTime.ToShortDateString();

        // Assert
        Assert.Equal("2/5/2025", result);
    }

    [Fact]
    public void ToLongDateString_Nullable_ShouldReturnEmptyStringForNull()
    {
        // Arrange
        DateTime? dateTime = null;

        // Act
        var result = dateTime.ToLongDateString();

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void ToLongDateString_Nullable_ShouldReturnCorrectFormat()
    {
        // Arrange
        DateTime? dateTime = new DateTime(2025, 2, 5);

        // Act
        var result = dateTime.ToLongDateString();

        // Assert
        Assert.Equal("Wednesday, February 5, 2025", result);
    }

    [Fact]
    public void ToShortTimeString_Nullable_ShouldReturnEmptyStringForNull()
    {
        // Arrange
        DateTime? dateTime = null;

        // Act
        var result = dateTime.ToShortTimeString();

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void ToShortTimeString_Nullable_ShouldReturnCorrectFormat()
    {
        // Arrange
        DateTime? dateTime = new DateTime(2025, 2, 5, 14, 30, 0);

        // Act
        var result = dateTime.ToShortTimeString();

        // Assert
        Assert.Equal("2:30 PM", result);
    }

    [Fact]
    public void ToLongTimeString_Nullable_ShouldReturnEmptyStringForNull()
    {
        // Arrange
        DateTime? dateTime = null;

        // Act
        var result = dateTime.ToLongTimeString();

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void ToLongTimeString_Nullable_ShouldReturnCorrectFormat()
    {
        // Arrange
        DateTime? dateTime = new DateTime(2025, 2, 5, 14, 30, 0);

        // Act
        var result = dateTime.ToLongTimeString();

        // Assert
        Assert.Equal("2:30:00 PM", result);
    }

    [Fact]
    public void ToShortDateTimeString_Nullable_ShouldReturnEmptyStringForNull()
    {
        // Arrange
        DateTime? dateTime = null;

        // Act
        var result = dateTime.ToShortDateTimeString();

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void ToShortDateTimeString_Nullable_ShouldReturnCorrectFormat()
    {
        // Arrange
        DateTime? dateTime = new DateTime(2025, 2, 5, 14, 30, 0);

        // Act
        var result = dateTime.ToShortDateTimeString();

        // Assert
        Assert.Equal("2/5/2025 2:30 PM", result);
    }

    [Fact]
    public void ToLongDateTimeString_Nullable_ShouldReturnEmptyStringForNull()
    {
        // Arrange
        DateTime? dateTime = null;

        // Act
        var result = dateTime.ToLongDateTimeString();

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void ToLongDateTimeString_Nullable_ShouldReturnCorrectFormat()
    {
        // Arrange
        DateTime? dateTime = new DateTime(2025, 2, 5, 14, 30, 0);

        // Act
        var result = dateTime.ToLongDateTimeString();

        // Assert
        Assert.Equal("Wednesday, February 5, 2025 2:30:00 PM", result);
    }

    [Fact]
    public void FirstDayOfMonth_ShouldReturnFirstDay()
    {
        // Arrange
        var dateTime = new DateTime(2025, 2, 5);

        // Act
        var result = dateTime.FirstDayOfMonth();

        // Assert
        Assert.Equal(new DateTime(2025, 2, 1), result);
    }

    [Fact]
    public void LastDayOfMonth_ShouldReturnLastDay()
    {
        // Arrange
        var dateTime = new DateTime(2025, 2, 5);

        // Act
        var result = dateTime.LastDayOfMonth();

        // Assert
        Assert.Equal(new DateTime(2025, 2, 28), result);
    }

    [Fact]
    public void FirstDateOfWeek_ShouldReturnFirstDateOfWeek()
    {
        // Arrange
        var dateTime = new DateTime(2025, 2, 5);

        // Act
        var result = dateTime.FirstDateOfWeek();

        // Assert
        Assert.Equal(new DateTime(2025, 2, 2), result);
    }

    [Fact]
    public void FirstDateOfWeek_WithStartDay_ShouldReturnFirstDateOfWeek()
    {
        // Arrange
        var dateTime = new DateTime(2025, 2, 5);
        var startDay = DayOfWeek.Monday;

        // Act
        var result = dateTime.FirstDateOfWeek(startDay);

        // Assert
        Assert.Equal(new DateTime(2025, 2, 3), result);
    }

    [Fact]
    public void MonthName_ShouldReturnCorrectMonthName()
    {
        // Arrange
        var dateTime = new DateTime(2025, 2, 5);

        // Act
        var result = dateTime.MonthName();

        // Assert
        Assert.Equal("February", result);
    }
}
