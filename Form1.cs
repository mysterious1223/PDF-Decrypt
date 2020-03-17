
using iText.Forms;
using iText.Kernel.Pdf;
using iText.Kernel.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDF_Unlocker_Kevin_2020
{
    

    public partial class Form1 : Form
    {

        
        public Form1()
        {
            InitializeComponent();

            


        }

        private bool GenerateNewPDF (string src)
        {

            string new_file_path = Path.GetDirectoryName(src);
            string new_file_name = new_file_path + "/" + Path.GetFileNameWithoutExtension(src) + "_unpwd.pdf";

      

            PdfDocument pdfDoc = new PdfDocument(//new PdfReader(Path).SetUnethicalReading(true), 
                new PdfWriter(new_file_name));
            PdfDocument insertDoc = new PdfDocument(new PdfReader(src).SetUnethicalReading(true));

           

            insertDoc.CopyPagesTo(1, insertDoc.GetNumberOfPages(), pdfDoc, 4);

            PdfAcroForm form = PdfAcroForm.GetAcroForm(pdfDoc, true);
            form.FlattenFields();



            insertDoc.Close();
            pdfDoc.Close();




            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = "";
            progressBar1.Value = 0;
            OpenFileDialog fd = new OpenFileDialog();

            fd.Filter = "PDF|*.pdf";
            fd.Title = "Open Mapping Sheet";
            fd.ShowDialog();
            fd.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;

            if (fd.FileName.Length > 2)
            {
                Console.WriteLine(fd.FileName);


                if (GenerateNewPDF(fd.FileName))
                {
                    //MessageBox.Show("done");
                    progressBar1.Value = 100;
                    MessageBox.Show("done");
                }
                else
                    MessageBox.Show("Failed");

            }
          
        }

        private void button2_Click(object sender, EventArgs e)
        {

            label1.Text = "";
            string myPath;
            string[] myfiles;
            float count = 0;
            float percent = 0;
            progressBar1.Value = 0;
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            folderBrowserDialog1.Description = "Directory Location";
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                myPath = folderBrowserDialog1.SelectedPath;
                myfiles = Directory.GetFiles(myPath, "*.pdf");

                count = myfiles.Length;



                for (int i = 0; i < count; i ++)
                {
                    if (GenerateNewPDF(myfiles[i]))
                    {
                        percent = (i+1) / count;
                        
                    }
                    else
                    {
                        MessageBox.Show("Error creating PDF at loc " + myfiles[i]);
                        percent = (i+1) / count;
                    }

                    progressBar1.Value =  (int)(percent * 100);
                    Console.WriteLine((percent * 100).ToString());
                    label1.Text = (i+1).ToString() +" of "+count.ToString();
                    label1.Update();
                }
                

            }


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
