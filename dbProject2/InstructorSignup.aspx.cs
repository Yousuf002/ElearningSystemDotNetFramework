using System;
using System.Data.SqlClient;

namespace dbProject2
{
    public partial class InstructorSignup : System.Web.UI.Page
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

            string insertQuery = $"INSERT INTO Instructor (InstructorID, InstructorName, InstructorEmail, InstructorPass) VALUES ({newInstructorID}, '{Name}', '{email}', '{password}')";

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
                Response.Redirect("InstructorLogin.aspx");
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

                string query = "SELECT MAX(InstructorID) FROM Instructor";
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
