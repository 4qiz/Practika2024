package com.lakedev.apteka.data.dtos.signinsignup

import com.google.gson.annotations.SerializedName

data class LoginResponseDto(
    @SerializedName("username") var username: String? = null,
    @SerializedName("email") var email: String? = null,
    @SerializedName("token") var token: String? = null
)
