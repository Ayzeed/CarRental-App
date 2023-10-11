using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    
    public partial class AddEditRentalRecords : Form
    {
        private bool isEditMode;
        private readonly CarRentalEntities1 _db;
        public AddEditRentalRecords()
        {
            InitializeComponent();
            _db = new CarRentalEntities1();
            lbTitle.Text = "Add New Rental Record";
            this.Text = "Add New Rental";
            isEditMode = false;
        }
        public AddEditRentalRecords(CarRentalRecord recordToEdit)
        {
            InitializeComponent();
            lbTitle.Text = "Edit Rental Record";
            this.Text = "Edit Rental Record";
            if (recordToEdit == null)
            {
                MessageBox.Show("Please Ensure you have selected a valid record to edit");
                Close();
            }
            else
            {
                isEditMode = true;
                PopulateFields(recordToEdit);
                _db = new CarRentalEntities1();
            }
        }

        private void PopulateFields(CarRentalRecord recordToEdit)
        {
            tbCustomerName.Text = recordToEdit.CustomerName;
            tbCost.Text = Convert.ToString(recordToEdit.Cost);
            dtpDateRented.Value = (DateTime)recordToEdit.DateRented;
            dtpDateReturned.Value = (DateTime)recordToEdit.DateReturned;
            lblRecodId.Text = recordToEdit.id.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //var cars = carRentalEntities.TypesofCars.ToList();
            var cars = _db.TypesofCars.Select(q => new 
            { 
                Id = q.Id, 
                Name = q.Make + " " + q.Model 
            }
            ).ToList();
            cbCarType.DisplayMember = "Name";
            cbCarType.ValueMember = "Id";
            cbCarType.DataSource = cars;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string customerName = tbCustomerName.Text;
                double cost = Convert.ToDouble(tbCost.Text);
                var dateRented = dtpDateRented.Value;
                var dateReturned = dtpDateReturned.Value;
                var carType = cbCarType.Text;

                var isValid = true;

                if (string.IsNullOrWhiteSpace(customerName) || string.IsNullOrWhiteSpace(carType))
                {
                    MessageBox.Show("Enter missing data");
                    isValid = false;
                }

                if (dateRented > dateReturned)
                {
                    MessageBox.Show("Incorrect date Input");
                    isValid = false;
                }

                if (isValid)
                {
                    // Declare an object of the record to be added 
                    var rentalRecord = new CarRentalRecord(); 
                    if (isEditMode)
                    {
                        // If in edit mode, get the id and retrieve the record from the database 
                        // and place the result in the record object
                        var id = int.Parse(lblRecodId.Text);
                        rentalRecord = _db.CarRentalRecords.FirstOrDefault(q => q.id == id);
                       
                    }
                    // Populate record object with the values from the form
                    rentalRecord.CustomerName = customerName;
                    rentalRecord.DateRented = dateRented;
                    rentalRecord.DateReturned = dateReturned;
                    rentalRecord.Cost = (decimal)cost;
                    rentalRecord.TypeofCars = (int)cbCarType.SelectedValue;
                    // If not in edit mode then the add record object to the database
                    if (!isEditMode)
                        _db.CarRentalRecords.Add(rentalRecord);
                    // Save the changes made to the entities
                    _db.SaveChanges();

                    MessageBox.Show($"Thank You for Renting {customerName}.\n\r" +
                        $"You Rented {carType} for N{cost} \n\r" +
                        $"on {dateRented} \n\r" +
                        $"Returned on {dateReturned}");
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //throw;
            }
            
        }
    }
}
