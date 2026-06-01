# Relaunch script for the backend (api)
# Rebuilds and restarts the api container

$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$projectRoot = Split-Path -Parent $scriptDir

Write-Host "Restarting api container..." -ForegroundColor Green
Push-Location "$projectRoot\.infra\local"
try {
    docker compose up -d --build api
    if ($LASTEXITCODE -ne 0) {
        Write-Host "Docker compose failed! Exiting..." -ForegroundColor Red
        exit 1
    }
} finally {
    Pop-Location
}

Write-Host "Backend relaunch completed successfully!" -ForegroundColor Green
