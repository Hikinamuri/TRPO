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
    public partial class AddProd : Form
    {
        private UserContext context = new UserContext();
        private List<Prep> preps;


        public AddProd(List<Prep> prep)
        {
            InitializeComponent();
            LoadPreps(prep);

        }



        public Product GetProduct()
        {
            string name = textBox1.Text;
            int selectedId = GetSelectedPrepId();
            return new Product(name, selectedId);

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
                int selectedId = GetSelectedPrepId();
                Product newProduct = new Product(name, selectedId);
                context.Product.Add(newProduct);
                context.SaveChanges();
                MessageBox.Show("Предмет добавлен.");
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении предмета: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                MessageBox.Show("Пожалуйста, введите правильно.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            FullNameItem selectedFullNameItem = (FullNameItem)comboBox1.SelectedItem;
            int selectedId = selectedFullNameItem.Id;
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


        public int GetSelectedPrepId()
        {
            FullNameItem selectedFullNameItem = (FullNameItem)comboBox1.SelectedItem;
            return selectedFullNameItem.Id;
        }

        private void AddProd_Load(object sender, EventArgs e)
        {

        }
    }
}
