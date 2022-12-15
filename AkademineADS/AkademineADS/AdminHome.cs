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
    public partial class frm_AdminHome : Form
    {
        public frm_AdminHome()
        {
            InitializeComponent();
            LoadGrid();
        }
        //Connects to database
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-G3THV3D\MSSQLEXPRESS;Initial Catalog=dsaDB;Integrated Security=True");

        public void ClearData() //Clears data that is written in textbox
        {
            txt_Id.Clear();
            txt_Name.Clear();
            txt_LastName.Clear();
            txt_Group.Clear();
            txt_Subject.Clear();
        }

        public void LoadGrid()  //Loads a datagrid
        {
            SqlCommand cmd = new SqlCommand("Select * from dsaTable", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            con.Close();
            
            dataGrid.DataSource = dt.DefaultView;
        }

        private void btn_Clear_Click(object sender, EventArgs e)    //A button to clear data
        {
            ClearData();
        }

        public bool isValid()   //Is textbox is empty
        {
            if (txt_Name.Text == string.Empty)
            {
                MessageBox.Show("The Name is required", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (txt_LastName.Text == string.Empty)
            {
                MessageBox.Show("The Last Name is required", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (txt_Subject.Text == string.Empty)
            {
                MessageBox.Show("The Subject is required", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }

        private void btn_Insert_Click(object sender, EventArgs e) //Inserts a new user
        {
                try
                {
                    if (isValid())
                    {   con.Open();
                        SqlCommand cmd = new SqlCommand("INSERT INTO dsaTable VALUES (@Name, @LastName, @Role, @SGroup, @Subject, @Test, @Practice, @Exam)", con);
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@Name", txt_Name.Text);
                        cmd.Parameters.AddWithValue("@LastName", txt_LastName.Text);
                        cmd.Parameters.AddWithValue("@Role", cb_Role.Text);
                        cmd.Parameters.AddWithValue("@SGroup", txt_Group.Text);
                        cmd.Parameters.AddWithValue("@Subject", txt_Subject.Text);
                        cmd.Parameters.AddWithValue("@Test", string.Empty);
                        cmd.Parameters.AddWithValue("@Practice", string.Empty);
                        cmd.Parameters.AddWithValue("@Exam", string.Empty);
                        
                        cmd.ExecuteNonQuery();
                        con.Close();
                        LoadGrid();
                        MessageBox.Show("Succesfully registered", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearData();
                     }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            
        }

 

        private void btn_Delete_Click(object sender, EventArgs e) //Deletes a user
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("delete from dsaTable where Id="+ txt_Id.Text +" ", con);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record has been deleted", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();
                ClearData();
                LoadGrid();
                con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Not deleted"+ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void btn_Update_Click(object sender, EventArgs e) //Updates users data
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("update dsaTable set Name = '"+ txt_Name.Text +"', LastName = '"+ txt_LastName.Text +"', Role = '"+cb_Role.Text+"', SGroup = '"+txt_Group.Text+"', Subject = '"+txt_Subject.Text+"' WHERE Id = '"+txt_Id.Text+"'", con);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Updated successfully", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (SqlException ex)
            {
                MessageBox.Show("Not deleted" + ex.Message);
            }
            finally
            {
                con.Close();
                ClearData();
                LoadGrid();
            }
        }

        private void btn_LogOut_Click(object sender, EventArgs e) //Logs out and enters login page
        {
            frm_Login log = new frm_Login();
            log.Show();
            this.Hide();
        }
    }
}
