using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace _11labbib
{
    public partial class Form2 : Form
    {

        public UserContext context = new UserContext();
        public List<Product> products;
        public List<User> user;
        public List<Prep> prep;
        public List<Rat> rat;
        public List<Prep> Prep => prep;
        public List<User> User => user;
        public List<Product> Product => products;
        public List<Rat> Rats => rat;

        public Form2()
        {
            InitializeComponent();
            LoadProducts();
            LoadUsers();
            LoadPreps();
            LoadRat();
        }

        public void LoadProducts()
        {
            products = context.Product.ToList();
        }

        public void LoadUsers()
        {
            user = context.User.ToList();
        }
        public void LoadPreps()
        {
            prep = context.Prep.ToList();
        }
        public void LoadRat()
        {
            rat = context.Rat.ToList();
        }
        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Пожалуйста, введите логин и пароль.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (textBox1.Text.Length > 20 || textBox2.Text.Length > 20)
            {
                MessageBox.Show("Логин и пароль не должны превышать 20 символов.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
            {
                return;
            }

            using (UserContext db = new UserContext())
            {
                int userId = -1;

                if (textBox1.Text == "admin" && textBox2.Text == "admin")
                {
                    MessageBox.Show("Вход успешен!");
                    UserForm userForm = new UserForm(userId);
                    userForm.Show();
                    return;
                }
                else
                {
                    foreach (User user in db.User)
                    {
                        if (textBox1.Text == user.Login && textBox2.Text == user.Password)
                        {
                            MessageBox.Show("Вход успешен!");
                            userId = user.Id;
                            Form3 form3 = new Form3(userId);
                            form3.Show();
                            return;
                        }
                    }

                    foreach (Prep rep in db.Prep)
                    {
                        if (textBox1.Text == rep.Login && textBox2.Text == rep.Password)
                        {
                            MessageBox.Show("Вход успешен!");
                            userId = rep.Id;
                            Form4 form4 = new Form4(userId, prep);
                            form4.Show();
                            return;
                        }
                    }
                }

                MessageBox.Show("Логин или пароль указан неверно!");
            }
        }






        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
