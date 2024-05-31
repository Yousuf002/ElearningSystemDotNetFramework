using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace dbProject2
{
    public partial class StudentSignupPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSignup_Click(object sender, EventArgs e)
        {
            // Provide the connection string directly
            string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=eLearningSystem;Integrated Security=True";

            // Get user input
            string Name = txtName.Text;
            string email = txtEmail.Text;
            string password = txtPassword.Text;

            // Fetch the current maximum InstructorID
            int maxInstructorID = GetMaxInstructorID(connectionString);

            // Increment the value for the new entry
            int newInstructorID = maxInstructorID + 1;

            string insertQuery = $"INSERT INTO Student (StudentID, StudentName, StudentEmail, StudentPass) VALUES ({newInstructorID}, '{Name}', '{email}', '{password}')";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand(insertQuery, connection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                Response.Redirect("StudentLogin.aspx");
            }
            catch (Exception ex)
            {
                Response.Write($"Error: {ex.Message}");
            }
        }

        private int GetMaxInstructorID(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT MAX(StudentID) FROM Student";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    object result = cmd.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        return Convert.ToInt32(result);
                    }
                    else
                    {
                        // If the table is empty, return 0 or any other default value
                        return 0;
                    }
                }
            }
        }
    }
}