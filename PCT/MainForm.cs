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
using PCT.Common;
using PCT.Common.Channels;

namespace PCT
{
    public partial class MainForm : Form, IView
    {
        private ComController controller;
        private IChannel channel = null;
        private ChannelType channeltype = new ChannelType();

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
                if(channel != null)
                {
                    //画线初始化
                    lsWatchData = InitWatchDataList();
                    //com数据处理
                    ComConfigVO ccvo = new ComConfigVO();
                    controller.OpenSerialPort(ccvo.Port, ccvo.BaudRates,
                        ccvo.Databits, ccvo.StopBits, ccvo.Parity,
                        "None");
                    Byte[] bytes = ComController.Hex2Bytes(channel.GetSendDataCmd());
                    controller.SendDataToCom(bytes);
                }
                else
                {
                    MessageBox.Show("Channel实例为空，请重新选定一个传感器，或联系系统管理员处理。");
                } 
            }
        }

        //private IChannel cflow;

        //private void RunDrawLine()
        //{
        //    lsWatchData = InitWatchDataList();
        //}

        private void DrawData(int[] data)
        {
            for (int i = 0; i < lsWatchData.Count; i++)
            {
                if (lsWatchData[i].Count == 30)
                {
                    lsWatchData[i].RemoveAt(0);

                }
                lsWatchData[i].Add(data);
            }
            for (int i = 0; i < lsWatchData.Count; i++)
            {
                chartLine.Series[i].Points.Clear();
                for (int j = 0; j < lsWatchData[i].Count; j++)
                {
                    int[] tempdata = (int[])lsWatchData[i][j];
                    chartLine.Series[i].Points.AddXY(tempdata[0], tempdata[1]);
                }
            }
        }

        private List<ArrayList> InitWatchDataList()
        {
            String[] lsSeries = getSensor();
            List<ArrayList> rtn = new List<ArrayList>();
            for (int i = 0; i < lsSeries.Length; i++)
            {
                ArrayList watchdata = new ArrayList();
                rtn.Add(watchdata);
            }
            return rtn;
        }
        private String[] getSensor()
        {
            return cmbSensor.SelectedItem.ToString().Split('/');
        }

        #region Chart初始化
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

        #endregion

        private void btnStop_Click(object sender, EventArgs e)
        {
            cmbSensor.Enabled = true;
            Byte[] bytes = ComController.Hex2Bytes(channel.GetStandbyCmd());
            controller.SendDataToCom(bytes);
            controller.CloseSerialPort();
        }        

        private void btnToZero_Click(object sender, EventArgs e)
        {
            //String[] lsSensor = getSensor();
            //for(int i = 0; i < lsSensor.Length; i++)
            //{
            //    chartLine.Series[i].Name = lsSensor[i] + "：" + lsWatchData[i][(lsWatchData[i]).Count - 1].ToString() + "digits";
            //}
        }

        public void SetController(ComController controller)
        {
            this.controller = controller;
        }

        public void OpenComEvent(object sender, SerialPortEventArgs e)
        {
            if (this.InvokeRequired)
            {
                Invoke(new Action<Object, SerialPortEventArgs>(OpenComEvent), sender, e);
                return;
            }
        }

        public void CloseComEvent(object sender, SerialPortEventArgs e)
        {
            if (this.InvokeRequired)
            {
                Invoke(new Action<Object, SerialPortEventArgs>(CloseComEvent), sender, e);
                return;
            }
        }

        public void ComReceiveDataEvent(object sender, SerialPortEventArgs e)
        {
            if (this.InvokeRequired)
            {
                try
                {
                    Invoke(new Action<Object, SerialPortEventArgs>(ComReceiveDataEvent), sender, e);
                }
                catch (System.Exception)
                {
                    //disable form destroy exception
                }
                return;
            }

            System.IO.StreamWriter sw = new System.IO.StreamWriter("d:\\sc66.txt", true);
            sw.WriteLine(string.Format("{0}\t{1}", System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss fff"), SerialPortUtil.ByteToHex(e.receivedBytes)));
            sw.Close();
            
        }

        private void cmbSensor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbSensor.SelectedIndex > -1)
            {
                channel = ChannelFactory.CreateChannelInstance(channeltype.GetValueFromName(cmbSensor.SelectedItem.ToString()));
            }
        }
    }
}
