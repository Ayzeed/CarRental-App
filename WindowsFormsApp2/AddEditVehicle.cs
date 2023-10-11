using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace WindowsFormsApp2
{
    public partial class AddEditVehicle : Form
    {
        private bool isEditMode;
        private ManageVehicleListing _manageVehicleListing;
        private readonly CarRentalEntities1 _db;
        public AddEditVehicle(ManageVehicleListing manageVehicleListing = null)
        {
            InitializeComponent();
            lbTitle.Text = "Add New Vehicle";
            this.Text = "Add New Vehicle";
            isEditMode = false;
            _manageVehicleListing = manageVehicleListing;
            _db = new CarRentalEntities1();
        }

        
        public AddEditVehicle(TypesofCar carToEdit, ManageVehicleListing manageVehicleListing = null)
        {
            InitializeComponent();
            lbTitle.Text = "Edit Vehicle";
            this.Text = "Edit Vehicle";
            _manageVehicleListing = manageVehicleListing;
            if (carToEdit == null)
            {
                MessageBox.Show("Please Ensure you have selected a valid record to edit");
                Close();
            }
            else
            {
                isEditMode = true;
                PopulateFields(carToEdit);
                _db = new CarRentalEntities1();
            }
        }

        private void PopulateFields(TypesofCar car)
        {
            lbId.Text = car.Id.ToString();
            tbMake.Text = car.Make;
            tbModel.Text = car.Model;
            tbVIN.Text = car.VIN;
            tbYear.Text = Convert.ToString(car.Year);
            tbLicencePlateNumber.Text = car.LicencePlateNumber;

        }

        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            try
            {
                if (isEditMode)
                {
                    // Edit code here
                    var id = int.Parse(lbId.Text);
                    var car = _db.TypesofCars.FirstOrDefault(q => q.Id == id);
                    car.Model = tbModel.Text;
                    car.Make = tbMake.Text;
                    car.VIN = tbVIN.Text;
                    car.Year = int.Parse(tbYear.Text);
                    car.LicencePlateNumber = tbLicencePlateNumber.Text;
                    
                    // this.Refresh();
                }
                else
                {
                    // Add code here

                    var newCar = new TypesofCar
                    {
                        Make = tbMake.Text,
                        Model = tbModel.Text,
                        VIN = tbVIN.Text,
                        Year = int.Parse(tbYear.Text),
                        LicencePlateNumber = tbLicencePlateNumber.Text
                    };
                    if (string.IsNullOrWhiteSpace(tbModel.Text) || string.IsNullOrWhiteSpace(tbMake.Text))
                    {
                        MessageBox.Show("Model and Make are mandatory");
                        return;
                    }
                    _db.TypesofCars.Add(newCar);
                    MessageBox.Show("New vehicle successfully added !");
                    /* this.Update();
                     this.Refresh();*/
                }
                _db.SaveChanges();
                _manageVehicleListing.PopulateGrid();
                MessageBox.Show("Operation Completed !");
                Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AddEditVehicle_Load(object sender, EventArgs e)
        {
            
        }
        
    }
}
