package com.lakedev.apteka.survey

import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.foundation.lazy.items
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.runtime.DisposableEffect

@Composable
fun MedicinesList(viewModel: SurveyViewModel) {
    val posts = viewModel.medicines
    LazyColumn {
        items(posts) {
                post ->
            Text(text = post.name ?: "")
        }
    }
    DisposableEffect(Unit) {
        viewModel.getMedicines()
        onDispose {}
    }
}