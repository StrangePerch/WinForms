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
    public partial class StatisticForm : Form
    {
        private Dictionary<string, List<double>> total_statistic;
        private List<double> cafe_statistic;
        private List<double> station_statistic;

        public StatisticForm(List<double> station, List<double> cafe, Dictionary<string, List<double>> total)
        {
            InitializeComponent();

            total_statistic = total;
            cafe_statistic = cafe;
            station_statistic = station;

            for (int i = 0; i < cafe_statistic.Count; i++)
            {
                chart1.Series["CafeProfit"].Points.AddXY(i, cafe_statistic[i]);
                chart1.Series["GasStationProfit"].Points.AddXY(i, station_statistic[i]);
            }

            chart2.Series.Clear();
            foreach (var obj in total_statistic)
            {
                chart2.Series.Add(obj.Key);
                double sum = 0;
                foreach (var val in obj.Value)
                {
                    sum += val;
                }
                chart2.Series[obj.Key].Points.AddY(sum);
            }

        }
    }
}
