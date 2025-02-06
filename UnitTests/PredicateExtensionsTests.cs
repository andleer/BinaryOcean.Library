using System.Linq.Expressions;

namespace BinaryOcean.Library.Tests
{
    public class PredicateExtensionsTests
    {
        [Fact]
        public void AndStub_ShouldReturnTrue()
        {
            // Arrange
            var predicate = PredicateExtensions.AndStub<int>();

            // Act
            bool result = predicate.Compile().Invoke(0);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void OrStub_ShouldReturnFalse()
        {
            // Arrange
            var predicate = PredicateExtensions.OrStub<int>();

            // Act
            bool result = predicate.Compile().Invoke(0);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void And_ShouldCombinePredicatesWithAnd()
        {
            // Arrange
            Expression<Func<int, bool>> expr1 = x => x > 2;
            Expression<Func<int, bool>> expr2 = x => x < 5;

            // Act
            var combined = expr1.And(expr2);
            bool result1 = combined.Compile().Invoke(3);
            bool result2 = combined.Compile().Invoke(5);

            // Assert
            Assert.True(result1);
            Assert.False(result2);
        }

        [Fact]
        public void Or_ShouldCombinePredicatesWithOr()
        {
            // Arrange
            Expression<Func<int, bool>> expr1 = x => x < 2;
            Expression<Func<int, bool>> expr2 = x => x > 5;

            // Act
            var combined = expr1.Or(expr2);
            bool result1 = combined.Compile().Invoke(1);
            bool result2 = combined.Compile().Invoke(6);
            bool result3 = combined.Compile().Invoke(3);

            // Assert
            Assert.True(result1);
            Assert.True(result2);
            Assert.False(result3);
        }
    }
}
