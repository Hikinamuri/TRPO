using _11labbib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        private UserForm userForm;
        private UserContext context;



        [TestInitialize]
        public void TestInitialize()
        {

            userForm = new UserForm();
            context = new UserContext();
            userForm.Show();

        }

        [TestMethod]
        public void LoadProduct()
        {
            userForm.LoadProducts();

            Product[] expectedProducts = context.Products.ToArray();

            Product[] actualProducts = userForm.Products.ToArray();

            Assert.AreEqual(expectedProducts.Length, actualProducts.Length, "Количество продуктов не совпадает");

            for (int i = 0; i < expectedProducts.Length; i++)
            {
                Assert.AreEqual(expectedProducts[i].Name, actualProducts[i].Name, $"Имя продукта {i} не совпадает");
                Assert.AreEqual(expectedProducts[i].Description, actualProducts[i].Description, $"Описание продукта {i} не совпадает");
                Assert.AreEqual(expectedProducts[i].Price, actualProducts[i].Price, $"Цена продукта {i} не совпадает");
                Assert.AreEqual(expectedProducts[i].ImagePath, actualProducts[i].ImagePath, $"Путь к изображению продукта {i} не совпадает");
            }
        }


        [TestMethod]
        public void AddProduct()
        {
            int initialRowCount = userForm.dataGridView1.Rows.Count;

            userForm.AddProduct();

            Assert.AreEqual(initialRowCount + 1, userForm.dataGridView1.Rows.Count);
        }

        [TestMethod]
        public async Task EditProduct()
        {
            if (userForm.dataGridView1.Rows.Count > 0)
            {
                int selectedIndex = 0;

                DataGridViewRow selectedRow = userForm.dataGridView1.Rows[selectedIndex];

                int productId = ((Product)selectedRow.DataBoundItem).Id;

                string expectedName = "1";
                string expectedDescription = "1";
                decimal expectedPrice = 1.0m;
                string expectedImagePath = "1";

                userForm.dataGridView1.Rows[selectedIndex].Selected = true;

 
                userForm.EditProduct();

                await Task.Delay(1000);

                Product editedProduct = context.Products.Find(productId);

                Assert.AreEqual(expectedName, editedProduct.Name, "Name не соответствует");
                Assert.AreEqual(expectedDescription, editedProduct.Description, "Description не соответствует");
                Assert.AreEqual(expectedPrice, editedProduct.Price, "Price не соответствует");
                Assert.AreEqual(expectedImagePath, editedProduct.ImagePath, "ImagePath не соответствует");
            }
        }


        [TestMethod]
        public void DeleteProduct()
        {
            int initialRowCount = userForm.dataGridView1.Rows.Count;

            if (initialRowCount >= 1)
            {
                for (int i = 0; i < 1; i++)
                {
                    userForm.dataGridView1.Rows[i].Selected = true;
                    userForm.DeleteProduct();
                }


                Assert.AreEqual(initialRowCount - 1, userForm.dataGridView1.Rows.Count);
            }
        }



        [TestCleanup]
        public void TestCleanup()
        {
            userForm.Close();
            context.Dispose();
        }
    }
}
