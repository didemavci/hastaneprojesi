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
    public partial class FrmHastaKayıt : Form
    {
        public FrmHastaKayıt()
        {
            InitializeComponent();
        }
        sqlBaglantısı bgl = new sqlBaglantısı();
        private void btnkayıtyap_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into Tbl_Hastalar (HastaAd,HastaSoyad,HastaTel,HastaTC,HastaCinsiyet,HastaSifre) values (@p1,@p2,@p3,@p4,@p5,@p6)",bgl.baglantı());
            komut.Parameters.AddWithValue("@p1", txtad.Text);
            komut.Parameters.AddWithValue("@p2", txtsoyad.Text);
            komut.Parameters.AddWithValue("@p3", msktel.Text);
            komut.Parameters.AddWithValue("@p4", maskedTC.Text);
            komut.Parameters.AddWithValue("@p5", cmbcinsiyet.Text);
            komut.Parameters.AddWithValue("@p6", txtsifre.Text);
            komut.ExecuteNonQuery();
            bgl.baglantı().Close();
            MessageBox.Show("kaydınız gerçekleşmiştir. Şifreniz: " + txtsifre,"Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }
    }
}
