package com.lakedev.apteka.data.repository

import android.net.http.HttpException
import android.os.Build
import android.util.Log
import androidx.annotation.RequiresExtension
import androidx.compose.runtime.Immutable
import com.lakedev.apteka.data.RetrofitInstance
import com.lakedev.apteka.data.dtos.signinsignup.LoginRequestDto
import com.lakedev.apteka.data.dtos.signinsignup.LoginResponseDto
import java.net.UnknownHostException

sealed class User {
    @Immutable
    data class LoggedInUser(val email: String, var response: LoginResponseDto) : User()
    object GuestUser : User()
    object NoUserLoggedIn : User()
}

/**
 * Repository that holds the logged in user.
 *
 * In a production app, this class would also handle the communication with the backend for
 * sign in and sign up.
 */
object UserRepository {
    private val apiService = RetrofitInstance.api
    private var _user: User = User.NoUserLoggedIn
    val user: User
        get() = _user

    private var _isLoggedIn: Boolean = false
    val isLoggedIn: Boolean
        get() = _isLoggedIn

    @RequiresExtension(extension = Build.VERSION_CODES.S, version = 7)
    suspend fun signIn(email: String, password: String) {
        val request = LoginRequestDto(email, password)
        try {
            val response = apiService.login(request)
            if (response.isSuccessful) {
                val headers = response.headers()
                val body = response.body()
                if (body != null) {
                    _user = User.LoggedInUser(email, body)
                    _isLoggedIn = true
                }
            } else {
                val code = response.code()
                Log.e("UserRepository", code.toString())
            }
        } catch (e: HttpException) {
            Log.e("UserRepository", e.message ?: "")
        } catch (e: UnknownHostException) {
            // TODO handle the error.
            Log.e("UserRepository", e.message ?: "")
        }
    }

//    @Suppress("UNUSED_PARAMETER")
//    fun signUp(email: String, password: String) {
//        _user = User.LoggedInUser(email)
//    }

    fun signInAsGuest() {
        _user = User.GuestUser
        _isLoggedIn = true
    }

    fun isKnownUserEmail(email: String): Boolean {
        // if the email contains "sign up" we consider it unknown
        return !email.contains("signup")
    }

    fun logout(){
        _isLoggedIn = false
        _user = User.NoUserLoggedIn
    }
}
