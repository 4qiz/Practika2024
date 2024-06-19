
namespace Api.Tests
{
    public class MedicineControllerHelpersTests
    {
        [Fact]
        public void IsEnough_WithWriteoffQuantityLessThanOrEqualToWarehouseQuantity_ShouldReturnTrue()
        {
            // Arrange
            int writeoffQuantity = 5;
            int warehouseQuantity = 10;

            // Act
            bool result = MedicineControllerHelpers.IsEnough(writeoffQuantity, warehouseQuantity);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsEnough_WithWriteoffQuantityGreaterThanWarehouseQuantity_ShouldReturnFalse()
        {
            // Arrange
            int writeoffQuantity = 15;
            int warehouseQuantity = 10;

            // Act
            bool result = MedicineControllerHelpers.IsEnough(writeoffQuantity, warehouseQuantity);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsEnough_WithWriteoffQuantityEqualToWarehouseQuantity_ShouldReturnTrue()
        {
            // Arrange
            int writeoffQuantity = 10;
            int warehouseQuantity = 10;

            // Act
            bool result = MedicineControllerHelpers.IsEnough(writeoffQuantity, warehouseQuantity);

            // Assert
            Assert.True(result);
        }
    }
}