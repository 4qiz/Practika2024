package com.lakedev.apteka.data

import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory

object RetrofitInstance {
    private const val BASE_URL = "https://backend.softsols.online/api/"
    val api: ApiService by lazy {
        val retrofit =
            Retrofit.Builder().baseUrl(BASE_URL)
                .addConverterFactory(GsonConverterFactory.create())
                .build()
        retrofit.create(ApiService::class.java)
    }
}