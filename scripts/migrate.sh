#!/bin/bash
set -e

echo "Running idempotent EF Core migrations..."
echo "------------------------------------------"

until pg_isready -h postgres -U template_user -d template_db; do
  echo "Postgres is unavailable - sleeping"
  sleep 2
done

echo "Postgres is up!"

PGPASSWORD="template_pass" \
psql "host=postgres port=5432 dbname=template_db user=template_user" \
    -f migrations.sql

echo "Migrations completed successfully!"