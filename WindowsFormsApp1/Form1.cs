using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        bool passc = true;
        int captcha = 0;
        public Form1()
        {
            InitializeComponent();
            Random rnd = new Random();
            captcha = rnd.Next(0, 9999);
            label4.Text = $"Captcha: {captcha}";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (passc)
            {
                textBox2.PasswordChar = '\0';
                passc = false;
                button2.Text = "Hide";
            }

            else
            {
                textBox2.PasswordChar = '\u25CF';
                passc = true;
                button2.Text = "Show";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (captcha != Convert.ToInt32(textBox3.Text))
                {
                    MessageBox.Show("Invalid captcha!", "Error");
                    Random rnd = new Random();
                    captcha = rnd.Next(0, 9999);
                    label4.Text = $"Captcha: {captcha}";
                    return;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid captcha!", "Error");
                Random rnd = new Random();
                captcha = rnd.Next(0, 9999);
                label4.Text = $"Captcha: {captcha}";
                return;
            }

            string loginuser = textBox1.Text;
            string passuser = textBox2.Text;
            DB db = new DB();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `login` = @uL AND `pass` = @uP", db.getConnection());
            command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = loginuser;
            command.Parameters.Add("@uP", MySqlDbType.VarChar).Value = passuser;

            adapter.SelectCommand = command;

            try
            {
                adapter.Fill(table);
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to connect. Check your internet connection or try later.");
            }

            if (table.Rows.Count > 0)
            {
                string name = "";
                string description = "";
                string age = "";
                foreach (DataRow row in table.Rows)
                {
                    name = row["name"].ToString();
                    description = row["description"].ToString();
                    age = row["age"].ToString();
                }
                label3.Text = $"Name: {name} \nAge: {age} \nDesc: {description}";
            }
            else
            {
                MessageBox.Show("Access denied: Invalid login or password!", "Error");
                Random rnd = new Random();
                captcha = rnd.Next(0, 9999);
                label4.Text = $"Captcha: {captcha}";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            f.Show();
        }
    }
}
