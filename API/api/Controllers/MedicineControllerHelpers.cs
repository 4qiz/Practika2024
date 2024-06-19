
public static class MedicineControllerHelpers
{
    /// <summary>
    /// Определяет, достаточно ли количества товара на складе для списания.
    /// </summary>
    /// <param name="writeoffQuantity">Количество для списания.</param>
    /// <param name="warehouseQuantity">Количество товара на складе.</param>
    /// <returns>True, если количества достаточно; в противном случае — false.</returns>
    public static bool IsEnough(int writeoffQuantity, int warehouseQuantity) =>
         writeoffQuantity <= warehouseQuantity;
    
}