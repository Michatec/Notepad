using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Printing;
using System.Drawing.Imaging;

namespace New_Notepad
{
    public partial class Form1 : Form
    {
        string filePath = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            richTextBox1.Font = new Font("Arial", 14);
            richTextBox1.BackColor = Color.White;
        }

        private void neuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Modified)
            {
                if(Properties.Settings.Default.BS == 0)
                {
                    var result = MessageBox.Show("Möchten Sie die Änderungen speichern?", "Änderungen speichern", MessageBoxButtons.YesNoCancel);
                    if (result == DialogResult.Yes)
                    {
                        SaveCurrentFile();
                    }
                    else if (result == DialogResult.Cancel)
                    {
                        return;
                    }
                }
                else{
                    SaveCurrentFile();
                }
            }

            filePath = "";
            richTextBox1.Text = "";

        }

        private void öffnenToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (richTextBox1.Modified)
            {
                if (Properties.Settings.Default.BS == 0)
                {
                    var result = MessageBox.Show("Möchten Sie die Änderungen speichern?", "Änderungen speichern", MessageBoxButtons.YesNoCancel);
                    if (result == DialogResult.Yes)
                    {
                        SaveCurrentFile();
                    }
                    else if (result == DialogResult.Cancel)
                    {
                        return;
                    }
                }
                else{
                    SaveCurrentFile();
                }
            }

            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "Text Dokument|*.txt", ValidateNames = true, Multiselect = false })

            {

                if (ofd.ShowDialog() == DialogResult.OK)

                {
                    try
                    {
                        filePath = ofd.FileName;
                        richTextBox1.Text = File.ReadAllText(filePath);
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show("Fehler beim Öffnen der Datei: " + ex.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }

        }
        private void speichernToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveCurrentFile();
        }
        private void speichernAlsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Textdokument|*.txt", ValidateNames = true })

            {

                if (sfd.ShowDialog() == DialogResult.OK)

                {

                    filePath = sfd.FileName;
                    SaveCurrentFile();

                }

            }

        }

        private void SaveCurrentFile()
        {
            if (string.IsNullOrEmpty(filePath))
            {
                using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Textdokument|*.txt", ValidateNames = true })
                {
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        filePath = sfd.FileName;
                    }
                    else
                    {
                        return;
                    }
                }
            }
            try
            {
                File.WriteAllText(filePath, richTextBox1.Text);
                richTextBox1.Modified = false;
            }
            catch (IOException ex)
            {
                MessageBox.Show("Fehler beim Speichern der Datei: " + ex.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void druckenToolStripMenuItem_Click(object sender, EventArgs e)
        {

            printPreviewDialog1.Document = printDocument1;

            printPreviewDialog1.ShowDialog();

        }
        private void schließenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Modified)
            {
                if (Properties.Settings.Default.BS == 0)
                {
                    var result = MessageBox.Show("Möchten Sie die Änderungen speichern?", "Änderungen speichern", MessageBoxButtons.YesNoCancel);
                    if (result == DialogResult.Yes)
                    {
                        SaveCurrentFile();
                    }
                    else if (result == DialogResult.Cancel)
                    {
                        return;
                    }
                }
                else{
                    SaveCurrentFile();
                }
            }

            this.Close();
        }

        private void druckenToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;

            if (printDialog1.ShowDialog() == DialogResult.OK)

            {

                printDocument1.Print();

            }

        }

        private void rückgängigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();
        }

        private void vorgänigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Redo();
        }

        private void ausschneidenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void kopierenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void einfügenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void löschenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectedText = "";
        }

        private void allesAuswählenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }

        private void zeilenumbruchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (zeilenumbruchToolStripMenuItem.Checked == true)

            {

                zeilenumbruchToolStripMenuItem.Checked = false;

                richTextBox1.WordWrap = false;

            }

            else

            {

                zeilenumbruchToolStripMenuItem.Checked = true;

                richTextBox1.WordWrap = true;

            }
        }

        private void schriftartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowDialog();
            richTextBox1.SelectionFont = new Font(fontDialog1.Font.FontFamily, fontDialog1.Font.Size, fontDialog1.Font.Style);

        }

        private void markierenTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionBackColor == Color.White)
            {
                richTextBox1.SelectionBackColor = Color.Yellow;
            }
            else
            {
                richTextBox1.SelectionBackColor = Color.White;
            }
        }

        private void überToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About About = new About();
            About.ShowDialog();
        }


        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)

        {

            e.Graphics.DrawString(richTextBox1.Text, richTextBox1.Font, Brushes.Black, 12, 10);

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)

        {

            if (richTextBox1.Text.Length > 0)

            {

                ausschneidenToolStripMenuItem.Enabled = true;

                kopierenToolStripMenuItem.Enabled = true;

            }

            else

            {

                ausschneidenToolStripMenuItem.Enabled = false;

                kopierenToolStripMenuItem.Enabled = false;

            }

        }

        private void einstellungenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings Settings = new Settings();
            Settings.ShowDialog();
        }
    }
}
