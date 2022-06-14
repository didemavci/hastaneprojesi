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
    public partial class sekreterDetay : Form
    {
        public sekreterDetay()
        {
            InitializeComponent();
        }
        public string tcnumara;
        sqlBaglantısı bgl = new sqlBaglantısı();
        private void sekreterDetay_Load(object sender, EventArgs e)
        {
            lbltc.Text = tcnumara;

            //ad soyad
            SqlCommand komut = new SqlCommand("select SekreterAdSoyad from Tbl_Sekreter where SekreterTC=@p1",bgl.baglantı());
            komut.Parameters.AddWithValue("@p1",lbltc.Text);
            SqlDataReader dr1 = komut.ExecuteReader();
            while (dr1.Read())
            {
                lbladsoyad.Text = dr1[0].ToString();
            }
            bgl.baglantı().Close();

            //branşları datagride aktar

            DataTable dt1 = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from Tbl_Brans",bgl.baglantı());
            da.Fill(dt1);
            dataGridView1.DataSource = dt1;

            //doktorları listeye aktarma

            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("select(DoktorAd +' '+ DoktorSoyad) as 'Doktorlar',DoktorBrans from Tbl_Doktor ", bgl.baglantı());
            da2.Fill(dt2);
            dataGridView2.DataSource = dt2;

            //branşı comboboxa aktarma
            SqlCommand komut2 = new SqlCommand("select BransAd from Tbl_Brans ", bgl.baglantı());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                cmbbrans.Items.Add(dr2[0]);
            }
            bgl.baglantı().Close();
        }

        private void btnkaydet_Click(object sender, EventArgs e)
        {
            SqlCommand kmtkaydet = new SqlCommand("insert into Tbl_Randevular (RandevuTarih,RandevuSaat,RandevuBrans,RandevuDoktor) values (@r1,@r2,@r3,@r4)",bgl.baglantı());
            kmtkaydet.Parameters.AddWithValue("@r1", msktarih.Text);
            kmtkaydet.Parameters.AddWithValue("@r2", msksaat.Text);
            kmtkaydet.Parameters.AddWithValue("@r3", cmbbrans.Text);
            kmtkaydet.Parameters.AddWithValue("@r4", cmbdoktor.Text);
            kmtkaydet.ExecuteNonQuery();
            bgl.baglantı().Close();
            MessageBox.Show("Randevu Oluşturuldu..");
        }

        private void cmbbrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbdoktor.Items.Clear();

            SqlCommand komut3= new SqlCommand("select DoktorAd,DoktorSoyad from Tbl_Doktor where DoktorBrans=@p1",bgl.baglantı());
            komut3.Parameters.AddWithValue("@p1", cmbbrans.Text);
            SqlDataReader dr = komut3.ExecuteReader();
            while (dr.Read())
            {
                cmbdoktor.Items.Add(dr[0] + " " + dr[1]);
            }
            bgl.baglantı().Close();
        }

        private void btnduyuruol_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into Tbl_Duyuru (duyuru) values (@d1)", bgl.baglantı());
            komut.Parameters.AddWithValue("@d1", richduyuru.Text);
            komut.ExecuteNonQuery();
            bgl.baglantı().Close();
            MessageBox.Show("duyuru oluşturuldu.");
        }

        private void btndoktorpan_Click(object sender, EventArgs e)
        {
            FrmDoktorPaneli drp = new FrmDoktorPaneli();
            drp.Show();

        }

        private void btnbranspan_Click(object sender, EventArgs e)
        {
            FrmBrans frb = new FrmBrans();
            frb.Show();
        }

        private void btnrandevulis_Click(object sender, EventArgs e)
        {
            FrmRandevuListesi frr = new FrmRandevuListesi();
            frr.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmDuyurular fr = new frmDuyurular();
            fr.Show();
        }
    }
}
