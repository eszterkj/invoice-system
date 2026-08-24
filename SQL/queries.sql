-- Return top 3 products by ordered quantity

SELECT p."Id", p."Name", SUM(oi."Quantity") AS "TotalOrderedQuantity"
FROM "Products" p
JOIN "OrderItems" oi ON p."Id" = oi."ProductId"
GROUP BY p."Id", p."Name"
ORDER BY "TotalOrderedQuantity" DESC
LIMIT 3;

-- Return orders containing at least one hazardous product

SELECT DISTINCT o.*
FROM "Orders" o
JOIN "OrderItems" oi ON o."Id" = oi."ProductId"
JOIN "Products" p ON oi."ProductId" = p."Id"
WHERE p."IsHazardous" = 1