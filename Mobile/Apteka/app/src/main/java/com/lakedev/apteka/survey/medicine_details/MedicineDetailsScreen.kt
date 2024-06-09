package com.lakedev.apteka.survey.medicine_details

import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.compose.foundation.Image
import androidx.compose.foundation.background
import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Box
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.Spacer
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.height
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.layout.wrapContentHeight
import androidx.compose.foundation.shape.RoundedCornerShape
import androidx.compose.foundation.text.KeyboardOptions
import androidx.compose.material3.Button
import androidx.compose.material3.Card
import androidx.compose.material3.CardDefaults
import androidx.compose.material3.ElevatedCard
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.OutlinedTextField
import androidx.compose.material3.Scaffold
import androidx.compose.material3.Snackbar
import androidx.compose.material3.SnackbarHost
import androidx.compose.material3.SnackbarHostState
import androidx.compose.material3.Text
import androidx.compose.material3.TextButton
import androidx.compose.runtime.LaunchedEffect
import androidx.compose.runtime.collectAsState
import androidx.compose.runtime.getValue
import androidx.compose.runtime.livedata.observeAsState
import androidx.compose.runtime.remember
import androidx.compose.runtime.rememberCoroutineScope
import androidx.compose.ui.Alignment
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.layout.ContentScale
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.text.input.KeyboardType
import androidx.compose.ui.text.style.TextAlign
import androidx.compose.ui.unit.dp
import androidx.compose.ui.window.Dialog
import androidx.lifecycle.compose.collectAsStateWithLifecycle
import coil.compose.rememberAsyncImagePainter
import com.lakedev.apteka.R
import com.lakedev.apteka.data.dtos.medicine.WarehouseHasMedicineDto
import com.lakedev.apteka.util.supportWideScreen
import kotlinx.coroutines.launch

@Composable
fun MedicineDetailsScreen(
    id: Int,
    viewModel: MedicineDetailsViewModel,
    modifier: Modifier = Modifier,
    onNavUp: () -> Unit,
) {
    val scope = rememberCoroutineScope()

    val medicine by viewModel.medicine.observeAsState()
    LaunchedEffect(Unit) {
        scope.launch {
            viewModel.fetchMedicine(id)
        }
    }

    val state by viewModel.state.collectAsState()

    val snackbarHostState = remember { SnackbarHostState() }

    val snackbarErrorText = stringResource(id = R.string.feature_not_available) //текст сообщения
    val snackbarActionLabel = stringResource(id = R.string.dismiss)
    Scaffold(topBar = {
        MedicineDetailsTopAppBar(
            topAppBarText = stringResource(id = R.string.medicine),
            onNavUp = onNavUp,
        )
    }, content = { contentPadding ->
        MedicineDetailsLayout(
            modifier = Modifier.supportWideScreen(),
            contentPadding = contentPadding,
        ) {
            Column(modifier = Modifier.fillMaxWidth()) {
                Box(
                    modifier = Modifier
                        .height(300.dp)
                        .background(Color.White),
                    contentAlignment = Alignment.Center
                ) {
                    Image(
                        painter = rememberAsyncImagePainter(medicine?.image ?: ""),
                        contentDescription = null,
                        contentScale = ContentScale.Inside,
                        modifier = Modifier.fillMaxWidth()
                    )
                }
                Spacer(modifier = Modifier.height(16.dp))
                DetailsContent(
                    name = medicine?.name ?: "",
                    modifier = Modifier.padding(horizontal = 12.dp),
                    price = medicine?.price.toString() ?: "",
                    manufacturer = medicine?.manufacturer ?: "",
                    warehousesList = medicine?.warehouseHasMedicineDto ?: emptyList(),
                    state = state,
                    onDismiss = viewModel::onDismiss,
                    onConfirmation = viewModel::onConfirmation,
                    onShowDialog = viewModel::onShowDialog,
                    updateQuantity = viewModel::updateQuantity,
                    quantity = viewModel.quantity
                )
                Spacer(modifier = Modifier.height(16.dp))
//                    TextButton(
//                        onClick = {
//                            scope.launch {
//                                snackbarHostState.showSnackbar(
//                                    message = snackbarErrorText,
//                                    actionLabel = snackbarActionLabel
//                                )
//                            }
//                        },
//                        modifier = Modifier.fillMaxWidth()
//                    ) {
//                        Text(text = "показать сообщение")
//                    }
            }
        }
    })

    Box(modifier = Modifier.fillMaxSize()) {
        ErrorSnackbar(
            snackbarHostState = snackbarHostState,
            onDismiss = { snackbarHostState.currentSnackbarData?.dismiss() },
            modifier = Modifier
                .align(Alignment.BottomCenter)
                .padding(bottom = 50.dp)
        )
    }
}

@Composable
fun DetailsContent(
    name: String,
    modifier: Modifier = Modifier,
    price: String,
    manufacturer: String,
    warehousesList: List<WarehouseHasMedicineDto>,
    state: MyState,
    onDismiss: () -> Unit,
    onConfirmation: () -> Unit,
    onShowDialog: (Int) -> Unit,
    updateQuantity: (String) -> Unit,
    quantity: Int
) {
    Column(
        modifier = modifier.fillMaxWidth(),
        //horizontalAlignment = Alignment.CenterHorizontally
    ) {
        Text(
            text = name,
            style = MaterialTheme.typography.headlineSmall,
        )

        Text(
            text = manufacturer,
            style = MaterialTheme.typography.bodyLarge,
            modifier = Modifier.padding(top = 8.dp)
        )

        ElevatedCard(
            elevation = CardDefaults.cardElevation(
                defaultElevation = 6.dp
            ), colors = CardDefaults.cardColors(
                containerColor = MaterialTheme.colorScheme.surfaceVariant,
            ), modifier = Modifier
                .fillMaxWidth()
                .padding(top = 16.dp)
        ) {
            Text(
                text = "$price ₽",
                modifier = Modifier.padding(16.dp),
                style = MaterialTheme.typography.titleLarge,
                textAlign = TextAlign.Center,
            )
        }

        Spacer(modifier = Modifier.height(16.dp))

        if (warehousesList.isNotEmpty()) {
            ProductDetails(
                warehousesList,
                state = state,
                onDismiss = onDismiss,
                onConfirmation = onConfirmation,
                onShowDialog = onShowDialog,
                updateQuantity = updateQuantity,
                quantity = quantity
            )
        }
    }
}

