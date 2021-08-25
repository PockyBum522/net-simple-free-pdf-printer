﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.Versioning;
using PdfSharp.Pdf.IO;

namespace SimpleFreePdfPrinter.Logic
{
    public class PdfToBitmapListConverter
    {
        public List<Bitmap> GetPdfPagesAsBitmapList(string pdfFileToWork)
        {
            using var document = PdfReader.Open(pdfFileToWork);
            
            var pageCount = document.Pages.Count;
            Console.WriteLine("Page count of input document: {PageCount}", pageCount);
            
            var outputList = new List<Bitmap>();

            using var inputStream = new FileStream(pdfFileToWork, FileMode.Open, FileAccess.Read);
            
            var bytesArrays = Freeware.Pdf2Png.ConvertAllPages(
                inputStream);

            foreach (var byteArrayImage in bytesArrays)
            {
                using MemoryStream pngStream = new MemoryStream(byteArrayImage);
                
                var outputImage = Image.FromStream(pngStream);
    
                var bitmapOfPage = new Bitmap(outputImage);
            
                outputList.Add(bitmapOfPage);
            }

            return outputList;
        }
    }
}