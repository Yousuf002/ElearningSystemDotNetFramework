using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace dbProject2
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        // Connection string from your web.config file
        string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=eLearningSystem;Integrated Security=True";

        // Declare courseId at the class level
        private int courseId;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if the instructor information is available in session variables
                if (Session["InstructorID"] != null && Session["InstructorName"] != null)
                {
                    int instructorID = (int)Session["InstructorID"];
                    string instructorName = Session["InstructorName"].ToString();

                    // Display instructor information on the page if needed
                    // lblWelcomeMessage.Text = $"Welcome, {instructorName} (ID: {instructorID})!";

                    if (Request.QueryString["courseID"] != null)
                    {
                        int courseID = Convert.ToInt32(Request.QueryString["courseID"]);
                        Session["SelectedCourseID"] = courseID;

                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();

                            // Query to retrieve course details, including the course name
                            string courseQuery = "SELECT CourseName FROM Course WHERE CourseID = @CourseID";

                            using (SqlCommand courseCommand = new SqlCommand(courseQuery, connection))
                            {
                                courseCommand.Parameters.AddWithValue("@CourseID", courseID);

                                // Execute the command to get the course name
                                object courseNameObject = courseCommand.ExecuteScalar();

                                if (courseNameObject != null)
                                {
                                    string courseName = courseNameObject.ToString();
                                    courseNameLiteral.Text = courseName;
                                }
                            }
                        }
                    }

                    // Ensure that SelectedCourseID is set before calling these functions
                    if (Session["SelectedCourseID"] != null)
                    {
                        // Bind the assignment list or other content based on the retrieved course ID
                        BindAssignmentList();
                        BindMaterialList();
                        BindAnnouncements();
                    }
                }
                else
                {
                    // Redirect to the login page if instructor information is not available
                    Response.Redirect("InstructorLogin.aspx");
                }
            }
        }



        protected void BindAssignmentList()
        {
            courseId = (int)Session["SelectedCourseID"];
            assignmentList.Items.Clear(); // Clear existing items before binding

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Query to retrieve assignment details from the database
                string query = "SELECT a.AssignmentID, a.AssignmentName, a.Description, a.DueDate, a.FileData FROM Assignment a INNER JOIN Assignment_Course ac ON a.AssignmentID = ac.AssignmentID WHERE ac.CourseID = @CourseID";

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
                            listItem.Attributes["data-assignmentName"] = reader["AssignmentName"].ToString();
                            listItem.Attributes["data-assignmentID"] = reader["AssignmentID"].ToString();
                            listItem.Attributes["data-description"] = reader["Description"].ToString();
                            listItem.Attributes["data-dueDate"] = reader["DueDate"].ToString();

                            int fileDataIndex = reader.GetOrdinal("FileData");
                            if (!reader.IsDBNull(fileDataIndex))
                            {
                                // Assuming FileData is a byte[] column
                                byte[] fileData = (byte[])reader["FileData"];

                                // Generate a unique download link for each assignment
                                string downloadLink = $"Download.ashx?assignmentID={reader["AssignmentID"]}";
                                listItem.Attributes["data-file-name"] = downloadLink;
                            }
                            else
                            {
                                // Handle the case where FileData is NULL
                                listItem.Attributes["data-file-name"] = "#"; // Provide a placeholder link or another appropriate value
                            }

                            // Add the new assignment to the list
                            assignmentList.Items.Add(listItem);
                        }
                    }
                }
            }
            
        }

        protected void BindMaterialList()
        {
            courseId = (int)Session["SelectedCourseID"];
            materialList.Items.Clear(); // Clear existing items before binding

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Query to retrieve material details from the database
                string query = "SELECT MaterialID, MaterialName, FileData FROM Materials WHERE CourseID = @CourseID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Set parameters
                    command.Parameters.AddWithValue("@CourseID", courseId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Create a new list item to display the material
                            ListItem listItem = new ListItem();

                            // Set the text property without HTML tags
                            listItem.Text = $"{reader["MaterialName"]} (ID: {reader["MaterialID"]})";

                            // Add material details as data attributes
                            listItem.Attributes["data-materialID"] = reader["MaterialID"].ToString();
                            listItem.Attributes["data-materialName"] = reader["MaterialName"].ToString();

                            // Handle file data
                            int fileDataIndex = reader.GetOrdinal("FileData");
                            if (!reader.IsDBNull(fileDataIndex))
                            {
                                byte[] fileData = (byte[])reader["FileData"];
                                listItem.Attributes["data-fileData"] = Convert.ToBase64String(fileData);
                            }
                            else
                            {
                                listItem.Attributes["data-fileData"] = string.Empty;
                            }

                            // Add the new material to the list
                            materialList.Items.Add(listItem);
                        }
                    }
                }
            }
        }
        protected void AddMaterialToDatabase(string materialName, FileUpload fileMaterial)
        {
            courseId = (int)Session["SelectedCourseID"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Perform an insert
                string insertMaterialQuery = "INSERT INTO Materials (CourseID, MaterialName, FileData) VALUES (@CourseID, @MaterialName, @FileData)";
                using (SqlCommand insertMaterialCommand = new SqlCommand(insertMaterialQuery, connection))
                {
                    // Set other parameters
                    insertMaterialCommand.Parameters.AddWithValue("@CourseID", courseId);
                    insertMaterialCommand.Parameters.AddWithValue("@MaterialName", materialName);

                    // Handle file data
                    if (fileMaterial.HasFile)
                    {
                        // Get the file data
                        byte[] fileBytes = fileMaterial.FileBytes;

                        // Store the file data in the database
                        insertMaterialCommand.Parameters.AddWithValue("@FileData", fileBytes);
                    }
                    else
                    {
                        // No file selected
                        insertMaterialCommand.Parameters.AddWithValue("@FileData", DBNull.Value);
                    }

                    insertMaterialCommand.ExecuteNonQuery();
                }

                // Display a success message
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Material added successfully.');", true);
            }
        }

        protected void btnAddMaterial_Click(object sender, EventArgs e)
        {
            // Get the values from the input controls
            string materialName = txtMaterialName.Text;

            // Call the method to add the material to the database
            AddMaterialToDatabase(materialName, fileMaterial);

            // Bind the material list to display the newly added material
            BindMaterialList();

            // Clear input controls
            txtMaterialName.Text = "";
        }


        protected void AddAssignmentToDatabase(int assignmentID, string assignmentName, string assignmentDescription, string dueDate, FileUpload fileAssignment)
        {
            courseId = (int)Session["SelectedCourseID"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Check if the assignment with the provided ID already exists
                string checkAssignmentQuery = "SELECT COUNT(*) FROM Assignment WHERE AssignmentID = @AssignmentID";
                using (SqlCommand checkAssignmentCommand = new SqlCommand(checkAssignmentQuery, connection))
                {
                    checkAssignmentCommand.Parameters.AddWithValue("@AssignmentID", assignmentID);
                    int existingAssignmentCount = (int)checkAssignmentCommand.ExecuteScalar();

                    if (existingAssignmentCount > 0)
                    {
                        // Assignment with the provided ID already exists, display a message
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Assignment with the provided ID already exists.');", true);
                    }
                    else
                    {
                        // Assignment with the provided ID doesn't exist, perform an insert
                        string insertAssignmentQuery = "INSERT INTO Assignment (AssignmentID, AssignmentName, Description, DueDate, CreatedAt, FileData) VALUES (@AssignmentID, @AssignmentName, @Description, @DueDate, @CreatedAt, @FileData)";
                        using (SqlCommand insertAssignmentCommand = new SqlCommand(insertAssignmentQuery, connection))
                        {
                            // Set other parameters
                            insertAssignmentCommand.Parameters.AddWithValue("@AssignmentID", assignmentID);
                            insertAssignmentCommand.Parameters.AddWithValue("@AssignmentName", assignmentName);
                            insertAssignmentCommand.Parameters.AddWithValue("@Description", assignmentDescription);
                            insertAssignmentCommand.Parameters.AddWithValue("@DueDate", dueDate);
                            insertAssignmentCommand.Parameters.AddWithValue("@CreatedAt", DateTime.Now);

                            // Handle file data
                            if (fileAssignment.HasFile)
                            {
                                // Get the file data
                                byte[] fileBytes = fileAssignment.FileBytes;

                                // Store the file data in the database
                                insertAssignmentCommand.Parameters.AddWithValue("@FileData", fileBytes);
                            }
                            else
                            {
                                // No file selected
                                insertAssignmentCommand.Parameters.AddWithValue("@FileData", DBNull.Value);
                            }

                            insertAssignmentCommand.ExecuteNonQuery();
                        }

                        string insertAssignmentCourseQuery = "INSERT INTO Assignment_Course (AssignmentID, CourseID) VALUES (@AssignmentID, @CourseID)";
                        using (SqlCommand assignmentCourseCommand = new SqlCommand(insertAssignmentCourseQuery, connection))
                        {
                            assignmentCourseCommand.Parameters.AddWithValue("@AssignmentID", assignmentID);
                            assignmentCourseCommand.Parameters.AddWithValue("@CourseID", courseId); // Use the class-level courseId

                            assignmentCourseCommand.ExecuteNonQuery();
                        }

                        // Display a success message
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Assignment added successfully.');", true);
                    }
                }
            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            // Get the new message from the textbox
            // string newMessage = txtNewMessage.Text;

            // You can add the logic to save the new message to the database or process it as needed
            // For now, let's just display an alert with the message
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", $"alert('New Message: {newMessage}');", true);

            // Clear the textbox after sending the message
            // txtNewMessage.Text = "";
        }
        protected void btnAddAssignment_Click(object sender, EventArgs e)
        {
            // Get the values from the input controls
            string assignmentName = txtAssignmentName.Text;
            string assignmentDescription = txtAssignmentDescription.Text;
            string dueDate = txtDueDate.Text;
            int assignmentID = int.Parse(txtAssignmentID.Text);

            // Call the method to add the assignment to the database
            AddAssignmentToDatabase(assignmentID, assignmentName, assignmentDescription, dueDate, fileAssignment);
            BindAssignmentList();
            // Display a success message in a modal
            ScriptManager.RegisterStartupScript(this, this.GetType(), "displaySuccessModal", "openModal();", true);


            txtAssignmentName.Text = "";
            txtAssignmentDescription.Text = "";
            txtDueDate.Text = "";
        }
        protected void btnCreateAnnouncement_Click(object sender, EventArgs e)
        {
            // Get the announcement content from the textarea
            string announcementContent = txtAnnouncement.Value;

            // Check if the content is not empty
            if (!string.IsNullOrEmpty(announcementContent))
            {
                // Insert the announcement into the InstructorAnnouncement table
                InsertInstructorAnnouncement(announcementContent);

                // Bind the announcements to refresh the display
                BindAnnouncements();

                // Clear the textarea
                txtAnnouncement.Value = "";
            }
        }

        private void InsertInstructorAnnouncement(string content)
        {
            // Get the Instructor ID and course ID from the session
            int InstructorID = (int)Session["InstructorID"];
            int courseID = (int)Session["SelectedCourseID"];

            // Insert announcement into the InstructorAnnouncement table
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Query to insert the announcement with course ID
                string query = "INSERT INTO InstructorAnnouncement (InstructorID, Content, Timestamp, CourseID) VALUES (@InstructorID, @Content, GETDATE(), @CourseID); SELECT SCOPE_IDENTITY()";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@InstructorID", InstructorID);
                    command.Parameters.AddWithValue("@Content", content);
                    command.Parameters.AddWithValue("@CourseID", courseID);

                    int announcementID = Convert.ToInt32(command.ExecuteScalar());

                }
            }
        }


        private void BindAnnouncements()
        {
            string query = "SELECT AnnouncementID, UserID, Content, Timestamp, UserType, UserName " +
                                  "FROM (" +
                                      "SELECT sa.AnnouncementID, sa.StudentID AS UserID, sa.Content, sa.Timestamp, 'Student' AS UserType, s.StudentName AS UserName " +
                                      "FROM StudentAnnouncement sa " +
                                      "INNER JOIN Student s ON sa.StudentID = s.StudentID " +
                                      "WHERE sa.CourseID = @CourseID " +
                                      "UNION ALL " +
                                      "SELECT ia.AnnouncementID, ia.InstructorID AS UserID, ia.Content, ia.Timestamp, 'Instructor' AS UserType, i.InstructorName AS UserName " +
                                      "FROM InstructorAnnouncement ia " +
                                      "INNER JOIN Instructor i ON ia.InstructorID = i.InstructorID " +
                                      "WHERE ia.CourseID = @CourseID " +
                                  ") AS CombinedAnnouncements " +
                                  "ORDER BY Timestamp DESC"; 

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Set the course ID parameter
                    command.Parameters.AddWithValue("@CourseID", courseId);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Bind the data to the repeater
                    rptAnnouncements.DataSource = dataTable;
                    rptAnnouncements.DataBind();
                }
            }
        }
    }




}