using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        int captcha = 0;
        bool passc = true;
        public Form2()
        {
            InitializeComponent();
            Random rnd = new Random();
            captcha = rnd.Next(0, 9999);
            label6.Text = $"Captcha: {captcha}";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (captcha != Convert.ToInt32(textBox6.Text))
                {
                    MessageBox.Show("Invalid captcha!", "Error");
                    Random rnd = new Random();
                    captcha = rnd.Next(0, 9999);
                    label6.Text = $"Captcha: {captcha}";
                    return;
                }
            }
            catch (Exception) 
            {
                MessageBox.Show("Invalid captcha!", "Error");
                Random rnd = new Random();
                captcha = rnd.Next(0, 9999);
                label6.Text = $"Captcha: {captcha}";
                return;
            }

            string loginuser = textBox1.Text;
            string passuser = textBox2.Text;
            int age = 0;
            try
            {
                age = Convert.ToInt32(textBox3.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Age must be a number!");
            }
            string description = textBox5.Text;
            string name = textBox4.Text;

            DB db = new DB();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `login` = @uL AND `pass` = @uP", db.getConnection());
            command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = loginuser;
            command.Parameters.Add("@uP", MySqlDbType.VarChar).Value = passuser;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("A user with this login already exists.", "Error");
                Random rnd = new Random();
                captcha = rnd.Next(0, 9999);
                label6.Text = $"Captcha: {captcha}";
            }
            else
            {
                MySqlCommand com = new MySqlCommand("INSERT INTO `users` (`login`, `pass`, `age`, `description`, `name`) VALUES (@login, @pass, @age, @description, @name)", db.getConnection());

                com.Parameters.Add("@login", MySqlDbType.VarChar).Value = loginuser;
                com.Parameters.Add("@pass", MySqlDbType.VarChar).Value = passuser;
                com.Parameters.Add("@age", MySqlDbType.Int32).Value = age;
                com.Parameters.Add("@description", MySqlDbType.VarChar).Value = description;
                com.Parameters.Add("@name", MySqlDbType.VarChar).Value = name;

                db.openConnection();

                if (com.ExecuteNonQuery() == 1)
                    MessageBox.Show("Account has been created");
                else
                    MessageBox.Show("Account hasn't been created");

                db.closeConnection();
            }
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
    }
}
