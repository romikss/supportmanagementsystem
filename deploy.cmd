:: 1. Restore nuget packages
call nuget.exe restore "%DEPLOYMENT_SOURCE%\SupportManagementSystem.sln" -MSBuildPath "%MSBUILD_15_DIR%"

:: 2. Build and publish
call "%MSBUILD_15_DIR%\MSBuild.exe" "%DEPLOYMENT_SOURCE%\SupportManagementSystem.sln" /t:SupportManagementSystem /p:DeployOnBuild=true /p:configuration=Release /p:publishurl="%DEPLOYMENT_TEMP%"

:: 3. KuduSync
call "%KUDU_SYNC_CMD%" -v 50 -f "%DEPLOYMENT_TEMP%" -t "%DEPLOYMENT_TARGET%" -n "%NEXT_MANIFEST_PATH%" -p "%PREVIOUS_MANIFEST_PATH%" -i ".git;.hg;.deployment;deploy.cmd"
