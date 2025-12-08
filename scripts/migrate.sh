#!/bin/bash
set -e

echo "Running idempotent EF Core migrations..."
echo "------------------------------------------"

# Ждём, пока PostgreSQL станет доступным
until pg_isready -h postgres -U template_user; do
  echo "Postgres is unavailable - sleeping"
  sleep 2
done

echo "Postgres is up!"

# Выполнить SQL файл
psql "host=postgres port=5432 dbname=template_db user=template_user password=template_pass" \
     -f migrations.sql

echo "Migrations completed successfully!"
