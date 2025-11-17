using Microsoft.AspNetCore.Mvc;
using pdf_deneme.Models;
using pdf_deneme.Services;
using System;
using System.IO;

namespace pdf_deneme.Controllers
{
    public class FormController : Controller
    {
        private readonly PdfFillerService _pdfFillerService;

        public FormController(PdfFillerService pdfFillerService)
        {
            _pdfFillerService = pdfFillerService;
        }

        public IActionResult Index()
        {
            return RedirectToAction("DownloadPartA");
        }

        public IActionResult DownloadPartA()
        {
            string templatePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "eta671.pdf");

            var data = new PartAData
            {
                FullName = "John S Doe",
                StreetAddress = "123 Main St, CA",
                SocialSecurityNumber = "000  00  0000",
                DateOfBirth = new DateTime(1990, 1, 1),
                Gender = "Male",
                IsHispanic = true,
                IsVeteran = false,
                EducationLevel = "High School",
                EmploymentStatus = "New",
                CareerConnection = "None"
            };

            byte[] pdfBytes;

            using (var fs = new FileStream(templatePath, FileMode.Open, FileAccess.Read))
            {
                pdfBytes = _pdfFillerService.FillFormA(fs, data);
            }

            return File(pdfBytes, "application/pdf", "PartA_Filled.pdf");
        }
    }
}
