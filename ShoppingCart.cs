using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace POSSYSTEMFINAL
{
    public partial class ShoppingCart : Form
    {
        public List<Vehicle> cartItems = new List<Vehicle>();
        private string connectionString;

        public ShoppingCart(string connectionString)
        {
            InitializeComponent();
            this.connectionString = connectionString;
            Button btnConfirm = new Button();
            btnConfirm.Text = "Confirm";
            btnConfirm.Click += BtnConfirm_Click;
            flowLayoutPanel.Controls.Add(btnConfirm);
        }

        // Method to add an item to the cart
        public void AddToCart(Vehicle vehicle)
        {
            cartItems.Add(vehicle);
            DisplayCartItems();
        }

        // Method to display cart items
        public void DisplayCartItems()
        {
            // Clear existing items
            flowLayoutPanel.Controls.Clear();

            // Display each item in the cart
            foreach (var vehicle in cartItems)
            {
                PictureBox pictureBox = new PictureBox();
                pictureBox.Image = Image.FromFile(vehicle.ImagePath);
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox.Width = 100;
                pictureBox.Height = 70;

                Button btnRemove = new Button();
                btnRemove.Text = "Remove";
                btnRemove.Tag = vehicle; // Store the associated vehicle object
                btnRemove.Click += BtnRemove_Click;

                Label label = new Label();
                label.Text = $"{vehicle.Make} {vehicle.Model} ({vehicle.Year}) - ${vehicle.Price}";

                // Add controls to the flow layout panel
                flowLayoutPanel.Controls.Add(pictureBox);
                flowLayoutPanel.Controls.Add(label);
                flowLayoutPanel.Controls.Add(btnRemove);
            }
        }

        // Event handler for removing an item from the cart
        private void BtnRemove_Click(object sender, EventArgs e)
        {
            Button btnRemove = (Button)sender;
            Vehicle vehicleToRemove = (Vehicle)btnRemove.Tag; // Retrieve the associated vehicle object

            // Remove the item from the cart
            cartItems.Remove(vehicleToRemove);

            // Refresh the cart display
            DisplayCartItems();
        }
        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                foreach (var vehicle in cartItems)
                {
                    string query = "DELETE FROM Inventory WHERE Make = @Make AND Model = @Model AND Year = @Year";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Make", vehicle.Make);
                    command.Parameters.AddWithValue("@Model", vehicle.Model);
                    command.Parameters.AddWithValue("@Year", vehicle.Year);

                    command.ExecuteNonQuery();
                }

                cartItems.Clear();

                DisplayCartItems();
            }
        }


        private void flowLayoutPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
