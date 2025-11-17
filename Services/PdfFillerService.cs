using PdfSharpCore.Pdf;
using PdfSharpCore.Pdf.IO;
using PdfSharpCore.Drawing;
using pdf_deneme.Models;
using System.IO;
using System;

namespace pdf_deneme.Services
{
    public class PdfFillerService
    {
        public byte[] FillFormA(Stream pdfStream, PartAData data)
        {
            using (var output = new MemoryStream())
            {
                // PDF'i import et
                PdfDocument inputPdf = PdfReader.Open(pdfStream, PdfDocumentOpenMode.Import);
                PdfDocument outputPdf = new PdfDocument();

                // Sayfayı kopyala
                PdfPage srcPage = inputPdf.Pages[0];
                PdfPage page = outputPdf.AddPage(srcPage);

                // Çizim yüzeyi
                XGraphics gfx = XGraphics.FromPdfPage(page);

                // Yazı fontu
                XFont font = new XFont("Arial", 10);

                // ----------------------------------------------------------------
                // DİKKAT: Buradaki Y değerleri artık doğrudan PDF koordinatıdır.
                // PdfSharpCore Y ekseni: ALT → YUKARI
                // Bu yüzden Y(...) fonksiyonu artık YOK.
                // ----------------------------------------------------------------

                // ------------------- PART A FIELDS -------------------

                // Full Name
                gfx.DrawString(data.FullName ?? "", font, XBrushes.Black,
                    new XPoint(40, 185));

                // Address
                gfx.DrawString(data.StreetAddress ?? "", font, XBrushes.Black,
                    new XPoint(40, 210));

                // Social Security Number
                gfx.DrawString(data.SocialSecurityNumber ?? "", font, XBrushes.Black,
                    new XPoint(196, 181));

                // Date of Birth
                gfx.DrawString(data.DateOfBirth.ToString("MM/dd/yyyy"), font, XBrushes.Black,
                    new XPoint(40, 240));

                // Gender
                if (data.Gender == "Male")
                {
                    gfx.DrawString("X", font, XBrushes.Black,
                        new XPoint(238, 235));
                }
                else if (data.Gender == "Female")
                {
                    gfx.DrawString("X", font, XBrushes.Black,
                        new XPoint(238, 235));
                }

                // Ethnic (Hispanic / Not Hispanic)
                if (data.IsHispanic)
                {
                    gfx.DrawString("X", font, XBrushes.Black,
                        new XPoint(306, 280));
                }
                else
                {
                    gfx.DrawString("X", font, XBrushes.Black,
                        new XPoint(306, 285));
                }

                // PDF'i kaydet
                outputPdf.Save(output);
                return output.ToArray();
            }
        }
    }
}
