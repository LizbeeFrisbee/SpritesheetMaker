using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Configuration;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace SpritesheetMaker
{
    public partial class MainForm : Form
    {
        private int imageScale;
        Size initialSize;
        bool isInitialSizeSet;
        int iteration;
        Image aspectChecker;
        bool imagesLoaded = false;

        private bool _Moving = false;
        private Point _Offset;
        public List<Image> images = new List<Image>();
        Bitmap SpriteSheet;
        enum arrangementTypes
        {
            horizontal,
            vertical,
            box
        }
        arrangementTypes arrangementType = arrangementTypes.horizontal;


        public MainForm()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = ((int)arrangementType);

        }

        public void SelectImages_Click(object sender, EventArgs e)
        {
            // Open The File Browser Dialogue
            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {


                aspectChecker = Image.FromFile(openFileDialog1.FileNames.ElementAt(0));
                if (!isInitialSizeSet)
                {
                    initialSize = aspectChecker.Size;
                    isInitialSizeSet = true;
                    imageRes.Text = aspectChecker.Size.Height.ToString() + "x" + aspectChecker.Size.Width.ToString();
                }

                if (aspectChecker.Size == initialSize)
                {
                    GenerateBitmap(arrangementType);
                    imagesLoaded = true;
                }
                else
                {
                    MessageBox.Show("Invalid image size.");
                }

            }
        }

        private void GenerateBitmap(arrangementTypes arrangementType)
        {
            switch (arrangementType)
            {
                case arrangementTypes.horizontal:
                    {
                        SpriteSheet = new Bitmap(aspectChecker.Width * openFileDialog1.FileNames.Length, aspectChecker.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                        Color backColour = SpriteSheet.GetPixel(1, 1);
                        SpriteSheet.MakeTransparent(backColour);
                        Graphics graphics = Graphics.FromImage(SpriteSheet);
                        graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                        graphics.CompositingQuality = CompositingQuality.HighQuality;
                        graphics.CompositingMode = CompositingMode.SourceOver;
                        graphics.SmoothingMode = SmoothingMode.None;

                        pictureBox1.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

                        //Pen pen = new Pen(Color.Red, 2);
                        //graphics.DrawLine(pen, 0, 0, aspectChecker.Width, aspectChecker.Height);
                        //graphics.DrawLine(pen, 0, aspectChecker.Height, aspectChecker.Width, 0);

                        foreach (String file in openFileDialog1.FileNames)
                        {
                            Image image = Image.FromFile(file);
                            images.Add(image);

                            graphics.DrawImage(images.ElementAt(iteration), initialSize.Width * iteration, 0, initialSize.Width, initialSize.Height);
                            iteration++;
                        }


                        iteration = 0;
                        pictureBox1.Image = SpriteSheet;
                    }
                    break;

                case arrangementTypes.vertical:
                    {
                        SpriteSheet = new Bitmap(aspectChecker.Width, aspectChecker.Height * openFileDialog1.FileNames.Length, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                        Color backColour = SpriteSheet.GetPixel(1, 1);
                        SpriteSheet.MakeTransparent(backColour);
                        Graphics graphics = Graphics.FromImage(SpriteSheet);
                        graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                        graphics.CompositingQuality = CompositingQuality.HighQuality;
                        graphics.CompositingMode = CompositingMode.SourceOver;
                        graphics.SmoothingMode = SmoothingMode.None;

                        pictureBox1.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

                        //Pen pen = new Pen(Color.Red, 2);
                        //graphics.DrawLine(pen, 0, 0, aspectChecker.Width, aspectChecker.Height);
                        //graphics.DrawLine(pen, 0, aspectChecker.Height, aspectChecker.Width, 0);

                        foreach (String file in openFileDialog1.FileNames)
                        {
                            Image image = Image.FromFile(file);
                            images.Add(image);

                            graphics.DrawImage(images.ElementAt(iteration), 0, initialSize.Height * iteration, initialSize.Width, initialSize.Height);
                            iteration++;
                        }


                        iteration = 0;
                        pictureBox1.Image = SpriteSheet;
                    }
                    break;

                case arrangementTypes.box:
                    {
                        SpriteSheet = new Bitmap(aspectChecker.Width, aspectChecker.Height * openFileDialog1.FileNames.Length, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                        Color backColour = SpriteSheet.GetPixel(1, 1);
                        SpriteSheet.MakeTransparent(backColour);
                        Graphics graphics = Graphics.FromImage(SpriteSheet);
                        graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                        graphics.CompositingQuality = CompositingQuality.HighQuality;
                        graphics.CompositingMode = CompositingMode.SourceOver;
                        graphics.SmoothingMode = SmoothingMode.None;

                        pictureBox1.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

                        //Pen pen = new Pen(Color.Red, 2);
                        //graphics.DrawLine(pen, 0, 0, aspectChecker.Width, aspectChecker.Height);
                        //graphics.DrawLine(pen, 0, aspectChecker.Height, aspectChecker.Width, 0);

                        foreach (String file in openFileDialog1.FileNames)
                        {
                            Image image = Image.FromFile(file);
                            images.Add(image);

                            graphics.DrawImage(images.ElementAt(iteration), 0, initialSize.Height * iteration, initialSize.Width, initialSize.Height);
                            iteration++;
                        }


                        iteration = 0;
                        pictureBox1.Image = SpriteSheet;
                    }
                    break;
            }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            
        }

        private void trackBar1_MouseUp(object sender, MouseEventArgs e)
        {

            //int currentValue = trackBar1.Value;
            //imageScale = currentValue;


            //if (flowLayoutPanel1.HasChildren)
            //{
            //    foreach (PictureBox tempBox in flowLayoutPanel1.Controls)
            //    {
            //        tempBox.Width = initialSize.Width * imageScale;
            //        tempBox.Height = initialSize.Height * imageScale;
            //    }


            //}
        }


        private void SaveButton_Click(object sender, EventArgs e)
        {
            DialogResult sr = saveFileDialog1.ShowDialog();

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                SpriteSheet.Save(saveFileDialog1.FileName);
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
            pictureBox1.Image = null;
            SpriteSheet.Dispose();
            isInitialSizeSet = false;
            imagesLoaded = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0: arrangementType = arrangementTypes.horizontal; break;

                case 1: arrangementType = arrangementTypes.vertical; break;

                case 2: arrangementType = arrangementTypes.box; break;
            }

            if (imagesLoaded)
            {
                GenerateBitmap(arrangementType);
            }
        }
    }
}
