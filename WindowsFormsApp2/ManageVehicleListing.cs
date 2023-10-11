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
    public partial class ManageVehicleListing : Form
    {
        private readonly CarRentalEntities1 _db;

        public ManageVehicleListing()
        {
            InitializeComponent();
            _db = new CarRentalEntities1();
        }

        private void ManageVehicleListing_Load(object sender, EventArgs e)
        {
            // Select ID as CarId and Name as CarName from TypesofCars
            /*var cars = _db.TypesofCars
                .Select(q => new { CarId = q.Id , CarName = q.Make })
                .ToList();*/
            var cars = _db.TypesofCars
                .Select(q => new
                {
                    Make = q.Make,
                    Model = q.Model,
                    VIN = q.VIN,
                    Year = q.Year,
                    LicencePlateNumber = q.LicencePlateNumber,
                    q.Id
                }
                ).ToList();
            gvVehicleList.DataSource = cars;
            gvVehicleList.Columns[4].HeaderText = "Licence Plate Number";
            gvVehicleList.Columns[5].Visible = false;

            /*gvVehicleList.Columns[0].HeaderText = "ID";
            gvVehicleList.Columns[1].HeaderText = "NAME";*/
        }

        private void btnAddCar_Click(object sender, EventArgs e)
        {
            try
            {
                var addEditVehicle = new AddEditVehicle(this);
                addEditVehicle.MdiParent = this.MdiParent;
                addEditVehicle.Show();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            

        }


        private void btnEditCar_Click(object sender, EventArgs e)
        {
            try
            {
                // Get Id of the selected row
                var id = (int)gvVehicleList.SelectedRows[0].Cells["Id"].Value;

                // Query the database for the record
                var car = _db.TypesofCars.FirstOrDefault(q => q.Id == id);

                // Launch AddEditVehicle window with data
                var addEditVehicle = new AddEditVehicle(car, this);
                addEditVehicle.MdiParent = this.MdiParent;
                addEditVehicle.Show();
            }
            catch (System.ArgumentOutOfRangeException)
            {
                MessageBox.Show("Ensure to select the row index portion prior to Edit Car"); // + ex.Message
                
            }

        }

        private void btnDeleteCar_Click(object sender, EventArgs e)
        {
            try
            {
                // Get Id of the selected row
                var id = (int)gvVehicleList.SelectedRows[0].Cells["Id"].Value;

                // Query the database for the record
                var car = _db.TypesofCars.FirstOrDefault(q => q.Id == id);

                DialogResult dr = MessageBox.Show("Are you sure you want to delete this Vehicle?",
                    "Delete", MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Warning);
                if (dr == DialogResult.Yes)
                {
                    // Delete Vechicle from table
                    _db.TypesofCars.Remove(car);
                    _db.SaveChanges();
                }
                PopulateGrid();
                
            }
            catch (System.ArgumentOutOfRangeException)
            {
                MessageBox.Show("Ensure to select the row index portion prior to Delete Car");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void PopulateGrid()
        {
            // Select a custom model collection of cars from database
            var cars = _db.TypesofCars
                .Select(q => new
                {
                    Make = q.Make,
                    Model = q.Model,
                    VIN = q.VIN,
                    Year = q.Year,
                    LicensePlateNumber = q.LicencePlateNumber,
                    q.Id
                })
                .ToList();

            gvVehicleList.DataSource = cars;
            gvVehicleList.Columns[4].HeaderText = "License Plate Number";
            //Hide the column for ID. Changed from the hard coded column value to the name, 
            // to make it more dynamic. 
            gvVehicleList.Columns["Id"].Visible = false;
        }
                
    }

}
