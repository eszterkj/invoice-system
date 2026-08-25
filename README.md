# Invoice System

A simple invoice management system built as a technical assignment.

The application allows products, customers and orders to be stored in a relational database. Orders can contain multiple products, and the system automatically calculates the total amount. An HTML invoice can be generated for each order.

A lightweight Angular frontend is also included to provide a simple user interface for the main ordering and invoice workflow.

## Technologies

### Backend

- .NET 9.0.100-rc.2 / C#
- ASP.NET Core Web API
- Entity Framework Core
- SQLite
- HTML invoice generation

### Frontend

- Angular
- TypeScript
- SCSS

## Features

### Backend

- Create and retrieve products
- Create and retrieve customers
- Create and retrieve orders
- Add multiple products and quantities to an order
- Automatic order total calculation
- 20% discount for discount-eligible products
- Special marking of hazardous and discounted products on invoices
- HTML invoice generation
- Relational SQLite database
- SQL script for database creation
- SQL queries for:
  - Top 3 products by ordered quantity
  - Orders containing at least one hazardous product

### Frontend

The Angular frontend provides a simple interface for the main invoice workflow:

- Select an existing customer
- Add one or more products to an order
- Specify quantities for each product
- Create an order
- View existing customers
- View existing products
- View existing orders
- Select an order and open its generated invoice

Product and customer creation is available through the API.

## Project Structure

- `Controllers/` - API endpoints for products, customers and orders
- `Models/` - Entity Framework database entities
- `DTOs/` - Data transfer objects used for order creation
- `Data/` - Entity Framework database context
- `Services/` - Invoice generation logic
- `Migrations/` - Entity Framework Core migrations
- `SQL/` - Database creation script and required SQL queries
- `InvoiceSystemUI/` - Angular frontend

## Running the Application

### Requirements

- .NET 9.0.100-rc.2
- Entity Framework Core CLI tools
- Node.js
- npm
- Angular CLI

### Setup

Clone the repository:

```bash
git clone https://github.com/eszterkj/invoice-system.git
```

Navigate to the project directory:

```bash
cd invoice-system
```

### Backend

Restore the dependencies:

```bash
dotnet restore
```

Create/update the SQLite database using the included Entity Framework migrations:

```bash
dotnet ef database update
```

Run the backend:

```bash
dotnet run
```

The application will display the local address in the terminal, for example:

```text
http://localhost:5270
```

### Frontend

Keep the backend running and open another terminal.

Navigate to the Angular frontend:

```bash
cd InvoiceSystemUI
```

Install the dependencies:

```bash
npm install
```

Run the frontend:

```bash
ng serve
```

The frontend will be available at:

```text
http://localhost:4200
```

## API Endpoints

### Products

```text
GET  /api/products
POST /api/products
```

### Customers

```text
GET  /api/customers
POST /api/customers
```

### Orders

```text
GET  /api/orders
POST /api/orders
GET  /api/orders/{id}/invoice
```

Example order request:

```json
{
  "customerId": 1,
  "items": [
    {
      "productId": 1,
      "quantity": 1
    },
    {
      "productId": 2,
      "quantity": 3
    }
  ]
}
```

The order total is calculated by the backend and cannot be supplied by the client.

## Invoice Generation

An invoice can be generated for an existing order using:

```text
GET /api/orders/{id}/invoice
```

The endpoint returns an HTML document containing:

- Customer information
- Order date
- Ordered products
- Quantities
- Unit prices
- Discounts
- Subtotals
- Total amount
- Special product markers

## Design Decisions

### Discounts

The specification identifies products that are eligible for a discount but does not define the discount amount.

For this implementation, discount-eligible products receive a fixed **20% discount**.

The applied unit price and discount are stored with each order item. This ensures that an existing order is not affected if the current price of a product is changed later.

### Invoice format

HTML was selected as the invoice document format because it can be generated without additional dependencies and can be opened directly in a web browser.

### Hazardous products

The implementation uses the provided `IsHazardous` property and marks hazardous products as `HAZARDOUS` on the generated invoice.

Discount-eligible products are marked as `DISCOUNTED`.

### Database

SQLite was selected because it is relational, lightweight and allows the project to be run locally without installing or configuring a separate database server.

Entity Framework Core is used as the ORM.

The `SQL/create_database.sql` file was generated from the Entity Framework Core migrations.

### Frontend scope

The frontend was added as an optional user interface for demonstrating the main ordering and invoice workflow.

Product and customer creation remain available through the API, while the frontend focuses on creating orders from existing data and viewing the generated results.

## SQL Queries

The required SQL queries can be found in:

```text
SQL/queries.sql
```

They contain:

1. The top 3 products by ordered quantity.
2. Orders containing at least one hazardous product.

## Testing the API

Example HTTP requests are included in:

```text
InvoiceSystem.http
```

These requests can be used to create sample products, customers and orders and to test invoice generation.
