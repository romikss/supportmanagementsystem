:: 1. Restore nuget packages
call nuget.exe restore "%DEPLOYMENT_SOURCE%\SupportManagementSystem.sln" -MSBuildPath "%MSBUILD_15_DIR%"

:: 2. Build and publish
call "%MSBUILD_15_DIR%\MSBuild.exe" "%DEPLOYMENT_SOURCE%\SupportManagementSystem.sln" /t:src\SupportManagementSystem /p:DeployOnBuild=true /p:configuration=Release /p:publishurl="%DEPLOYMENT_TEMP%"