@Composable
fun ProductDetails(
    warehousesList: List<WarehouseHasMedicineDto>,
    state: MyState,
    onDismiss: () -> Unit,
    onConfirmation: () -> Unit,
    onShowDialog: (Int) -> Unit,
    updateQuantity: (String) -> Unit,
    quantity: Int
) {
    Text(
        text = stringResource(id = R.string.availability),
        style = MaterialTheme.typography.headlineSmall,
        fontWeight = FontWeight.Bold,
    )
    Column(modifier = Modifier.padding(top = 8.dp)) {
        warehousesList.forEach { warehouse ->
            Row(
                modifier = Modifier.fillMaxWidth(),
                horizontalArrangement = Arrangement.SpaceBetween,
                verticalAlignment = Alignment.CenterVertically
            ) {
                Text(
                    text = "Склад №${warehouse.warehouseId}", modifier = Modifier
                )
                Text(
                    text = "${warehouse.stockQuantity} шт.",
                    color = if (warehouse.stockQuantity!! < 10) MaterialTheme.colorScheme.error else MaterialTheme.colorScheme.onBackground
                )
                Button(onClick = { onShowDialog(warehouse.warehouseId ?: 0) } ) {
                    Text(text = "Списать")
                }
            }
            if (state.showDialog) {
                DialogWithImage(
                    onDismissRequest = onDismiss,
                    onConfirmation = onConfirmation,
                    warehouseId = warehouse.warehouseId ?: -1,
                    medicineId = 1,
                    updateQuantity = updateQuantity,
                    quantity = quantity
                )
            }
        }
    }
}


@Composable
fun WarehouseItem(warehouse: WarehouseHasMedicineDto) {
    Row(
        horizontalArrangement = Arrangement.SpaceBetween,
        modifier = Modifier.padding(vertical = 9.dp)
    ) {
        Text(text = "Склад №" + warehouse.warehouseId)
        Text(text = warehouse.stockQuantity.toString() + "шт.")
    }
}

@Composable
fun ErrorSnackbar(
    snackbarHostState: SnackbarHostState, modifier: Modifier = Modifier, onDismiss: () -> Unit = { }
) {
    SnackbarHost(
        hostState = snackbarHostState, snackbar = { data ->
            Snackbar(modifier = Modifier.padding(16.dp), content = {
                Text(
                    text = data.visuals.message,
                    style = MaterialTheme.typography.bodyMedium,
                )
            }, action = {
                data.visuals.actionLabel?.let {
                    TextButton(onClick = onDismiss) {
                        Text(
                            text = stringResource(id = R.string.dismiss),
                            color = MaterialTheme.colorScheme.inversePrimary
                        )
                    }
                }
            })
        }, modifier = modifier
            .fillMaxWidth()
            .wrapContentHeight(Alignment.Bottom)
    )
}

@Composable
fun DialogWithImage(
    medicineId: Int, warehouseId: Int, onDismissRequest: () -> Unit, onConfirmation: (
        //medicineId: Int, warehouseId: Int, quantity: Int
    ) -> Unit, updateQuantity: (String) -> Unit, quantity: Int
) {

    Dialog(onDismissRequest = { onDismissRequest() }) {
        Card(
            modifier = Modifier
                .fillMaxWidth()
                .height(280.dp)
                .padding(16.dp),
            shape = RoundedCornerShape(16.dp),
        ) {
            Column(
                modifier = Modifier
                    .fillMaxSize()
                    .padding(
                        end = 8.dp,
                        start = 8.dp,
                        bottom = 8.dp,
                    ),
                verticalArrangement = Arrangement.Center,
                horizontalAlignment = Alignment.CenterHorizontally,
            ) {
                Text(
                    text = stringResource(id = R.string.writeoff_dialog_text),
                    modifier = Modifier.padding(16.dp),
                )
                OutlinedTextField(
                    value = quantity.toString(),
                    onValueChange = { quantity -> updateQuantity(quantity) },
                    keyboardOptions = KeyboardOptions(
                        keyboardType = KeyboardType.Number
                    ),
                    label = { Text(stringResource(id = R.string.writeoff_dialog_label)) },
                    modifier = Modifier.padding(horizontal = 8.dp),
                )
                Row(
                    modifier = Modifier
                        .fillMaxWidth()
                        .padding(top = 6.dp),
                    horizontalArrangement = Arrangement.SpaceEvenly,
                ) {
                    TextButton(
                        onClick = { onDismissRequest() },
                        modifier = Modifier.padding(8.dp),
                    ) {
                        Text(stringResource(id = R.string.dismiss))
                    }
                    TextButton(
                        onClick = {
                            onConfirmation()
                        },
                        modifier = Modifier.padding(8.dp),
                    ) {
                        Text(
                            stringResource(id = R.string.writeoff),
                            color = MaterialTheme.colorScheme.error
                        )
                    }
                }
            }
        }
    }
}