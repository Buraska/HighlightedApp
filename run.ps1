# Run HighlightedApp with Docker Compose from GitHub
# Usage: .\run.ps1 [hub|build]
#   hub   - use pre-built image (default)
#   build - build from source

$ErrorActionPreference = "Stop"

$RepoUrl = "https://github.com/Buraska/HighlightedApp.git"
$ProjectDir = "HighlightedApp"

Write-Host "=== HighlightedApp Docker Runner ===" -ForegroundColor Cyan

# Clone or update repo
if (Test-Path "$ProjectDir\.git") {
    Write-Host "Updating existing clone..."
    Set-Location $ProjectDir
    git pull
} else {
    Write-Host "Cloning repository..."
    git clone $RepoUrl $ProjectDir
    Set-Location $ProjectDir
}

Set-Location Backend

$Mode = if ($args[0]) { $args[0] } else { "hub" }

if ($Mode -eq "build") {
    Write-Host "Building frontend..."
    Set-Location ..\FrontEnd
    npm ci
    npm run build:wwwroot
    Set-Location ..\Backend
    Write-Host "Starting with docker-compose (build from source)..."
    docker compose up --build -d
} else {
    Write-Host "Starting with docker-compose (pre-built image)..."
    docker compose -f docker-compose.hub.yml up -d
}

Write-Host ""
Write-Host "=== Done ===" -ForegroundColor Green
Write-Host "App: http://localhost:5192"
Write-Host "PostgreSQL: localhost:5432"
Write-Host ""
Write-Host "Stop with: cd Backend; docker compose down"
