# Dochadzka

## Instalation
1. Get project to your local drive from the github
1. Set your SQL connection in /Doch.api/appsettings.json
1. Go to the Doch.Data directory
1. run migration (code first): dotnet ef database update
1. In VS Solution Properties check as startup multiple projects Doch.Api & Doch.Web
1. Run

## Workflow
Doch.Data <---- Doch.Api <-*-*-*- Doch.Web

