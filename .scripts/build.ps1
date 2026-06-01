# Build script for siradig-calc
# Installs frontend dependencies and builds all sub-projects

$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$projectRoot = Split-Path -Parent $scriptDir

# Build frontend (npm)
Write-Host "Building frontend..." -ForegroundColor Green
& "$scriptDir\build-project.ps1" -SubPath "$projectRoot\frontend"
if ($LASTEXITCODE -ne 0) { exit 1 }

# Build backend (.NET)
Write-Host "Building backend..." -ForegroundColor Green
Push-Location "$projectRoot\backend"
try {
    dotnet build siradig-calc.sln -c Release
    if ($LASTEXITCODE -ne 0) {
        Write-Host "dotnet build failed! Exiting..." -ForegroundColor Red
        exit 1
    }
} finally {
    Pop-Location
}

Write-Host "All sub-projects built successfully!" -ForegroundColor Green
