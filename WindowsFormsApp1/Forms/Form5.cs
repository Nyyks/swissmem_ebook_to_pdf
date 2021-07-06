﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text;
using System.IO;
using System.Net;
using System.Diagnostics;
using System.Runtime.InteropServices;
using PdfSharp;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using PdfSharp.Drawing;

namespace WindowsFormsApp1
{
    public partial class Form5 : System.Windows.Forms.Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string SelectedBook = File.ReadAllText(@"C:/Temp/SelectedBook.txt");
            int FileCount = Directory.GetFiles(@"C:/Temp/fixed/").Length;
            int i = 0;
            int filename = 001;
            string filepath = "C:/Temp/fixed/fixed" + i + ".html";
            string exportpath = "C:/Temp/pdf/" + filename.ToString("0000") + ".pdf";
            int pageHeight = 0;
            int pageWidth = 0;

            if (SelectedBook == "FM")
            {
                pageWidth = 128;
                pageHeight = 180;
            }
            else if (SelectedBook == "TD")
            {
                pageWidth = 158;
                pageHeight = 223;
            }
            else if (SelectedBook == "MW")
            {
                pageWidth = 158;
                pageHeight = 223;
            }
            else if (SelectedBook == "NA")
            {
                pageWidth = 90;
                pageHeight = 200;
            }
            else if (SelectedBook == "RM")
            {
                pageWidth = 195;
                pageHeight = 275;
            }
            while (i < FileCount + 1)
            {

                string cmd = "C:\ncd C:/Program Files/wkhtmltopdf/bin";
                string cmd2 = "\nstart wkhtmltopdf.exe --page-width " + pageWidth + "mm --page-height " + pageHeight +"mm --margin-bottom 0 --margin-left 0 --margin-right 0 --margin-top 0 --disable-smart-shrinking --viewport-size 1920x1080 " + filepath + " " + exportpath;
                File.WriteAllText(@"C:/Temp/bat/" + i + ".bat", cmd + cmd2);
                Process.Start(@"C:/Temp/bat/" + i + ".bat");
                i++;
                filename++;
            }


            int XPoint1 = 0;
            int XPoint2 = 0;
            int XSize1 = 0;
            int XSize2 = 0;
            int PdfCount = Directory.GetFiles(@"C:/Temp/pdf/").Length;

            if (SelectedBook == "NA")
            {
                XPoint1 = 0;
                XPoint2 = 200;
                XSize1 = 300;
                XSize2 = 400;

            }


            int x = 1;
            int FileName2 = 1;
            while (x < PdfCount + 1)
            {
                string file = @"C:/Temp/pdf/" + FileName2.ToString("0000") + ".pdf";
                PdfDocument document = PdfReader.Open(file);
                PdfPage page = document.Pages[0];
                page.CropBox = new PdfRectangle(new XPoint(XPoint1, XPoint2),
                                                new XSize(XSize1, XSize2));
                document.Save(file);
                x++;
                FileName2++;
            }



            string FileName = File.ReadAllText(@"C:/Temp/SelectedBook.txt");
            string[] pdfs = Directory.GetFiles(@"C:/Temp/pdf/", "*.pdf*", SearchOption.AllDirectories);

            string targetPath = @"C:/Temp/" + FileName + ".pdf";
            using (var targetDoc = new PdfSharp.Pdf.PdfDocument())
            {
                foreach (var pdf in pdfs)
                {
                    using (var pdfDoc = PdfSharp.Pdf.IO.PdfReader.Open(pdf, PdfDocumentOpenMode.Import))
                    {
                        for (var y = 0; y < pdfDoc.PageCount; y++)
                            targetDoc.AddPage(pdfDoc.Pages[y]);
                    }
                }
                targetDoc.Save(targetPath);
            }
            Process.Start(@"C:/Temp/" + FileName + ".pdf");
            MessageBox.Show("Die Umwandlung ist abgeschlossen, du kannst nun vortfahren.", "Umwandlung Abgeschlossen", MessageBoxButtons.OK);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            System.IO.Directory.Delete("C:/Temp/bat", true);
            System.IO.Directory.Delete("C:/Temp/fixed", true);
            System.IO.Directory.Delete("C:/Temp/original", true);
            System.IO.Directory.Delete("C:/Temp/pdf", true);
            System.Windows.Forms.Application.ExitThread();
        }

    }

}
