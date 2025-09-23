using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SpritesheetMaker
{
    public partial class MainForm : Form
    {
        private int imageScale;
        Size initialSize;
        bool isInitialSizeSet;

        private bool _Moving = false;
        private Point _Offset;
        public List<Image> images = new List<Image>();


        public MainForm()
        {
            InitializeComponent();
        }

        public void SelectImages_Click(object sender, EventArgs e)
        {
            // Open The File Browser Dialogue
            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                

                Image aspectChecker = Image.FromFile(openFileDialog1.FileNames.ElementAt(0));
                if (!isInitialSizeSet)
                {
                    initialSize = aspectChecker.Size;
                    isInitialSizeSet = true;
                    imageRes.Text = aspectChecker.Size.Height.ToString();
                }

                if (aspectChecker.Width == aspectChecker.Height && aspectChecker.Size == initialSize)
                {
                    // Read the files
                    foreach (String file in openFileDialog1.FileNames)
                    {
                        // Add to Panel
                        try
                        {
                            Image image = Image.FromFile(file);
                            images.Add(image);
                            PictureBoxWithInterpolationMode pictureBox = new PictureBoxWithInterpolationMode();
                            pictureBox.Width = image.Width;
                            pictureBox.Height = image.Height;
                            pictureBox.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                            pictureBox.BackColor = Color.Transparent;
                            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;

                            pictureBox.Image = image;
                            flowLayoutPanel1.Controls.Add(pictureBox);
                        }
                        catch (SecurityException ex)
                        {
                            // The user lacks appropriate permissions to read files, discover paths, etc.
                            MessageBox.Show("Security error. Please contact your administrator for details.\n\n" +
                                "Error message: " + ex.Message + "\n\n" +
                                "Details (send to Support):\n\n" + ex.StackTrace
                            );
                        }
                        catch (Exception ex)
                        {
                            // Could not load the image - probably related to Windows file system permissions.
                            MessageBox.Show("Cannot display the image: " + file.Substring(file.LastIndexOf('\\'))
                                + ". You may not have permission to read the file, or " +
                                "it may be corrupt.\n\nReported error: " + ex.Message);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Invalid image size.");
                }

            }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            zoomLevel.Text = trackBar1.Value.ToString();
        }

        private void trackBar1_MouseUp(object sender, MouseEventArgs e)
        {

            int currentValue = trackBar1.Value;
            imageScale = currentValue;


            if (flowLayoutPanel1.HasChildren)
            {
                foreach (PictureBox tempBox in flowLayoutPanel1.Controls)
                {
                    tempBox.Width = initialSize.Width * imageScale;
                    tempBox.Height = initialSize.Height * imageScale;
                }


            }
        }


        private void SaveButton_Click(object sender, EventArgs e)
        {
            DialogResult sr = saveFileDialog1.ShowDialog();
            Stream myStream;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = saveFileDialog1.OpenFile()) != null)
                {
                    // Code to write the stream goes here.
                    //Bitmap myBitmap = new Bitmap(flowLayoutPanel1.Width, flowLayoutPanel1.Height, myGraphics);
                    myStream.Close();
                }
            }
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            _Moving = true;
            _Offset = new Point(e.X, e.Y);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (_Moving)
            {
                Point newlocation = this.Location;
                newlocation.X += e.X - _Offset.X;
                newlocation.Y += e.Y - _Offset.Y;
                this.Location = newlocation;
            }
        }
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (_Moving)
            {
                _Moving = false;
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            Form1_MouseDown(sender, e);
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            Form1_MouseMove(sender, e);
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            Form1_MouseUp(sender, e);
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Maximize_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Maximized;
            }
            else
            {
                WindowState = FormWindowState.Normal;
            }
            
        }

        private void Minimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            images.Clear();
            flowLayoutPanel1.Controls.Clear();
            isInitialSizeSet = false;
        }
    }
}
