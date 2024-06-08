package com.lakedev.apteka.survey.medicine_details

import androidx.lifecycle.ViewModel
import androidx.lifecycle.ViewModelProvider

class MedicineDetailsViewModel() : ViewModel() {

    /**
     * Consider all sign ins successful
     */

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