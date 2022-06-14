using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Hastane_Projesi
{
    class sqlBaglantısı
    {
        public SqlConnection baglantı() //metot oluşturdum
        {
            SqlConnection baglan = new SqlConnection("Data Source=HUAWEI;Initial Catalog=HastaneProjesi;Integrated Security=True");
            baglan.Open();
            return baglan;
        }
    }
}
