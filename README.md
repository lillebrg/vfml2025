# AI Chatbot Projekt – Starter Repo

Dette repo er et skelet til jeres AI Chatbot-projekt.

## Opgave
I skal bygge en chatbot i C# og Blazor baseret på et af de fire scenarier:

- Dyrelæge Support-bot

Chatbotten skal bygges trinvist gennem kurset:
1. **Dag 2**: Lav et arkitekturdiagram og beslut jer for jeres lagdeling (UI, logik, data).
2. **Dag 3–9**: Implementer funktionalitet trin for trin (NLP, regler, state machine, ML.NET, hukommelse, UI/UX, test & logging).
3. **Dag 10**: Fremlæg jeres chatbot.

## Struktur
- `Chatbot/` → backend-logik (intents, state, NLP, ML.NET).
- `BlazorUI/` → frontend i Blazor, hvor brugeren chatter med botten.
- `Data/` → dummy data eller databaseforbindelse.
- `Services/` → NLP, hukommelse, integration med ekstern AI.

- Dag 2 → I arbejder med arkitektur og dokumenterer i Docs/.
- Dag 3–6 → NLP, regler, ML – koden skrives i Chatbot/.
- Dag 7 → kontekst og hukommelse – udvidelser i Chatbot/Data.
- Dag 8 → UI/UX og tilgængelighed – fokus i BlazorUI/.
- Dag 9 → test, logging og fejlhåndtering – i både Chatbot og BlazorUI.
- Til sidst afleveres projektet med README og diagrammer i Docs/.

## Kom i gang
1. Installer [.NET SDK](https://dotnet.microsoft.com/en-us/download).
2. Åbn repo i VS Code eller Visual Studio.
3. Kør:
   ```bash
   dotnet build
   dotnet run --project Chatbot
   ```
4. Start BlazorUI med:
   ```bash
   dotnet run --project BlazorUI
   ```
5. Åbn browseren på `https://localhost:5001` (eller den port, der står i terminalen).
## Opgave til Dag 1

## Opgave til Dag 2
- Tegn jeres arkitekturdiagram (fx i draw.io, Lucidchart, eller som ASCII i README).  
- Placér diagrammet i repo’et under `/Docs/ArchitectureDiagram.png`.  
- Beskriv i README hvilken type chatbot I bygger (Hotel, Support, Uddannelse, Butik).
