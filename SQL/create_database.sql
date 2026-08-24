CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" TEXT NOT NULL CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY,
    "ProductVersion" TEXT NOT NULL
);

BEGIN TRANSACTION;

CREATE TABLE "Customers" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Customers" PRIMARY KEY AUTOINCREMENT,
    "Name" TEXT NOT NULL,
    "Country" TEXT NOT NULL,
    "Address" TEXT NOT NULL
);

CREATE TABLE "Products" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Products" PRIMARY KEY AUTOINCREMENT,
    "Name" TEXT NOT NULL,
    "Category" TEXT NOT NULL,
    "UnitPrice" TEXT NOT NULL,
    "IsHazardous" INTEGER NOT NULL,
    "IsDiscountEligible" INTEGER NOT NULL
);

CREATE TABLE "Orders" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Orders" PRIMARY KEY AUTOINCREMENT,
    "CustomerID" INTEGER NOT NULL,
    "OrderDate" TEXT NOT NULL,
    CONSTRAINT "FK_Orders_Customers_CustomerID" FOREIGN KEY ("CustomerID") REFERENCES "Customers" ("Id") ON DELETE CASCADE
);

CREATE TABLE "OrderItems" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_OrderItems" PRIMARY KEY AUTOINCREMENT,
    "OrderId" INTEGER NOT NULL,
    "ProductId" INTEGER NOT NULL,
    "Quantity" INTEGER NOT NULL,
    CONSTRAINT "FK_OrderItems_Orders_OrderId" FOREIGN KEY ("OrderId") REFERENCES "Orders" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_OrderItems_Products_ProductId" FOREIGN KEY ("ProductId") REFERENCES "Products" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_OrderItems_OrderId" ON "OrderItems" ("OrderId");

CREATE INDEX "IX_OrderItems_ProductId" ON "OrderItems" ("ProductId");

CREATE INDEX "IX_Orders_CustomerID" ON "Orders" ("CustomerID");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20260824161040_InitialCreate', '9.0.0-rc.2.24474.1');

ALTER TABLE "Orders" ADD "Total" TEXT NOT NULL DEFAULT '0.0';

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20260824182613_AddOrderTotalAmount', '9.0.0-rc.2.24474.1');

ALTER TABLE "OrderItems" ADD "Discount" TEXT NOT NULL DEFAULT '0.0';

ALTER TABLE "OrderItems" ADD "UnitPrice" TEXT NOT NULL DEFAULT '0.0';

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20260824192539_AddOrderItemPricing', '9.0.0-rc.2.24474.1');

COMMIT;

