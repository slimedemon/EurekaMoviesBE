> This project is a clone of Eureka-Movies-BE (https://github.com/git03-Nguyen/Eureka-Movies-BE).

# EurekaMoviesBE 🎬

**Frontend:** [Eureka-Movies-FE](https://github.com/nhantrnh/Eureka-Movies-FE)  
**Team Members:**  
- 21120172 – Nguyễn Tuấn Đạt  
- 21120171 – Nguyễn Đình Ánh  
- 21120105 – Trương Thành Nhân  

## 🧭 Overview
**EurekaMoviesBE** is a backend for an AI-powered movie streaming platform built with **ASP.NET Core**.  
It supports **OAuth2/OIDC authentication**, **AI recommendations**, and **multi-database integration** using **PostgreSQL** and **MongoDB**.

## 🚀 Features
- 🔐 Authentication (Email, Google OAuth, Password Reset)
- 🎞️ Movie search, trending lists, latest trailers
- 🤖 AI-powered recommendations (RAG & LLM)
- ❤️ User personalization (Watchlist, Favorites, Ratings)
- 📧 Gmail SMTP notifications

## 🧩 Tech Stack
- **Backend:** ASP.NET Core Web API, MediatR (CQRS), FluentValidation  
- **Databases:** PostgreSQL (users, identity), MongoDB (movie catalog)  
- **Auth:** ASP.NET Identity + Duende IdentityServer  
- **Services:** Azure LLM, Google OAuth, Gmail SMTP  
- **Deployment:** Docker & Docker Compose  

## ⚙️ Setup
### Prerequisites
- .NET 6+  
- PostgreSQL & MongoDB  
- Docker (optional)

### Run Locally
```bash
dotnet ef database update
dotnet run --project MovieStreaming
