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
using System.Web.Script.Serialization;

namespace ZulrahHelper
{
    public partial class Form1 : Form
    {
        //{Location,Phase}
        int cWave = 0;
        Rot[] rot;
        ZulrahLocations[] zLocation = new ZulrahLocations[4];
        PlayerLocation[] pLocation = new PlayerLocation[5];

        Bitmap DrawArea;
        int rotation = 1;

        public Form1()
        {
            InitializeComponent();

            DrawArea = new Bitmap(pictureBox1.Size.Width, pictureBox1.Size.Height);
            pictureBox1.Image = DrawArea;

            //z locations
            zLocation[0] = new ZulrahLocations { x = 64, y = 64 + 16 };
            zLocation[1] = new ZulrahLocations { x = 10, y = 64 - 16 };
            zLocation[2] = new ZulrahLocations { x = 64, y = 10 };
            zLocation[3] = new ZulrahLocations { x = 128 - 10, y = 64 - 16 };
            //p location
            pLocation[0] = new PlayerLocation { x = 34, y = 95 };
            pLocation[1] = new PlayerLocation { x = 34, y = 60 };
            pLocation[2] = new PlayerLocation { x = 64, y = 33 };
            pLocation[3] = new PlayerLocation { x = 94, y = 60 };
            pLocation[4] = new PlayerLocation { x = 94, y = 95 };


            JavaScriptSerializer js = new JavaScriptSerializer();
            rot = js.Deserialize<Rot[]>(File.ReadAllText(@"rot1.txt"));

            UpdatePic(pictureBox1);



        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Application.Exit();

            JavaScriptSerializer js = new JavaScriptSerializer();
            rot = js.Deserialize<Rot[]>(File.ReadAllText(@"rot1.txt"));

            UpdatePic(pictureBox1);

        }

        private Image DrawPhase(int mage)
        {
            Bitmap cc = new Bitmap(24, 24);
            Graphics gg;
            gg = Graphics.FromImage(cc);
            gg.DrawImage(Properties.Resources.Phases, -24 * mage, 0, 72, 24);
            return (Image)cc;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            cWave++;
            if (cWave >= rot.Length) cWave = rot.Length - 1;
            Console.WriteLine("Wave: " + cWave);
            UpdatePic(pictureBox1);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            cWave--;
            if (cWave <= 0) cWave = 0;
            Console.WriteLine("Wave: " + cWave);
            UpdatePic(pictureBox1);
        }

        private void UpdatePic(PictureBox pp)
        {
            if (cWave >= rot.Length) cWave = rot.Length - 1;

            Graphics g;
            g = Graphics.FromImage(DrawArea);
            g.DrawImage(Properties.Resources.Base, 0, 0, 128, 128);

            Pen mypen = new Pen(Brushes.Gray);
            //Draw circles
            //Console.WriteLine(phases[rotation, cWave]);
            g.DrawImage(DrawPhase(rot[cWave].phase), zLocation[rot[cWave].location].x - 12, zLocation[rot[cWave].location].y - 12);

            SolidBrush myBrush = new SolidBrush(Color.Purple);
            g.FillEllipse(myBrush, new Rectangle(pLocation[rot[cWave].pl].x-5, pLocation[rot[cWave].pl].y-5, 10, 10));

            updateProtection(rot[cWave].pray);



            mypen = new Pen(Brushes.Black);

            //g.DrawLine(mypen, 0, 0, 128, 128);

            pp.Image = DrawArea;
            label1.Text = "Rotation: " + rotation + " Wave: " + (cWave + 1)+"/"+ rot.Length;
        }

        private void updateProtection(int pray)
        {
            Graphics g;
            Bitmap DrawArea2 = new Bitmap(pictureBox2.Size.Width, pictureBox2.Size.Height);
            g = Graphics.FromImage(DrawArea2);
            g.DrawImage(Properties.Resources.protection, -108 * pray, 0, 108 * 5, 108);

            pictureBox2.Image = DrawArea2;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            rot = js.Deserialize<Rot[]>(File.ReadAllText(@"rot1.txt"));
            rotation = 1;

            UpdatePic(pictureBox1);

        }

        private void button5_Click(object sender, EventArgs e)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            rot = js.Deserialize<Rot[]>(File.ReadAllText(@"rot2.txt"));
            rotation = 2;

            UpdatePic(pictureBox1);

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            cWave = 0;
            UpdatePic(pictureBox1);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            rot = js.Deserialize<Rot[]>(File.ReadAllText(@"rot3.txt"));
            rotation = 3;

            UpdatePic(pictureBox1);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            rot = js.Deserialize<Rot[]>(File.ReadAllText(@"rot4.txt"));
            rotation = 4;

            UpdatePic(pictureBox1);
        }
    }

    public class Rot
    {
        public int phase { get; set; }
        public int location { get; set; }
        public int pray { get; set; }
        public int pl { get; set; }
    }

    public class ZulrahLocations
    {
        public int x { get; set; }
        public int y { get; set; }
    }

    public class PlayerLocation
    {
        public int x { get; set; }
        public int y { get; set; }
    }
}
