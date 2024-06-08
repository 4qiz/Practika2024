package com.lakedev.apteka.data.dtos.signinsignup

import com.google.gson.annotations.SerializedName

data class LoginRequestDto(
    @SerializedName("username" ) var username : String? = null,
    @SerializedName("password" ) var password : String? = null
)
