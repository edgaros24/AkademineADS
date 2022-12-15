using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace AkademineADS
{
    public partial class frm_StudentHome : Form
    {
        public frm_StudentHome(string name, string lname)
        {
            InitializeComponent();
            LoadGrid(name, lname);
        }
        //Connects to database
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-G3THV3D\MSSQLEXPRESS;Initial Catalog=dsaDB;Integrated Security=True");


        public void LoadGrid(string name, string lname)  //Loads student datagrid
        {
            SqlCommand cmd = new SqlCommand("Select * from dsaTable where Name = '" + name + "' AND LastName = '"+ lname +"'", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            con.Close();

            dataGrid.DataSource = dt.DefaultView;
        }

        private void button1_Click(object sender, EventArgs e) //Logs out and enters login page
        {
            frm_Login log = new frm_Login();
            log.Show();
            this.Hide();
        }
    }
}
