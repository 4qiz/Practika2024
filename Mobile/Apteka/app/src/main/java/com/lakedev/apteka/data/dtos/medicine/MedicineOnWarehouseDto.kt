package com.lakedev.apteka.data.dtos.medicine

import com.google.gson.annotations.SerializedName

data class MedicineOnWarehouseDto (
    @SerializedName("medicineId"  ) var medicineId  : Int? = null,
    @SerializedName("warehouseId" ) var warehouseId : Int? = null,
    @SerializedName("quantity"    ) var quantity    : Int? = null
)