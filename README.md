
# Open Referrals (OR)
This repo contains the source code for the Open Referrals project API.

The OpenReferralApi uses the OpenReferral standards and allows anyone to search query Services, Organisations and Locations.
There are other controllers on the API which allow OpenReferralUI, the frontend of the OR project,
to meet all the project requirements.
Another aspect of this project demonstrates that Siccar, a distributed ledger platform,
can support the implementation of an OR standard API.


Siccar uses a distributed ledger to store data and it was a requirement of this project to store Organisation and Service data in the ledger.
However the app does not require Siccar to function. It can be run in a standalone mode.
This will save the data in an Azure CosmosDB and an AzureSearchService.

This Project requires the use of an Azure AD B2C tenant to store user credentials and 
and to do authentication.  

## Prerequisites
 - Azure B2C Tenant
 - Access to Azure resources i.e CosmosDB, AzureSearchService, AppServices
 - Siccar (Only if you require to store data in a distributed ledger), this will need to be agreed and setup by the Siccar team.

## Source Code
This project uses ASP.Net Core and is written in C#

## Setup Azure Resources
- Create an OpenReferralAPI app registration
- Create a client secret and copy it down
- Expose the _user_impersonation permission from the api
- Create a CosmosDB that uses the MongoDB driver
- Create an AzureSearchService and index (copy the contents of the azuresearch.json file and replace it with the fields in the index definition)
- Create a SendGrid account and model a dynamic email template, it takes in parameters: OpenReferralAppUrl(baseUrl of the frontendapp), OrganisationName and UserName

## Getting started Locally

- Clone the repository
- Create a local file named 'appsettings.Development.json'
- Copy the appsettings.json file contents into the file you just created.
- The value of ConnectToSiccar should be false if you don't intend to use Siccar
- Add all the settings
- Open the sln file in VS 2019
- Run

## Deployment
This project can easily deployed using VS2019

 - Right click on the project
 - Select Publish
 - Target Azure
 - Use windows app service
 - Target an existing app service instance or create a new one.
 - Skip the api managment step
 - Save and publish

  This project also has azure-pipelines.yml setup for Azure DevOps.
 
 - Copy the repository into your own DevOps Repo.
 - Create a new pipeline called OpenReferralAPI and target the azure-pipelines.yml
 - Create a release which targets the OpenReferralAPI artifact and targets and app service.
 - Setup triggers or manually create a build and then release to the app service.