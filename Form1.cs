using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using MongoDB.Driver;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace PersonelKayit
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source = DESKTOP-Q0J7R6D\\SQLEXPRESS; Initial Catalog = PersonelVeriTabani; Integrated Security = True");
        void temizle()
        {
            
            txtid.Text = "";
            txtAd.Text = "";
            txtMeslek.Text = "";
            txtSoyad.Text = "";
            CmbSehir.Text = "";
            mskMaas.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            txtAd.Focus();

        }

        private void Form1_Load(object sender, EventArgs e)
        {


            string sql = "Select*from Tbl_Personel";

            SqlDataAdapter da = new SqlDataAdapter(sql, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds, "Personel");
            dataGridView1.DataSource = ds.Tables["Personel"].DefaultView;
        }

        private void btnListele_Click(object sender, EventArgs e)
        {
            string sql = "Select*from Tbl_Personel";
              
            SqlDataAdapter da = new SqlDataAdapter(sql, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds, "Personel");
            dataGridView1.DataSource = ds.Tables["Personel"].DefaultView;
            
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();

            SqlCommand komut = new SqlCommand("insert into Tbl_Personel (PerAd,PerSoyad,PerSehir,PerMaas,PerDurum,PerMeslek) values (@p1,@p2,@p3,@p4,@p5,@p6)",baglanti);
           
            try
            {
                int maas = Convert.ToInt32(mskMaas.Text);

                komut.Parameters.AddWithValue("@p1", txtAd.Text);
                komut.Parameters.AddWithValue("@p2", txtSoyad.Text);
                komut.Parameters.AddWithValue("@p3", CmbSehir.Text);
                komut.Parameters.Add("@p4", SqlDbType.Int).Value = maas;
                komut.Parameters.AddWithValue("@p6", txtMeslek.Text);
                komut.ExecuteNonQuery();
                MessageBox.Show("Personel Eklendi.");
            }
            catch
            {
                MessageBox.Show("hata");
            }
           
            baglanti.Close();
           
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtid.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtSoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            txtMeslek.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();
            CmbSehir.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            mskMaas.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            label10.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();


        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            label10.Text = "true";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            label10.Text = "false";
        }

        private void label10_TextChanged(object sender, EventArgs e)
        {
            if (label10.Text == "True")
            {
                radioButton1.Checked = true;
            }
            if (label10.Text == "False")
            {
                radioButton2.Checked = true;
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {

            
            baglanti.Open();
            SqlCommand sil = new SqlCommand("delete from Tbl_Personel Where PerID=@k1",baglanti);
            sil.Parameters.AddWithValue("@k1", txtid.Text);
            sil.ExecuteNonQuery();
            MessageBox.Show("Kayıt silindi.");
            SqlCommand sil2 = new SqlCommand("DBCC CHECKIDENT (Tbl_Personel, RESEED, 0)",baglanti);
            sil2.ExecuteNonQuery();
            baglanti.Close();
            string sql = "Select*from Tbl_Personel";

            SqlDataAdapter da = new SqlDataAdapter(sql, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds, "Personel");
            dataGridView1.DataSource = ds.Tables["Personel"].DefaultView;
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand guncelle = new SqlCommand("Update Tbl_Personel Set PerAd=@a1, Persoyad=@a2, Persehir=@a3, Permaas=@a4, Perdurum=@a5, Permeslek=@a6 where PerID=@a7", baglanti);
            guncelle.Parameters.AddWithValue("@a1", txtAd.Text);
            guncelle.Parameters.AddWithValue("@a2", txtSoyad.Text);
            guncelle.Parameters.AddWithValue("@a3", CmbSehir.Text);
            guncelle.Parameters.AddWithValue("@a4", mskMaas.Text);
            guncelle.Parameters.AddWithValue("@a5", label10.Text);
            guncelle.Parameters.AddWithValue("@a6", txtMeslek.Text);
            guncelle.Parameters.AddWithValue("@a7", txtid.Text);
            MessageBox.Show("Kayıt güncellendi.");
            guncelle.ExecuteNonQuery();
            baglanti.Close();
        }

        private void btnistatistik_Click(object sender, EventArgs e)
        {
            istatistik frm = new istatistik();
            frm.Show();
        }

        private void btnGrafikler_Click(object sender, EventArgs e)
        {
            Grafik frm = new Grafik();
            frm.Show();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
