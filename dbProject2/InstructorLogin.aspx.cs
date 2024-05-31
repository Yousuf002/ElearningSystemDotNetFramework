using System;
using System.Data.SqlClient;
using System.Runtime.Remoting.Messaging;
using System.Web;

namespace dbProject2
{
    public partial class InstructorLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            // Provide the connection string directly
            string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=eLearningSystem;Integrated Security=True";

            // Get user input
            string email = txtEmail.Text;
            string password = txtPassword.Text;

            // Query to check if the entered credentials are valid and get the instructor's ID
            string query = $"SELECT InstructorID, InstructorName FROM Instructor WHERE InstructorEmail = '{email}' AND InstructorPass = '{password}'";

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
                            // Valid credentials, get the instructor's ID and name
                            int instructorID = reader.GetInt32(reader.GetOrdinal("InstructorID"));
                            string instructorName = reader.GetString(reader.GetOrdinal("InstructorName"));

                            // Store instructorID and instructorName in session variables
                            Session["InstructorID"] = instructorID;
                            Session["InstructorName"] = instructorName;

                            // Redirect to the main instructor page
                            Response.Redirect("mainpageInstructor.aspx");
                        }
                        else
                        {
                            // Invalid credentials, show an error message
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
            // Redirect to the signup page
            Response.Redirect("InstructorSignup.aspx");
        }
    }
}
