#!/bin/bash
set -e

echo ">>> Installing dotnet-ef tool..."
dotnet tool install --global dotnet-ef --version 9.*

export PATH="$PATH:/root/.dotnet/tools"

echo ">>> Restoring packages..."
dotnet restore BjjTracker.Api/BjjTracker.Api.csproj

MIGRATION_FILES=$(ls BjjTracker.Infrastructure/Migrations/*.cs 2>/dev/null | grep -v ModelSnapshot | wc -l)

if [ "$MIGRATION_FILES" -eq 0 ]; then
  echo ">>> No migrations found — creating InitialMigration..."
  dotnet ef migrations add InitialMigration \
    --project BjjTracker.Infrastructure \
    --startup-project BjjTracker.Api
else
  echo ">>> Migrations already exist — skipping creation."
fi

echo ">>> Applying migrations..."
dotnet ef database update \
  --project BjjTracker.Infrastructure \
  --startup-project BjjTracker.Api

echo ">>> Done."
