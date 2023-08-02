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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace UAS_PARKING
{
    public partial class Form1 : Form
    {
        OdbcCommand dml;
        OdbcDataReader dr;

        const int minibus = 3000 ;
        const int  motor = 5000;
        const int truk = 7000; 
        const int bus = 10000 ;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            connection getconn = new connection();
            getconn.BukaKoneksi();
            MaximizeBox= false; 
            show();

        }
       public void show()
        {
            String sql;
            sql = "SELECT * FROM user";
            dml = new OdbcCommand(sql, connection.koneksi);
            dr = dml.ExecuteReader();
            lvData.Items.Clear();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    while (dr.Read())
                    {
                        ListViewItem Data = lvData.Items.Add(dr.GetString(0));
                        Data.SubItems.Add(dr.GetString(1));
                        Data.SubItems.Add(dr.GetString(2));
                        Data.SubItems.Add(dr.GetString(3));
                        Data.SubItems.Add(dr.GetString(4));

                    }
                }
            }
            else
            {
                lvData.Items.Clear();
            }
        }
        private void clean()
        {
          txtKarcis.Text = string.Empty;
            txtNopol.Text =   string.Empty;
            cmbJenis.Text = "";

        }
        private void kirimMasuk()
        {
            string sql;
            sql = "INSERT INTO user VALUES('" + txtKarcis.Text + "','" + txtNopol.Text + "','" + cmbJenis.SelectedItem + "','" + dateTimePicker1.Value + "','" + "MASUK" + "')";
            dml = new OdbcCommand(sql, connection.koneksi);
            dr = dml.ExecuteReader();

        }




        private void btnSimpan_Click(object sender, EventArgs e)
        {
            if (txtNopol.Text == "" || txtKarcis.Text == "" || cmbJenis.Text == "")
            {
                MessageBox.Show("Harap isi seluruh kolom! ");
            }
            else
            {


                String sql, sqll;
                sql = "SELECT * FROM user WHERE no_karcis = '" + txtKarcis.Text + "'";
                dml = new OdbcCommand(sql, connection.koneksi);
                dr = dml.ExecuteReader();
                if (dr.HasRows)
                {
                    if (txtNopol.Text == dr.GetString(1))
                    {
                        double bayar;
                        bayar = 0;
                        DateTime awal;
                        awal = DateTime.Parse(dr.GetString(3));
                        TimeSpan ts = dateTimePicker1.Value - awal;
                        int tWaktu = (int)Math.Ceiling(ts.TotalHours);
                       
                        //fungsi harga
                        if (cmbJenis.SelectedIndex == 0)
                        {
                            labelHarga.Text = "Rp. " + minibus.ToString() + "/jam";
                            bayar = tWaktu * minibus;
                            MessageBox.Show("Waktu Masuk : " + awal + "\n" + "Waktu Keluar : " + dateTimePicker1.Value + "\n" + "Lama Parkir : " + tWaktu + " Jam \n Total Pembayaran : Rp" + bayar);
                        }
                        else if (cmbJenis.SelectedIndex == 1)
                        {
                            labelHarga.Text = "Rp. " + motor.ToString() + "/jam"; ;
                            bayar = tWaktu * motor;
                            MessageBox.Show("Waktu Masuk : " + awal + "\n" + "Waktu Keluar : " + dateTimePicker1.Value + "\n" + "Lama Parkir : " + tWaktu + " Jam \n Total Pembayaran : Rp" + bayar);
                        }
                        else if (cmbJenis.SelectedIndex == 2)
                        {
                            labelHarga.Text = "Rp. " + truk.ToString() + "/jam"; ;
                            bayar = tWaktu * truk;
                            MessageBox.Show("Waktu Masuk : " + awal + "\n" + "Waktu Keluar : " + dateTimePicker1.Value + "\n" + "Lama Parkir : " + tWaktu + " Jam \n Total Pembayaran : Rp" + bayar);
                        }
                        else if (cmbJenis.SelectedIndex == 3)
                        {
                            labelHarga.Text = "Rp. " + bus.ToString() + "/jam"; ;
                            bayar = tWaktu * bus;
                            MessageBox.Show("Waktu Masuk : " + awal + "\n" + "Waktu Keluar : " + dateTimePicker1.Value + "\n" + "Lama Parkir : " + tWaktu + " Jam \nTotal Pembayaran : Rp " + bayar);
                        }

                        sql = " UPDATE user SET status = '" + "Keluar " + "', date = '" + dateTimePicker1.Value + "' WHERE no_karcis = '" + txtKarcis.Text + "'";
                        dml = new OdbcCommand(sql, connection.koneksi);
                        dr = dml.ExecuteReader();
                        show();

                    }
                    else
                    {
                        MessageBox.Show("Tiket Salah!");
                    }
                }
                else
                {
                    kirimMasuk();
                    show();
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void lvData_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2(this);
            frm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            clean();
        }
    }

    }

