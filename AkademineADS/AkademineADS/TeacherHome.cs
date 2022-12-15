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
    public partial class frm_TeacherHome : Form
    {
        public frm_TeacherHome()
        {
            InitializeComponent();
            LoadGrid();
        }
        //Connects to database
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-G3THV3D\MSSQLEXPRESS;Initial Catalog=dsaDB;Integrated Security=True");


        private void TeacherHome_Load(object sender, EventArgs e)
        {

        }
        public void ClearData()
        {
            txt_Id.Clear();
            txt_Test.Clear();
            txt_PW.Clear();
            txt_Exam.Clear();
        }

        public void LoadGrid()  //Loads students data
        {
            SqlCommand cmd = new SqlCommand("Select * from dsaTable where Role = '"+"Student"+"'", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            con.Close();

            dataGrid.DataSource = dt.DefaultView;
        }

        private void btn_Update_Click(object sender, EventArgs e)   //Updates student data
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("update dsaTable set Test = '" + txt_Test.Text + "', Practice = '" + txt_PW.Text + "', Exam = '" + txt_Exam.Text + "' WHERE Id = '" + txt_Id.Text + "'", con);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Updated successfully", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (SqlException ex)
            {
                MessageBox.Show("Not updated" + ex.Message);
            }
            finally
            {
                con.Close();
                ClearData();
                LoadGrid();
            }
        }

        private void btn_Clear_Click(object sender, EventArgs e) //Clears all textboxes
        {
            ClearData();
        }

        private void btn_LogOut_Click(object sender, EventArgs e) //Logs out and enters login page
        {
            frm_Login log = new frm_Login();
            log.Show();
            this.Hide();
        }
    }
}
