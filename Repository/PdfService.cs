using InventoryRepo.Models;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace InventoryRepo.Repository
{
    public class PdfService
    {
        public byte[] GenerateInvoice(Supplierorder order)
        {
          
            PdfDocument document = new PdfDocument();

           
            PdfPage page = document.AddPage();

           
            XGraphics gfx = XGraphics.FromPdfPage(page);

            
            XFont font = new XFont("Arial", 12, XFontStyle.Regular);

            // Set page dimensions and margins
            double pageWidth = page.Width.Point;
            double pageHeight = page.Height.Point;
            double margin = 40;

            // Draw border around the entire page
            gfx.DrawRectangle(XPens.Black, margin, margin, pageWidth - 2 * margin, pageHeight - 2 * margin);

            // Draw company details
            DrawCompanyDetails(gfx, margin + 10, font); // Added left padding to company details

            // Draw text on the page using order details
            string invoiceTitle = $"Invoice for Order #{order.SupplierorderId}";
            XSize titleSize = gfx.MeasureString(invoiceTitle, font);
            double titleX = (pageWidth - titleSize.Width) / 2;
            double titleY = margin + 30; // Increase top padding
            gfx.DrawString(invoiceTitle, font, XBrushes.Black, new XRect(titleX, titleY, titleSize.Width, titleSize.Height), XStringFormats.TopLeft);

            // Draw supplier details on the right side
            int yOffset = (int)(titleY + titleSize.Height + 30); // Starting Y-coordinate for supplier details
            double supplierDetailsX = pageWidth - margin - 200; // Adjust the value based on your preference

            gfx.DrawString($"Supplier: {order.Supplier.SupplierName}", font, XBrushes.Black, new XRect(supplierDetailsX, yOffset, 200, 20), XStringFormats.TopLeft);
            yOffset += 20; // Increment the yOffset for the next line

            gfx.DrawString($"Number: {order.Supplier.ContactNumber}", font, XBrushes.Black, new XRect(supplierDetailsX, yOffset, 200, 20), XStringFormats.TopLeft);
            yOffset += 20; // Increment the yOffset for the next line

            gfx.DrawString($"Reference Name: {order.Supplier.ContactPerson}", font, XBrushes.Black, new XRect(supplierDetailsX, yOffset, 200, 20), XStringFormats.TopLeft);
            yOffset += 20; // Increment the yOffset for the next line

            gfx.DrawString($"Email: {order.Supplier.Email}", font, XBrushes.Black, new XRect(supplierDetailsX, yOffset, 200, 20), XStringFormats.TopLeft);
            yOffset += 20; // Increment the yOffset for the next line

            // Add order items to a table
            int rowHeight = 20;
            double tableWidth = pageWidth - 2 * margin;
            double tableX = margin + 10; // Add left padding
            double tableY = yOffset + 10; // Starting Y-coordinate for the table
            double contentWidth = pageWidth - 2 * margin;
            double contentHeight = pageHeight - 2 * margin;

            // Add more top padding to the table
            tableY += 30; // Increase top padding

            // Draw table header with column borders
            tableY += (contentHeight - tableY - rowHeight) / 8; // Adjust tableY to center the header
            gfx.DrawLine(XPens.Black, tableX, tableY, tableX, tableY + rowHeight); // Left border
            gfx.DrawLine(XPens.Black, tableX, tableY, tableX + tableWidth- 20, tableY); // Top border
            gfx.DrawString("Product Name", font, XBrushes.Black, new XRect(tableX, tableY, tableWidth * 0.3, rowHeight), XStringFormats.TopLeft);
            gfx.DrawLine(XPens.Black, tableX + tableWidth * 0.3, tableY, tableX + tableWidth * 0.3, tableY + rowHeight);
            gfx.DrawString("Quantity", font, XBrushes.Black, new XRect(tableX + tableWidth * 0.3, tableY, tableWidth * 0.15, rowHeight), XStringFormats.TopLeft);
            gfx.DrawLine(XPens.Black, tableX + tableWidth * 0.45, tableY, tableX + tableWidth * 0.45, tableY + rowHeight);
            gfx.DrawString("Price", font, XBrushes.Black, new XRect(tableX + tableWidth * 0.45, tableY, tableWidth * 0.15, rowHeight), XStringFormats.TopLeft);
            gfx.DrawLine(XPens.Black, tableX + tableWidth * 0.6, tableY, tableX + tableWidth * 0.6, tableY + rowHeight);
            gfx.DrawString("Total Price", font, XBrushes.Black, new XRect(tableX + tableWidth * 0.6, tableY, tableWidth * 0.2, rowHeight), XStringFormats.TopLeft);
            gfx.DrawLine(XPens.Black, tableX + tableWidth - 10, tableY, tableX + tableWidth - 10, tableY + rowHeight); // Right border
            gfx.DrawLine(XPens.Black, tableX + tableWidth - 20, tableY, tableX + tableWidth - 20, tableY + rowHeight); // Right top border
            gfx.DrawLine(XPens.Black, tableX, tableY + rowHeight, tableX + tableWidth - 20, tableY + rowHeight); // Horizontal line after the header
            tableY += rowHeight;


            // Draw order items with column borders
            foreach (var orderItem in order.Items)
            {
                /*tableY += 5; */// Add spacing between rows
                double col1X = tableX + (tableWidth * 0.3 - gfx.MeasureString(orderItem.ProductName, font).Width) / 2;
                double col2X = tableX + tableWidth * 0.3 + (tableWidth * 0.15 - gfx.MeasureString(orderItem.Quantity.ToString(), font).Width) / 2;
                double col3X = tableX + tableWidth * 0.45 + (tableWidth * 0.15 - gfx.MeasureString(orderItem.Price.ToString(), font).Width) / 2;
                double col4X = tableX + tableWidth * 0.6 + (tableWidth * 0.2 - gfx.MeasureString(orderItem.TotalPrice.ToString(), font).Width) / 2;
             
                gfx.DrawString(orderItem.ProductName, font, XBrushes.Black, new XRect(col1X, tableY, tableWidth * 0.3, rowHeight), XStringFormats.TopLeft);
                gfx.DrawLine(XPens.Black, tableX + tableWidth * 0.3, tableY, tableX + tableWidth * 0.3, tableY + rowHeight); // Vertical line after Product Name

                gfx.DrawString(orderItem.Quantity.ToString(), font, XBrushes.Black, new XRect(col2X, tableY, tableWidth * 0.15, rowHeight), XStringFormats.TopLeft);
                gfx.DrawLine(XPens.Black, tableX + tableWidth * 0.45, tableY, tableX + tableWidth * 0.45, tableY + rowHeight); // Vertical line after Quantity

                gfx.DrawString(orderItem.Price.ToString(), font, XBrushes.Black, new XRect(col3X, tableY, tableWidth * 0.15, rowHeight), XStringFormats.TopLeft);
                gfx.DrawLine(XPens.Black, tableX + tableWidth * 0.6, tableY, tableX + tableWidth * 0.6, tableY + rowHeight); // Vertical line after Price

                gfx.DrawString(orderItem.TotalPrice.ToString(), font, XBrushes.Black, new XRect(col4X, tableY, tableWidth * 0.2, rowHeight), XStringFormats.TopLeft);
                gfx.DrawLine(XPens.Black, tableX + tableWidth - 10, tableY, tableX + tableWidth - 10, tableY + rowHeight);
                gfx.DrawLine(XPens.Black, tableX + tableWidth - 20, tableY, tableX + tableWidth - 20, tableY + rowHeight); // Right top border
                gfx.DrawLine(XPens.Black, tableX, tableY + rowHeight, tableX + tableWidth - 20, tableY + rowHeight);
                gfx.DrawLine(XPens.Black, tableX, tableY, tableX, tableY + rowHeight);
                gfx.DrawLine(XPens.Black, tableX, tableY + rowHeight, tableX + tableWidth - 20, tableY + rowHeight); // Horizontal line after each row
                tableY += rowHeight;
            }

            // Draw horizontal line after the last row
            gfx.DrawLine(XPens.Black, tableX, tableY, tableX + tableWidth - 20, tableY); // Adjust the width based on the right padding

            // Add padding to the bottom of the table
            /*tableY += 20;*/ // Increase bottom padding

            // Draw Grand Total with left and right borders
           /* tableY += 5;*/ // Add spacing
            gfx.DrawLine(XPens.Black, tableX, tableY, tableX, tableY + rowHeight); // Left border
            gfx.DrawString("Grand Total", font, XBrushes.Black, new XRect(tableX, tableY, tableWidth * 0.8, rowHeight), XStringFormats.TopLeft);
            gfx.DrawString(order.GrandTotal.ToString(), font, XBrushes.Black, new XRect(tableX + tableWidth * 0.8, tableY, tableWidth * 0.2, rowHeight), XStringFormats.TopLeft);
            gfx.DrawLine(XPens.Black, tableX + tableWidth - 20, tableY, tableX + tableWidth - 20, tableY + rowHeight); // Right border

            // Draw vertical lines for the Grand Total row
            gfx.DrawLine(XPens.Black, tableX, tableY, tableX + tableWidth - 20, tableY); // Adjust the width based on the right padding// Adjust the width based on the right padding
            gfx.DrawLine(XPens.Black, tableX, tableY + rowHeight, tableX + tableWidth - 20, tableY + rowHeight);
            // Horizontal line after the
            // Save the document to a byte array
            byte[] pdfBytes;
            using (MemoryStream stream = new MemoryStream())
            {
                document.Save(stream, false);
                pdfBytes = stream.ToArray();
            }

            return pdfBytes;
        }

        private void DrawCompanyDetails(XGraphics gfx, double margin, XFont font)
        {
            // Draw company logo and a line after the logo
            XImage img = XImage.FromFile("C:\\Users\\Nutan.Bolgir\\Desktop\\Inventory\\InventoryRepo\\wwwroot\\Images\\handcart.png");
            gfx.DrawImage(img, margin, margin, 120, 40);
            gfx.DrawLine(XPens.Black, margin, margin + 40, gfx.PageSize.Width - margin, margin + 40);

            // Draw company details with left and right padding
            double detailsX = margin + 10; // Add left padding
            double detailsY = margin + 50; // Adjust the value based on your preference
            gfx.DrawString("Inventorio", font, XBrushes.Black, new XRect(detailsX, detailsY, 200, 20), XStringFormats.TopLeft);
            gfx.DrawString("Address: 123 Street, City, Country", font, XBrushes.Black, new XRect(detailsX, detailsY + 20, 200, 20), XStringFormats.TopLeft);
            gfx.DrawString("Email: info@inventorio.com", font, XBrushes.Black, new XRect(detailsX, detailsY + 40, 200, 20), XStringFormats.TopLeft);

            // Add right padding to the company details
            gfx.DrawString("Phone: +123 456 7890", font, XBrushes.Black, new XRect(detailsX, detailsY + 60, 200, 20), XStringFormats.TopLeft);
            gfx.DrawString("Website: www.inventorio.com", font, XBrushes.Black, new XRect(detailsX, detailsY + 80, 200, 20), XStringFormats.TopLeft);
        }
    }

}









