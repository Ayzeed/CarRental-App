using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LiveUpdateForm
{
    public partial class Form1 : Form
    {
        private readonly LiveUpdateEntities _db;
        public Form1()
        {
            InitializeComponent();
            _db = new LiveUpdateEntities();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            //  SqlConnection connection = new SqlConnection(Convert.ToString(_db));
            var connection = _db.TypesofCars;
            /*connection.Open();
            string sql = "select * from TypesofCars";
            SqlCommand command = new SqlCommand(sql, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataSet set = new DataSet();
            adapter.Fill(set);*/
            dataGridView1.DataSource = connection;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(Convert.ToString(_db));
            connection.Open();
            //string sql = s
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }
    }
}
