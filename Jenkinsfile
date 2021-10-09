pipeline {
    agent any

    stages {
        stage('Restore packages') {
            steps {
                echo 'Restoring...'
				echo "${workspace}"
				sh "dotnet restore ${workspace}/src/Application/Application.sln"
            }
        }
		stage('Build') {
            steps {
                echo 'Building...'
				sh "dotnet build ${workspace}/src/Application/Application.sln"
            }
        }
		stage("Tests"){
            steps {
                sh "dotnet test ${workspace}/src/Application/Application.sln /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput='/var/lib/jenkins/workspace/Net5_main/src/Application/Application.Tests/results/result.xml' --no-build"
            }
        }
		stage('Coverage') {
			environment {
				scannerHome = tool 'SonarQubeScanner'
			}
			steps {
				withSonarQubeEnv('sonarqube') {
					 sh """dotnet restore ${workspace}/src/Application/Application.sln"""
					 sh """dotnet ${scannerHome}/SonarScanner.MSBuild.dll begin /k:'Net5""" """/d:sonar.cs.opencover.reportsPaths='/var/lib/jenkins/workspace/Net5_main/src/Application/Application.Tests/results/result.xml'""" """/d:sonar.test.exclusions='test/**'"""
					 sh "dotnet build ${workspace}/src/Application/Application.sln"
					 sh "dotnet ${scannerHome}/SonarScanner.MSBuild.dll end"
				}
			}
		}
        stage('Deploy') {
            steps {
                echo 'Deploying....'
            }
        }
    }
}