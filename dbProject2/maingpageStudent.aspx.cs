using System;using System.Data.SqlClient;using System.Web.UI;using System.Web.UI.WebControls;namespace dbProject2{    public partial class mainpageStudent : System.Web.UI.Page    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["StudentID"] != null && Session["StudentName"] != null)
                {
                    // Retrieve values from session variables
                    int StudentID = (int)Session["StudentID"];
                    string StudentName = Session["StudentName"].ToString();

                    // Now you can use StudentID and StudentName as needed
                    // For example, display them on the page or use them in database queries
                    // lblWelcomeMessage.Text = $"Welcome, {StudentName} (ID: {StudentID})!";

                    // Call the method to bind Student courses
                    BindStudentCourses();
                }
                else
                {
                    Response.Redirect("StudentLogin.aspx");
                }
            }
        }

    

        private void BindStudentCourses()
        {
            string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=eLearningSystem;Integrated Security=True";

            int StudentID = (int)Session["StudentID"];

            string query = "SELECT Course.CourseName FROM Course " +
                           "INNER JOIN Student_Course ON Course.CourseID = Student_Course.CourseID " +
                           "WHERE Student_Course.StudentID = @StudentID";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@StudentID", StudentID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (reader["CourseName"] != DBNull.Value)
                                {
                                    string courseName = reader["CourseName"].ToString();
                                    int courseID = GetCourseID(courseName); // Add a method to get the course ID based on the course name

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
        protected void btnJoinCourse_Click(object sender, EventArgs e)        {            string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=eLearningSystem;Integrated Security=True";            int courseIDToJoin = int.Parse(txtJoinCourseID.Text);            int studentID = (int)Session["StudentID"];            try            {                using (SqlConnection connection = new SqlConnection(connectionString))                {                    connection.Open();                                        string checkEnrollmentQuery = "SELECT COUNT(*) FROM Student_Course WHERE StudentID = @StudentID AND CourseID = @CourseID";                    using (SqlCommand checkEnrollmentCmd = new SqlCommand(checkEnrollmentQuery, connection))                    {                        checkEnrollmentCmd.Parameters.AddWithValue("@StudentID", studentID);                        checkEnrollmentCmd.Parameters.AddWithValue("@CourseID", courseIDToJoin);                        int enrollmentCount = (int)checkEnrollmentCmd.ExecuteScalar();                        if (enrollmentCount > 0)                        {                            Response.Write("You are already enrolled in this course.");                        }                        else                        {                            string checkCourseQuery = "SELECT COUNT(*) FROM Course WHERE CourseID = @CourseID";                            using (SqlCommand checkCourseCmd = new SqlCommand(checkCourseQuery, connection))                            {                                checkCourseCmd.Parameters.AddWithValue("@CourseID", courseIDToJoin);                                int courseCount = (int)checkCourseCmd.ExecuteScalar();                                if (courseCount > 0)                                {                                    string joinCourseQuery = "INSERT INTO Student_Course (StudentID, CourseID) VALUES (@StudentID, @CourseID)";                                    using (SqlCommand cmd = new SqlCommand(joinCourseQuery, connection))                                    {                                        cmd.Parameters.AddWithValue("@StudentID", studentID);                                        cmd.Parameters.AddWithValue("@CourseID", courseIDToJoin);                                        cmd.ExecuteNonQuery();                                    }                                    BindStudentCourses();                                }                                else                                {                                }                                {                                    Response.Write("Error: Course does not exist.");                                }                            }                        }                    }                }            }            catch (Exception ex)            {                Response.Write($"Error: {ex.Message}");            }        }
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
        }    }}