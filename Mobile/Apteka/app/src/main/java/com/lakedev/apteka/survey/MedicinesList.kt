package com.lakedev.apteka.survey

import android.os.Build
import androidx.annotation.RequiresExtension
import androidx.compose.foundation.background
import androidx.compose.foundation.clickable
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.Spacer
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.height
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.layout.width
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.foundation.lazy.items
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.runtime.DisposableEffect
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.text.font.FontStyle
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.text.style.TextOverflow
import androidx.compose.ui.unit.dp
import androidx.compose.ui.unit.sp
import com.lakedev.apteka.data.dtos.medicine.MedicineGetDto

@RequiresExtension(extension = Build.VERSION_CODES.S, version = 7)
@Composable
fun MedicinesList(
    viewModel: SurveyViewModel,
    modifier: Modifier = Modifier,
    onNavigateToDetails: (id: Int) -> Unit,
) {
    val medicines = viewModel.medicines
    LazyColumn(modifier = modifier) {
        items(medicines) { medicine ->
            MedicineItem(
                medicine = medicine, modifier = Modifier
                    .fillMaxWidth()
                    .clickable {
                        medicine.medicineId?.let { onNavigateToDetails(it) }
                    } // переход на страницу medicine details
                    .padding(16.dp)
            )
        }
    }
    DisposableEffect(Unit) {
        viewModel.getMedicines()
        onDispose {}
    }
}

@Composable
fun MedicineItem(medicine: MedicineGetDto, modifier: Modifier) {
    Row(modifier = modifier, verticalAlignment = Alignment.CenterVertically) {
        Column(modifier = Modifier.weight(1f)) {
            Row(modifier = Modifier.fillMaxWidth()) {
                Text(
                    text = medicine.name ?: "",
                    fontWeight = FontWeight.SemiBold,
                    fontSize = 16.sp,
                    color = MaterialTheme.colorScheme.onBackground,
                    overflow = TextOverflow.Ellipsis,
                    maxLines = 1,
                    modifier = Modifier.weight(1f)
                )
                Spacer(modifier = Modifier.width(4.dp))
            }
            Spacer(modifier = Modifier.height(8.dp))
            Row(modifier = Modifier.fillMaxWidth()) {
                Text(
                    text = medicine.manufacturer ?: "",
                    fontStyle = FontStyle.Italic,
                    fontSize = 16.sp,
                    color = MaterialTheme.colorScheme.onBackground,
                    overflow = TextOverflow.Ellipsis,
                    maxLines = 1,
                    modifier = Modifier.weight(1f)
                )
                Spacer(modifier = Modifier.width(4.dp))
                Text(
                    text = medicine.price.toString() + "₽",
                    fontWeight = FontWeight.Light,
                    color = MaterialTheme.colorScheme.onBackground,
                    fontSize = 16.sp,
                )
            }
        }
    }
}



