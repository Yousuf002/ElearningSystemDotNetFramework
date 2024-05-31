using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace dbProject2
{
    public partial class fileupload : Page
    {
        private string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=eLearningSystem;Integrated Security=True"; // Update with your database connection string
        private int courseId;
        protected void Page_Load(object sender, EventArgs e)
        {
            // Your Page_Load logic (if any)
        }

        protected void btnAddAssignment_Click(object sender, EventArgs e)
        {
            // Get the values from the input controls
            int assignmentID;
            if (!int.TryParse(txtAssignmentID.Text, out assignmentID))
            {
                // Handle the case where assignmentID is not a valid integer
                // Show an error message or log the issue
                return;
            }

            string assignmentName = txtAssignmentName.Text;
            string assignmentDescription = txtAssignmentDescription.Text;
            string dueDate = txtDueDate.Text;

            // Call the method to add the assignment to the database
            AddAssignmentToDatabase(assignmentID, assignmentName, assignmentDescription, dueDate, fileAssignment);

            // Bind the assignment list to display the newly added assignment
            BindAssignmentList();

            // Clear input controls
            txtAssignmentID.Text = "";
            txtAssignmentName.Text = "";
            txtAssignmentDescription.Text = "";
            txtDueDate.Text = "";
        }

        protected void BindAssignmentList()
        {
            courseId = 2;
            assignmentList.Items.Clear(); // Clear existing items before binding

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Query to retrieve assignment details from the database
                string query = "SELECT AssignmentID, AssignmentName FROM Assignment WHERE AssignmentID IN (SELECT AssignmentID FROM Assignment_Course WHERE CourseID = @CourseID)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Set parameters
                    command.Parameters.AddWithValue("@CourseID", courseId); // Use the class-level courseId

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Create a new list item to display the assignment
                            ListItem listItem = new ListItem();

                            // Set the text property without HTML tags
                            listItem.Text = $"{reader["AssignmentName"]} (ID: {reader["AssignmentID"]})";

                            // Add assignment details as data attributes
                            listItem.Value = reader["AssignmentID"].ToString();

                            // Add the new assignment to the list
                            assignmentList.Items.Add(listItem);
                        }
                    }
                }
            }
        }

        protected void assignmentList_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the selected assignment ID
            string selectedAssignmentID = assignmentList.SelectedValue;

            // Display the assignment details in the modal
            DisplayAssignmentDetails(selectedAssignmentID);
        }

        private void AddAssignmentToDatabase(int assignmentID, string assignmentName, string assignmentDescription, string dueDate, FileUpload fileAssignment)
        {
            courseId = 2;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Perform an insert into Assignment table
                string insertAssignmentQuery = "INSERT INTO Assignment (AssignmentID, AssignmentName, Description, DueDate, CreatedAt) VALUES (@AssignmentID, @AssignmentName, @Description, @DueDate, GETDATE())";
                using (SqlCommand insertAssignmentCommand = new SqlCommand(insertAssignmentQuery, connection))
                {
                    // Set other parameters
                    insertAssignmentCommand.Parameters.AddWithValue("@AssignmentID", assignmentID);
                    insertAssignmentCommand.Parameters.AddWithValue("@AssignmentName", assignmentName);
                    insertAssignmentCommand.Parameters.AddWithValue("@Description", assignmentDescription);
                    insertAssignmentCommand.Parameters.AddWithValue("@DueDate", dueDate);

                    insertAssignmentCommand.ExecuteNonQuery();
                }

                // Perform an insert into PdfFiles table
                if (fileAssignment.HasFile)
                {
                    byte[] fileBytes = fileAssignment.FileBytes;

                    string fileName = fileAssignment.FileName;
                    string contentType = fileAssignment.PostedFile.ContentType;

                    // Insert PDF data into PdfFiles table
                    string insertPdfFileQuery = "INSERT INTO PdfFiles (FileName, ContentType, Content, UploadDate, AssignmentID) VALUES (@FileName, @ContentType, @Content, GETDATE(), @AssignmentID)";
                    using (SqlCommand insertPdfFileCommand = new SqlCommand(insertPdfFileQuery, connection))
                    {
                        insertPdfFileCommand.Parameters.AddWithValue("@FileName", fileName);
                        insertPdfFileCommand.Parameters.AddWithValue("@ContentType", contentType);
                        insertPdfFileCommand.Parameters.AddWithValue("@Content", fileBytes);
                        insertPdfFileCommand.Parameters.AddWithValue("@AssignmentID", assignmentID);

                        insertPdfFileCommand.ExecuteNonQuery();
                    }
                }

                // Perform an insert into Assignment_Course table
                string insertAssignmentCourseQuery = "INSERT INTO Assignment_Course (AssignmentID, CourseID) VALUES (@AssignmentID, @CourseID)";
                using (SqlCommand insertAssignmentCourseCommand = new SqlCommand(insertAssignmentCourseQuery, connection))
                {
                    insertAssignmentCourseCommand.Parameters.AddWithValue("@AssignmentID", assignmentID);
                    insertAssignmentCourseCommand.Parameters.AddWithValue("@CourseID", courseId);

                    insertAssignmentCourseCommand.ExecuteNonQuery();
                }

                // Display a success message
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Assignment added successfully.');", true);
            }
        }


        private void DisplayAssignmentDetails(string assignmentID)
        {
            // Retrieve the assignment details with course information from the database based on the assignment ID
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Query to retrieve assignment details with course information
                string query = "SELECT A.AssignmentID, A.AssignmentName, A.Description, A.DueDate, C.CourseName " +
                               "FROM Assignment A " +
                               "INNER JOIN Assignment_Course AC ON A.AssignmentID = AC.AssignmentID " +
                               "INNER JOIN Course C ON AC.CourseID = C.CourseID " +
                               "WHERE A.AssignmentID = @AssignmentID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@AssignmentID", assignmentID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Retrieve assignment details from the database
                            string assignmentName = reader["AssignmentName"].ToString();
                            string description = reader["Description"].ToString();
                            string dueDate = reader["DueDate"].ToString();
                            string courseName = reader["CourseName"].ToString();

                            // Display the assignment details in the modal
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "displayAssignmentDetails",
                                $"displayAssignmentDetails('Assignment Name: {assignmentName}', 'Description: {description}', 'Due Date: {dueDate}', 'Course Name: {courseName}');", true);
                        }
                    }
                }
            }
        }

    }
}
