#!/bin/bash
# Run HighlightedApp with Docker Compose from GitHub
# Usage: ./run.sh [hub|build]
#   hub   - use pre-built image (default)
#   build - build from source

set -e

REPO_URL="https://github.com/Buraska/HighlightedApp.git"
PROJECT_DIR="HighlightedApp"

echo "=== HighlightedApp Docker Runner ==="

# Clone or update repo
if [ -d "$PROJECT_DIR" ] && [ -d "$PROJECT_DIR/.git" ]; then
  echo "Updating existing clone..."
  cd "$PROJECT_DIR"
  git pull
else
  echo "Cloning repository..."
  git clone "$REPO_URL" "$PROJECT_DIR"
  cd "$PROJECT_DIR"
fi

cd Backend

MODE="${1:-hub}"

if [ "$MODE" = "build" ]; then
  echo "Building frontend..."
  cd ../FrontEnd
  npm ci
  npm run build:wwwroot
  cd ../Backend
  echo "Starting with docker-compose (build from source)..."
  docker compose up --build -d
else
  echo "Starting with docker-compose (pre-built image)..."
  docker compose -f docker-compose.hub.yml up -d
fi

echo ""
echo "=== Done ==="
echo "App: http://localhost:5192"
echo "PostgreSQL: localhost:5432"
echo ""
echo "Stop with: cd Backend && docker compose down"
