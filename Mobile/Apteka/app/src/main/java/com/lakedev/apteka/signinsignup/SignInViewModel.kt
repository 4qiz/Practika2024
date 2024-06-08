package com.lakedev.apteka.signinsignup

import android.os.Build
import android.util.Log
import androidx.annotation.RequiresExtension
import androidx.compose.runtime.MutableState
import androidx.compose.runtime.mutableStateOf
import androidx.lifecycle.ViewModel
import androidx.lifecycle.ViewModelProvider
import androidx.lifecycle.viewModelScope
import com.lakedev.apteka.data.RetrofitInstance
import kotlinx.coroutines.launch

class SignInViewModel(private val userRepository: UserRepository) : ViewModel() {

    @RequiresExtension(extension = Build.VERSION_CODES.S, version = 7)
    fun signIn(
        email: String,
        password: String,
        onSignInComplete: () -> Unit,
    ) {
        viewModelScope.launch {
            try {
                userRepository.signIn(email, password)
                if(userRepository.isLoggedIn){
                    onSignInComplete()
                }
            } catch (e: Exception) {
                Log.e("SignInViewModel", e.message ?: "")
            }
        }
    }

    fun signInAsGuest(
        onSignInComplete: () -> Unit,
    ) {
        userRepository.signInAsGuest()
        onSignInComplete()
    }
}

class SignInViewModelFactory : ViewModelProvider.Factory {
    @Suppress("UNCHECKED_CAST")
    override fun <T : ViewModel> create(modelClass: Class<T>): T {
        if (modelClass.isAssignableFrom(SignInViewModel::class.java)) {
            return SignInViewModel(UserRepository) as T
        }
        throw IllegalArgumentException("Unknown ViewModel class")
    }
}