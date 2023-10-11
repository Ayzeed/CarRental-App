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
    public partial class ResetPassword : Form
    {
        private readonly CarRentalEntities1 _db;
        private readonly Login _login;
        public User _user;

        public ResetPassword(User user)
        {
            InitializeComponent();
            _db = new CarRentalEntities1();
            _user = user;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                var defaultHashedPassword = Utils.DefaultHashPassword();
                var newPassword = tbNewPassword.Text;
                var confirmPassword = tbConfirmPassword.Text;
                var user = _db.Users.FirstOrDefault(q => q.id == _user.id);
                if (newPassword == confirmPassword)
                {
                    if (Utils.HashPassword(newPassword) != defaultHashedPassword)
                    {
                        user.password = Utils.HashPassword(newPassword);
                        _db.SaveChanges();
                        MessageBox.Show("Password Changed Successful");
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("New Password cannot be the same as the Default Password");
                    }
                }
                else
                {
                    MessageBox.Show("New Password and Confirm Password do not match");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Something went wrong, {ex.Message}");
            }
        }
    }
}
