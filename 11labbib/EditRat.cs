using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static _11labbib.AddRat;

namespace _11labbib
{
    public partial class EditRat : Form
    {
        private readonly UserContext context = new UserContext();
        private List<User> users;
        private List<Product> products;
        private List<Prep> prep;
        private readonly Rat rat;
        private int userId;


        public EditRat(Rat rat, List<User> users, List<Product> products, List<Prep> prep, int userId)
        {
            InitializeComponent();
            this.users = users;
            this.rat = rat;
            this.products = products;
            this.prep = prep;
            LoadUsers(users);
            LoadProducts(products);
            LoadRatData();
            this.userId = userId;
             LoadPrep(prep,userId);
        }

        private void LoadRatData()
        {
            textBox1.Text = rat.Ratt.ToString();
            textBox2.Text = rat.Zad;
            dateTimePicker1.Value = rat.Date;
            SelectComboBoxItem(comboBox1, rat.STId);
            SelectComboBoxItem(comboBox2, rat.SBId);
            SelectComboBoxItem(comboBox3, rat.PRId);
        }

        private void SelectComboBoxItem(ComboBox comboBox, int id)
        {
            foreach (var item in comboBox.Items)
            {
                if (item is FullNameItem fullNameItem && fullNameItem.Id == id)
                {
                    comboBox.SelectedItem = item;
                    break;
                }
            }
        }

        public Rat GetEdited()
        {
            int ratt = int.Parse(textBox1.Text);
            string zad = textBox2.Text;
            DateTime date = dateTimePicker1.Value.Date;
            int selectedId1 = GetSelectedId1();
            int selectedId = GetSelectedId();
            int selectedId2 = GetSelectedId2();

            return new Rat(selectedId, selectedId1, ratt, zad, date, selectedId2);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateInput())
                {
                    return;
                }

                int ratt = int.Parse(textBox1.Text);
                string zad = textBox2.Text;
                DateTime date = dateTimePicker1.Value.Date;
                int selectedId1 = GetSelectedId1();
                int selectedId = GetSelectedId();
                int selectedId2 = GetSelectedId2();

                rat.Ratt = ratt;
                rat.Zad = zad;
                rat.Date = date;
                rat.STId = selectedId1;
                rat.SBId = selectedId;
                rat.PRId = selectedId2;

                context.SaveChanges();

                MessageBox.Show("Изменения успешно сохранены.");
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении изменений: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (textBox1.Text.Length != 1 || !IsNumeric(textBox1.Text) || int.Parse(textBox1.Text) < 0 || int.Parse(textBox1.Text) > 5)
            {
                MessageBox.Show("Пожалуйста, введите корректное значение.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (textBox2.Text.Length > 20)
            {
                MessageBox.Show("Пожалуйста, введите не более 20 символов для задания.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!IsNumeric(textBox1.Text))
            {
                MessageBox.Show("Пожалуйста, введите только цифры для рейтинга.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
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

        public class FullNameIte
        {
            public string FullNam { get; set; }
            public int I { get; set; }

            public FullNameIte(string fullName, int id)
            {
                FullNam = fullName;
                I = id;
            }

            public override string ToString()
            {
                return FullNam;
            }
        }

        public void LoadUsers(List<User> users)
        {
            this.users = users;

            List<FullNameItem> fullNameItems = new List<FullNameItem>();

            foreach (var user in users)
            {
                string fullName = $"{user.Name} {user.Fam} {user.Otch}";
                fullNameItems.Add(new FullNameItem(fullName, user.Id));
            }

            comboBox1.DataSource = fullNameItems;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            FullNameItem selectedFullNameItem = (FullNameItem)comboBox1.SelectedItem;
            int selectedId = selectedFullNameItem.Id;
        }

        public int GetSelectedId1()
        {
            FullNameItem selectedFullNameItem = (FullNameItem)comboBox1.SelectedItem;
            return selectedFullNameItem.Id;
        }

        public void LoadProducts(List<Product> products)
        {
            this.products = products;

            List<FullNameItem> fullNameItems = new List<FullNameItem>();

            foreach (var product in products)
            {
                string fullName = $"{product.Name}";
                fullNameItems.Add(new FullNameItem(fullName, product.Id));
            }

            comboBox2.DataSource = fullNameItems;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            FullNameItem selectedFullNameItem = (FullNameItem)comboBox2.SelectedItem;
            int selectedId1 = selectedFullNameItem.Id;
        }

        public int GetSelectedId()
        {
            FullNameItem selectedFullNameItem = (FullNameItem)comboBox2.SelectedItem;
            return selectedFullNameItem.Id;
        }

        public void LoadPrep(List<Prep> rep, int userId)
        {
            this.prep = rep;

            List<FullNameIte> fullNameItems = new List<FullNameIte>();

            foreach (var prepItem in rep)
            {
                if (prepItem.Id == userId)
                {
                    string fullName = $"{prepItem.Name} {prepItem.Fam} {prepItem.Otch}";
                    fullNameItems.Add(new FullNameIte(fullName, prepItem.Id));
                    break;

                }
                else
                {
                    string fullName = $"{prepItem.Name} {prepItem.Fam} {prepItem.Otch}";
                    fullNameItems.Add(new FullNameIte(fullName, prepItem.Id));
                }
            }

            comboBox3.DataSource = fullNameItems;

            if (fullNameItems.Count > 0)
            {
                comboBox3.SelectedIndex = 0;
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            FullNameIte selectedFullNameItem = (FullNameIte)comboBox3.SelectedItem;
            int selectedId2 = selectedFullNameItem.I;
        }

        public int GetSelectedId2()
        {
            FullNameIte selectedFullNameItem = (FullNameIte)comboBox3.SelectedItem;
            return selectedFullNameItem.I;
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void EditRat_Load(object sender, EventArgs e)
        {

        }
    }
}
