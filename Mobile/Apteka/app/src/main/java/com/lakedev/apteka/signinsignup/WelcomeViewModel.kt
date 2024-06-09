package com.lakedev.apteka.signinsignup

import androidx.lifecycle.ViewModel
import androidx.lifecycle.ViewModelProvider
import com.lakedev.apteka.data.repository.UserRepository

class WelcomeViewModel(private val userRepository: UserRepository) : ViewModel() {

    fun handleContinue(
        email: String,
        onNavigateToSignIn: (email: String) -> Unit,
        onNavigateToSignUp: (email: String) -> Unit,
    ) {
        if (userRepository.isKnownUserEmail(email)) {
            onNavigateToSignIn(email)
        }
        // регистрация отключена
//        else {
//            onNavigateToSignUp(email)
//        }
    }

    fun signInAsGuest(
        onSignInComplete: () -> Unit,
    ) {
        userRepository.signInAsGuest()
        onSignInComplete()
    }
}

class WelcomeViewModelFactory : ViewModelProvider.Factory {
    @Suppress("UNCHECKED_CAST")
    override fun <T : ViewModel> create(modelClass: Class<T>): T {
        if (modelClass.isAssignableFrom(WelcomeViewModel::class.java)) {
            return WelcomeViewModel(UserRepository) as T
        }
        throw IllegalArgumentException("Unknown ViewModel class")
    }
}
