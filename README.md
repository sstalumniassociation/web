# SST Alumni Web App

This repository contains the following for the SST Alumni App Platform (SSTAAP):

* Web PWA
  * SSTAA app
  * Admin dashboard
  * Guardhouse app
* API

## Getting Started

`SSTAlumniAssociation.AppHost` uses the latest `dotnet8` NuGet feed. To add the feed, run the following command:

```shell
dotnet nuget add source --name dotnet8 https://pkgs.dev.azure.com/dnceng/public/_packaging/dotnet8/nuget/v3/index.json
dotnet workload update --skip-sign-check --source https://pkgs.dev.azure.com/dnceng/public/_packaging/dotnet8/nuget/v3/index.json
```

Please get access or set up the following services:

* Turso
* Firebase

You will need to provide the following env vars in a `.env` file:

| Name                             | Purpose                          |
| -------------------------------- | -------------------------------- |
| `FIREBASE_APP_CHECK_DEBUG_TOKEN` | Firebase App Check Debug Token   |
| `TURSO_URL`                      | Database to connect to           |
| `TURSO_AUTH_TOKEN`               | Token to authenticate with Turso |

### Development

Start the dev server:

```bash
pnpm dev
```

## Sibling projects

[iOS App](https://github.com/sstalumniassociation/ios)
