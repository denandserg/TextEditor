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

namespace TextRedactor
{

    public partial class MainForm : Form
    {
        string file_Name;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            saveToolStripMenuItem.Enabled = false;
            toolStripButton3.Enabled = false;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveToolStripMenuItem.Enabled = true;
            toolStripButton3.Enabled = true;
            openFileDialog.DefaultExt = "txt";
            openFileDialog.Filter = "All Files(*.*)|*.*|Text files (*.txt)|*.txt|RTF Files (*.rtf)|*.rtf";
            openFileDialog.FilterIndex = 2;
            

            
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                if (Path.GetExtension(openFileDialog.FileName) == ".rtf")
                {
                    file_Name = openFileDialog.FileName;
                    using (StreamReader reader = new StreamReader(openFileDialog.OpenFile(), Encoding.UTF8))
                    {
                        textBox1.Rtf = reader.ReadToEnd();
                    }
                }
                else
                {
                    file_Name = openFileDialog.FileName;
                    using (StreamReader reader = new StreamReader(openFileDialog.OpenFile(), Encoding.UTF8))
                    {
                        textBox1.Text = reader.ReadToEnd();
                    }
                }
                
            }

        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != String.Empty)
            {
                DialogResult rez = MessageBox.Show("Документ содержит текст, сохранить ?", "Внимание!",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (rez == DialogResult.Yes)
                {
                    saveFileDialog.DefaultExt = "txt";
                    saveFileDialog.Filter = "All Files(*.*)|*.*|Text files (*.txt)|*.txt|RTF Files (*.rtf)|*.rtf";
                    saveFileDialog.FilterIndex = 2;
                    if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
                    {
                        if (Path.GetExtension(openFileDialog.FileName) == ".rtf")
                        {
                            using (StreamWriter writer = new StreamWriter(saveFileDialog.OpenFile(), Encoding.UTF8))
                            {
                                writer.WriteLine(textBox1.Rtf);
                            }
                           

                        }
                        else
                        {
                            using (StreamWriter writer = new StreamWriter(saveFileDialog.OpenFile(), Encoding.UTF8))
                            {

                                writer.Write(textBox1.Text);
                            }
                        }
                        
                    }

                    textBox1.ResetText();

                }
                else
                {
                    textBox1.ResetText();
                }
            }

            return;

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            if (Path.GetExtension(openFileDialog.FileName) == ".rtf")
            {
                string plainText = textBox1.Rtf;

                using (System.IO.StreamWriter file =
                    new System.IO.StreamWriter(openFileDialog.SafeFileName))
                {
                    file.WriteLine(plainText);
                }
            }
            else
            {
                File.WriteAllText(saveFileDialog.FileName, textBox1.Text);
            }
            
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog.DefaultExt = "txt";
            saveFileDialog.Filter = "All Files(*.*)|*.*|Text files (*.txt)|*.txt|RTF Files (*.rtf)|*.rtf";
            saveFileDialog.FilterIndex = 2;
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                if (Path.GetExtension(openFileDialog.FileName) == ".rtf")
                {
                    using (StreamWriter writer = new StreamWriter(saveFileDialog.OpenFile(), Encoding.UTF8))
                    {
                        writer.WriteLine(textBox1.Rtf);
                    }


                }
                else
                {
                    using (StreamWriter writer = new StreamWriter(saveFileDialog.OpenFile(), Encoding.UTF8))
                    {

                        writer.Write(textBox1.Text);
                    }
                }
            }
               
            
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void toolStripContainer1_ContentPanel_Load(object sender, EventArgs e)
        {

        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fontDialog.Font = textBox1.Font;
            if (fontDialog.ShowDialog(this) == DialogResult.OK)
            {
                textBox1.SelectionFont = fontDialog.Font;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            label3.Text = textBox1.Text.Length.ToString();
            CountWords();

        }

        private void CountWords()
        {
            var text = textBox1.Text.Trim();
            int wordCount = 0, index = 0;

            while (index < text.Length)
            {

                while (index < text.Length && !char.IsWhiteSpace(text[index]))
                    index++;

                wordCount++;


                while (index < text.Length && char.IsWhiteSpace(text[index]))
                    index++;
            }

            label4.Text = wordCount.ToString();
        }

        private void toolStripButton_Font_Color_Click(object sender, EventArgs e)
        {
            fontDialog.ShowColor = true;
            fontDialog.Color = textBox1.ForeColor;

            if (fontDialog.ShowDialog() != DialogResult.Cancel)
            {
               
                textBox1.SelectionColor = fontDialog.Color;
            }
        }

        private void toolStripButton_Back_Color_Click(object sender, EventArgs e)
        {
            colorDialog.Color = textBox1.BackColor;
            if (colorDialog.ShowDialog() != DialogResult.Cancel)
            {

                textBox1.SelectionBackColor = colorDialog.Color;
            }
        }

       
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Clipboard.SetText(textBox1.SelectedText, TextDataFormat.UnicodeText);
            textBox1.Copy();
            
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Paste();
        }

       

       
    }
}
