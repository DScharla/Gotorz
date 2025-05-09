# GodTur – Client-Server WebApp med Blazor

Dette projekt er et rejsebureau-system udviklet som et semesterprojekt til virksomheden GoTorz. Formålet er at give salgspersonale mulighed for at administrere og sammensætte rejsepakker (fly + hotel) via en moderne webapplikation baseret på en client-server arkitektur.

Teknologistak:

- .NET 9
- Blazor WebAssembly (Client)
- Blazor Server (Backend API)
- Entity Framework Core
- SQL Server(local i øjeblikket)

Funktionaliter:

- API kald til en ekstern virksomhed fra klientens side med brugerinput gennem vores Blazor Server(På Main branch)
- Admin-panel til oprettelse af nye rejser(igangværende udvikling på Development branch og udvalgte under branches)
- Søgefilter (lufthavne, by, dato)

Sådan kører du projektet:

1. Åbn løsningen i Visual Studio.
3. Sørg for at både Client og Server projekterne er sat som startup-projekter.
4. Tryk på `F5` eller `Ctrl + F5` for at køre.
5. Webappen åbner i din browser og loader WebAssembly-clienten fra serveren.
5. Vælg "TravelBuilder" punktet i venstre menu.

> NB: Projektet kræver .NET 9 SDK og SQL Server lokalt installeret(migration via EFCore fra dens respective udviklings branch).

Mappestruktur:

- Client – Blazor WebAssembly frontend
- Server – API, database.
- Shared – Delt kode mellem client og server (DTOs, Validations, etc.)

Kendte begrænsninger:

- Brugeradgang og rolle styring er ikke endnu implementeret.


