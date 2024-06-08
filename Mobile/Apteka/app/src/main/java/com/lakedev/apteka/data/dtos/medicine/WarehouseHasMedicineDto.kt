package com.lakedev.apteka.data.dtos.medicine

import com.google.gson.annotations.SerializedName

data class WarehouseHasMedicineDto(
    @SerializedName("stockQuantity" ) var stockQuantity : Int? = null,
    @SerializedName("warehouseId"   ) var warehouseId   : Int? = null
)
