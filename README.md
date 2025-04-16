# SST Alumni Web App

This repository contains the following for the SST Alumni App Platform (SSTAAP):

* Web PWA
    * SSTAA app
    * Admin dashboard
    * Guardhouse app
* API
    * App / Member API
    * Admin API
    * Guardhouse / Service account API

## Getting Started

Have you ever taken 3 days just to setup your local environment? Say goodbye to that horrible experience
with [Aspire](https://github.com/dotnet/aspire), the latest and greatest Microsoft has to offer. With Aspire, your
onboarding time can be shortened to just 15 minutes!

Prerequisites:

* Docker
* .NET 9 SDK or greater
* NodeJS + NPM

You will also need the following env vars in `SSTAlumniAssociation.WebApp/.env`:

| Name                                | Purpose                        |
|-------------------------------------|--------------------------------|
| `FIREBASE_APP_CHECK_DEBUG_TOKEN`    | Firebase App Check Debug Token |
| `NUXT_PUBLIC_GROWTHBOOK_CLIENT_KEY` | Growthbook client key          |

If you're running on Linux, ensure that the user running the project has access to `docker` without `sudo`.

### Development

Start the dev server:

```powershell
cd SSTAlumniAssociation.AppHost/;
dotnet run;
```

### Directory structure

```
.WebApp/
  |- api/ # kiota generated api clients
  |- components/
    |- admin/
    |- app/
      |- ...# route
        |- page.vue # f7 page component 
        |- other-component.vue # other components in the page
    |- guard/
      |- ...
  |- pages/
    |- admin/ # admin app
    |- app/ # member app (f7)
    |- guard/ # guard house app (f7)
    |- pass/ # entry passes page (f7)
    
.AdminApi/ # admin api
.MemberApi/ # member api
.ServiceAccountrApi/ # service account api

.Core/ # entity models
.Migrations/ # database migrations
```

f7 refers to [Framework7](https://framework7.io) apps. These differ slightly from normal Nuxt apps as they rely on f7's
router instead.

## Hosting

The APIs are hosted on [Unikraft Cloud](https://unikraft.cloud).

The web app is hosted on Cloudflare Pages.

## Sibling projects

[iOS App](https://github.com/sstalumniassociation/ios)
