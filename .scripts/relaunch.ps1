# Relaunch script for siradig-calc
# Relaunches the frontend and backend containers

$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path

Write-Host "=== Relaunching frontend ===" -ForegroundColor Cyan
& "$scriptDir\relaunch-front.ps1"
if ($LASTEXITCODE -ne 0) { exit 1 }

Write-Host "=== Relaunching backend ===" -ForegroundColor Cyan
& "$scriptDir\relaunch-back.ps1"
if ($LASTEXITCODE -ne 0) { exit 1 }

Write-Host "Full relaunch completed successfully!" -ForegroundColor Green
