# Installs dependencies and builds a single frontend sub-project
# Usage: build-project.ps1 -SubPath <absolute-path>

param(
    [Parameter(Mandatory)][string]$SubPath
)

$nodeModulesPath = "$SubPath\node_modules"
$lockFilePath = "$SubPath\package-lock.json"
$lockHashFile = "$SubPath\node_modules\.package-lock-hash"

Push-Location $SubPath
try {
    $needsInstall = $true
    if (Test-Path $nodeModulesPath) {
        if (Test-Path $lockFilePath) {
            $currentHash = (Get-FileHash $lockFilePath -Algorithm MD5).Hash
            if (Test-Path $lockHashFile) {
                $savedHash = Get-Content $lockHashFile -Raw
                if ($currentHash -eq $savedHash.Trim()) {
                    Write-Host "Dependencies up to date, skipping npm install..." -ForegroundColor Yellow
                    $needsInstall = $false
                }
            }
        }
    }

    if ($needsInstall) {
        Write-Host "Installing dependencies..." -ForegroundColor Cyan
        $env:npm_config_loglevel = "error"
        npm install --legacy-peer-deps --prefer-offline --no-audit --no-fund
        if ($LASTEXITCODE -ne 0) {
            Write-Host "npm install failed! Exiting..." -ForegroundColor Red
            exit 1
        }
        if (Test-Path $lockFilePath) {
            $currentHash = (Get-FileHash $lockFilePath -Algorithm MD5).Hash
            $currentHash | Out-File -FilePath $lockHashFile -NoNewline
        }
    }

    npm run build
    if ($LASTEXITCODE -ne 0) {
        Write-Host "Build failed! Exiting..." -ForegroundColor Red
        exit 1
    }
} finally {
    Pop-Location
}
