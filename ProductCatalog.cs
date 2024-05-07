using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using System;
using System.Drawing;
using POSSYSTEMFINAL;



public partial class ProductCatalog : Form
{
    private List<Vehicle> vehicles = new List<Vehicle>();
    private string connectionString;

    public ProductCatalog(string connectionString)
    {
        this.connectionString = connectionString;
        InitializeComponent();
        try
        {
            PopulateVehiclesFromDatabase();
            DisplayVehicles();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}");
        }
    }

    private void PopulateVehiclesFromDatabase(string brand = "", string type = "")
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT Type, Brand, Year, Price, Color, Mileage_, Model, ImagePath FROM Vehicle WHERE 1=1";
            if (!string.IsNullOrWhiteSpace(brand))
                query += $" AND Brand = '{brand}'";
            if (!string.IsNullOrWhiteSpace(type))
                query += $" AND Type = '{type}'";

            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string make = reader.GetString(0);
                string model = reader.GetString(1);
                int year = reader.GetInt32(2);
                decimal price = reader.GetDecimal(3);
                string imagePath = reader.GetString(7); // ImagePath is at index 7
                vehicles.Add(new Vehicle(make, model, year, price, imagePath));
            }
            reader.Close();
        }
    }

    private void DisplayVehicles()
    {
        flowLayoutPanel.Controls.Clear();
        foreach (var vehicle in vehicles)
        {
            try
            {
                PictureBox pictureBox = new PictureBox();
                pictureBox.Image = Image.FromFile(vehicle.ImagePath);
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox.Width = 150;
                pictureBox.Height = 100;
                pictureBox.Click += (sender, e) => AddToCart(vehicle);

                Label label = new Label();
                label.Text = $"{vehicle.Make} {vehicle.Model} ({vehicle.Year}) - ${vehicle.Price}";

                flowLayoutPanel.Controls.Add(pictureBox);
                flowLayoutPanel.Controls.Add(label);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading image: {ex.Message}");
            }
        }
    }

    private void AddToCart(Vehicle vehicle)
    {
        // Add logic to add the vehicle to the checkout cart
        MessageBox.Show($"Added {vehicle.Make} {vehicle.Model} to cart.");
    }

    private void btnSearch_Click(object sender, EventArgs e)
    {
        string brand = txtBrand.Text.Trim();
        string type = txtType.Text.Trim();
        try
        {
            vehicles.Clear();
            PopulateVehiclesFromDatabase(brand, type);
            DisplayVehicles();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error searching: {ex.Message}");
        }
    }

    private void btnClear_Click(object sender, EventArgs e)
    {
        txtBrand.Text = "";
        txtType.Text = "";
        vehicles.Clear();
        PopulateVehiclesFromDatabase();
        DisplayVehicles();
    }

    private void button1_Click(object sender, EventArgs e)
    {

    }

    

    private void flowLayoutPanel_Paint(object sender, PaintEventArgs e)
    {

    }

    private void Searchbtn_Click(object sender, EventArgs e)
    {
        string brand = txtBrand.Text.Trim();
        string type = txtType.Text.Trim();
        try
        {
            vehicles.Clear();
            PopulateVehiclesFromDatabase(brand, type);
            DisplayVehicles();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error searching: {ex.Message}");
        }
    }

    private void Clearbtn_Click(object sender, EventArgs e)
    {
        txtBrand.Text = "";
        txtType.Text = "";
        vehicles.Clear();
        PopulateVehiclesFromDatabase();
        DisplayVehicles();
    }
}


public class Vehicle
{
    public string Make { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public decimal Price { get; set; }
    public string ImagePath { get; set; }

    public Vehicle(string make, string model, int year, decimal price, string imagePath)
    {
        Make = make;
        Model = model;
        Year = year;
        Price = price;
        ImagePath = imagePath;
    }
}
