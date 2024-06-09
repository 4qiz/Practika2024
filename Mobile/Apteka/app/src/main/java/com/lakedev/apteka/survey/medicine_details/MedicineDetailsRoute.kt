package com.lakedev.apteka.survey.medicine_details

import android.os.Build
import androidx.annotation.RequiresExtension
import androidx.compose.runtime.Composable
import androidx.lifecycle.viewmodel.compose.viewModel

@RequiresExtension(extension = Build.VERSION_CODES.S, version = 7)
@Composable
fun MedicineDetailsRoute(
    id: Int,
    onNavUp: () -> Unit,
) {
    val medicineDetailsViewModel: MedicineDetailsViewModel = viewModel(factory = MedicineDetailsViewModelFactory())

    if (id < 1){
        onNavUp()
    }

    MedicineDetailsScreen(
        id = id,
        medicineDetailsViewModel,
        onNavUp = onNavUp,
    )
}