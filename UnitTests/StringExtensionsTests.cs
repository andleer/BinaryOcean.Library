namespace BinaryOcean.Library.Tests;

public class StringExtensionsTests
{
    [Fact]
    public void BeforeFirst_ShouldReturnSubstringBeforeFirstOccurrence()
    {
        // Arrange
        string input = "Hello, world!";
        string value = ",";
        string expected = "Hello";

        // Act
        string result = input.BeforeFirst(value);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void BeforeFirst_ShouldReturnEmptyString_WhenValueNotFound()
    {
        // Arrange
        string input = "Hello, world!";
        string value = "x";

        // Act
        string result = input.BeforeFirst(value);

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void BeforeLast_ShouldReturnSubstringBeforeLastOccurrence()
    {
        // Arrange
        string input = "Hello, world, again!";
        string value = ",";
        string expected = "Hello, world";

        // Act
        string result = input.BeforeLast(value);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void BeforeLast_ShouldReturnEmptyString_WhenValueNotFound()
    {
        // Arrange
        string input = "Hello, world!";
        string value = "x";

        // Act
        string result = input.BeforeLast(value);

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void AfterFirst_ShouldReturnSubstringAfterFirstOccurrence()
    {
        // Arrange
        string input = "Hello, world!";
        string value = ",";
        string expected = " world!";

        // Act
        string result = input.AfterFirst(value);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void AfterFirst_ShouldReturnEmptyString_WhenValueNotFound()
    {
        // Arrange
        string input = "Hello, world!";
        string value = "x";

        // Act
        string result = input.AfterFirst(value);

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void AfterLast_ShouldReturnSubstringAfterLastOccurrence()
    {
        // Arrange
        string input = "Hello, world, again!";
        string value = ",";
        string expected = " again!";

        // Act
        string result = input.AfterLast(value);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void AfterLast_ShouldReturnEmptyString_WhenValueNotFound()
    {
        // Arrange
        string input = "Hello, world!";
        string value = "x";

        // Act
        string result = input.AfterLast(value);

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void Replace_ShouldReplaceKeysWithValuesFromDictionary()
    {
        // Arrange
        string input = "Hello, world!";
        var dictionary = new Dictionary<string, string>
        {
            { "Hello", "Hi" },
            { "world", "Earth" }
        };
        string expected = "Hi, Earth!";

        // Act
        string result = input.Replace(dictionary);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void AsBoolean_ShouldReturnBooleanValue_WhenValid()
    {
        // Arrange
        string input = "true";

        // Act
        bool? result = input.AsBoolean();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void AsBoolean_ShouldReturnNull_WhenInvalid()
    {
        // Arrange
        string input = "notabool";

        // Act
        bool? result = input.AsBoolean();

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void IsBoolean_ShouldReturnTrue_WhenValidBoolean()
    {
        // Arrange
        string input = "true";

        // Act
        bool result = input.IsBoolean();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsBoolean_ShouldReturnFalse_WhenInvalidBoolean()
    {
        // Arrange
        string input = "notabool";

        // Act
        bool result = input.IsBoolean();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void ToBoolean_ShouldReturnBooleanValue()
    {
        // Arrange
        string input = "true";

        // Act
        bool result = input.ToBoolean();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void AsGuid_ShouldReturnGuidValue_WhenValid()
    {
        // Arrange
        string input = "d3b07384-d9a0-4f1b-8bff-3f6b0b8b9c3b";

        // Act
        Guid? result = input.AsGuid();

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void AsGuid_ShouldReturnNull_WhenInvalid()
    {
        // Arrange
        string input = "notaguid";

        // Act
        Guid? result = input.AsGuid();

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void IsGuid_ShouldReturnTrue_WhenValidGuid()
    {
        // Arrange
        string input = "d3b07384-d9a0-4f1b-8bff-3f6b0b8b9c3b";

        // Act
        bool result = input.IsGuid();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsGuid_ShouldReturnFalse_WhenInvalidGuid()
    {
        // Arrange
        string input = "notaguid";

        // Act
        bool result = input.IsGuid();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void ToGuid_ShouldReturnGuidValue()
    {
        // Arrange
        string input = "d3b07384-d9a0-4f1b-8bff-3f6b0b8b9c3b";

        // Act
        Guid result = input.ToGuid();

        // Assert
        Assert.Equal(Guid.Parse(input), result);
    }

    [Fact]
    public void AsDateTime_ShouldReturnDateTimeValue_WhenValid()
    {
        // Arrange
        string input = "2025-02-05";

        // Act
        DateTime? result = input.AsDateTime();

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void AsDateTime_ShouldReturnNull_WhenInvalid()
    {
        // Arrange
        string input = "notadate";

        // Act
        DateTime? result = input.AsDateTime();

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void IsDateTime_ShouldReturnTrue_WhenValidDateTime()
    {
        // Arrange
        string input = "2025-02-05";

        // Act
        bool result = input.IsDateTime();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsDateTime_ShouldReturnFalse_WhenInvalidDateTime()
    {
        // Arrange
        string input = "notadate";

        // Act
        bool result = input.IsDateTime();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void IsSqlDateTime_ShouldReturnTrue_WhenValidSqlDateTime()
    {
        // Arrange
        string input = "2025-02-05";

        // Act
        bool result = input.IsSqlDateTime();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsSqlDateTime_ShouldReturnFalse_WhenInvalidSqlDateTime()
    {
        // Arrange
        string input = "1000-01-01";

        // Act
        bool result = input.IsSqlDateTime();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void ToDateTime_ShouldReturnDateTimeValue()
    {
        // Arrange
        string input = "2025-02-05";

        // Act
        DateTime result = input.ToDateTime();

        // Assert
        Assert.Equal(DateTime.Parse(input), result);
    }

    [Fact]
    public void AsTimeSpan_ShouldReturnTimeSpanValue_WhenValid()
    {
        // Arrange
        string input = "12:30:00";

        // Act
        TimeSpan? result = input.AsTimeSpan();

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void AsTimeSpan_ShouldReturnNull_WhenInvalid()
    {
        // Arrange
        string input = "notatime";

        // Act
        TimeSpan? result = input.AsTimeSpan();

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void IsTimeSpan_ShouldReturnTrue_WhenValidTimeSpan()
    {
        // Arrange
        string input = "12:30:00";

        // Act
        bool result = input.IsTimeSpan();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsTimeSpan_ShouldReturnFalse_WhenInvalidTimeSpan()
    {
        // Arrange
        string input = "notatime";

        // Act
        bool result = input.IsTimeSpan();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void ToTimeSpan_ShouldReturnTimeSpanValue()
    {
        // Arrange
        string input = "12:30:00";

        // Act
        TimeSpan result = input.ToTimeSpan();

        // Assert
        Assert.Equal(TimeSpan.Parse(input), result);
    }

    [Fact]
    public void AsDecimal_ShouldReturnDecimalValue_WhenValid()
    {
        // Arrange
        string input = "123.45";

        // Act
        decimal? result = input.AsDecimal();

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void AsDecimal_ShouldReturnNull_WhenInvalid()
    {
        // Arrange
        string input = "notanumber";

        // Act
        decimal? result = input.AsDecimal();

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void IsDecimal_ShouldReturnTrue_WhenValidDecimal()
    {
        // Arrange
        string input = "123.45";

        // Act
        bool result = input.IsDecimal();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsDecimal_ShouldReturnFalse_WhenInvalidDecimal()
    {
        // Arrange
        string input = "notanumber";

        // Act
        bool result = input.IsDecimal();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void ToDecimal_ShouldReturnDecimalValue()
    {
        // Arrange
        string input = "123.45";

        // Act
        decimal result = input.ToDecimal();

        // Assert
        Assert.Equal(decimal.Parse(input), result);
    }

    [Fact]
    public void AsDouble_ShouldReturnDoubleValue_WhenValid()
    {
        // Arrange
        string input = "123.45";

        // Act
        double? result = input.AsDouble();

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void AsDouble_ShouldReturnNull_WhenInvalid()
    {
        // Arrange
        string input = "notanumber";

        // Act
        double? result = input.AsDouble();

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void IsDouble_ShouldReturnTrue_WhenValidDouble()
    {
        // Arrange
        string input = "123.45";

        // Act
        bool result = input.IsDouble();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsDouble_ShouldReturnFalse_WhenInvalidDouble()
    {
        // Arrange
        string input = "notanumber";

        // Act
        bool result = input.IsDouble();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void IsNumeric_ShouldReturnTrue_WhenValidDouble()
    {
        // Arrange
        string input = "123.45";

        // Act
        bool result = input.IsNumeric();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsNumeric_ShouldReturnFalse_WhenInvalidDouble()
    {
        // Arrange
        string input = "notanumber";

        // Act
        bool result = input.IsNumeric();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void ToDouble_ShouldReturnDoubleValue()
    {
        // Arrange
        string input = "123.45";

        // Act
        double result = input.ToDouble();

        // Assert
        Assert.Equal(double.Parse(input), result);
    }

    [Fact]
    public void AsSingle_ShouldReturnSingleValue_WhenValid()
    {
        // Arrange
        string input = "123.45";

        // Act
        float? result = input.AsSingle();

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void AsSingle_ShouldReturnNull_WhenInvalid()
    {
        // Arrange
        string input = "notanumber";

        // Act
        float? result = input.AsSingle();

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void IsSingle_ShouldReturnTrue_WhenValidSingle()
    {
        // Arrange
        string input = "123.45";

        // Act
        bool result = input.IsSingle();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsSingle_ShouldReturnFalse_WhenInvalidSingle()
    {
        // Arrange
        string input = "notanumber";

        // Act
        bool result = input.IsSingle();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void ToSingle_ShouldReturnSingleValue()
    {
        // Arrange
        string input = "123.45";

        // Act
        float result = input.ToSingle();

        // Assert
        Assert.Equal(float.Parse(input), result);
    }

    [Fact]
    public void AsInt32_ShouldReturnInt32Value_WhenValid()
    {
        // Arrange
        string input = "123";

        // Act
        int? result = input.AsInt32();

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void AsInt32_ShouldReturnNull_WhenInvalid()
    {
        // Arrange
        string input = "notanumber";

        // Act
        int? result = input.AsInt32();

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void IsInt32_ShouldReturnTrue_WhenValidInt32()
    {
        // Arrange
        string input = "123";

        // Act
        bool result = input.IsInt32();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsInt32_ShouldReturnFalse_WhenInvalidInt32()
    {
        // Arrange
        string input = "notanumber";

        // Act
        bool result = input.IsInt32();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void ToInt32_ShouldReturnInt32Value()
    {
        // Arrange
        string input = "123";

        // Act
        int result = input.ToInt32();

        // Assert
        Assert.Equal(int.Parse(input), result);
    }

    [Fact]
    public void AsByte_ShouldReturnByteValue_WhenValid()
    {
        // Arrange
        string input = "123";

        // Act
        byte? result = input.AsByte();

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void AsByte_ShouldReturnNull_WhenInvalid()
    {
        // Arrange
        string input = "notanumber";

        // Act
        byte? result = input.AsByte();

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void IsByte_ShouldReturnTrue_WhenValidByte()
    {
        // Arrange
        string input = "123";

        // Act
        bool result = input.IsByte();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsByte_ShouldReturnFalse_WhenInvalidByte()
    {
        // Arrange
        string input = "notanumber";

        // Act
        bool result = input.IsByte();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void ToByte_ShouldReturnByteValue()
    {
        // Arrange
        string input = "123";

        // Act
        byte result = input.ToByte();

        // Assert
        Assert.Equal(byte.Parse(input), result);
    }

    [Fact]
    public void AsEnum_ShouldReturnEnumValue_WhenValid()
    {
        // Arrange
        string input = "Monday";

        // Act
        DayOfWeek? result = input.AsEnum<DayOfWeek>();

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void AsEnum_ShouldReturnNull_WhenInvalid()
    {
        // Arrange
        string input = "NotADay";

        // Act
        DayOfWeek? result = input.AsEnum<DayOfWeek>();

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void IsEnum_ShouldReturnTrue_WhenValidEnum()
    {
        // Arrange
        string input = "Monday";

        // Act
        bool result = input.IsEnum<DayOfWeek>();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsEnum_ShouldReturnFalse_WhenInvalidEnum()
    {
        // Arrange
        string input = "NotADay";

        // Act
        bool result = input.IsEnum<DayOfWeek>();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void ToEnum_ShouldReturnEnumValue()
    {
        // Arrange
        string input = "Monday";

        // Act
        DayOfWeek result = input.ToEnum<DayOfWeek>();

        // Assert
        Assert.Equal(DayOfWeek.Monday, result);
    }
}
