using System;
using Xunit;
using BinaryOcean.Library;

namespace BinaryOcean.Library.Tests;

public class ArrayExtensionsTests
{
    [Fact]
    public void SubArray_ShouldReturnCorrectSubArray()
    {
        // Arrange
        int[] array = { 1, 2, 3, 4, 5 };
        int start = 1;
        int length = 3;

        // Act
        int[] result = array.SubArray(start, length);

        // Assert
        Assert.Equal(new int[] { 2, 3, 4 }, result);
    }

    [Fact]
    public void SubArray_ShouldThrowArgumentException_WhenLengthIsOutOfRange()
    {
        // Arrange
        int[] array = { 1, 2, 3, 4, 5 };
        int start = 1;
        int length = 10;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => array.SubArray(start, length));
    }

    [Fact]
    public void Concat_ShouldReturnConcatenatedArray()
    {
        // Arrange
        int[] array1 = { 1, 2, 3 };
        int[] array2 = { 4, 5, 6 };

        // Act
        int[] result = array1.Concat(array2);

        // Assert
        Assert.Equal(new int[] { 1, 2, 3, 4, 5, 6 }, result);
    }

    [Fact]
    public void Concat_ShouldReturnFirstArray_WhenSecondArrayIsEmpty()
    {
        // Arrange
        int[] array1 = { 1, 2, 3 };
        int[] array2 = { };

        // Act
        int[] result = array1.Concat(array2);

        // Assert
        Assert.Equal(new int[] { 1, 2, 3 }, result);
    }

    [Fact]
    public void Concat_ShouldReturnSecondArray_WhenFirstArrayIsEmpty()
    {
        // Arrange
        int[] array1 = { };
        int[] array2 = { 4, 5, 6 };

        // Act
        int[] result = array1.Concat(array2);

        // Assert
        Assert.Equal(new int[] { 4, 5, 6 }, result);
    }
}
