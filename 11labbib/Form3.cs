using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

namespace _11labbib
{
    public partial class Form3 : Form
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
        public Form3(int userId)
        {
            InitializeComponent();
            this.userId = userId;
            LoadRat();
            LoadProducts();
            LoadUsers();
            SetupColumnHeaders();

            dataGridView1.CellFormatting += dataGridView1_CellFormatting;
            dataGridView1.CellFormatting += dataGridView1_CellFormatting1;
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

        public void LoadRat()
        {
            rat = context.Rat.Where(r => r.STId == userId).ToList();
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void UpdateDataGridView()
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

        private void button1_Click(object sender, EventArgs e)
        {
            UpdateDataGridView();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
