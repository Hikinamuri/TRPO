using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace _11labbib
{
    public partial class EditProd : Form
    {
        private UserContext context = new UserContext();

        private Product prod;
        private readonly List<Prep> preps;


        public EditProd(Product prod, List<Prep> preps)
        {
            InitializeComponent();
            this.prod = prod;
            this.preps = preps;
            LoadProductData();
        }

        private void LoadProductData()
        {
            textBox1.Text = prod.Name;
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

        public Product GetEdited()
        {
            return prod;
        }

        private void button2_Click(object sender, EventArgs e)
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

                prod.Name = textBox1.Text;
                if (comboBox1.SelectedItem != null)
                {
                    FullNameItem selectedFullNameItem = (FullNameItem)comboBox1.SelectedItem;
                    prod.PRId = selectedFullNameItem.Id;
                }

                context.SaveChanges();

                MessageBox.Show("Предмет Изменен.");
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при изменении: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (textBox1.Text.Length > 20)
            {
                MessageBox.Show("Пожалуйста, введите не более 20 символов.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!IsRussian(textBox1.Text))
            {
                MessageBox.Show("Пожалуйста, введите только русские символы.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
        private bool IsRussian(string text)
        {
            foreach (char c in text)
            {
                if (!(char.IsLetter(c) && (c >= 'А' && c <= 'я' || c == 'ё' || c == 'Ё' || c == ' ')))
                {
                    return false;
                }
            }
            return true;
        }



        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                FullNameItem selectedFullNameItem = (FullNameItem)comboBox1.SelectedItem;
            }
        }

        private void EditProd_Load(object sender, EventArgs e)
        {

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
}
