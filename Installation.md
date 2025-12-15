# NetChat – Installation Guide

This guide explains how to set up **NetChat** on a Linux machine. It covers **Node.js (frontend)**, **.NET 9 (backend)**, and **PostgreSQL via Docker**. You can also do this through WSL or Windows, but it's recommended to do through either Linux or WSL.
If you are using WSL, be sure to change localhost to 127.0.0.1 as WSL is a bit different from exposing it's ports.


## 1. Install Node.js (Frontend)



### 1.1 Install curl and update system

```bash
sudo apt update && sudo apt upgrade -y
sudo apt install -y curl
```

### 1.2 Install NVM (Node Version Manager)

```bash
curl -o- https://raw.githubusercontent.com/nvm-sh/nvm/v0.40.3/install.sh | bash
```

Reload your shell:

```bash
export NVM_DIR="$([ -z "${XDG_CONFIG_HOME-}" ] && printf %s "${HOME}/.nvm" || printf %s "${XDG_CONFIG_HOME}/nvm")"
[ -s "$NVM_DIR/nvm.sh" ] && . "$NVM_DIR/nvm.sh"
```
or copy this through terminal.

Verify installation:

```bash
command -v nvm
```

### 1.3 Install Node.js (LTS)

```bash
nvm install --lts
nvm use --lts
```

Verify:

```bash
node -v
npm -v
```

---

## 2. Install .NET 9 (Backend)

For Ubuntu **22.04**, follow Microsoft’s official instructions:

🔗 [https://learn.microsoft.com/en-us/dotnet/core/install/linux-ubuntu-install?tabs=dotnet9&pivots=os-linux-ubuntu-2204](https://learn.microsoft.com/en-us/dotnet/core/install/linux-ubuntu-install?tabs=dotnet9&pivots=os-linux-ubuntu-2204)

If you are on different distribution, follow that for other Linux distros. You only need the SDK, as that will install the run time as well.


Verify:

```bash
dotnet --version
```
---

## 3. Install Docker (Database)

```bash
https://docs.docker.com/engine/install/ubuntu/
```

If you are on WSL: https://learn.microsoft.com/en-us/windows/wsl/install

(Optional – avoid sudo every time for docker):

```bash
https://docs.docker.com/engine/install/linux-postinstall
```
---

## 4. Database Setup (PostgreSQL via Docker)

### 4.1 Navigate to scripts folder

```bash
cd netChat/server/scripts
```

### 4.2 Start PostgreSQL container

```bash
./run.sh
```

### 4.3 Initialize database schema

Copy SQL file into container:

```bash
docker cp create.sql postgres-db:/create.sql
```

Enter Postgres shell:

```bash
docker exec -it postgres-db psql -U postgres
```

Inside psql:

```sql
\c netchatdb
\i /create.sql
```

Verify tables:

```sql
\dt
```

---

## 5. Backend Setup (.NET API + SignalR)

```bash
cd netChat/server
dotnet run
```

Backend will run on:

```
http://localhost:5019
```

---

## 6. Frontend Setup (React)

```bash
cd netChat/app
npm install
npm run dev
```

Frontend will run on:

```
http://localhost:5173
```

---

## 7. Running the Application

1. Start **Docker** (Postgres)
2. Run **backend** (`dotnet run`)
3. Run **frontend** (`npm run dev`)
4. Open browser → [http://localhost:5173](http://localhost:5173)

---

## 8. Notes

* Users can join rooms via **room code**
* Real-time messaging uses **SignalR WebSockets**
* Database persists inside Docker container

---

## 9. Troubleshooting

* If DB errors occur, ensure you are connected to `netchatdb`
* Check Docker container status:

```bash
docker ps
```

* Restart container if needed:

Go to netChat/server/scripts and run clean.sh
```bash
./clean.sh
```
---

