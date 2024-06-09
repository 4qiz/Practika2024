package com.lakedev.apteka.data.repository

import com.lakedev.apteka.data.RetrofitInstance
import com.lakedev.apteka.data.dtos.medicine.MedicineGetDto
import com.lakedev.apteka.data.dtos.medicine.MedicineOnWarehouseDto

class MedicineRepository {
    private val apiService = RetrofitInstance.api

    suspend fun getMedicines(): List<MedicineGetDto> {
        return apiService.getMedicines()
    }

    suspend fun getMedicine(id: Int): MedicineGetDto {
        return apiService.getMedicine(id)
    }

    suspend fun writeOffMedicine(
        medicineId: Int, warehouseId: Int, quantity: Int
    ): String {
        val medicineOnWarehouseDto = MedicineOnWarehouseDto(medicineId, warehouseId, quantity)
        return apiService.writeOff(medicineOnWarehouseDto).message()
    }
}