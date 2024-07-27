using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PostgreUrunProje
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localhost; port=5432; Database=dburunler; user ID = postgres; password=emre123");

        private void btnListele_Click(object sender, EventArgs e)
        {
            string sorgu = "select * from kategoriler";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut1 = new NpgsqlCommand("insert into kategoriler (kategoriid, kategoriad) values (@p1, @p2)", baglanti);
            komut1.Parameters.AddWithValue("@p1", int.Parse(TxtKategoriId.Text));
            komut1.Parameters.AddWithValue("@p2", TxtAd.Text);
            komut1.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kategori ekleme işlemi başarılı bir şekilde gerçekleşti");

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
