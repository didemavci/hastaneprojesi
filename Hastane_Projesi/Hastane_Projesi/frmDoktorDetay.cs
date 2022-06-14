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
    public partial class frmDoktorDetay : Form
    {
        public frmDoktorDetay()
        {
            InitializeComponent();
        }
        sqlBaglantısı bgl = new sqlBaglantısı();
        public string tc;
        private void frmDoktorDetay_Load(object sender, EventArgs e)
        {
            label1.Text = tc;

            //doktor ad soyad
            SqlCommand komut = new SqlCommand("select DoktorAd,DoktorSoyad from Tbl_Doktor where DoktorTC=@p1",bgl.baglantı());
            komut.Parameters.AddWithValue("@p1", label1.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                lblladsoyad.Text = dr[0] + " " + dr[1];
            }
            bgl.baglantı().Close();


            //randevular
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select*from Tbl_Randevular where RandevuDoktor='"+lblladsoyad.Text+"'",bgl.baglantı());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            frmDoktorBilgiDüzenle fr = new frmDoktorBilgiDüzenle();
            fr.tc = label1.Text;
            fr.Show();
        }

        private void btnduyuru_Click(object sender, EventArgs e)
        {
            frmDuyurular fr = new frmDuyurular();
            fr.Show();
        }

        private void btncıkıs_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            richTextBox1.Text = dataGridView1.Rows[secilen].Cells[7].Value.ToString();
        }

       
    }
}
