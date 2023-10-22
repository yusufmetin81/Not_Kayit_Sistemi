using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Not_Kayit_Sistemi
{
    public partial class FrmOgretmenDetay : Form
    {   // MS SQL BAĞLANTI KISMI 
        SqlConnection baglanti = new SqlConnection(@"Data Source=YUSUFUN-PC-HP-P;Initial Catalog=DbNotKayit;Integrated Security=True");

        public FrmOgretmenDetay()
        {
            InitializeComponent();
        }

        private void FrmOgretmenDetay_Load(object sender, EventArgs e)//Otomatik Yenileme
        {
            LoadData();
        }

        private void LoadData()//DataGridView SQL Komutları
        {
            try
            {
                baglanti.Open();

                // Veri çekme sorgusu (örnek olarak)
                string query = "SELECT * FROM TBLDERS";

                // SqlDataAdapter kullanarak veriyi çekme
                SqlDataAdapter adapter = new SqlDataAdapter(query, baglanti);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // DataGridView'e veriyi atama
                dataGridView1.DataSource = dataTable;
            }
            catch (Exception ex)
            {
              MessageBox.Show("Veri çekme hatası: " + ex.Message);
            }
            finally
            {
                baglanti.Close();
            }
        } 

        private void Kaydet_Click(object sender, EventArgs e)//Kaydetme İşlemi
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into TBLDERS(OGRNUMARA,OGRAD,OGRSOYAD) values(@P1,@P2,@P3)", baglanti);
            komut.Parameters.AddWithValue("@P1", MskNumara.Text);
            komut.Parameters.AddWithValue("@P2", TxtAd.Text);
            komut.Parameters.AddWithValue("@P3", TxtSoyad.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Öğrenci Sisteme Eklendi","Öğrenci Kaydı",MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadData();//Eklenen kayıdı otomatik görüntülemeye yarıyor

            // Bu kod kısmına Şunu yap; Bilgi girilmediği halde sisteme BOŞ kayıt yapıyor. Bu bir mantık hatasıdır !
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)//Hücre Doldurma
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;

            MskNumara.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            TxtAd.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            TxtSoyad.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            TxtSinav1.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            TxtSinav2.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            TxtSinav3.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();
        }

        private void Güncelle_Click(object sender, EventArgs e)//Güncelleme İşlemi
        {
            double s1, s2, s3, ort;
            string durum;

            s1 = Convert.ToDouble(TxtSinav1.Text);
            s2 = Convert.ToDouble(TxtSinav2.Text);
            s3 = Convert.ToDouble(TxtSinav3.Text);
            ort = (s1 + s2 + s3) / 3;
            LblOrtalama.Text = ort.ToString();

            if (ort>50)
            {
                durum = "True";
            }
            else
            {
                durum = "False";
            }


            baglanti.Open();
            SqlCommand komut = new SqlCommand("update TBLDERS set OGRS1=@P1,OGRS2=@P2,OGRS3=@P3,ORTALAMA=@P4,DURUM=@P5 WHERE OGRNUMARA=@P6", baglanti);
            komut.Parameters.AddWithValue("@P1", TxtSinav1.Text);
            komut.Parameters.AddWithValue("@P2", TxtSinav2.Text);
            komut.Parameters.AddWithValue("@P3", TxtSinav3.Text);
            komut.Parameters.AddWithValue("@P4", decimal.Parse(LblOrtalama.Text));
            komut.Parameters.AddWithValue("@P5", durum);
            komut.Parameters.AddWithValue("@P6", MskNumara.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Öğrenci Notları Güncelleştirildi", "Güncelleme Bilgisi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadData();

        }
    }
}

