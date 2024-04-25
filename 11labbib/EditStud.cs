using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _11labbib
{
    public partial class EditStud : Form
    {
        private readonly UserContext context = new UserContext();
        private readonly User user;
        private readonly List<Prep> preps;

        public EditStud(User user, List<Prep> preps)
        {
            InitializeComponent();
            this.user = user;
            this.preps = preps;
            LoadUserData();
        }

        private void LoadUserData()
        {
            textBox1.Text = user.Name;
            textBox2.Text = user.Fam;
            textBox3.Text = user.Otch;
            textBox6.Text = user.Login;
            textBox4.Text = user.Password;
            textBox5.Text = user.Kurs.ToString();
            comboBox1.DataSource = GetPrepFullNameItems();
        }

        private List<FullNameItem> GetPrepFullNameItems()
        {
            List<FullNameItem> fullNameItems = new List<FullNameItem>();
            preps.ForEach(prep =>
            {
                string fullName = $"{prep.Name} {prep.Fam} {prep.Otch}";
                fullNameItems.Add(new FullNameItem(fullName, prep.Id));
            });
            return fullNameItems;
        }

        public User GetEditedProduct()
        {
            return user;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateInput())
                {
                    return;
                }
                user.Name =  textBox1.Text;
                user.Fam = textBox2.Text;
                user.Otch = textBox3.Text;
                user.Login = textBox6.Text;
                user.Password = textBox4.Text;
                user.Kurs = int.Parse(textBox5.Text);
                if (comboBox1.SelectedItem != null)
                {
                    FullNameItem selectedFullNameItem = (FullNameItem)comboBox1.SelectedItem;
                    user.PRId = selectedFullNameItem.Id;
                }
                context.SaveChanges();

                MessageBox.Show("Студент успешно изменен в базе данных.");
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при изменении студента: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            if (!IsRussian(textBox1.Text) || !IsRussian(textBox2.Text) || !IsRussian(textBox3.Text))
            {
                MessageBox.Show("Пожалуйста, введите только русские символы для имени, фамилии и отчества.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (textBox1.Text.Length > 20 || textBox2.Text.Length > 20 || textBox3.Text.Length > 20 || textBox6.Text.Length > 20 || textBox4.Text.Length > 20)
            {
                MessageBox.Show("Пожалуйста, введите не более 20 символов для имени, фамилии, отчества, логина и пароля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!IsNumeric(textBox5.Text) || textBox5.Text.Length != 1 || int.Parse(textBox5.Text) < 1 || int.Parse(textBox5.Text) > 4)
            {
                MessageBox.Show("Пожалуйста, введите только одну цифру от 1 до 4 для курса.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private bool IsRussian(string text)
        {
            foreach (char c in text)
            {
                if (!(c >= 'А' && c <= 'я'))
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


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                FullNameItem selectedFullNameItem = (FullNameItem)comboBox1.SelectedItem;
            }
        }

        private void EditStud_Load(object sender, EventArgs e)
        {

        }
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
}
