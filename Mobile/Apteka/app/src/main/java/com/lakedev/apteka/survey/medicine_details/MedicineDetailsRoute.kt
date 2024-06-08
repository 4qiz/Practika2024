package com.lakedev.apteka.survey.medicine_details

import android.os.Build
import androidx.activity.compose.BackHandler
import androidx.annotation.RequiresExtension
import androidx.compose.foundation.layout.padding
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.compose.ui.platform.LocalContext
import androidx.lifecycle.viewmodel.compose.viewModel
import com.lakedev.apteka.signinsignup.UserRepository
import com.lakedev.apteka.survey.MedicinesList
import com.lakedev.apteka.survey.PhotoUriManager
import com.lakedev.apteka.survey.SurveyQuestionsScreen
import com.lakedev.apteka.survey.SurveyViewModel
import com.lakedev.apteka.survey.SurveyViewModelFactory

@RequiresExtension(extension = Build.VERSION_CODES.S, version = 7)
@Composable
fun MedicineDetailsRoute(
    id: Int,
    onNavUp: () -> Unit,
) {
    val medicineDetailsViewModel: MedicineDetailsViewModel = viewModel(factory = MedicineDetailsViewModelFactory())

    if (id < 1)
        onNavUp()
    //MedicineDetailsScreen(id)
    SignInScreen(
        id = id,
        onNavUp = onNavUp,
    )
}