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
        stage('Test') {
            steps {
                echo 'Testing..'
				sh "dotnet test ${workspace}/src/Application/Application.Tests /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
					dotnet build-server shutdown
					dotnet sonarscanner begin /k:"net5" /d:sonar.host.url=http://192.168.88.100:9000 /d:sonar.cs.opencover.reportsPaths="${workspace}/src/Application/Application.Tests/coverage.opencover.xml" /d:sonar.coverage.exclusions="**Tests*.cs"
					dotnet build
					dotnet sonarscanner end"
            }
        }
        stage('Deploy') {
            steps {
                echo 'Deploying....'
            }
        }
    }
}