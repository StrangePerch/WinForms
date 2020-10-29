using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
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

        private Dictionary<string, List<double>> total_statistic;
        private List<double> cafe_statistic;
        private List<double> station_statistic;
        private List<Product> products;
        private List<CheckBox> checkBoxes;
        private List<NumericUpDown> numericUpDowns;
        private List<TextBox> textBoxes;
        public Form1()
        {

            InitializeComponent();

            cafe_statistic = new List<double>();
            station_statistic = new List<double>();
            

            comboBox1.SelectedIndex = 0;

            products = new List<Product>
            {
                new Product{name = "Hot-Dog", cost = 8},
                new Product{name = "Coffee", cost = 5},
                new Product{name = "Burger", cost = 13},
                new Product{name = "Coca-Cola", cost = 3},
                new Product{name = "Salad", cost = 10},
                new Product{name = "Chocolate", cost = 7},
                new Product{name = "Sandwich", cost = 8},
                new Product{name = "Soup", cost = 10}
            };

            total_statistic = new Dictionary<string, List<double>>(products.Count + 4);

            total_statistic.Add("A-92", new List<double>());
            total_statistic.Add("A-95", new List<double>());
            total_statistic.Add("Diesel", new List<double>());
            total_statistic.Add("Gas", new List<double>());

            foreach (var item in products)
            {
                total_statistic.Add(item.name, new List<double>());
            }

            checkBoxes = new List<CheckBox>(products.Count);
            numericUpDowns = new List<NumericUpDown>(products.Count);
            textBoxes = new List<TextBox>(products.Count);

            for (int i = 0; i < products.Count; i++)
            {
                CheckBox checkBox = new CheckBox
                {
                    Location = new Point(12, 10 + 30 * i), 
                    Size = new Size(80, 17), 
                    Text = products[i].name
                };
                checkBox.CheckedChanged += CheckedChanged;
                checkBoxes.Add(checkBox);

                TextBox textBox = new TextBox
                {
                    Location = new Point(100, 10 + 30 * i),
                    Size = new Size(80, 17),
                    Text = products[i].cost.ToString("F"),
                    Enabled = false
                };
                textBoxes.Add(textBox);
                NumericUpDown numericUpDown = new NumericUpDown
                {
                    Location = new Point(200, 10 + i * 30),
                    Size = new Size(30, 17),
                    Enabled = false
                };
                numericUpDown.TextChanged += CafeCalc;
                numericUpDowns.Add(numericUpDown);

            }

            for (int i = 0; i < products.Count; i++)
            {
                panel1.Controls.Add(textBoxes[i]);
                panel1.Controls.Add(numericUpDowns[i]);
                panel1.Controls.Add(checkBoxes[i]);
            }
        }

        private void CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            int index = checkBoxes.FindIndex(a => a == checkBox);
            numericUpDowns[index].Enabled = checkBox.Checked;
            numericUpDowns[index].Text = "0";
        }

        private void CafeCalc(object sender, EventArgs e)
        {
            double res = 0;

            for (int i = 0; i < products.Count; i++)
            {
                double.TryParse(numericUpDowns[i].Text, out var temp1);
                double.TryParse(textBoxes[i].Text, out var temp2);
                double sum = temp1 * temp2;
                res += sum;
            }

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

        private void Calculate(object sender, EventArgs e)
        {
            double cafe = double.Parse(label9.Text);
            double station = double.Parse(label6.Text);
            double money = (cafe + station);
            label11.Text = (double.Parse(label11.Text) + money).ToString("F");

            string name = comboBox1.Text;

            double res = 0;

            for (int i = 0; i < products.Count; i++)
            {
                double.TryParse(numericUpDowns[i].Text, out var temp1);
                double.TryParse(textBoxes[i].Text, out var temp2);
                double sum = temp1 * temp2;
                total_statistic[products[i].name].Add(sum);
            }

            
            total_statistic[name].Add(double.Parse(label6.Text));

            cafe_statistic.Add(cafe);
            station_statistic.Add(station);
            Print();
        }

        private void Print()
        {
            FileStream stream = new FileStream("Чек.txt", FileMode.Create);
            StreamWriter write = new StreamWriter(stream);
            double num;
            double cost;
            write.WriteLine("==========================");
            write.WriteLine("            ЧЕК           ");
            write.WriteLine("==========================");
            for (int i = 0; i < products.Count; i++)
            {
                double.TryParse(numericUpDowns[i].Text, out num);
                double.TryParse(textBoxes[i].Text, out cost);
                double sum = num * cost;
                if (num >= 1)
                {
                    if(i > 0)
                         write.WriteLine("--------------------------");
                    write.WriteLine($"X{num}");
                    write.WriteLine($"{products[i].name} {cost}грн");
                    write.WriteLine($"СУММА: {sum}грн");
                }

            }

            string name = comboBox1.Text;
            num = int.Parse(textBox2.Text);
            
            if (num > 0)
            {
                cost = double.Parse(textBox1.Text);
                write.WriteLine("--------------------------");
                write.WriteLine($"{num}л");
                write.WriteLine($"{name} {cost}грн");
                write.WriteLine($"СУММА: {num * cost}грн");
            }

            write.WriteLine("==========================");
            write.WriteLine($"ВСЕГО: {label11.Text}грн");
            write.WriteLine("==========================");

            write.Flush();
            write.Close();

            Process.Start("Чек.txt");

            stream = new FileStream("Чек.txt", FileMode.Open);
            StreamReader read = new StreamReader(stream);
            var buffer = read.ReadToEnd();
            read.Close();
            stream = new FileStream("СписокЧеков.txt", FileMode.Append);
            write = new StreamWriter(stream);
            write.WriteLine("");
            write.WriteLine("");
            write.WriteLine(buffer);
            write.Flush();
            write.Close();

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

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2(station_statistic,cafe_statistic,total_statistic);
            form.Show();
        }
    }

    class Product
    {
        public string name;
        public double cost;
    }
}
