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
    public partial class FrmHastaDetay : Form
    {
        public FrmHastaDetay()
        {
            InitializeComponent();
        }
        public string tc;
        sqlBaglantısı bgl = new sqlBaglantısı();
        private void FrmHastaDetay_Load(object sender, EventArgs e)
        {
            lbltc.Text = tc;
            //ad soyad çekme
            SqlCommand komut = new SqlCommand("select HastaAd,HastaSoyad from Tbl_Hastalar where HastaTC=@p1", bgl.baglantı());
            komut.Parameters.AddWithValue("@p1", lbltc.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                lblladsoyad.Text = dr[0] +" "+ dr[1];
            }
            bgl.baglantı().Close();

            //randevu geçmişi
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select*from Tbl_Randevular where HastaTC=" + tc, bgl.baglantı());
            da.Fill(dt);
            dataGridView1.DataSource = dt;



            //branşları çekme

            SqlCommand komut2 = new SqlCommand("select BransAd from Tbl_Brans", bgl.baglantı());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                cmbbrans.Items.Add(dr2[0]);
            }
            bgl.baglantı().Close();
        }

        private void cmbbrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlCommand komut3 = new SqlCommand("select DoktorAd,DoktorSoyad from Tbl_Doktor where DoktorBrans=@p1",bgl.baglantı());
            komut3.Parameters.AddWithValue("@p1", cmbbrans.Text);
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                cmbdoktor.Items.Add(dr3[0]+" "+dr3[1]);
            }
            bgl.baglantı().Close();
        }

        private void cmbdoktor_SelectedIndexChanged(object sender, EventArgs e)
        {

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select*from Tbl_Randevular where RandevuBrans='" + cmbbrans.Text+"'"+" and RandevuDoktor='"+cmbdoktor.Text+"' and RandevuDurum=0",bgl.baglantı());
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmbilgiduzenle fr = new frmbilgiduzenle();
            fr.tcno = lbltc.Text;
            fr.Show();
        }

        private void btnrandevual_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update Tbl_Randevular set RandevuDurum=1,HastaTC=@p1,HastaSikayet=@p2 where RandevuID=@p3", bgl.baglantı());
            komut.Parameters.AddWithValue("@p1", lbltc.Text);
            komut.Parameters.AddWithValue("@p2", richsikayet.Text);
            komut.Parameters.AddWithValue("@p3", textBox1.Text);
            komut.ExecuteNonQuery();
            bgl.baglantı().Close();
            MessageBox.Show("randevu alındı", "uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

      
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            int secilen = dataGridView2.SelectedCells[0].RowIndex;
            textBox1.Text = dataGridView2.Rows[secilen].Cells[0].Value.ToString();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
