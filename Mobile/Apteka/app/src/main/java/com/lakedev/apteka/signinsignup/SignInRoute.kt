package com.lakedev.apteka.signinsignup

import android.os.Build
import androidx.annotation.RequiresExtension
import androidx.compose.runtime.Composable
import androidx.lifecycle.viewmodel.compose.viewModel

@RequiresExtension(extension = Build.VERSION_CODES.S, version = 7)
@Composable
fun SignInRoute(
    email: String?,
    onSignInSubmitted: () -> Unit,
    onSignInAsGuest: () -> Unit,
    onNavUp: () -> Unit,
) {
    val signInViewModel: SignInViewModel = viewModel(factory = SignInViewModelFactory())
    SignInScreen(
        email = email,
        onSignInSubmitted = { email, password ->
            signInViewModel.signIn(email, password, onSignInSubmitted)
        },
        onSignInAsGuest = {
            signInViewModel.signInAsGuest(onSignInAsGuest)
        },
        onNavUp = onNavUp,
    )
}