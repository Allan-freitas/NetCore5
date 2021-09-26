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
            steps{
                sh "dotnet test ${workspace}/src/Application/Application.sln /p:CollectCoverage=true /p:CoverletOutputFormat=opencover --no-build"
            }
        }
		stage('Dotnet Test') {
			steps {
				sh 'python3 bin/build.py --test'
			}
        }
		stage('Sonarqube') {			
			environment {
				scannerHome = tool 'SonarQubeScanner'
			}
			steps {
				echo 'Analisando o que você fez...'
				withSonarQubeEnv('sonarqube') {
					sh "dotnet restore ${workspace}/src/Application/Application.sln"
					sh ("""dotnet ${scannerHome}/SonarScanner.MSBuild.dll begin /k:'Net5'""")
					sh "dotnet build ${workspace}/src/Application/Application.sln"
					sh "dotnet ${scannerHome}/SonarScanner.MSBuild.dll end"
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