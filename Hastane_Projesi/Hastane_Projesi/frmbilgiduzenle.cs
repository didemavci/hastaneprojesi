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
    public partial class frmbilgiduzenle : Form
    {
        public frmbilgiduzenle()
        {
            InitializeComponent();
        }
        public string tcno;
        sqlBaglantısı bgl = new sqlBaglantısı();
        private void frmbilgiduzenle_Load(object sender, EventArgs e)
        {
            maskedTC.Text = tcno;
            SqlCommand komut = new SqlCommand("select *from Tbl_Hastalar where HastaTC=@p1", bgl.baglantı());
            komut.Parameters.AddWithValue("@p1", maskedTC.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                txtad.Text = dr[1].ToString();
                txtsoyad.Text = dr[2].ToString();
                maskedTC.Text = dr[3].ToString();
                msktel.Text = dr[4].ToString();
                txtsifre.Text = dr[5].ToString();
                cmbcinsiyet.Text = dr[6].ToString();
                
            }
            bgl.baglantı().Close();
        }

        private void btnkayıtyap_Click(object sender, EventArgs e)
        {
            SqlCommand komut2 = new SqlCommand("update Tbl_Hastalar set HastaAd=@p1,HastaSoyad=@p2,HastaTel=@p3,HastaSifre=@p4,HastaCinsiyet=@p5 where HastaTC=@p6",bgl.baglantı());
            komut2.Parameters.AddWithValue("@p1", txtad.Text);
            komut2.Parameters.AddWithValue("@p2", txtsoyad.Text);
            komut2.Parameters.AddWithValue("@p3", msktel.Text);
            komut2.Parameters.AddWithValue("@p4", txtsifre.Text);
            komut2.Parameters.AddWithValue("@p5", cmbcinsiyet.Text);
            komut2.Parameters.AddWithValue("@p6", maskedTC.Text);
            komut2.ExecuteNonQuery();
            bgl.baglantı().Close();
            MessageBox.Show("Bilgileriniz güncellendi.","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Warning);



        }
    }
}
