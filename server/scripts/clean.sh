#!/bin/bash

# Stops container and removes the container.

docker stop postgres-db

docker rm -f postgres-db
