    using System;
    using System.Data.SqlClient;
    using System.Web.UI;

    namespace dbProject2
    {
        public partial class mainpageInstructor : System.Web.UI.Page
        {
            protected void Page_Load(object sender, EventArgs e)
            {
                if (!IsPostBack)
                {
                    if (Session["InstructorID"] != null && Session["InstructorName"] != null)
                    {
                        int instructorID = (int)Session["InstructorID"];
                        string instructorName = Session["InstructorName"].ToString();

                     
                       // lblWelcomeMessage.Text = $"Welcome, {instructorName} (ID: {instructorID})!";

                        BindInstructorCourses();
                    }
                    else
                    {
                        Response.Redirect("InstructorLogin.aspx");
                    }
                }
            }

            private void BindInstructorCourses()
            {
                string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=eLearningSystem;Integrated Security=True";

                int instructorID = (int)Session["InstructorID"];

                string query = "SELECT Course.CourseName FROM Course " +
                               "INNER JOIN Instructor_Course ON Course.CourseID = Instructor_Course.CourseID " +
                               "WHERE Instructor_Course.InstructorID = @InstructorID";

                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        using (SqlCommand cmd = new SqlCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@InstructorID", instructorID);

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    if (reader["CourseName"] != DBNull.Value)
                                    {
                                        string courseName = reader["CourseName"].ToString();
                                        int courseID = GetCourseID(courseName); 

                                        Session["SelectedCourseID"] = courseID;

                                        LiteralControl courseControl = new LiteralControl($"<p onclick='redirectToCourseContent({courseID})'>{courseName}</p>");

                                        courseDiv.Controls.Add(courseControl);
                                    }
                                }

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Response.Write($"Error: {ex.Message}");
                }
            }

            protected void btnAddCourse_Click(object sender, EventArgs e)
            {
                string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=eLearningSystem;Integrated Security=True";

                string newCourseName = txtNewCourseName.Text;
                int newCourseID = int.Parse(txtNewCourseID.Text);
                string newCourseDescription = txtNewCourseDescription.Text;

                int instructorID = (int)Session["InstructorID"];

                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        string insertQuery = "INSERT INTO Course (CourseName, CourseID, Description) " +
                                             "OUTPUT INSERTED.CourseID " +
                                             "VALUES (@CourseName, @CourseID, @Description)";

                        using (SqlCommand cmd = new SqlCommand(insertQuery, connection))
                        {
                            cmd.Parameters.AddWithValue("@CourseName", (object)newCourseName ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@CourseID", newCourseID);
                            cmd.Parameters.AddWithValue("@Description", (object)newCourseDescription ?? DBNull.Value);

                            int insertedCourseID = Convert.ToInt32(cmd.ExecuteScalar());

                            string insertInstructorCourseQuery = "INSERT INTO Instructor_Course (InstructorID, CourseID) " +
                                                                 "VALUES (@InstructorID, @CourseID)";

                            using (SqlCommand instructorCourseCmd = new SqlCommand(insertInstructorCourseQuery, connection))
                            {
                                instructorCourseCmd.Parameters.AddWithValue("@InstructorID", instructorID);
                                instructorCourseCmd.Parameters.AddWithValue("@CourseID", insertedCourseID);

                                instructorCourseCmd.ExecuteNonQuery();
                            }
                        }

                        BindInstructorCourses();
                    }
                }
                catch (Exception ex)
                {
                    Response.Write($"Error: {ex.Message}");
                }
            }



            private int GetCourseID(string courseName)
            {
                string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=eLearningSystem;Integrated Security=True";

                string query = "SELECT CourseID FROM Course WHERE CourseName = @CourseName";

                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        using (SqlCommand cmd = new SqlCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@CourseName", courseName);

                            object result = cmd.ExecuteScalar();

                            if (result != null && result != DBNull.Value)
                            {
                                return Convert.ToInt32(result);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Response.Write($"Error in GetCourseID: {ex.Message}");
                }

                return -1; // Or any other default value
            }

        }
    }
