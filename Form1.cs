using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RecapProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ListProduct();
            ListCategories();
        }
        private void ListProduct()
        {
            using (NorthWindContext context = new NorthWindContext())
            {
                dgwProducts.DataSource = context.Products.ToList();
            }
        }
        private void ListProductsByCategory(int categoryId)
        {
            using (NorthWindContext context = new NorthWindContext())
            {
                dgwProducts.DataSource = context.Products.Where(p=>p.CategoryId==categoryId).ToList();
            }
        }
        private void ListProductsByProductName(string key)
        {
            using (NorthWindContext context = new NorthWindContext())
            {
                dgwProducts.DataSource = context.Products.Where(p => p.ProductName.Contains(tbxSearch.Text)).ToList();
            }
        }
        private void ListCategories()
        {
            using (NorthWindContext context = new NorthWindContext())
            {
                cbxCategory.DataSource = context.Categories.ToList();
                cbxCategory.DisplayMember = "CategoryName";
                cbxCategory.ValueMember = "CategoryId";
            }
        }

        private void cbxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ListProductsByCategory(Convert.ToInt32(cbxCategory.SelectedValue));
            }
            catch
            {

            }
        }

        private void tbxSearch_TextChanged(object sender, EventArgs e)
        {
            string key = tbxSearch.Text;
            if (string.IsNullOrEmpty(key))
            {
                ListProduct();
            }
            else
            {
                ListProductsByProductName(key);
            }
            
        }
    }
}
