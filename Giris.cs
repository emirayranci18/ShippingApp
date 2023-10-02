using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace yazlabdeneme
{
    public partial class Giris : Form
    {
        MySqlConnection con;
        MySqlCommand cmd;
        MySqlDataReader dr;

        MySqlConnection con2;
        MySqlCommand cmd2;
        MySqlDataReader dr2;



        int count = 0;

        public Giris()
        {
            InitializeComponent();
            con = new MySqlConnection("Server=localhost;Database=kullanici;user=root;Pwd=1234;SslMode=none");
            con2 = new MySqlConnection("Server=localhost;Database=kullanici;user=root;Pwd=1234;SslMode=none");
            labelGiris.BackColor = System.Drawing.Color.Transparent;
            label1.BackColor = System.Drawing.Color.Transparent;
            label2.BackColor = System.Drawing.Color.Transparent;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Giris_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            cmd = new MySqlCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM users where nameuser = '" + boxName.Text + "' AND passuser = '" + boxPass.Text + "'";
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                DateTime now = DateTime.Now;
                String date = now.ToString();

                cmd2 = new MySqlCommand();
                con2.Open();
                cmd2.Connection = con2;
                cmd2.CommandText = "UPDATE users SET dateuser= '" + date + "' WHERE nameuser = '" + boxName.Text + "'";
                cmd2.ExecuteNonQuery();
                MessageBox.Show("Giriş Başarılı. Biraz bekleyiniz. Harita Yükleniyor...");
                Form2 frm2 = new Form2(this,boxName.Text);
                frm2.Show();
                this.Hide();

            }
            else
            {
                
                count++;
                if (count == 3)
                {
                    System.Windows.Forms.Application.Exit();
                }
                else
                {
                    MessageBox.Show("Hatalı Kullanıcı Adı veya Şifre Girdiniz. (Kalan Giriş Hakkı: " + (3 - count)+")");
                }
            }
            con.Close();
        }
    }
}
