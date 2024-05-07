using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using POSSYSTEMFINAL;

namespace POSSYSTEMFINAL
{
    public partial class STAFF : Form
    {
        private string connectionString = (@"Data Source=34.105.253.161;Initial Catalog=Cardearlership;Persist Security Info=True;User ID=sqlserver;Password=Faris200510;Encrypt=True;");
        private string offlineConnectionString = (@"Data Source=Data Source=(localdb)\Local;Initial Catalog=master;Integrated Security=True;");

        public STAFF()
        {
            InitializeComponent();
        }

        private void STAFF_Load(object sender, EventArgs e)
        {

        }

        private void Adminbtn_Click(object sender, EventArgs e)
        {
            ADMIN aDMIN = new ADMIN(connectionString);
            DialogResult result = aDMIN.ShowDialog();
            if (result == DialogResult.OK)
            {
                aDMIN.Close();
            }
        }

        private void Employeebtn_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    // If the online connection succeeds, proceed with the online database
                    ProductCatalog productCatalog = new ProductCatalog(offlineConnectionString);
                    DialogResult result = productCatalog.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        productCatalog.Close();
                    }
                }
            }
            catch (Exception)
            {
                ProductCatalog productCatalog = new ProductCatalog(offlineConnectionString);
                DialogResult result = productCatalog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    productCatalog.Close();
                }
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}