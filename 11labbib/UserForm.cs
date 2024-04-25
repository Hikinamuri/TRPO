using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing; 

namespace _11labbib
{
    public partial class UserForm : Form
    {
        public UserContext context = new UserContext();
        public List<Product> products;
        public List<User> users;
        public List<Prep> prep;
        public List<Rat> rat;
        public List<Prep> Prep => prep;
        public List<User> User => users;
        public List<Product> Product => products;
        public List<Rat> Rats => rat;
        private int userId;


        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 2) 
            {
                int prepId = Convert.ToInt32(e.Value);

                Prep foundPrep = prep.FirstOrDefault(p => p.Id == prepId);

                if (foundPrep != null)
                {
                    e.Value = $"{foundPrep.Name} {foundPrep.Fam} {foundPrep.Otch}";
                    e.FormattingApplied = true;
                }
            }
        }
        private void dataGridView2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 7)
            {
                int prepId = Convert.ToInt32(e.Value);

                Prep foundPrep = prep.FirstOrDefault(p => p.Id == prepId);

                if (foundPrep != null)
                {
                    e.Value = $"{foundPrep.Name} {foundPrep.Fam} {foundPrep.Otch}";
                    e.FormattingApplied = true;
                }
            }


        }
        private void dataGridView4_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
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

        private void dataGridView4_CellFormatting1(object sender, DataGridViewCellFormattingEventArgs e)
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
        public UserForm(int userId)
        {
            InitializeComponent();
            InitializeDataGridView();
            LoadProducts();
            LoadUsers();
            LoadPreps();
            LoadRat();
            SetupColumnHeaders();
            this.userId = userId;


            dataGridView1.CellFormatting += dataGridView1_CellFormatting;
            dataGridView2.CellFormatting += dataGridView2_CellFormatting;
            dataGridView4.CellFormatting += dataGridView4_CellFormatting;
            dataGridView4.CellFormatting += dataGridView4_CellFormatting1;


        }

        private void SetupColumnHeaders()
        {
            dataGridView1.Columns[0].HeaderText = "Идентификатор";
            dataGridView1.Columns[1].HeaderText = "Название предмета";
            dataGridView1.Columns[2].HeaderText = "Преподаватель";

            dataGridView2.Columns[0].HeaderText = "Идентификатор";
            dataGridView2.Columns[1].HeaderText = "Логин";
            dataGridView2.Columns[2].HeaderText = "Пароль";
            dataGridView2.Columns[3].HeaderText = "Имя";
            dataGridView2.Columns[4].HeaderText = "Фамилия";
            dataGridView2.Columns[5].HeaderText = "Отчество";
            dataGridView2.Columns[6].HeaderText = "Курс";
            dataGridView2.Columns[7].HeaderText = "Преподаватель";

            dataGridView3.Columns[0].HeaderText = "Идентификатор";
            dataGridView3.Columns[1].HeaderText = "Логин";
            dataGridView3.Columns[2].HeaderText = "Пароль";
            dataGridView3.Columns[3].HeaderText = "Имя";
            dataGridView3.Columns[4].HeaderText = "Фамилия";
            dataGridView3.Columns[5].HeaderText = "Отчество";

            dataGridView4.Columns[0].HeaderText = "Идентификатор";
            dataGridView4.Columns[1].HeaderText = "Студент";
            dataGridView4.Columns[2].HeaderText = "Предмет";
            dataGridView4.Columns[3].HeaderText = "Оценка";
            dataGridView4.Columns[4].HeaderText = "Задание";
            dataGridView4.Columns[5].HeaderText = "Дата";

        }

        public void InitializeDataGridView()
        {

        }

        public void LoadProducts()
        {
            products = context.Product.ToList();
            dataGridView1.DataSource = products;
        }

        public void LoadUsers()
        {
            users = context.User.ToList();
            dataGridView2.DataSource = users;
        }
        public void LoadPreps()
        {
            prep = context.Prep.ToList();
            dataGridView3.DataSource = prep;
        }
        public void LoadRat()
        {
            rat = context.Rat.ToList();
            dataGridView4.DataSource = rat;
        }






        public void button1_Click(object sender, EventArgs e)
        {
            AddPrep();
        }

        public void button2_Click(object sender, EventArgs e)
        {
            EditPrep();
        }

        public void button3_Click(object sender, EventArgs e)
        {
            DeletePrep();
        }

        public void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }



        public void AddPrep()
        {
            AddProductForm addProductForm = new AddProductForm();
            DialogResult result = addProductForm.ShowDialog();

            if (result == DialogResult.OK)
            {
                LoadPreps();
            }
        }

        public void EditPrep()
        {
            if (dataGridView3.SelectedRows.Count > 0)
            {
                int selectedIndex = dataGridView3.SelectedRows[0].Index;
                EditProductForm editProductForm = new EditProductForm(prep[selectedIndex]);
                DialogResult result = editProductForm.ShowDialog();

                if (result == DialogResult.OK)
                {
                    Prep editedProduct = editProductForm.GetEditedProduct();
                    context.SaveChanges();
                    LoadPreps();
                }
            }
            else
            {
                MessageBox.Show("Выберите преподавателя для редактирования.");
            }
        }

        public void DeletePrep()
        {
            if (dataGridView3.SelectedRows.Count > 0)
            {
                int selectedIndex = dataGridView3.SelectedRows[0].Index;
                context.Prep.Remove(prep[selectedIndex]);
                context.SaveChanges();
                LoadPreps(); 
            }
            else
            {
                MessageBox.Show("Выберите преподавателя для удаления.");
            }
        }

        public void AddStud()
        {
            AddStud addStud = new AddStud(prep);
            DialogResult result = addStud.ShowDialog();

            if (result == DialogResult.OK)
            {
                LoadUsers();
            }
        }

        public void EditStud()
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                int selectedIndex = dataGridView2.SelectedRows[0].Index;
                EditStud editProductForm = new EditStud(users[selectedIndex],prep);
                DialogResult result = editProductForm.ShowDialog();

                if (result == DialogResult.OK)
                {
                    User editedStudt = editProductForm.GetEditedProduct();
                    context.SaveChanges();
                    LoadUsers();
                }
            }
            else
            {
                MessageBox.Show("Выберите преподавателя для редактирования.");
            }
        }

        public void DeleteStud()
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                int selectedIndex = dataGridView2.SelectedRows[0].Index;
                context.User.Remove(users[selectedIndex]);
                context.SaveChanges();
                LoadUsers();
            }
            else
            {
                MessageBox.Show("Выберите преподавателя для удаления.");
            }
        }

        public void AddProd()
        {
            AddProd addStud = new AddProd(prep);
            DialogResult result = addStud.ShowDialog();

            if (result == DialogResult.OK)
            {
                LoadProducts();
            }
        }

        public void EditProd()
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedIndex = dataGridView1.SelectedRows[0].Index;
                EditProd editProductForm = new EditProd(products[selectedIndex],prep);
                DialogResult result = editProductForm.ShowDialog();

                if (result == DialogResult.OK)
                {
                    Product editedProduct = editProductForm.GetEdited();
                    context.SaveChanges();
                    LoadProducts();
                }
            }
            else
            {
                MessageBox.Show("Выберите продукт для редактирования.");
            }
        }


        public void DeleteProd()
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedIndex = dataGridView1.SelectedRows[0].Index;
                context.Product.Remove(products[selectedIndex]);
                context.SaveChanges();
                LoadProducts();
            }
            else
            {
                MessageBox.Show("Выберите преподавателя для удаления.");
            }
        }

        public void AddRat()
        {
            AddRat addStud = new AddRat(users,products,prep,userId);
            DialogResult result = addStud.ShowDialog();

            if (result == DialogResult.OK)
            {
                LoadRat();
            }
        }

        public void EditRat()
        {
            if (dataGridView4.SelectedRows.Count > 0)
            {
                int selectedIndex = dataGridView4.SelectedRows[0].Index;
                EditRat editProductForm = new EditRat(rat[selectedIndex], users, products, prep, userId);
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
            if (dataGridView4.SelectedRows.Count > 0)
            {
                int selectedIndex = dataGridView4.SelectedRows[0].Index;
                context.Rat.Remove(rat[selectedIndex]);
                context.SaveChanges();
                LoadRat();
            }
            else
            {
                MessageBox.Show("Выберите преподавателя для удаления.");
            }
        }

        public void UpdateDataGridView()
        {
            dataGridView1.Refresh();
            dataGridView2.Refresh();
            dataGridView3.Refresh();
            dataGridView4.Refresh();
        }
        public void UserForm_Load(object sender, EventArgs e)
        {

        }

        public void button4_Click(object sender, EventArgs e)
        {
            UpdateDataGridView();

        }

        private void fileSystemWatcher1_Changed(object sender, System.IO.FileSystemEventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            AddStud();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            EditStud();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DeleteStud();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            AddProd();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            EditProd();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            DeleteProd();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            AddRat();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            EditRat();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            DeleteRat();
        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
