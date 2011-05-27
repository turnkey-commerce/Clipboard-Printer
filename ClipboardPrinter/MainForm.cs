using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ClipboardPrinter
{
    public partial class MainForm : Form
    {
        //StringReader myReader;
        ClipboardPrintDocument clipboardPrintDocument = new ClipboardPrintDocument();

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            LoadClipBoard();
            //Open the print dialog
            PrintDialog printDialog = new PrintDialog();
            clipboardPrintDocument.Text = rtbPreview.Text;
            clipboardPrintDocument.Font = rtbPreview.Font;
            printDialog.Document = clipboardPrintDocument;
            printDialog.UseEXDialog = true;
            if (DialogResult.OK == printDialog.ShowDialog())
            {
                clipboardPrintDocument.Print();
            }

        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            LoadClipBoard();
        }

        private void LoadClipBoard()
        {
            IDataObject clipData = Clipboard.GetDataObject();

            // Determines whether the data is in a format you can use.
            if (clipData.GetDataPresent(DataFormats.Text))
            {
                rtbPreview.Text = (String)clipData.GetData(DataFormats.Text);
            }
            else if (clipData.GetDataPresent(DataFormats.Rtf))
            {
                rtbPreview.Rtf = (String)clipData.GetData(DataFormats.Rtf);
            }
            else
            {
                rtbPreview.Text = "Could not retrieve data off the clipboard.";
            }
        }
    }
}