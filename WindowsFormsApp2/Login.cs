using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Login : Form
    {
        private readonly CarRentalEntities1 _db;
        public Login()
        {
            InitializeComponent();
            _db = new CarRentalEntities1();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                var username = tbUsername.Text.Trim();
                var password = tbPassword.Text;

                var hashedPassword = Utils.HashPassword(password);

                var user = _db.Users.FirstOrDefault(q => q.username == username && q.password == hashedPassword && q.isActive == true);
                if (user !=  null)
                {
                    var mainWindow = new MainWindow(this, user);
                    mainWindow.Show();
                    Hide();

                }
                else
                {
                    MessageBox.Show("Please provide valid credentials");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong, please try again");
            }
        }
    }
}
