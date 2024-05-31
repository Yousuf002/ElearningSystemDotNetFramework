using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace dbProject2
{
    public partial class StudentLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            

        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=eLearningSystem;Integrated Security=True";

            string email = txtEmail.Text;
            string password = txtPassword.Text;

            string query = $"SELECT StudentID, StudentName FROM Student WHERE StudentEmail = '{email}' AND StudentPass = '{password}'";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            int StudentID = reader.GetInt32(reader.GetOrdinal("StudentID"));
                            string StudentName = reader.GetString(reader.GetOrdinal("StudentName"));

                            Session["StudentID"] = StudentID;
                            Session["StudentName"] = StudentName;

                            Response.Redirect("maingpageStudent.aspx");
                        }
                        else
                        {
                            lblMessage.Text = "Invalid username or password. Please try again.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write($"Error: {ex.Message}");
            }
        }
        protected void btnSignup_Click(object sender, EventArgs e)
        {
            Response.Redirect("StudentSignupPage.aspx");
        }
    }
}