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
    public partial class AddUser : Form
    {
        private ManageUsers _ManageUsers;
        private readonly CarRentalEntities1 _db;
        public AddUser(ManageUsers manageUsers = null)
        {
            InitializeComponent();
            _db = new CarRentalEntities1();
            _ManageUsers = manageUsers;
        }

        private void AddUser_Load(object sender, EventArgs e)
        {
            var roles = _db.Roles.ToList();
            cbRole.DataSource = roles;
            cbRole.DisplayMember = "name";
            cbRole.ValueMember = "id";

        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                //var username = tbUsername.Text;
                var roleId = (int)cbRole.SelectedValue;
                //var password = Utils.DefaultHashPassword();
                var newUser = new User
                {
                    username = tbUsername.Text,
                    password = Utils.DefaultHashPassword(),
                    isActive = true,
                    isDefaultPass = true
                };

                _db.Users.Add(newUser);
                _db.SaveChanges();

                var userId = newUser.id;
                var newUserRole = new UserRole
                {
                    roleid = roleId,
                    userid = userId
                };

                _db.UserRoles.Add(newUserRole);
                _db.SaveChanges();

                MessageBox.Show("New user has been added !");
                Close();

                _ManageUsers.PopulateGrid();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                _ManageUsers.PopulateGrid();
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
