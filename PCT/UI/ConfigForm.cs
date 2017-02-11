using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace PCT.UI
{
    public partial class ConfigForm : Form
    {
        public ConfigForm()
        {
            InitializeComponent();
            InitList();
            SetConfigData();
        }

        private void SetConfigData()
        {
            cmbPort.SelectedItem = ConfigurationManager.AppSettings["Port"];
            cmbBaudRates.SelectedItem = ConfigurationManager.AppSettings["BaudRates"];
            cmbDatabits.SelectedItem = ConfigurationManager.AppSettings["Databits"];
            cmbParity.SelectedItem = ConfigurationManager.AppSettings["Parity"];
            cmbStopBits.SelectedItem = ConfigurationManager.AppSettings["StopBits"];
        }

        private void InitList()
        {
            SerialPortUtil.SetPortNameValues(cmbPort);
            SerialPortUtil.SetBauRateValues(cmbBaudRates);
            SerialPortUtil.SetDataBitsValues(cmbDatabits);
            SerialPortUtil.SetParityValues(cmbParity);
            SerialPortUtil.SetStopBitValues(cmbStopBits);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            cfa.AppSettings.Settings["Port"].Value = "111";
            cfa.AppSettings.Settings["BaudRates"].Value = cmbBaudRates.SelectedItem.ToString();
            cfa.AppSettings.Settings["Databits"].Value = cmbDatabits.SelectedItem.ToString();
            cfa.AppSettings.Settings["Parity"].Value = cmbParity.SelectedItem.ToString();
            cfa.AppSettings.Settings["StopBits"].Value = cmbStopBits.SelectedItem.ToString();
            cfa.Save(ConfigurationSaveMode.Modified);
            System.Configuration.ConfigurationManager.RefreshSection("appSettings");
        }
    }
}
