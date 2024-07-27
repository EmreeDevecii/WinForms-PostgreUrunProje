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
    public partial class FormUrun : Form
    {
        public FormUrun()
        {
            InitializeComponent();
        }
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localhost; port=5432; Database=dburunler; user ID = postgres; password=emre123");
        private void btnListele_Click(object sender, EventArgs e)
        {
            string sorgu = "select * from urunler";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void FormUrun_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("select * from kategoriler", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBox1.DisplayMember = "kategoriad";
            comboBox1.ValueMember = "kategoriid";
            comboBox1.DataSource = dt;
            baglanti.Close();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("insert into urunler (urunid, urunad, stok, alisfiyat, satisfiyat, gorsel, kategori) values (@p1, @p2, @p3, @p4, @p5, @p6, @p7)", baglanti);
            komut.Parameters.AddWithValue("@p1", int.Parse(TxtID.Text));
            komut.Parameters.AddWithValue("@p2", TxtAd.Text);
            komut.Parameters.AddWithValue("@p3", int.Parse(numericUpDown1.Value.ToString()));
            komut.Parameters.AddWithValue("@p4", double.Parse(TxtAlisFiyat.Text));
            komut.Parameters.AddWithValue("@p5", double.Parse(TxtSatisFiyat.Text));
            komut.Parameters.AddWithValue("@p6", TxtGorsel.Text);
            komut.Parameters.AddWithValue("@p7", int.Parse(comboBox1.SelectedValue.ToString()));

            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Ürün kaydı başarılı bir şekilde gerçekleşti.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut2 = new NpgsqlCommand("Delete from urunler where urunid=@p1", baglanti);
            komut2.Parameters.AddWithValue("@p1", int.Parse(TxtID.Text));
            komut2.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Ürün silme işlemi başarılı bir şekilde gerçekleşti.","Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Stop);


        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut3 = new NpgsqlCommand("update urunler set urunad=@p1, stok=@p2, alisfiyat=@p3 where urunid=@p4", baglanti);
            komut3.Parameters.AddWithValue("@p1", TxtAd.Text);
            komut3.Parameters.AddWithValue("@p2", int.Parse(numericUpDown1.Value.ToString()));
            komut3.Parameters.AddWithValue("@p3", double.Parse(TxtAlisFiyat.Text));
            komut3.Parameters.AddWithValue("@p4", int.Parse(TxtID.Text));
            komut3.ExecuteNonQuery();
            MessageBox.Show("Ürün güncelleme işlemi başarılı bir şekilde gerçekleşti.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            baglanti.Close();
        }

        private void btbView_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut4 = new NpgsqlCommand("Select * from urunlistesi", baglanti);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(komut4);
            DataSet dt = new DataSet();
            da.Fill(dt);
            dataGridView1.DataSource = dt.Tables[0];
            baglanti.Close();

        }
    }
}
