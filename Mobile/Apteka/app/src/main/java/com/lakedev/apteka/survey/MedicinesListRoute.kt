package com.lakedev.apteka.survey

import android.content.Context
import android.content.ContextWrapper
import android.os.Build
import androidx.activity.compose.BackHandler
import androidx.annotation.RequiresExtension
import androidx.appcompat.app.AppCompatActivity
import androidx.compose.animation.AnimatedContentTransitionScope
import androidx.compose.foundation.layout.padding
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.compose.ui.platform.LocalContext
import androidx.fragment.app.FragmentManager
import androidx.lifecycle.viewmodel.compose.viewModel
import com.google.android.material.datepicker.MaterialDatePicker
import com.lakedev.apteka.data.repository.UserRepository

private const val CONTENT_ANIMATION_DURATION = 300

/**
 * Displays a [MedicineListLayout] tied to the passed [MedicineListViewModel]
 */
@RequiresExtension(extension = Build.VERSION_CODES.S, version = 7)
@Composable
fun MedicinesListRoute(
    onSurveyComplete: () -> Unit,
    onNavigateToDetails: (id: Int) -> Unit,
    onNavUp: () -> Unit,
) {
    val viewModel: MedicineListViewModel = viewModel(
        factory = SurveyViewModelFactory(PhotoUriManager(LocalContext.current))
    )

    val surveyScreenData = viewModel.surveyScreenData ?: return
    BackHandler {
        if (!viewModel.onBackPressed()) {
            onNavUp()
        }
    }

    MedicineListLayout(
        surveyScreenData = surveyScreenData,
        isNextEnabled = viewModel.isNextEnabled,
        onClosePressed = {
            onNavUp()
            UserRepository.logout()
        },
        onPreviousPressed = { viewModel.onPreviousPressed() },
        onNextPressed = { viewModel.onNextPressed() },
        onDonePressed = { viewModel.onDonePressed(onSurveyComplete) }
    ) { paddingValues ->

        val modifier = Modifier.padding(paddingValues)

        //content
        MedicinesListScreen(viewModel, modifier, onNavigateToDetails)

    }
}

private fun getTransitionDirection(
    initialIndex: Int,
    targetIndex: Int
): AnimatedContentTransitionScope.SlideDirection {
    return if (targetIndex > initialIndex) {
        // Going forwards in the survey: Set the initial offset to start
        // at the size of the content so it slides in from right to left, and
        // slides out from the left of the screen to -fullWidth
        AnimatedContentTransitionScope.SlideDirection.Left
    } else {
        // Going back to the previous question in the set, we do the same
        // transition as above, but with different offsets - the inverse of
        // above, negative fullWidth to enter, and fullWidth to exit.
        AnimatedContentTransitionScope.SlideDirection.Right
    }
}

private fun showTakeawayDatePicker(
    date: Long?,
    supportFragmentManager: FragmentManager,
    onDateSelected: (date: Long) -> Unit,
) {
    val picker = MaterialDatePicker.Builder.datePicker()
        .setSelection(date)
        .build()
    picker.show(supportFragmentManager, picker.toString())
    picker.addOnPositiveButtonClickListener {
        picker.selection?.let {
            onDateSelected(it)
        }
    }
}

private tailrec fun Context.findActivity(): AppCompatActivity =
    when (this) {
        is AppCompatActivity -> this
        is ContextWrapper -> this.baseContext.findActivity()
        else -> throw IllegalArgumentException("Could not find activity!")
    }