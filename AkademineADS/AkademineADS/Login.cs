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
    public partial class frm_Login : Form
    {
        public frm_Login()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //Connects to database
                SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-G3THV3D\MSSQLEXPRESS;Initial Catalog=dsaDB;Integrated Security=True");
                SqlCommand cmd = new SqlCommand("sp_role_login", con); //sp_role_login - Stored procedure
                cmd.CommandType = CommandType.StoredProcedure;
                
                con.Open();
                //used for stored procedure
                cmd.Parameters.AddWithValue("@uname", txt_Name.Text);
                cmd.Parameters.AddWithValue("@upass", txt_Password.Text);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    dr.Read();
                    //Logs in by role
                    if (dr[3].ToString() == "Admin")
                    {
                        frm_AdminHome ah = new frm_AdminHome();
                        ah.Show();
                        con.Close();
                        this.Hide();

                    }
                    else if (dr[3].ToString() == "Teacher")
                    {
                        frm_TeacherHome th = new frm_TeacherHome();
                        th.Show();
                        con.Close();
                        this.Hide();
                    }
                    else if (dr[3].ToString() == "Student")
                    {   //This string is needed in order to show only logged in student data
                        string name = txt_Name.Text;
                        string lname = txt_Password.Text;
                        frm_StudentHome sh = new frm_StudentHome(name, lname);
                        sh.Show();
                        con.Close();
                        this.Hide();
                    }

                }
                else
                {
                    MessageBox.Show("Name or password are incorrect", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }
    }
}
