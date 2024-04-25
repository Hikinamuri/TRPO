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
    public partial class Form4 : Form
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

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                int prepId = Convert.ToInt32(e.Value);

                User foundPrep = users.FirstOrDefault(p => p.Id == prepId);

                if (foundPrep != null)
                {
                    e.Value = $"{foundPrep.Name} {foundPrep.Fam} {foundPrep.Otch}";
                    e.FormattingApplied = true;
                }
            }
        }

        private void dataGridView1_CellFormatting1(object sender, DataGridViewCellFormattingEventArgs e)
        {

            if (e.ColumnIndex == 2)
            {
                int prepId = Convert.ToInt32(e.Value);

                Product foundPrep = products.FirstOrDefault(p => p.Id == prepId);

                if (foundPrep != null)
                {
                    e.Value = $"{foundPrep.Name}";
                    e.FormattingApplied = true;
                }
            }
        }
        public Form4(int userId, List<Prep> prep)
        {
            InitializeComponent();
            this.userId = userId;
            this.prep = prep;
            LoadRat();
            LoadProducts();
            LoadUsers();
            SetupColumnHeaders();

            dataGridView1.CellFormatting += dataGridView1_CellFormatting;
            dataGridView1.CellFormatting += dataGridView1_CellFormatting1;
        }

        public void InitializeDataGridView()
        {

        }
        private void SetupColumnHeaders()
        {

            dataGridView1.Columns[1].HeaderText = "Студент";
            dataGridView1.Columns[2].HeaderText = "Предмет";
            dataGridView1.Columns[3].HeaderText = "Оценка";
            dataGridView1.Columns[4].HeaderText = "Задание";
            dataGridView1.Columns[5].HeaderText = "Дата";
            dataGridView1.Columns[0].Visible = false;

        }
        public void LoadRat()
        {
            rat = context.Rat.Where(r => r.PRId == userId).ToList();
            dataGridView1.DataSource = rat;
        }

        public void LoadProducts()
        {
            products = context.Product.ToList();
        }

        public void LoadUsers()
        {
            users = context.User.ToList();
        }

        public void AddRat()
        {
            AddRat addStud = new AddRat(users, products, prep, userId);
            DialogResult result = addStud.ShowDialog();

            if (result == DialogResult.OK)
            {
                LoadRat();
            }
        }

        public void EditRat()
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedIndex = dataGridView1.SelectedRows[0].Index;
                EditRat editProductForm = new EditRat(rat[selectedIndex], users, products,prep, userId);
                DialogResult result = editProductForm.ShowDialog();

                if (result == DialogResult.OK)
                {
                    Rat editedProduct = editProductForm.GetEdited();
                    context.SaveChanges();
                    LoadRat();
                }
            }
            else
            {
                MessageBox.Show("Выберите продукт для редактирования.");
            }
        }



        public void DeleteRat()
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedIndex = dataGridView1.SelectedRows[0].Index;
                context.Rat.Remove(rat[selectedIndex]);
                context.SaveChanges();
                LoadRat();
            }
            else
            {
                MessageBox.Show("Выберите преподавателя для удаления.");
            }
        }


        private void button4_Click(object sender, EventArgs e)
        {
            AddRat();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EditRat();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DeleteRat();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }
    }
}
