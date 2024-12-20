using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Buoii05
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            toolStripComboBox1.Text = "Tahoma";
            toolStripComboBox2.Text = "14";
            foreach (FontFamily font in new InstalledFontCollection().Families)
            {
                toolStripComboBox1.Items.Add(font.Name);
            }
            List<int> listSize = new List<int> { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };
            foreach (var s in listSize)
            {
                toolStripComboBox2.Items.Add(s);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Form1_Load(sender, e);
            richTextBox1.Text = null;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "RTF File| *.rtf| txt File| *.txt";
            if (dlg.ShowDialog() == DialogResult.OK)
                richTextBox1.LoadFile(dlg.FileName);
        }

        private void tạoVănBảnMớiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1_Load(sender, e);
            richTextBox1.Text = null;
        }

        private void mởTệpTinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Rich Text Files (*.rtf)|*.rtf|Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                openFileDialog.Title = "Chọn tập tin để mở";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    if (filePath.EndsWith(".rtf"))
                    {
                        richTextBox1.LoadFile(filePath, RichTextBoxStreamType.RichText);
                    }
                    else
                    {
                        richTextBox1.Text = File.ReadAllText(filePath);
                    }
                }
            }
        }
        private string currentFilePath = string.Empty;

        private void lưuNộiDungVănBảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentFilePath))
            {
                SaveNewFile();
            }
            else
            {
                SaveExistingFile();
            }
        }
        private void SaveNewFile()
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Rich Text Files (*.rtf)|*.rtf";
                saveFileDialog.Title = "Lưu tập tin";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    currentFilePath = saveFileDialog.FileName;
                    richTextBox1.SaveFile(currentFilePath, RichTextBoxStreamType.RichText);
                    MessageBox.Show("Văn bản đã được lưu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void SaveExistingFile()
        {
            richTextBox1.SaveFile(currentFilePath, RichTextBoxStreamType.RichText);
            MessageBox.Show("Văn bản đã được lưu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            ToggleFontStyle(FontStyle.Bold);
        }
        private void ToggleFontStyle(FontStyle style)
        {
            if (richTextBox1.SelectionFont != null)
            {
                Font currentFont = richTextBox1.SelectionFont;
                FontStyle newFontStyle;

                // Kiểm tra xem kiểu chữ hiện tại đã có style hay chưa
                if (currentFont.Style.HasFlag(style))
                {
                    newFontStyle = currentFont.Style & ~style; // Gỡ bỏ style
                }
                else
                {
                    newFontStyle = currentFont.Style | style; // Thêm style
                }

                // Cập nhật font cho đoạn văn bản được chọn
                richTextBox1.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newFontStyle);
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            ToggleFontStyle(FontStyle.Italic);
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            ToggleFontStyle(FontStyle.Underline);
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        

        private void toolStripComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionFont != null)
            {
                Font currentFont = richTextBox1.SelectionFont;
                float newSize = float.Parse(toolStripComboBox2.Text);
                Font newFont = new Font(currentFont.FontFamily, newSize, currentFont.Style);
                richTextBox1.SelectionFont = newFont;
            }
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionFont != null)
            {
                Font currentFont = richTextBox1.SelectionFont;
                Font newFont = new Font(toolStripComboBox1.Text, currentFont.Size, currentFont.Style);
                richTextBox1.SelectionFont = newFont;
            }
        }
    }

}
