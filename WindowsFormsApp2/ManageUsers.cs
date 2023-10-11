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
    public partial class ManageUsers : Form
    {
        private readonly CarRentalEntities1 _db;
        public ManageUsers()
        {
            InitializeComponent();
            _db = new CarRentalEntities1();
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            if (!Utils.FormIsOpen("AddUser"))
            {
                var addUser = new AddUser(this);
                addUser.MdiParent = this.MdiParent;
                addUser.Show();
            }
        }
        private void btnDeactivateUser_Click(object sender, EventArgs e)
        {
            try
            {
                // Get Id of the selected row
                var id = (int)gvUserList.SelectedRows[0].Cells["id"].Value;

                // Query the database for the record
                var user = _db.Users.FirstOrDefault(q => q.id == id);
                user.isActive = user.isActive == true ? false : true;

                _db.SaveChanges();

                if (user.isActive == true)
                {
                    MessageBox.Show($"{user.username} has been activated");
                }
                else
                {
                    MessageBox.Show($"{user.username} has been deactivated");
                }
                PopulateGrid();
            }
            catch (System.ArgumentOutOfRangeException)
            {
                MessageBox.Show("Ensure to select the row index portion prior to Password Reset"); // + ex.Message

            }
        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            try
            {
                // Get Id of the selected row
                var id = (int)gvUserList.SelectedRows[0].Cells["id"].Value;

                // Query the database for the record
                var user = _db.Users.FirstOrDefault(q => q.id == id);
                var HashedPassword = Utils.DefaultHashPassword();
                user.password = HashedPassword;
                _db.SaveChanges();

                MessageBox.Show($"{user.username}'s password has been reset");
                PopulateGrid();
            }
            catch (System.ArgumentOutOfRangeException)
            {
                MessageBox.Show("Ensure to select the row index portion prior to Password Reset"); // + ex.Message

            }
        }

        
        public void PopulateGrid()
        {
            string defaultHashPassword = Utils.DefaultHashPassword();
            var user = _db.Users.Select(q => new
            {
                
                q.username,
                role = q.UserRoles.FirstOrDefault().Role.name,
                // q.password,
                isActive = (bool)q.isActive ? "Yes" : "No",
                isDefaultPass = q.password == defaultHashPassword ? "Yes" : "No",
                q.id

            }).ToList();

            gvUserList.DataSource = user;
            gvUserList.Columns["username"].HeaderText = "Username";
            gvUserList.Columns["role"].HeaderText = "Roles";
            gvUserList.Columns["isActive"].HeaderText = "Active";
            gvUserList.Columns["isDefaultPass"].HeaderText = "Password(Default)";

            gvUserList.Columns["id"].Visible = false;

        }

        private void ManageUsers_Load(object sender, EventArgs e)
        {
            PopulateGrid();
        }
       
    }

}
