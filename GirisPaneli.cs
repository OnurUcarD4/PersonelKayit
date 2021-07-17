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
namespace PersonelKayit
{
    public partial class GirisPaneli : Form
    {

        SqlConnection baglanti = new SqlConnection("Data Source = DESKTOP-Q0J7R6D\\SQLEXPRESS; Initial Catalog = PersonelVeriTabani; Integrated Security = True");

        public GirisPaneli()
        {
            InitializeComponent();
        }

        private void btnGirisYap_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand giris = new SqlCommand("Select * from Tbl_Yonetici where KullaniciAd=@p1 and Sifre=@p2", baglanti);
            giris.Parameters.AddWithValue("@p1", txtKullaniciAd.Text);
            giris.Parameters.AddWithValue("@p2", txtSifre.Text);
            SqlDataReader dr = giris.ExecuteReader();
            if (dr.Read())
            {
                Form1 anaform = new Form1();
                anaform.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı kullanıcı adı ya da şifre.");
            }
            baglanti.Close();
        }

        private void GirisPaneli_Load(object sender, EventArgs e)
        {


        }

        private void GirisPaneli_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand kayit = new SqlCommand("insert into Tbl_Yonetici (KullaniciAd,Sifre) values (@p1,@p2)", baglanti);
            kayit.Parameters.AddWithValue("@p1", txtKullaniciAd.Text);
            kayit.Parameters.AddWithValue("@p2", txtSifre.Text);
            kayit.ExecuteNonQuery();
            MessageBox.Show("Kayıt Oluşturuldu. Giriş yapabilirsiniz.");
            baglanti.Close();
        }

    }
}
