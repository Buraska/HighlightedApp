![LogoH](https://github.com/Buraska/HighlightedApp/assets/73584781/ac0c7474-3a31-4587-9d5c-1f0801b1f4f1)
# Highlighted - Book Reading and Note-taking App

This is an MVP of the Highlighted platform. The idea is simple: users read books on the web, create highlights and notes, and interact with other users by sharing, commenting on, and liking highlights and notes.

In this version, the core features are already implemented, including highlighting and note-taking. While the project is not perfect yet, its main goal is to demonstrate my knowledge of backend architecture and my skills in building a Vue application.  

## Tech Stack

- **Backend:** ASP.NET Core 6 (C#), REST API, JWT/Cookie auth
- **Database:** PostgreSQL
- **Frontend:** Vue 3 + TypeScript + Vite
- **State/HTTP:** Pinia + Axios
- **Styling:** Tailwind CSS
- **Containerization:** Docker + Docker Compose
- **API Docs:** Swagger (OpenAPI)

## Architecture Notes

- Backend and API are hosted in ASP.NET Core.
- Vue frontend is built and served by the backend (`wwwroot`).
- API + app are exposed on **port `5192`** by default.
- PostgreSQL runs in a separate container (`webapp-postgres`).

## Quick Start (Docker Hub)

Make sure port `5192` is free before launching.

### Windows (PowerShell)

```powershell
curl -o docker-compose.yml -uri https://raw.githubusercontent.com/Buraska/HighlightedApp/refs/heads/main/Backend/docker-compose.hub.yml; docker-compose up;start http://localhost:5192/
```

```bash
curl -L "https://raw.githubusercontent.com/Buraska/HighlightedApp/refs/heads/main/Backend/docker-compose.hub.yml" -o docker-compose.yml
docker compose up -d
xdg-open http://localhost:5192/ 2>/dev/null || open http://localhost:5192/
```
