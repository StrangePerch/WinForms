using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApp
{
    public partial class Form1 : Form
    {
        private double a92 = 22.7;
        private double a95 = 23.85;
        private double diesel = 22.78;
        private double gas = 11.75;

        private int counter = 0;

        public Form1()
        {
            InitializeComponent();

            comboBox1.SelectedIndex = 0;

        }

        private void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {
            textBox5.Enabled = checkBox1.CheckState == CheckState.Checked;
            textBox5.Text = @"0";
        }

        private void checkBox2_CheckStateChanged(object sender, EventArgs e)
        {
            textBox6.Enabled = checkBox2.CheckState == CheckState.Checked;
            textBox6.Text = @"0";
        }

        private void checkBox3_CheckStateChanged(object sender, EventArgs e)
        {
            textBox8.Enabled = checkBox3.CheckState == CheckState.Checked;
            textBox8.Text = @"0";
        }

        private void checkBox4_CheckStateChanged(object sender, EventArgs e)
        {
            textBox10.Enabled = checkBox4.CheckState == CheckState.Checked;
            textBox10.Text = @"0";
        }

        private void CafeCalc(object sender, EventArgs e)
        {
            double res = 0;
            double.TryParse(textBox5.Text, out var temp);
            res += temp * double.Parse(textBox4.Text);
            double.TryParse(textBox6.Text, out temp);
            res += temp * double.Parse(textBox7.Text);
            double.TryParse(textBox8.Text, out temp);
            res += temp * double.Parse(textBox9.Text);
            double.TryParse(textBox10.Text, out temp);
            res += temp * double.Parse(textBox11.Text);
            label9.Text = res.ToString("F");
        }

        private void StationCalc(object sender, EventArgs e)
        {
            if (textBox2.Enabled)
            {
                int amount = 0;
                double cost = 0;
                int.TryParse(textBox2.Text, out amount);
                double.TryParse(textBox1.Text, out cost);
                textBox3.Text = (cost * amount).ToString("F");
            }
            else
            {
                int sum = 0;
                double cost = 0;
                int.TryParse(textBox3.Text, out sum);
                double.TryParse(textBox1.Text, out cost);
                textBox2.Text = (sum / cost).ToString("F");
            }
            label6.Text = textBox3.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double cafe = double.Parse(label9.Text);
            double station = double.Parse(label6.Text);
            double money = (cafe + station);
            label11.Text = (double.Parse(label11.Text) + money).ToString("F");

            chart1.Series["GasStationProfit"].Points.AddXY(counter, station);
            chart1.Series["CafeProfit"].Points.AddXY(counter++, cafe);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = comboBox1.SelectedIndex;
            double cost = 0;
            switch (index)
            {
                case 0:
                    cost = a92;
                    break;
                case 1:
                    cost = a95;
                    break;
                case 2:
                    cost = diesel;
                    break;
                case 3:
                    cost = gas;
                    break;
                default: cost = 0;
                    break;
            }

            textBox1.Text = cost.ToString("F");
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.Enabled = radioButton1.Checked;
            textBox3.Enabled = !radioButton1.Checked;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            textBox3.Enabled = radioButton2.Checked;
            textBox2.Enabled = !radioButton2.Checked;
        }
    }
}
