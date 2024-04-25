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
    public partial class AddRat : Form
    {
        private int userId;

        public UserContext context = new UserContext();
        public List<Product> products;
        public List<User> users;
        public List<Prep> prep;
        public List<Rat> rat;
        public List<Prep> Prep => prep;
        public List<User> User => users;
        public List<Product> Product => products;
        public List<Rat> Rats => rat;



        public AddRat(List<User> users, List<Product> products, List<Prep> prep, int userId)

        {
            InitializeComponent();
            this.userId = userId;
            LoadUsers(users);
            LoadUsers1(products);
            LoadUsers2(prep, userId);
            textBox1.KeyPress += TextBox1_KeyPress;
            textBox2.KeyPress += TextBox2_KeyPress;

        }

        public Rat GetProduct()
        {
            int name;
            if (!int.TryParse(textBox1.Text, out name) || name < 0 || name > 5)
            {
                MessageBox.Show("Пожалуйста, введите число от 0 до 5.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            string fam = textBox2.Text;
            DateTime date = DateTime.Now;
            int selectedId1 = GetSelectedPrepId1();
            int selectedId = GetSelectedPrepId();
            int selectedId2 = GetSelectedPrepId2();

            return new Rat(selectedId, selectedId1, name, fam, date, selectedId2);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateInput())
                {
                    return;
                }

                int name;
                if (!int.TryParse(textBox1.Text, out name) || name < 0 || name > 5)
                {
                    MessageBox.Show("Пожалуйста, введите число от 0 до 5.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string fam = textBox2.Text;
                DateTime date = dateTimePicker1.Value.Date;
                int selectedId1 = GetSelectedPrepId1();
                int selectedId = GetSelectedPrepId();
                int selectedId2 = GetSelectedPrepId2();

                Rat newPrep = new Rat(selectedId, selectedId1, name, fam, date, selectedId2);
                context.Rat.Add(newPrep);

                context.SaveChanges();

                MessageBox.Show("Cтудент успешно добавлен.");
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении студента: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (textBox2.Text.Length > 20 || textBox1.Text.Length != 1)
            {
                MessageBox.Show("Пожалуйста, введите правильное значение.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != ' ' && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void LoadUsers1(List<Product> prep)
        {
            products = prep;

            List<FullNameItemm> fullNameItems = new List<FullNameItemm>();

            foreach (var prepItem in prep)
            {
                string fullName = $"{prepItem.Name}";
                fullNameItems.Add(new FullNameItemm(fullName, prepItem.Id));
            }

            comboBox2.DataSource = fullNameItems;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            FullNameItemm selectedFullNameItem = (FullNameItemm)comboBox2.SelectedItem;
            int selectedId1 = selectedFullNameItem.Idd;
        }

        private int GetSelectedPrepId1()
        {
            FullNameItemm selectedFullNameItem = (FullNameItemm)comboBox2.SelectedItem;
            return selectedFullNameItem.Idd;
        }

        private void LoadUsers(List<User> prep)
        {
            users = prep;

            List<FullNameItem> fullNameItems = new List<FullNameItem>();

            foreach (var prepItem in users)
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

        private int GetSelectedPrepId()
        {
            FullNameItem selectedFullNameItem = (FullNameItem)comboBox1.SelectedItem;
            return selectedFullNameItem.Id;
        }

        public void LoadUsers2(List<Prep> rep, int userId)
        {
            prep = rep;

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

        private int GetSelectedPrepId2()
        {
            FullNameIte selectedFullNameItem = (FullNameIte)comboBox3.SelectedItem;
            return selectedFullNameItem.I;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            FullNameIte selectedFullNameItem = (FullNameIte)comboBox3.SelectedItem;
            int selectedId2 = selectedFullNameItem.I;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void AddRat_Load(object sender, EventArgs e)
        {

        }

        public class FullNameItemm
        {
            public string FullNamee { get; set; }
            public int Idd { get; set; }

            public FullNameItemm(string fullName, int id)
            {
                FullNamee = fullName;
                Idd = id;
            }

            public override string ToString()
            {
                return FullNamee;
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

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
