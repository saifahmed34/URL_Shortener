# URL Shortener

A simple **C#/.NET** web application that shortens long URLs and redirects users via generated short links.

## 🚀 Features

- Accepts a long URL, generates a short key.
- Redirects short URLs to the original long URLs.
- Built using C# and ASP.NET (assuming from `.sln` file).
- Easy to deploy and extend.

## 🗂️ Project Structure

- URL_Shortener.sln # Solution file

   - URL_Shortener/ # Main project folder

   - Controllers/ # MVC or API controllers

   - Models/ # Data models (e.g., URL mapping)

   - Services/ # Business logic (URL generation, DB storage)

   - Data/ # Database context & migrations

   - .gitignore

   - .gitattributes


## 🧰 Prerequisites

- [.NET SDK (6.0+)](https://dotnet.microsoft.com/download)
- A database of your choice:
  - SQL Server, SQLite, or any supported by Entity Framework
  - Optional: Update `appsettings.json` to configure connection strings.

## 🛠️ Getting Started

1. **Clone this repo**
   ```bash
   git clone https://github.com/saifahmed34/URL_Shortener.git
   cd URL_Shortener

Update the connection string in appsettings.json.

Apply migrations:
```
dotnet ef database update
```
Run the app
```
dotnet run --project URL_Shortener
```
🧩 Usage
Access the homepage to paste a long URL and receive a short link.

Use the returned short link to redirect to the original URL.

Short links are formatted like https://yourdomain/{shortKey}.

🛡️ Testing & Validation
Include unit tests for key components:

URL generation

Redirect logic and error handling

Database CRUD operations

