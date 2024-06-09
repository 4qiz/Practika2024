package com.lakedev.apteka.data

import com.lakedev.apteka.data.dtos.medicine.MedicineGetDto
import com.lakedev.apteka.data.dtos.signinsignup.LoginRequestDto
import com.lakedev.apteka.data.dtos.signinsignup.LoginResponseDto
import retrofit2.Response
import retrofit2.http.Body
import retrofit2.http.GET
import retrofit2.http.POST
import retrofit2.http.Path

interface ApiService {
    @POST("Account/login")
    suspend fun login(@Body loginRequestDto: LoginRequestDto): Response<LoginResponseDto>

    @GET("Medicine")
    suspend fun getMedicines(): List<MedicineGetDto>

    @GET("Medicine/{id}")
    suspend fun getMedicine(@Path("id") id: Int): MedicineGetDto
}