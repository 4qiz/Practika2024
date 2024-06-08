package com.lakedev.apteka

import android.os.Build
import androidx.annotation.RequiresExtension
import androidx.compose.runtime.Composable
import androidx.navigation.NavHostController
import androidx.navigation.compose.NavHost
import androidx.navigation.compose.composable
import androidx.navigation.compose.rememberNavController
import com.lakedev.apteka.Destinations.SIGN_IN_ROUTE
import com.lakedev.apteka.Destinations.SIGN_UP_ROUTE
import com.lakedev.apteka.Destinations.SURVEY_RESULTS_ROUTE
import com.lakedev.apteka.Destinations.SURVEY_ROUTE
import com.lakedev.apteka.Destinations.WELCOME_ROUTE
import com.lakedev.apteka.signinsignup.SignInRoute
import com.lakedev.apteka.signinsignup.WelcomeRoute
import com.lakedev.apteka.survey.SurveyResultScreen
import com.lakedev.apteka.survey.SurveyRoute

object Destinations {
    const val WELCOME_ROUTE = "welcome"
    const val SIGN_UP_ROUTE = "signup/{email}"
    const val SIGN_IN_ROUTE = "signin/{email}"
    const val SURVEY_ROUTE = "survey"
    const val SURVEY_RESULTS_ROUTE = "surveyresults"
}

@RequiresExtension(extension = Build.VERSION_CODES.S, version = 7)
@Composable
fun AptekaNavHost(
    navController: NavHostController = rememberNavController(),
) {
    NavHost(
        navController = navController,
        startDestination = WELCOME_ROUTE,
    ) {
        composable(WELCOME_ROUTE) {
            WelcomeRoute(
                onNavigateToSignIn = {
                    navController.navigate("signin/$it")
                },
                onNavigateToSignUp = {
                    navController.navigate("signup/$it")
                },
                onSignInAsGuest = {
                    navController.navigate(SURVEY_ROUTE)
                },
            )
        }

        composable(SIGN_IN_ROUTE) {
            val startingEmail = it.arguments?.getString("email")
            SignInRoute(
                email = startingEmail,
                onSignInSubmitted = {
                    navController.navigate(SURVEY_ROUTE)
                },
                onSignInAsGuest = {
                    navController.navigate(SURVEY_ROUTE)
                },
                onNavUp = navController::navigateUp,
            )
        }
//
//        composable(SIGN_UP_ROUTE) {
//            val startingEmail = it.arguments?.getString("email")
//            SignUpRoute(
//                email = startingEmail,
//                onSignUpSubmitted = {
//                    navController.navigate(SURVEY_ROUTE)
//                },
//                onSignInAsGuest = {
//                    navController.navigate(SURVEY_ROUTE)
//                },
//                onNavUp = navController::navigateUp,
//            )
//        }
//
        composable(SURVEY_ROUTE) {
            SurveyRoute(
                onSurveyComplete = {
                    navController.navigate(SURVEY_RESULTS_ROUTE)
                },
                onNavUp = navController::navigateUp,
            )
        }

        composable(SURVEY_RESULTS_ROUTE) {
            SurveyResultScreen {
                navController.popBackStack(WELCOME_ROUTE, false)
            }
        }
    }
}
