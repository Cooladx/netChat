#!/bin/bash


# Run a fresh Postgres container
docker run -d \
  --name postgres-db \
  -e POSTGRES_PASSWORD=password \
  -v netchat-postgres-data:/var/lib/postgresql \
  -p 5432:5432 \
  postgres:latest
