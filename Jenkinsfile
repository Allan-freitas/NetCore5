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
		stage('Sonarqube') {			
			environment {
				scannerHome = tool 'SonarQubeScanner'
			}
			steps {
				echo 'Analisando o que você fez...'
				withSonarQubeEnv('sonarqube') {
					sh "${scannerHome}/SonarScanner.MSBuild.dll begin /k:'Net5'"
				}
				timeout(time: 1, unit: 'MINUTES') {
					waitForQualityGate abortPipeline: true
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