using InvoiceSystem.Models;
using System.Text;

namespace InvoiceSystem.Services;

public class InvoiceService
{
    public string GenerateInvoiceHtml(Order order)
    {
        var html = new StringBuilder();

        html.Append("""
        <!DOCTYPE html>
        <html>
        <head>
            <meta charset="UTF-8">
            <title>Invoice</title>
            <style>
                body {
                    font-family: Arial, sans-serif;
                    max-width: 900px;
                    margin: 40px auto;
                    padding: 20px;
                }

                h1 {
                    margin-bottom: 5px;
                }

                table {
                    width: 100%;
                    border-collapse: collapse;
                    margin-top: 30px;
                }

                th, td {
                    border: 1px solid #ccc;
                    padding: 10px;
                    text-align: left;
                }

                th {
                    background-color: #f4f4f4;
                }

                .total {
                    text-align: right;
                    font-size: 20px;
                    font-weight: bold;
                    margin-top: 20px;
                }

                .special {
                    font-weight: bold;
                }
            </style>
        </head>
        <body>
        """);

        html.Append($"<h1>Invoice #{order.Id}</h1>");
        html.Append($"<p>Date: {order.OrderDate:yyyy-MM-dd}</p>");

        html.Append("<h2>Customer</h2>");
        html.Append($"<p>{order.Customer?.Name}<br>");
        html.Append($"{order.Customer?.Country}<br>");
        html.Append($"{order.Customer?.Address}</p>");

        html.Append("""
        <table>
            <thead>
                <tr>
                    <th>Product</th>
                    <th>Quantity</th>
                    <th>Unit price</th>
                    <th>Discount</th>
                    <th>Subtotal</th>
                </tr>
            </thead>
            <tbody>
        """);

        foreach (var item in order.Items)
        {
            var discountedUnitPrice =
                item.UnitPrice * (1 - item.Discount / 100);

            var subtotal =
                discountedUnitPrice * item.Quantity;

            var markers = new List<string>();

            if (item.Product?.IsHazardous == true)
            {
                markers.Add("HAZARDOUS");
            }

            if (item.Product?.IsDiscountEligible == true)
            {
                markers.Add("DISCOUNTED");
            }

            var markerText =
                markers.Count > 0
                    ? $" [{string.Join(", ", markers)}]"
                    : "";

            html.Append("<tr>");

            html.Append(
                $"<td>{item.Product?.Name}<span class=\"special\">{markerText}</span></td>"
            );

            html.Append($"<td>{item.Quantity}</td>");

            html.Append($"<td>{item.UnitPrice:N0}</td>");

            html.Append($"<td>{item.Discount}%</td>");

            html.Append($"<td>{subtotal:N0}</td>");

            html.Append("</tr>");
        }

        html.Append("</tbody></table>");

        html.Append(
            $"<div class=\"total\">Total: {order.Total:N0}</div>"
        );

        html.Append("</body></html>");

        return html.ToString();
    }
}