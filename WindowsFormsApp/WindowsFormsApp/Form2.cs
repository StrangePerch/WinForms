using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    public partial class Form2 : Form
    {
        private List<double> profit;
        private List<double> calc;
        public Form2(List<double> profit)
        {
            InitializeComponent();
            this.profit = profit;
            calc = new List<double>();

            LabelMax.Text = profit.Max().ToString("F");
            LabelMin.Text = profit.Min().ToString("F");
        }

        private void Form2_Paint(object sender, PaintEventArgs e)
        {
            double max = profit.Max();
            double min = profit.Min();
            foreach (var i in profit)
            {
                calc.Add((i - min) / max);
            }

            Graphics paint = e.Graphics;
            int step = (this.Width - 100) / profit.Count;
            Pen pen = new Pen(Color.Green, 3);
            for (int i = 0; i < calc.Count; i++)
            {
                int minx = 300;
                int maxy = 100;
                int x = 100 + step * i;
                int h1;
                if (i == 0) h1 = minx;
                else h1 = minx - Convert.ToInt32(maxy * calc[i - 1]);
                int h2 = minx - Convert.ToInt32(maxy * calc[i]);
                paint.DrawLine(pen, x, h1, x + step, h2);
            }
        }
    }
}
