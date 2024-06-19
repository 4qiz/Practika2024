
public static class MedicineControllerHelpers
{

    public static bool IsEnough(int writeoffQuantity, int warehouseQuantity)
    {
        return writeoffQuantity <= warehouseQuantity;
    }
}