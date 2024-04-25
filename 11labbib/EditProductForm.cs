using System;
using System.Windows.Forms;

namespace _11labbib
{
    public partial class EditProductForm : Form
    {
        private UserContext context = new UserContext();
        private Prep prep;

        public EditProductForm(Prep prep)
        {
            InitializeComponent();
            this.prep = prep;
            LoadProductData();
        }

        private void LoadProductData()
        {
            textBox1.Text = prep.Name;
            textBox2.Text = prep.Fam;
            textBox3.Text = prep.Otch;
            textBox4.Text = prep.Login;
            textBox5.Text = prep.Password;
        }

        public Prep GetEditedProduct()
        {
            prep.Name = textBox1.Text;
            prep.Fam = textBox2.Text;
            prep.Otch = textBox3.Text;
            prep.Login = textBox4.Text;
            prep.Password = textBox5.Text;
            return prep;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateInput())
                {
                    return;
                }

                prep.Name = textBox1.Text;
                prep.Fam = textBox2.Text;
                prep.Otch = textBox3.Text;
                prep.Login = textBox4.Text;
                prep.Password = textBox5.Text;

                context.SaveChanges();

                MessageBox.Show("Преподаватель успешно изменен в базе данных.");
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при изменении: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text) ||
                string.IsNullOrWhiteSpace(textBox4.Text) ||
                string.IsNullOrWhiteSpace(textBox5.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (textBox1.Text.Length > 20 ||
                textBox2.Text.Length > 20 ||
                textBox3.Text.Length > 20 ||
                textBox4.Text.Length > 20 ||
                textBox5.Text.Length > 20)
            {
                MessageBox.Show("Пожалуйста, введите не более 20 символов в каждом поле для имени, фамилии и отчества.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!IsRussian(textBox1.Text) || !IsRussian(textBox2.Text) || !IsRussian(textBox3.Text))
            {
                MessageBox.Show("Пожалуйста, введите только русские символы для имени, фамилии и отчества.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private bool IsRussian(string text)
        {
            foreach (char c in text)
            {
                if (!(c >= 'А' && c <= 'я' || c == 'ё' || c == 'Ё'))
                {
                    return false;
                }
            }
            return true;
        }

        private bool IsNumeric(string text)
        {
            foreach (char c in text)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void EditProductForm_Load(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
