#docker run -e POSTGRES_PASSWORD=password postgres-db -it  --name postgres_db  -d postgres



docker run -d  --name postgres-db -e POSTGRES_PASSWORD=password --mount type=bind,src=../../database,target=/var/lib/postgresql/data postgres
