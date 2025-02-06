using System;
using System.Linq;
using System.Linq.Expressions;
using Xunit;
using BinaryOcean.Library;

namespace BinaryOcean.Library.Tests
{
    public class QueryableExtensionsTests
    {
        [Fact]
        public void WhereIf_ShouldFilterElements_WhenConditionIsTrue()
        {
            // Arrange
            var source = new[] { 1, 2, 3, 4, 5 }.AsQueryable();

            // Act
            var result = source.WhereIf(true, x => x > 3);

            // Assert
            Assert.Equal(new[] { 4, 5 }, result);
        }

        [Fact]
        public void WhereIf_ShouldReturnOriginalSequence_WhenConditionIsFalse()
        {
            // Arrange
            var source = new[] { 1, 2, 3, 4, 5 }.AsQueryable();

            // Act
            var result = source.WhereIf(false, x => x > 3);

            // Assert
            Assert.Equal(source, result);
        }

        [Fact]
        public void WhereIf_WithIndex_ShouldFilterElements_WhenConditionIsTrue()
        {
            // Arrange
            var source = new[] { 1, 2, 3, 4, 5 }.AsQueryable();

            // Act
            var result = source.WhereIf(true, (x, index) => index % 2 == 0);

            // Assert
            Assert.Equal(new[] { 1, 3, 5 }, result);
        }

        [Fact]
        public void WhereIf_WithIndex_ShouldReturnOriginalSequence_WhenConditionIsFalse()
        {
            // Arrange
            var source = new[] { 1, 2, 3, 4, 5 }.AsQueryable();

            // Act
            var result = source.WhereIf(false, (x, index) => index % 2 == 0);

            // Assert
            Assert.Equal(source, result);
        }

        [Fact]
        public void RandomElement_ShouldReturnAnElementFromSource()
        {
            // Arrange
            var source = new[] { 1, 2, 3, 4, 5 }.AsQueryable();

            // Act
            var result = source.RandomElement();

            // Assert
            Assert.Contains(result, source);
        }

        [Fact]
        public void RandomElement_ShouldThrowInvalidOperationException_WhenSourceIsEmpty()
        {
            // Arrange
            var source = new int[] { }.AsQueryable();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => source.RandomElement());
        }
    }
}

