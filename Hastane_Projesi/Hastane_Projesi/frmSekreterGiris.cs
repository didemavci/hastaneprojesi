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
    public partial class frmSekreterGiris : Form
    {
        public frmSekreterGiris()
        {
            InitializeComponent();
        }
        sqlBaglantısı bgl = new sqlBaglantısı();
        private void btngirisyap_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("select * from Tbl_Sekreter where SekreterTC=@p1 and SekreterSifire=@p2", bgl.baglantı());
            komut.Parameters.AddWithValue("@p1", maskedTC.Text);
            komut.Parameters.AddWithValue("@p2", txtsifre.Text);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                sekreterDetay frs = new sekreterDetay();
                frs.tcnumara = maskedTC.Text;
                frs.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("hatalı tc veya şifre girişi");
            }
            bgl.baglantı().Close();
        }
    }
}
