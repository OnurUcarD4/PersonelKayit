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
    public partial class Grafik : Form
    {

        SqlConnection baglanti = new SqlConnection("Data Source = DESKTOP-Q0J7R6D\\SQLEXPRESS; Initial Catalog = PersonelVeriTabani; Integrated Security = True");

        public Grafik()
        {
            InitializeComponent();
        }



        private void Grafik_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand g2 = new SqlCommand("Select PerMeslek,Avg(PerMaas) from Tbl_Personel Group by PerMeslek", baglanti);
            SqlDataReader dr2 = g2.ExecuteReader();
            while (dr2.Read())
            {
                chart2.Series["Meslek-Maas"].Points.AddXY(dr2[0], dr2[1]);
            }


            baglanti.Close();

            baglanti.Open();
            SqlCommand g1 = new SqlCommand("Select PerSehir,Count(*) from Tbl_Personel Group by PerSehir", baglanti);
            SqlDataReader dr1 = g1.ExecuteReader();
            while (dr1.Read())
            {
                chart1.Series["Sehirler"].Points.AddXY(dr1[0], dr1[1]);
            }

            baglanti.Close();
        }
    }
}
