# API Use cases

The API uses the OpenReferral Standard. This should be possible to be queried by anyone. 
The following documentation outlines potential common use cases.

## Organizations

An Organization can represent a company or charity.  

### Get Request

Currently services can not be queried via organisation. This should be a future feature. 

The user can query all Organizations using:
https://localhost:5004/Organizations

The user can query Organization names using:
https://localhost:5004/Organizations?text=my%20query

## Services

A Service is provided by an Organization. It contains data about the service.
A Service usually has a Location mapped to it using a list of ServiceAtLocations on the Service model.
There is usually only one ServicaAtLocation.

### Get Request

#### Location search
Proximity defaults to 5 miles. 

https://localhost:5004/Services?postcode=EH4%201AT

https://localhost:5004/Services?postcode=EH4%201AT&proximity=10

#### Text search

https://localhost:5004/Services?text=test


The user can query all Services using:
https://localhost:5004/Organizations?text=my%20query

## Locations

A Location is created and then associated with a Service. Users can query all services, which are not vulnerable. 
Once locations have been returned the user can use the Services, ServiceAtLocation to map the locationId to the service.

### Get Request

https://localhost:5004/Locations
