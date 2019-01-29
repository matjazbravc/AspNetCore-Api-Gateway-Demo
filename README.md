# ASP.NET Core 2.2 Web API Gateway Demo

In this POC guide, we'll build an simple API gateway application. The API will contain three sub-domains that we'll call "Company", "Department" and "Employee" contexts. The gateway will behave like a reverse proxy that routes incoming HTTP requests to the mentioned downstream services.

## Introducing Ocelot

Ocelot is an open source framework used for building .NET core API gateways, the project is aimed at people using .NET Core to build applications designed with microservices or SOA architectures. It provides an easy way to write a mapping file (ocelot.json) that could be used to route incoming HTTP requests to the appropriate downstream services.

Read more about Ocelot [here](https://ocelot.readthedocs.io/en/latest/introduction/bigpicture.html).

## POC

Test the APIs by running each of them and issuing a GET request against the following urls:
* Department: [http://localhost:51260/api/department](http://localhost:51260/api/department)
* Employee: [http://localhost:51261/api/employee](http://localhost:51261/api/employee)
* Company: [http://localhost:51262/api/company](http://localhost:51262/api/company)

**Note: You can configure a solution to run multiple APIs at the same time.**

## Configuring Ocelot
The first thing that we have to do is configure the **ocelot.json** file and add the code below.

```json
{
  "ReRoutes": [
    {
      "DownstreamPathTemplate": "/api/company/",
      "DownstreamScheme": "http",
      "DownstreamHostAndPorts": [
        {
          "Host": "localhost",
          "Port": 51262
        }
      ],
      "UpstreamPathTemplate": "/api/company/",
      "UpstreamHttpMethod": [ "GET" ]
    },
    {
      "DownstreamPathTemplate": "/api/company/getbyid/{id}",
      "DownstreamScheme": "http",
      "DownstreamHostAndPorts": [
        {
          "Host": "localhost",
          "Port": 51262
        }
      ],
      "UpstreamPathTemplate": "/api/company/getbyid/{id}",
      "UpstreamHttpMethod": [ "GET" ],
      "Key": "company-profile"
    },
    {
      "DownstreamPathTemplate": "/api/employee/",
      "DownstreamScheme": "http",
      "DownstreamHostAndPorts": [
        {
          "Host": "localhost",
          "Port": 51261
        }
      ],
      "UpstreamPathTemplate": "/api/employee/",
      "UpstreamHttpMethod": [ "GET" ]
    },
    {
      "DownstreamPathTemplate": "/api/employee/getbycompanyid/{id}",
      "DownstreamScheme": "http",
      "DownstreamHostAndPorts": [
        {
          "Host": "localhost",
          "Port": 51261
        }
      ],
      "UpstreamPathTemplate": "/api/employee/getbycompanyid/{id}",
      "UpstreamHttpMethod": [ "GET" ],
      "Key": "company-employees"
    },
    {
      "DownstreamPathTemplate": "/api/department/",
      "DownstreamScheme": "http",
      "DownstreamHostAndPorts": [
        {
          "Host": "localhost",
          "Port": 51260
        }
      ],
      "UpstreamPathTemplate": "/api/department/",
      "UpstreamHttpMethod": [ "GET" ]
    },
    {
      "DownstreamPathTemplate": "/api/department/getbyid/{id}",
      "DownstreamScheme": "http",
      "DownstreamHostAndPorts": [
        {
          "Host": "localhost",
          "Port": 51260
        }
      ],
      "UpstreamPathTemplate": "/api/department/getbyid/{id}",
      "UpstreamHttpMethod": [ "GET" ],
      "Key": "company-departments"
    }
  ],
  "Aggregates": [
    {
      "ReRouteKeys": [
        "company-employees",
        "company-profile"
      ],
      "UpstreamPathTemplate": "/api/company-employees/{id}"
    }
  ],
  "GlobalConfiguration": {
    "BaseUrl": "http://localhost:51263"
  }
}
```
The code above setups up the mappings dictionary of (see below) your application gateway to the downstream services.

* Gateway to Company API
* Gateway to Department API
* Gateway to Employee API

## Testing the routes
To test the routes, start all projects and navigate to each link listed below.

* Company Endpoint [http://localhost:51263/api/company](http://localhost:51263/api/company)
* Department Endpoint [http://localhost:51263/api/department](http://localhost:51263/api/department)
* Employee Endpoint [http://localhost:51263/api/employee](http://localhost:51263/api/employee)

## Basic Response Aggregation
Response aggregation is a technique used for merging response from multiple downstream services into a single object. API gateways achieve this by accepting a single request from clients and issuing multiple parallelized requests to downstream services, once all downstream services responds, API gateways perform merging of data into single object and serves it to the clients. This technique results to the following benefits:

* Cross continental communication become faster.
* Client only needs to know a single schema.
* Fewer HTTP transactions between client and server

**Company-Profile Endpoint Mapping**
```json
    // Company Routes
    {
      "DownstreamPathTemplate": "/api/company/",
      "DownstreamScheme": "http",
      "DownstreamHostAndPorts": [
        {
          "Host": "localhost",
          "Port": 51262
        }
      ],
      "UpstreamPathTemplate": "/api/company/",
      "UpstreamHttpMethod": [ "GET" ]
    },
    {
      "DownstreamPathTemplate": "/api/company/getbyid/{id}",
      "DownstreamScheme": "http",
      "DownstreamHostAndPorts": [
        {
          "Host": "localhost",
          "Port": 51262
        }
      ],
      "UpstreamPathTemplate": "/api/company/getbyid/{id}",
      "UpstreamHttpMethod": [ "GET" ],
      "Key": "company-profile"
    },
```

**Company-Employees Endpoint Mapping**
```json
    // Employee Routes
    {
      "DownstreamPathTemplate": "/api/employee/",
      "DownstreamScheme": "http",
      "DownstreamHostAndPorts": [
        {
          "Host": "localhost",
          "Port": 51261
        }
      ],
      "UpstreamPathTemplate": "/api/employee/",
      "UpstreamHttpMethod": [ "GET" ]
    },
    {
      "DownstreamPathTemplate": "/api/employee/getbycompanyid/{id}",
      "DownstreamScheme": "http",
      "DownstreamHostAndPorts": [
        {
          "Host": "localhost",
          "Port": 51261
        }
      ],
      "UpstreamPathTemplate": "/api/employee/getbycompanyid/{id}",
      "UpstreamHttpMethod": [ "GET" ],
      "Key": "company-employees"
    },
```
**Aggregated Route Configuration**
```json
    "Aggregates": [
    {
      "ReRouteKeys": [
        "company-employees",
        "company-profile"
      ],
      "UpstreamPathTemplate": "/api/company-employees/{id}"
    }
  ],
```
## Test routes

You can test the output of the individual endpoint re-routes and aggregated route using the links below:

**Company-Profile**
[http://localhost:51263/api/company/getbyid/1](http://localhost:51263/api/company/getbyid/1)

**Company-Employees**
[http://localhost:51263/api/employee/getbycompanyid/1](http://localhost:51263/api/employee/getbycompanyid/1)

**Company-Profile + Company-Employees**
[http://localhost:51263/api/company-employees/1](http://localhost:51263/api/company-employees/1)

## Aggregated Response
```json
{
  "company-profile": {
    "companyId": 1,
    "name": "Company One",
    "employees": []
  },
  "company-employees": [
    {
      "employeeId": 1,
      "firstName": "John",
      "lastName": "Whyne",
      "birthDate": "1991-08-07T00:00:00",
      "companyId": 1
    },
    {
      "employeeId": 2,
      "firstName": "Mathias",
      "lastName": "Gernold",
      "birthDate": "1997-10-12T00:00:00",
      "companyId": 1
    },
    {
      "employeeId": 3,
      "firstName": "Julia",
      "lastName": "Reynolds",
      "birthDate": "1955-12-16T00:00:00",
      "companyId": 1
    }
  ]
}
```

**Ocelot** offers a simple yet powerful mechanism to aggregate multiple downstream endpoints. This feature eases the life of front-end developers that previously have to deal with multiple endpoints.

## Prerequisites:
- [Visual Studio](https://www.visualstudio.com/vs/community) 2017 15.9 or greater
- [.NET Core SDK 2.2.102](https://dotnet.microsoft.com/download/dotnet-core/2.2)

## Technologies and frameworks used:
- ASP.NET MVC Core 2.2
- Ocelot

