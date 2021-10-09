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
					 sh "dotnet ${scannerHome}/SonarScanner.MSBuild.dll begin /k:'Net5' /d:sonar.cs.opencover.reportsPaths='/var/lib/jenkins/workspace/Net5_main/src/Application/Application.Tests/results/result.xml' /d:sonar.test.exclusions='test/**' /d:sonar.login='c1aff3a8a3c52de7b5278d95e8716406b3f7c963'"
					 sh """dotnet build ${workspace}/src/Application/Application.sln"""
					 sh """dotnet ${scannerHome}/SonarScanner.MSBuild.dll end /d:sonar.login='c1aff3a8a3c52de7b5278d95e8716406b3f7c963'"""
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