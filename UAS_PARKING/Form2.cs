using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace UAS_PARKING
{
    public partial class Form2 : Form
    {
        Form opener;
        OdbcCommand dml;
        OdbcDataReader dr;
        public Form2(Form parentForm)
        {
            InitializeComponent();
            opener = parentForm;
            MaximizeBox = false; //disable maximizze 
        }

        public void lihat()
        {
            String sql;
            sql = "SELECT * FROM user";
            dml = new OdbcCommand(sql, connection.koneksi);
            dr = dml.ExecuteReader();
            lvData2.Items.Clear();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    while (dr.Read())
                    {
                        ListViewItem Data = lvData2.Items.Add(dr.GetString(0));
                        Data.SubItems.Add(dr.GetString(1));
                        Data.SubItems.Add(dr.GetString(2));
                        Data.SubItems.Add(dr.GetString(3));
                        Data.SubItems.Add(dr.GetString(4));

                    }
                }
            }
            else
            {
                lvData2.Items.Clear();
            }
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            connection getconn = new connection();
            getconn.BukaKoneksi();

            lihat();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            lihat();
        }
    }
}
