using System;
using System.Linq;
using System.Windows.Forms;

namespace _11labbib
{
    public partial class AddProductForm : Form
    {
        private UserContext context = new UserContext();

        public AddProductForm()
        {
            InitializeComponent();

            textBox1.KeyPress += TextBox_KeyPress;
            textBox2.KeyPress += TextBox_KeyPress;
            textBox3.KeyPress += TextBox_KeyPress;
            textBox5.KeyPress += TextBox_KeyPress;
            textBox6.KeyPress += TextBox_KeyPress;
        }

        public Prep GetProduct()
        {
            string name = textBox1.Text;
            string fam = textBox2.Text;
            string otch = textBox3.Text;
            string login = textBox5.Text;
            string password = textBox6.Text;

            return new Prep(login, password, name, fam, otch);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateInput())
                {
                    return;
                }

                string name = textBox1.Text;
                string fam = textBox2.Text;
                string otch = textBox3.Text;
                string login = textBox5.Text;
                string password = textBox6.Text;

                if (context.Prep.Any(p => p.Login == login))
                {
                    MessageBox.Show("Преподаватель с таким логином уже существует.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Prep newPrep = new Prep(login, password, name, fam, otch);
                context.Prep.Add(newPrep);

                context.SaveChanges();

                MessageBox.Show("Преподаватель успешно Добавлен.");
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении преподавателя: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox5.Text) ||
                string.IsNullOrWhiteSpace(textBox6.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (textBox1.Text.Length > 20 ||
                textBox2.Text.Length > 20 ||
                textBox3.Text.Length > 20 ||
                textBox5.Text.Length > 20 ||
                textBox6.Text.Length > 20)
            {
                MessageBox.Show("Пожалуйста, введите не более 20 символов в каждом поле.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) || !IsRussianLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private bool IsRussianLetter(char symbol)
        {
            return symbol >= 'А' && symbol <= 'я';
        }

        private void AddProductForm_Load(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
