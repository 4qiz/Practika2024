package com.lakedev.apteka.data.repository

import com.lakedev.apteka.data.RetrofitInstance
import com.lakedev.apteka.data.dtos.medicine.MedicineGetDto

class MedicineRepository {
    private val apiService = RetrofitInstance.api

    suspend fun getMedicines(): List<MedicineGetDto> {
        return apiService.getMedicines()
    }

    suspend fun getMedicine(id: Int): MedicineGetDto {
        return apiService.getMedicine(id)
    }
}