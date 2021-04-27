
# Open Referrals (OR)
This repo contains the source code for the Open Referrals project API.

Its purpose is to demonstrate that Siccar can support the implementation of an OR standard API.
The OpenReferralApi.

Siccar uses distributed ledger to store data and it was a requirement of this project to store Organisation and Service data in the ledger.
However the app does not require Siccar to function. It can be run in a standalone mode.
This will save the data in a Azure CosmosDB and an AzureSearchService.

This Project requires the use of an Azure AD B2C tenant to store user credentials and 
and to do authentication.  

## Prerequisites
 - Azure B2C Tenant
 - Access to Azure resources i.e CosmosDB, AzureSearchService, AppServices
 - Siccar (Only if you require to store data in a distributed ledger)

## Source Code
The code is C#

## Getting started 

- Clone the repository
- Create a local file named 'appsettings.Development.json'
- Copy the appsettings.json file contents into the file you just created.
- The value of ConnectToSiccar should be false if you don't intend to user Siccar
- Add all the described settings, 
- Open the sln file in VS 2019
- Run 