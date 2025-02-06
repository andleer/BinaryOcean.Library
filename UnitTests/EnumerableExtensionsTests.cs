using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using BinaryOcean.Library;

namespace BinaryOcean.Library.Tests;

public class EnumerableExtensionsTests
{
    [Fact]
    public void WhereIf_ShouldFilterElements_WhenConditionIsTrue()
    {
        // Arrange
        var source = new[] { 1, 2, 3, 4, 5 };

        // Act
        var result = source.WhereIf(true, x => x > 3);

        // Assert
        Assert.Equal(new[] { 4, 5 }, result);
    }

    [Fact]
    public void WhereIf_ShouldReturnOriginalSequence_WhenConditionIsFalse()
    {
        // Arrange
        var source = new[] { 1, 2, 3, 4, 5 };

        // Act
        var result = source.WhereIf(false, x => x > 3);

        // Assert
        Assert.Equal(source, result);
    }

    [Fact]
    public void WhereIf_WithIndex_ShouldFilterElements_WhenConditionIsTrue()
    {
        // Arrange
        var source = new[] { 1, 2, 3, 4, 5 };

        // Act
        var result = source.WhereIf(true, (x, index) => index % 2 == 0);

        // Assert
        Assert.Equal(new[] { 1, 3, 5 }, result);
    }

    [Fact]
    public void WhereIf_WithIndex_ShouldReturnOriginalSequence_WhenConditionIsFalse()
    {
        // Arrange
        var source = new[] { 1, 2, 3, 4, 5 };

        // Act
        var result = source.WhereIf(false, (x, index) => index % 2 == 0);

        // Assert
        Assert.Equal(source, result);
    }

    [Fact]
    public void EmptyIfNull_ShouldReturnEmptySequence_WhenSourceIsNull()
    {
        // Arrange
        IEnumerable<int> source = null;

        // Act
        var result = source.EmptyIfNull();

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void EmptyIfNull_ShouldReturnOriginalSequence_WhenSourceIsNotNull()
    {
        // Arrange
        var source = new[] { 1, 2, 3 };

        // Act
        var result = source.EmptyIfNull();

        // Assert
        Assert.Equal(source, result);
    }

    [Fact]
    public void RandomElement_ShouldReturnAnElementFromSource()
    {
        // Arrange
        var source = new[] { 1, 2, 3, 4, 5 };

        // Act
        var result = source.RandomElement();

        // Assert
        Assert.Contains(result, source);
    }

    [Fact]
    public void RandomElement_ShouldThrowInvalidOperationException_WhenSourceIsEmpty()
    {
        // Arrange
        var source = new int[] { };

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => source.RandomElement());
    }
}
