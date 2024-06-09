package com.lakedev.apteka.survey.medicine_details

import android.util.Log
import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import androidx.lifecycle.ViewModelProvider
import androidx.lifecycle.viewModelScope
import com.lakedev.apteka.data.dtos.medicine.MedicineGetDto
import com.lakedev.apteka.data.repository.MedicineRepository
import kotlinx.coroutines.launch

class MedicineDetailsViewModel() : ViewModel() {
    private val repository = MedicineRepository()

    private val _medicine = MutableLiveData<MedicineGetDto>()
    val medicine: LiveData<MedicineGetDto> = _medicine

    suspend fun fetchMedicine(id: Int) {
        viewModelScope.launch {
            try {
                val cards = repository.getMedicine(id)
                _medicine.value = cards
            } catch (e: Exception) {
                Log.e("MedicineDetailsVM", e.message ?: "")
            }
        }
    }
}

class MedicineDetailsViewModelFactory : ViewModelProvider.Factory {
    @Suppress("UNCHECKED_CAST")
    override fun <T : ViewModel> create(modelClass: Class<T>): T {
        if (modelClass.isAssignableFrom(MedicineDetailsViewModel::class.java)) {
            return MedicineDetailsViewModel() as T
        }
        throw IllegalArgumentException("Unknown ViewModel class")
    }
}