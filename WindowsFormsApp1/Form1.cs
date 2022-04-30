using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        private Bitmap bmp;
        private Pen blackPen;
        private Graphics g;

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files (*.BMP, *.JPG, " +
            "*.GIF, *.PNG)|*.bmp;*.jpg;*.gif;*.png";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Image image = Image.FromFile(dialog.FileName);
                int width = image.Width;
                int height = image.Height;
                pictureBox1.Width = width;
                pictureBox1.Height = height;
                bmp = new Bitmap(image, width, height);
                pictureBox1.Image = bmp;
                g = Graphics.FromImage(pictureBox1.Image);
            }
        }         
        private void button2_Click(object sender, EventArgs e)
        {           
            SaveFileDialog savedialog = new SaveFileDialog();
            savedialog.Title = "Сохранить картинку как ...";
            savedialog.OverwritePrompt = true;
            savedialog.CheckPathExists = true;
            savedialog.Filter =
            "Bitmap File(*.bmp)|*.bmp|" +
            "GIF File(*.gif)|*.gif|" +            
"JPEG File(*.jpg)|*.jpg|" +
"PNG File(*.png)|*.png";
            if (savedialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = savedialog.FileName;
                string strFilExtn = fileName.Remove(0,
                fileName.Length - 3);
                switch (strFilExtn)
                {
                    case "bmp":
                        bmp.Save(fileName,
                        System.Drawing.Imaging.ImageFormat.Bmp);
                        break;
                    case "jpg":
                        bmp.Save(fileName,
                        System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;
                    case "gif":
                        bmp.Save(fileName,
                        System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                    case "tif":
                        bmp.Save(fileName,
                        System.Drawing.Imaging.ImageFormat.Tiff);
                        break;
                    case "png":
                        bmp.Save(fileName,
                        System.Drawing.Imaging.ImageFormat.Png);
                        break;
                    default:
                        break;

                }
            } 
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int width = pictureBox1.Image.Width;
            int height = pictureBox1.Image.Height;
            pictureBox1.Width = width;
            pictureBox1.Height = height;
            bmp = new Bitmap(pictureBox1.Image, width, height);
            pictureBox1.Image = bmp;
            g = Graphics.FromImage(pictureBox1.Image);
            Point[] mas = new Point[3];
            mas[0] = new Point(0, bmp.Height / 2);
            mas[1] = new Point(bmp.Width / 2, 0);
            mas[2] = new Point(bmp.Width, bmp.Height / 2);
            g.DrawPolygon(blackPen, mas);

            for (int i = 0; i < bmp.Width; i++)
                for (int j = 0; j < bmp.Height; j++)
                {
                                     Point point = new Point(i, j);
                    Point p1, p2;
                    bool inside = false;

                    if (mas.Length < 3)
                    {
                        inside=false;
                    }

                    Point oldPoint = new Point(
                        mas[mas.Length - 1].X,  mas[mas.Length - 1].Y);

                    for (int k = 0; k < mas.Length; k++)
                    {
                        Point newPoint = new Point(mas[k].X, mas[k].Y);

                        if (newPoint.X > oldPoint.X)
                        {
                            p1 = oldPoint;
                            p2 = newPoint;
                        }
                        else
                        {
                            p1 = newPoint;
                            p2 = oldPoint;
                        }

                        if ((newPoint.X < point.X) == (point.X <= oldPoint.X)
                            && (point.Y - (long)p1.Y) * (p2.X - p1.X)
                            < (p2.Y - (long)p1.Y) * (point.X - p1.X))
                        {
                            inside = !inside;
                        }

                        oldPoint = newPoint;
                    }                
            if (inside)
                    {

                        int R = bmp.GetPixel(i, j).R;

                        int G = bmp.GetPixel(i, j).G;

                        int B = bmp.GetPixel(i, j).B;

                        int Gray = (R + G + B) / 3;

                        Color p = Color.FromArgb(255, Gray, Gray,
                        Gray);

                        bmp.SetPixel(i, j, p);
                    }
                    else
                    {
                        Color b = Color.FromArgb(255, 0, 0, bmp.GetPixel(i, j).B);
                        bmp.SetPixel(i, j, b);
                    }
                    Refresh();
                }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            blackPen = new Pen(Color.Black, 2);
            
        }

        
    }
}
