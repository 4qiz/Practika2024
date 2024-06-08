package com.lakedev.apteka.data.dtos.medicine

import com.google.gson.annotations.SerializedName

data class MedicineGetDto(
    @SerializedName("medicineId") var medicineId: Int? = null,
    @SerializedName("name") var name: String? = null,
    @SerializedName("tradeName") var tradeName: String? = null,
    @SerializedName("manufacturer") var manufacturer: String? = null,
    @SerializedName("image") var image: String? = null,
    @SerializedName("price") var price: Int? = null,
    @SerializedName("warehouseHasMedicineDto") var warehouseHasMedicineDto: List<WarehouseHasMedicineDto> = emptyList<WarehouseHasMedicineDto>()
)
