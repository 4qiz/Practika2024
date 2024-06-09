package com.lakedev.apteka.survey.medicine_details

import android.util.Log
import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableIntStateOf
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.setValue
import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import androidx.lifecycle.ViewModelProvider
import androidx.lifecycle.viewModelScope
import com.lakedev.apteka.data.dtos.medicine.MedicineGetDto
import com.lakedev.apteka.data.repository.MedicineRepository
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.StateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.flow.update
import kotlinx.coroutines.launch

class MedicineDetailsViewModel(private val medicineId: Int) : ViewModel() {
    private val repository = MedicineRepository()

    private val _medicine = MutableLiveData<MedicineGetDto>()
    val medicine: LiveData<MedicineGetDto> = _medicine

    suspend fun fetchMedicine(id: Int) {
        try {
            val cards = repository.getMedicine(id)
            _medicine.value = cards
        } catch (e: Exception) {
            Log.e("MedicineDetailsVM", e.message ?: "")
        }

    }

    private suspend fun sendMedicineWriteOff() {
        if (chosedWarehouse < 1 || medicineId < 1 || quantity < 1)
            return

        try {
            val response = repository.writeOffMedicine(medicineId, chosedWarehouse, quantity)
            apiResponse = response
        } catch (e: Exception) {
            Log.e("MedicineDetailsVM", e.message ?: "")
        }
    }

    fun onConfirmation() {
        onDismiss()
        viewModelScope.launch {
            sendMedicineWriteOff()
            fetchMedicine(medicineId)
        }
    }

    private val _state = MutableStateFlow<MyState>(MyState(showDialog = false))
    val state: StateFlow<MyState>
        get() = _state.asStateFlow()

    // call this when you want to show the dialog
    fun onShowDialog(warehouse: Int) {
        _state.update { state ->
            state.copy(showDialog = true)
        }
        updateChosedWarehouse(warehouse)
    }

    fun onDismiss() {
        _state.update { state ->
            state.copy(showDialog = false)
        }
    }

    var quantity by mutableIntStateOf(1)
        private set

    fun updateQuantity(input: String) {
        quantity = input.toIntOrNull() ?: 1
        quantity = if (quantity < 1) 1 else quantity
    }

    var chosedWarehouse by mutableIntStateOf(0)
        private set

    fun updateChosedWarehouse(warehouse: Int) {
        chosedWarehouse = warehouse
    }

    var apiResponse by mutableStateOf("")
        private set
}

data class MyState(
    val showDialog: Boolean,
)

class MedicineDetailsViewModelFactory(private val medicineId: Int) : ViewModelProvider.Factory {
    @Suppress("UNCHECKED_CAST")
    override fun <T : ViewModel> create(modelClass: Class<T>): T {
        if (modelClass.isAssignableFrom(MedicineDetailsViewModel::class.java)) {
            return MedicineDetailsViewModel(medicineId) as T
        }
        throw IllegalArgumentException("Unknown ViewModel class")
    }
}