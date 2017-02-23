using PCT.Common.Channels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PCT.UI
{
    public partial class GainForm : Form
    {
        public delegate void GainEventHandler(Object send, GainEventArgs e);
        public event GainEventHandler gainSavedEvent = null;
        private IChannel channel;
        public GainForm()
        {
            InitializeComponent();            
        }

        private void InitGridView()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt.TableName = "dt1";
            dt.Columns.Add(new DataColumn("name"));
            dt.Columns.Add(new DataColumn("testdata"));
            dt.Columns.Add(new DataColumn("gaindata"));
            foreach(ChannelTestObjectVO voTO in channel.GetChannelTestObjects())
            {
                DataRow row = dt.NewRow();
                row["name"] = voTO.DisplayName;
                row["testdata"] = voTO.GainTestData;
                row["gaindata"] = voTO.GainFixData;
                dt.Rows.Add(row);
            }
            ds.Tables.Add(dt);
            dgView.DataSource = ds;
            dgView.DataMember = "dt1";

        }

        public void SetChannel(IChannel c)
        {
            channel = c;
            InitGridView();
        }

        private void dgView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgView.BeginEdit(true);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            GainEventArgs args = new GainEventArgs();
            try
            {
                int i = 0;
                foreach (DataGridViewRow row in dgView.Rows)
                {
                    double gaindata = 0.00;
                    double.TryParse(row.Cells[2].Value.ToString(), out gaindata);
                    channel.GetChannelTestObjects()[i].GainFixData = gaindata;
                    i++;
                }
                args.isSuccess = true;
            }
            catch (System.Exception)
            {
                
            }
            if (gainSavedEvent != null)
            {
                gainSavedEvent.Invoke(this, args);
            }
            this.Close();
            this.Dispose();
        }
    }

    public class GainEventArgs : EventArgs
    {
        public Boolean isSuccess = false;
    }
}
