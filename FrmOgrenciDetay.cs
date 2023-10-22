﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;




namespace Not_Kayit_Sistemi
{
    public partial class FrmOgrenciDetay : Form
    { 
        public FrmOgrenciDetay()
        {
            InitializeComponent();
        }

        public string numara;
        //YUSUFUN-PC-HP-P [DbNotKayit]
        SqlConnection baglanti = new SqlConnection(@"Data Source=YUSUFUN-PC-HP-P;Initial Catalog=DbNotKayit;Integrated Security=True");

        private void FrmOgrenciDetay_Load(object sender, EventArgs e)
        {
            LblNumara.Text = numara;

            baglanti.Open();
            SqlCommand komut = new SqlCommand("SELECT * FROM TBLDERS WHERE OGRNUMARA = @p1", baglanti);
            komut.Parameters.AddWithValue("@p1", numara);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                LblAdsoyad.Text = dr[2].ToString() + " " + dr[3].ToString();
                LblSinav1.Text = dr[4].ToString();
                LblSinav2.Text = dr[5].ToString();
                LblSinav3.Text = dr[6].ToString();
                LblOrtalama.Text = dr[7].ToString();
                LblDurum.Text = dr[8].ToString();

            }

            baglanti.Close();

        }
    }
}
