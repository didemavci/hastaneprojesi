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
namespace Hastane_Projesi
{
    public partial class frmDoktorBilgiDüzenle : Form
    {
        public frmDoktorBilgiDüzenle()
        {
            InitializeComponent();
        }
        sqlBaglantısı bgl = new sqlBaglantısı();
        public string tc;
        private void frmDoktorBilgiDüzenle_Load(object sender, EventArgs e)
        {
            maskedTC.Text = tc;
            SqlCommand komut = new SqlCommand("select * from Tbl_Doktor where DoktorTC=@p1", bgl.baglantı());
            komut.Parameters.AddWithValue("@p1", maskedTC.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                txtad.Text = dr[1].ToString();
                txtsoyad.Text = dr[2].ToString();
                cmbbrans.Text = dr[3].ToString();
                txtsifre.Text = dr[5].ToString();
            }
            bgl.baglantı().Close();
        }
        private void btnguncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update Tbl_Doktor set DoktorAd=@p1,DoktorSoyad=@p2,DoktorBrans=@p3,DoktorSifre=@p4 where DoktorTC=@p5", bgl.baglantı());
            komut.Parameters.AddWithValue("@p1", txtad.Text);
            komut.Parameters.AddWithValue("@p2", txtsoyad.Text);
            komut.Parameters.AddWithValue("@p3", cmbbrans.Text);
            komut.Parameters.AddWithValue("@p4", maskedTC.Text);
            komut.Parameters.AddWithValue("@p5", txtsifre.Text);
            komut.ExecuteNonQuery();
            bgl.baglantı().Close();
            MessageBox.Show("kayıt güncellendi");
        }
    }
}
