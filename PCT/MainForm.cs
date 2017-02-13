using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using PCT.UI;
using System.Windows.Forms.DataVisualization.Charting;
using System.Collections;

namespace PCT
{
    public partial class MainForm : Form
    {
        private List<ArrayList> lsWatchData;
        public MainForm()
        {
            InitializeComponent();
        }

        private void menu2_Click(object sender, EventArgs e)
        {
            ConfigForm cf = new ConfigForm();
            cf.ShowDialog();
            cf.Close();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (cmbSensor.SelectedIndex == -1)
            {
                MessageBox.Show("请选定一个传感器！");
            }else
            {
                cmbSensor.Enabled = false;
                InitChart();
                RunDrawLine();
            }
        }

        private void RunDrawLine()
        {
            lsWatchData = InitWatchDataList();
            timeReadData.Enabled = true;
        }

        private List<ArrayList> InitWatchDataList()
        {
            String[] lsSeries = getSensor();
            List<ArrayList> rtn = new List<ArrayList>();
            for (int i = 0; i < lsSeries.Length; i++)
            {
                ArrayList watchdata = new ArrayList();
                //for(int j = 0; j < 31; j++)
                //{
                //    watchdata.Add(0.00);
                //}
                rtn.Add(watchdata);
            }
            return rtn;
        }

        private void InitChart()
        {
            chartLine.Series.Clear();
            String[] lsSeries = getSensor();
            for (int i=0; i<lsSeries.Length; i++)
            {
                Series se = CreateSeries(i, lsSeries[i]);             
                chartLine.Series.Add(se);
            }
        }
        private Series CreateSeries(int i, string seriename)
        {
            Series series = new Series(seriename);

            //Series的类型
            series.ChartType = SeriesChartType.Line;
            //Series的边框颜色
            series.BorderColor = Color.FromArgb(180, 26, 59, 105);
            //线条宽度
            series.BorderWidth = 3;
            //线条阴影颜色
            //series.ShadowColor = Color.Black;
            //阴影宽度
            //series.ShadowOffset = 2;
            //是否显示数据说明
            series.IsVisibleInLegend = true;
            //线条上数据点上是否有数据显示
            series.IsValueShownAsLabel = false;
            //线条上的数据点标志类型
            series.MarkerStyle = MarkerStyle.None;
            //线条数据点的大小
            //series.MarkerSize = 8;
            //线条颜色
            switch (i)
            {
                case 0:
                    series.Color = Color.FromArgb(220, 65, 140, 240);
                    break;
                case 1:
                    series.Color = Color.FromArgb(220, 224, 64, 10);
                    break;
                case 2:
                    series.Color = Color.FromArgb(220, 120, 150, 20);
                    break;
                case 3:
                    series.Color = Color.FromArgb(220, 12, 128, 232);
                    break;
                default:
                    series.Color = Color.FromName("black");
                    break;
            }
            return series;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            cmbSensor.Enabled = true;
            timeReadData.Enabled = false;
        }

        private String[] getSensor()
        {
            return cmbSensor.SelectedItem.ToString().Split('/');
        }

        private void timeReadData_Tick(object sender, EventArgs e)
        {
            ReadDataFromPort();
            for (int i = 0; i < lsWatchData.Count; i++)
            {
                chartLine.Series[i].Points.Clear();
                for (int j = 0; j < lsWatchData[i].Count; j++)
                {
                    chartLine.Series[i].Points.AddXY(j + 1, lsWatchData[i][j]);
                }
            }
        }

        private void ReadDataFromPort()
        {
            for (int i = 0; i < lsWatchData.Count; i++)
            {
                if (lsWatchData[i].Count == 30)
                {
                    lsWatchData[i].RemoveAt(0);
                    
                }
                lsWatchData[i].Add(System.DateTime.Now.Second * 1000 + 1000 * i);
            }
        }

        private void btnToZero_Click(object sender, EventArgs e)
        {
            String[] lsSensor = getSensor();
            for(int i = 0; i < lsSensor.Length; i++)
            {
                chartLine.Series[i].Name = lsSensor[i] + "：" + lsWatchData[i][(lsWatchData[i]).Count - 1].ToString() + "digits";
            }
        }
    }
}
