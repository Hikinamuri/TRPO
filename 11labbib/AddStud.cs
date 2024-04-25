using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _11labbib
{
    public partial class AddStud : Form
    {
        private UserContext context = new UserContext();
        private List<Prep> preps;

        public AddStud(List<Prep> prep)
        {
            InitializeComponent();
            LoadPreps(prep);
            textBox1.KeyPress += textBox1_KeyPress;
            textBox2.KeyPress += textBox2_KeyPress;
            textBox3.KeyPress += textBox3_KeyPress;
            textBox5.KeyPress += textBox5_KeyPress;
            textBox6.MaxLength = 20; // Ограничение на количество символов для логина
            textBox4.MaxLength = 20; // Ограничение на количество символов для пароля
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        public User GetProduct()
        {
            string name = textBox1.Text;
            string fam = textBox2.Text;
            string otch = textBox3.Text;
            int kurs = int.Parse(textBox5.Text);
            string login = textBox6.Text;
            string password = textBox4.Text;
            int selectedId = GetSelectedPrepId();

            return new User(login, password, name, fam, otch, kurs, selectedId);
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
                int kurs = int.Parse(textBox5.Text);
                string login = textBox6.Text;
                string password = textBox4.Text;
                int selectedId = GetSelectedPrepId();

                if (context.User.Any(u => u.Login == login))
                {
                    MessageBox.Show("Студент с таким логином уже существует.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Если логин уникален, создаем нового пользователя и добавляем его в базу данных
                User newPrep = new User(login, password, name, fam, otch, kurs, selectedId);
                context.User.Add(newPrep);

                context.SaveChanges();

                MessageBox.Show("Студент успешно добавлен.");
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
                string.IsNullOrWhiteSpace(textBox3.Text) ||
                string.IsNullOrWhiteSpace(textBox4.Text) ||
                string.IsNullOrWhiteSpace(textBox5.Text) ||
                string.IsNullOrWhiteSpace(textBox6.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (textBox1.Text.Length > 20 ||
                textBox2.Text.Length > 20 ||
                textBox3.Text.Length > 20 ||
                textBox4.Text.Length > 20 ||
                textBox5.Text.Length != 1 || // Проверка на одну цифру
                int.Parse(textBox5.Text) < 1 || // Проверка на значение не меньше 1
                int.Parse(textBox5.Text) > 4) // Проверка на значение не больше 4
            {
                MessageBox.Show("Пожалуйста, введите правильные значения.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        public class FullNameItem
        {
            public string FullName { get; set; }
            public int Id { get; set; }

            public FullNameItem(string fullName, int id)
            {
                FullName = fullName;
                Id = id;
            }

            public override string ToString()
            {
                return FullName;
            }
        }

        public void LoadPreps(List<Prep> prep)
        {
            preps = prep;

            List<FullNameItem> fullNameItems = new List<FullNameItem>();

            foreach (var prepItem in preps)
            {
                string fullName = $"{prepItem.Name} {prepItem.Fam} {prepItem.Otch}";
                fullNameItems.Add(new FullNameItem(fullName, prepItem.Id));
            }

            comboBox1.DataSource = fullNameItems;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            FullNameItem selectedFullNameItem = (FullNameItem)comboBox1.SelectedItem;
            int selectedId = selectedFullNameItem.Id;
        }

        public int GetSelectedPrepId()
        {
            FullNameItem selectedFullNameItem = (FullNameItem)comboBox1.SelectedItem;
            return selectedFullNameItem.Id;
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
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

        private void AddStud_Load(object sender, EventArgs e)
        {

        }
    }
}
