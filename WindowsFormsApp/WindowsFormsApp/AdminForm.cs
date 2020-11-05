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
    public delegate void SwitchBack();
    public partial class AdminForm : Form
    {
        public SwitchBack SwitchBack;
        private Dictionary<string, double> fuel;
        private List<Product> products;
        public AdminForm(SwitchBack switchBack)
        {
            this.SwitchBack = switchBack;

            InitializeComponent();
        }

        public void Show(Dictionary<string, double> fuel, List<Product> products)
        {

            FuelGrid.Rows.Clear();
            CafeGrid.Rows.Clear();

            foreach (var obj in fuel)
            {
                FuelGrid.Rows.Add(obj.Key, obj.Value);
            }

            foreach (var obj in products)
            {
                CafeGrid.Rows.Add(obj.name, obj.cost);
            }
            Show();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Hide();
            SwitchBack();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            foreach (DataGridViewRow row in CafeGrid.Rows)
            {
                if (row.Cells[0].Value == null || row.Cells[0].Value.ToString().Length == 0 || row.Cells[1].Value == null)
                {
                    MessageBox.Show("Заполни нормально таблицу с блюдами для кафе", "ОШИБКА"
                        , MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 1 && double.TryParse(textBox2.Text, out var temp))
            {
                FuelGrid.Rows.Add(textBox1.Text, temp);
                textBox1.Text = "";
                textBox2.Text = "";
            }
            else
            {
                MessageBox.Show("Неправильный формат данных", "ОШИБКА"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox3.Text.Length > 1 && double.TryParse(textBox4.Text, out var temp))
            {
                CafeGrid.Rows.Add(textBox3.Text, temp);
                textBox3.Text = "";
                textBox4.Text = "";
            }
            else
            {
                MessageBox.Show("Неправильный формат данных", "ОШИБКА"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
