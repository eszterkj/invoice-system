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
                * {
                    box-sizing: border-box;
                }

                body {
                    margin: 0;
                    background: #fff9fd;
                    font-family: Arial, sans-serif;
                    color: #3d303a;
                }

                .page {
                    max-width: 900px;
                    margin: 40px auto;
                    padding: 0 20px;
                }

                .invoice-card {
                    background: #ffffff;
                    border-radius: 20px;
                    padding: 36px;
                    box-shadow: 0 10px 30px rgba(255, 115, 218, 0.10);
                    border: 1px solid #f9dcef;
                }

                .invoice-header {
                    display: flex;
                    justify-content: space-between;
                    align-items: flex-start;
                    gap: 20px;
                    margin-bottom: 30px;
                }

                .invoice-title {
                    margin: 0;
                    color: #d94eb4;
                    font-size: 32px;
                }

                .invoice-meta {
                    text-align: right;
                    color: #806477;
                    font-size: 14px;
                    line-height: 1.6;
                }

                .section-title {
                    margin: 26px 0 10px;
                    color: #b9479a;
                    font-size: 18px;
                }

                .customer-card {
                    background: #fff8fd;
                    border: 1px solid #f9dcef;
                    border-radius: 14px;
                    padding: 16px;
                    line-height: 1.6;
                }

                table {
                    width: 100%;
                    border-collapse: collapse;
                    margin-top: 24px;
                    overflow: hidden;
                    border-radius: 14px;
                }

                th {
                    background: #ffe8f8;
                    color: #9c5b8b;
                    font-weight: 600;
                    text-align: left;
                    padding: 12px;
                    border-bottom: 1px solid #f4c9e9;
                }

                td {
                    padding: 12px;
                    border-bottom: 1px solid #f7e8f2;
                }

                tbody tr:nth-child(even) {
                    background: #fffafd;
                }

                tbody tr:hover {
                    background: #fff3fb;
                }

                .special {
                    display: inline-block;
                    margin-left: 6px;
                    color: #d94eb4;
                    font-size: 12px;
                    font-weight: 700;
                }

                .total-box {
                    margin-top: 26px;
                    display: flex;
                    justify-content: flex-end;
                }

                .total {
                    min-width: 260px;
                    background: #fff2fb;
                    border: 1px solid #f4c9e9;
                    border-radius: 14px;
                    padding: 16px 20px;
                    text-align: right;
                    font-size: 22px;
                    font-weight: 700;
                    color: #b9479a;
                }

                .footer {
                    margin-top: 24px;
                    text-align: center;
                    color: #aa8aa0;
                    font-size: 12px;
                }

                @media (max-width: 700px) {
                    .invoice-card {
                        padding: 22px;
                    }

                    .invoice-header {
                        flex-direction: column;
                    }

                    .invoice-meta {
                        text-align: left;
                    }

                    table {
                        font-size: 13px;
                    }
                }
            </style>
        </head>

        <body>
            <div class="page">
                <div class="invoice-card">
        """);

        html.Append("""
                    <div class="invoice-header">
                        <div>
        """);

        html.Append($"<h1 class=\"invoice-title\">Invoice #{order.Id}</h1>");

        html.Append("""
                        </div>
                        <div class="invoice-meta">
        """);

        html.Append($"<div><strong>Date:</strong> {order.OrderDate:yyyy-MM-dd}</div>");

        html.Append("""
                        </div>
                    </div>
        """);

        html.Append("<h2 class=\"section-title\">Customer</h2>");

        html.Append("<div class=\"customer-card\">");
        html.Append($"<strong>{order.Customer?.Name}</strong><br>");
        html.Append($"{order.Customer?.Country}<br>");
        html.Append($"{order.Customer?.Address}");
        html.Append("</div>");

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
                $"<td><strong>{item.Product?.Name}</strong><span class=\"special\">{markerText}</span></td>"
            );

            html.Append($"<td>{item.Quantity}</td>");
            html.Append($"<td>{item.UnitPrice:N0} Ft</td>");
            html.Append($"<td>{item.Discount}%</td>");
            html.Append($"<td>{subtotal:N0} Ft</td>");

            html.Append("</tr>");
        }

        html.Append("""
            </tbody>
        </table>
        """);

        html.Append("<div class=\"total-box\">");
        html.Append(
            $"<div class=\"total\">Total: {order.Total:N0} Ft</div>"
        );
        html.Append("</div>");

        html.Append("""
                    <div class="footer">
                        Generated by Invoice System
                    </div>
                </div>
            </div>
        </body>
        </html>
        """);

        return html.ToString();
    }
}