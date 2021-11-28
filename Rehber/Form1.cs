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

namespace Rehber
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=KOBOOM\SQLEXPRESS;Initial Catalog=Rehber;Integrated Security=True");

        void Listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from KISILER",baglanti);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        void Temizle()
        {
            txtID.Text = "";
            txtAD.Text = "";
            txtSoyad.Text = "";
            mskTelefon.Text = "";
            txtMail.Text = "";
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            Listele();
            baglanti.Close();
        }


        private void btnEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into KISILER(AD,SOYAD,TELEFON,MAIL) values (@p1,@p2,@p3,@p4)", baglanti);
            komut.Parameters.AddWithValue("@p1", txtAD.Text);
            komut.Parameters.AddWithValue("@p2", txtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", mskTelefon.Text);
            komut.Parameters.AddWithValue("@p4", txtMail.Text);
            komut.ExecuteNonQuery();
            Listele();
            baglanti.Close();
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
           baglanti.Open();

            DialogResult SecveSil = new DialogResult();

            SecveSil = MessageBox.Show("Onaylayınız!", "Sil", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if(SecveSil==DialogResult.Yes)
            {
                SqlCommand komut = new SqlCommand("Delete from KISILER where ID=" + txtID.Text, baglanti);
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Kişi Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Listele();
                Temizle();
            }
            else
            {
                MessageBox.Show("Kişi Silinmedi", "uyarı", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtAD.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtSoyad.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            mskTelefon.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtMail.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update KISILER set AD=@p2,SOYAD=@p3,telefon=@p4,mail=@p5 where ID=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1", txtID.Text);
            komut.Parameters.AddWithValue("@p2", txtAD.Text);
            komut.Parameters.AddWithValue("@p3", txtSoyad.Text);
            komut.Parameters.AddWithValue("@p4", mskTelefon.Text);
            komut.Parameters.AddWithValue("@p5", txtMail.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            Listele();

        }
    }
}
