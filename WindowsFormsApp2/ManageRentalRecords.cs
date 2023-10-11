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
    public partial class ManageRentalRecords : Form
    {
        private readonly CarRentalEntities1 _db;
        public ManageRentalRecords()
        {
            InitializeComponent();
            _db = new CarRentalEntities1();
        }

        private void btnAddRecord_Click(object sender, EventArgs e)
        {
            var addRentalRecords = new AddEditRentalRecords
            {
                MdiParent = this.MdiParent
            };
            addRentalRecords.Show();
        }
        private void btnEditRecord_Click(object sender, EventArgs e)
        {
            try
            {
                // Get Id of the selected row
                var id = (int)gvRecordList.SelectedRows[0].Cells["Id"].Value;

                // Query the database for the record
                var record = _db.CarRentalRecords.FirstOrDefault(q => q.id == id);

                // Launch AddEditVehicle window with data
                var addEditRentalRecord = new AddEditRentalRecords(record);
                addEditRentalRecord.MdiParent = this.MdiParent;
                addEditRentalRecord.Show();
            }
            catch (System.ArgumentOutOfRangeException)
            {
                MessageBox.Show("Ensure to select the row index portion prior to Edit Car"); // + ex.Message

            }
        }
        private void btnDeleteRecord_Click(object sender, EventArgs e)
        {
            try
            {
                // Get Id of the selected row
                var id = (int)gvRecordList.SelectedRows[0].Cells["Id"].Value;

                // Query the database for the record
                var record = _db.CarRentalRecords.FirstOrDefault(q => q.id == id);

                // Delete Vechicle from table
                _db.CarRentalRecords.Remove(record);
                _db.SaveChanges();

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

        

        private void ManageRentalRecords_Load(object sender, EventArgs e)
        {
            PopulateGrid();
        }

        private void PopulateGrid()
        {
            var records = _db.CarRentalRecords.Select(q => new
            {
                Customer = q.CustomerName,
                DateOut = q.DateRented,
                DateIn = q.DateReturned,
                Id = q.id,
                q.Cost,
                Car = q.TypesofCar.Make + " " + q.TypesofCar.Model
            }).ToList();

            gvRecordList.DataSource = records;
            gvRecordList.Columns["DateOut"].HeaderText = "Date Rented";
            gvRecordList.Columns["DateIn"].HeaderText = "Date Returned";
            //Hide the column for ID. Changed from the hard coded column value to the name, 
            // to make it more dynamic. 
            gvRecordList.Columns["Id"].Visible = false;
        }
    }
}
