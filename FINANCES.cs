using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace POSSYSTEMFINAL
{
    public partial class FINANCES : Form
    {
        private string connectionString;

        public FINANCES(string connectionString)
        {
            this.connectionString = connectionString;
            InitializeComponent();
        }

        private void FINANCES_Load(object sender, EventArgs e)
        {
            // Load finance data into DataGridView
            LoadFinanceData();
        }

        private void LoadFinanceData()
        {
            string query = "SELECT * FROM Finance";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataGridView1.DataSource = dataTable;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Handle cell content click event if needed
        }
    }
}
