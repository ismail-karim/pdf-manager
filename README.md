# PDF.Manager

Outil web en **Blazor Server (.NET 8)** permettant de gérer des fichiers PDF.
Première fonctionnalité : **fusionner N fichiers PDF** dans l'ordre choisi par
l'utilisateur, et produire un unique document **compressé**.

## Fonctionnalités (v1)

- Import de N fichiers PDF depuis l'interface.
- Listing des fichiers importés (nom, taille, position).
- Réordonnancement via des boutons **Monter / Descendre** (et suppression).
- Génération d'un **document PDF unique et compressé**, puis téléchargement.

## Architecture

Le projet suit une **architecture en oignon (Onion)** respectant les principes
**SOLID**. Les dépendances pointent toujours vers le cœur du domaine.

```
PDF.Manager.Domain          → Entités, exceptions et interfaces du domaine (aucune dépendance externe)
PDF.Manager.Application      → Cas d'usage, DTOs, validation (dépend de Domain)
PDF.Manager.Infrastructure   → Implémentation PDFsharp de la fusion (dépend de Domain)
PDF.Manager.Web (Blazor)     → Interface utilisateur + composition root (dépend de Application & Infrastructure)
```

| Élément | Rôle |
| --- | --- |
| `IPdfMergeService` (Domain) | Contrat de fusion, implémenté en Infrastructure (inversion de dépendance) |
| `IMergePdfDocumentsUseCase` (Application) | Point d'entrée du cas d'usage utilisé par l'UI |
| `PdfSharpMergeService` (Infrastructure) | Fusion et compression via PDFsharp |
| `MergePdf.razor` (Web) | Interface d'import, d'ordonnancement et de téléchargement |

## Choix techniques

- **Blazor Server** avec rendu interactif côté serveur (`InteractiveServer`).
- **PDFsharp** (`PdfSharp` 6.x, licence MIT) pour la manipulation des PDF.
- Compression des flux de contenu activée (`CompressContentStreams`,
  `FlateEncodeMode.BestCompression`).
- Limites par défaut : 50 fichiers max, 50 Mo par fichier (configurables dans
  `MergePdf.razor`).

## Prérequis

- [SDK .NET 8](https://dotnet.microsoft.com/download/dotnet/8.0)

## Compiler et exécuter

```bash
# Restaurer les dépendances
dotnet restore

# Compiler la solution
dotnet build

# Lancer les tests unitaires
dotnet test

# Démarrer l'application web
dotnet run --project src/PDF.Manager.Web
```

L'application est ensuite accessible sur `https://localhost:7080` (ou
`http://localhost:5080`).

## Structure du dépôt

```
PDF.Manager.sln
src/
  PDF.Manager.Domain/
  PDF.Manager.Application/
  PDF.Manager.Infrastructure/
  PDF.Manager.Web/
tests/
  PDF.Manager.Application.Tests/
```
