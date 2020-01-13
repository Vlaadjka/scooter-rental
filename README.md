# Scooter rental service

### Features

* Add, get and remove scooters
* Start and end rent of a scooter
* Calculate income from rental
* In memory data storage

*Given interfaces and class definition was not changed.*

### Structure

Solution has 2 projects `ScooterRental` which is API and `ScooterRentalTests` contains unittests.
API files are located in multiple directories `Models`, `Services` and `Controllers`.

### Routes

All routes are `RPC-Style`, `POST` only and with `JSON` body.

#### POST `v1/scooter.getAll` Get all scooters

Response:
```
[
    {
        "id": "ninebot123",
        "isRented": true,
        "pricePerMinute": 0.07
    }
]
```

#### POST `v1/scooter.get` Get scooter by id

Request body:
```
{
    "id": "ninebot123"
}
```
Response:
```
[
    {
        "id": "ninebot123",
        "isRented": true,
        "pricePerMinute": 0.07
    }
]
```

#### POST `v1/scooter.add` Add new scooter

Request body:
```
{
    "id": "ninebot123",
    "pricePerMinute": 0.07
}
```
Empty `200` response.
Adding same scooter twice will also return `200` with empty body.

#### POST `v1/scooter.remove` Remove scooter

Request body:
```
{
    "id": "ninebot123"
}
```
Empty `200` response.
Removing not existing scooter will also return `200` with empty body.

#### POST `v1/rent.start` Start rent of a scooter

Request body:
```
{
    "scooterId": "ninebot123"
}
```
Empty `200` response.
Trying to rent scooter which is already rented will also return `200` with empty body.

#### POST `v1/rent.end` End rent of a scooter

Request body:
```
{
    "scooterId": "ninebot123"
}
```
Response:
```
{
    "RentalPrice": 0.14
}
```
Trying to end rent of scooter scooter which is not rented will also return zero price.

#### POST `v1/income.get` Get company income

Request body:
```
{
    "year": 2020,
    "includeNotCompletedRentals": true
}
```
`year` is optional, returns all by default.
`includeNotCompletedRentals` is optional and is `false` by default.
Response:
```
{
    "Income": 3.14
}
```

### ToDo
* Mock time for better date dependent feature testing
* Add exception handling and return additional information in responses
* Test controllers
