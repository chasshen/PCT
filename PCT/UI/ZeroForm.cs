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
    public partial class ZeroForm : Form
    {
        public delegate void ZeroEventHandler(Object send, ZeroEventArgs e);
        public event ZeroEventHandler zeroSavedEvent = null;
        private IChannel channel;
        public ZeroForm()
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
            dt.Columns.Add(new DataColumn("zerodata"));
            foreach(ChannelTestObjectVO voTO in channel.GetChannelTestObjects())
            {
                DataRow row = dt.NewRow();
                row["name"] = voTO.DisplayName;
                row["testdata"] = voTO.ZeroTestData;
                row["zerodata"] = voTO.ZeroFixData;
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
            ZeroEventArgs args = new ZeroEventArgs();
            try
            {
                int i = 0;
                foreach (DataGridViewRow row in dgView.Rows)
                {
                    double zerodata = 0.00;
                    double.TryParse(row.Cells["zerodata"].Value.ToString(), out zerodata);
                    channel.GetChannelTestObjects()[i].ZeroFixData = zerodata;
                    i++;
                }
                args.isSuccess = true;
            }
            catch (System.Exception)
            {
                
            }
            if (zeroSavedEvent != null)
            {
                zeroSavedEvent.Invoke(this, args);
            }
        }
    }

    public class ZeroEventArgs : EventArgs
    {
        public Boolean isSuccess = false;
    }
}
