<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="instructorContent.aspx.cs" Inherits="dbProject2.WebForm4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Course Content</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            margin: 0;
            padding: 0;
            background-color: #f1f1f1;
            color: #000066; /* Set font color */
        }

        header {
            background-color: #000066;
            padding: 15px;
            text-align: center;
            color: white;
            font-size: 24px;
        }

        nav {
            background-color: #ffffff;
            padding: 15px;
            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
        }

        section {
            display: flex;
            justify-content: space-between;
            padding: 20px;
            margin: 20px;
        }

        .left-column {
            flex: 0.3; /* 30% width for the left column */
        }

        .right-column {
            flex: 0.7; /* 70% width for the right column */
        }

        .announcement,
        .material,
        .assignment,
        .discussion {
            padding: 10px;
            margin: 10px;
            background-color: #ffffff;
            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
            border-radius: 5px;
            overflow-y: auto;
        }

        .announcement {
            height: 95%; /* Set height to 100% to match the left column's height */
        }

        .chat-container {
            height: 350px;
            overflow-y: auto;
        }

        .message {
            margin: 5px;
            padding: 10px;
            background-color: #f1f1f1;
            border-radius: 5px;
        }

        .assignment .plus-sign {
            font-size: 30px;
            color: #000066;
            cursor: pointer;
            margin-left: 200px;
            border-radius: 10%;
            background-color: aliceblue;
            padding: 6px;
        }

        .material .plus-sign {
            font-size: 30px;
            color: #000066;
            cursor: pointer;
            margin-left: 242px;
            border-radius: 10%;
            background-color: aliceblue;
            padding: 6px;
        }

        .discussion button {
            padding: 10px;
            background-color: #000066;
            color: white;
        }

            .discussion button:hover {
                background-color: white;
                color: #000066;
            }

        .modal {
            display: none;
            position: fixed;
            z-index: 1;
            left: 0;
            top: 0;
            width: 100%;
            height: 100%;
            overflow: auto;
            background-color: rgba(0, 0, 0, 0.5);
            padding-top: 50px;
        }

        .modal-content {
            background-color: #fefefe;
            margin: 5% auto;
            padding: 20px;
            border: 1px solid #888;
            width: 80%;
        }

        .close {
            color: #aaa;
            float: right;
            font-size: 28px;
            font-weight: bold;
        }

            .close:hover,
            .close:focus {
                color: black;
                text-decoration: none;
                cursor: pointer;
            }

        .modal-content button {
            height: 30px;
            background-color: #000066;
            border-radius: 8%;
            color: white;
        }

        .modal-content {
            background-color: #ffffff;
            margin: 5% auto;
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
            text-align: left;
        }

            .modal-content h2 {
                color: #000066;
                margin-bottom: 20px;
            }

            .modal-content label {
                display: block;
                margin: 10px 0 5px;
                color: #000066;
            }

            .modal-content input {
                width: 80%;
                padding: 8px;
                margin-bottom: 15px;
                box-sizing: border-box;
                display: block;
                height: 40px;
            }

            .modal-content button {
                background-color: #000066;
                color: #ffffff;
                padding: 10px;
                border: none;
                border-radius: 4px;
                cursor: pointer;
                width: 40%;
                display: block;
                margin: 0 auto;
            }

                .modal-content button:hover {
                    background-color: #003366;
                }

        .close {
            color: #aaa;
            float: right;
            font-size: 28px;
            font-weight: bold;
        }

            .close:hover,
            .close:focus {
                color: black;
                text-decoration: none;
                cursor: pointer;
            }

        #assignmentList li {
            margin-bottom: 10px;
            padding: 10px;
            background-color: #ffffff;
            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
            border-radius: 5px;
            overflow-y: auto;
        }

        .assignment-list {
            list-style-type: initial;
            padding: 0;
        }

            .assignment-list p {
                color: aqua;
            }

        ListBox {
            margin-bottom: 15px;
            padding: 15px;
            background-color: black;
            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
            border-radius: 8px;
            overflow-y: auto;
            cursor: pointer;
            transition: background-color 0.3s ease;
        }

        .assignment-item:hover {
            background-color: #f0f0f0;
        }

        .assignment-item strong {
            font-size: 18px;
            color: #000066;
        }

        .assignment-item p {
            margin: 0;
            color: #333;
        }

        .assignment-item span {
            display: block;
            font-size: 1px;
            color: #777;
        }

        .assignment-list li {
            margin-bottom: 10px;
            padding: 10px;
            background-color: #ffffff;
            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
            border-radius: 5px;
            overflow-y: auto;
        }

        .assignment-list {
            list-style-type: initial;
            padding: 0;
        }

            .assignment-list p {
                color: aqua;
            }

        .assignment-item span {
            display: block;
            font-size: 14px;
            color: #777;
        }

        .plus-sign {
            font-size: 24px;
            color: #000066;
            cursor: pointer;
            border-radius: 50%;
            background-color: aliceblue;
            padding: 8px;
            transition: background-color 0.3s ease;
        }

            .plus-sign:hover {
                background-color: #e0f0ff;
            }

        .assignment-list .assignment-item {
            margin-bottom: 10px;
            padding: 10px;
            background-color: #ffffff;
            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
            border-radius: 5px;
            overflow-y: auto;
            background-color: black;
        }

        .assignment-list {
            list-style-type: initial;
            padding: 0;
        }

        .announcement-container {
            margin-top: 20px;
        }

            .announcement-container textarea {
                width: 100%;
            }

            .announcement-container button {
                margin-top: 10px;
                padding: 10px;
                background-color: #000066;
                color: white;
                cursor: pointer;
            }

            .announcement-container .announcement-list {
                list-style-type: none;
                padding: 0;
            }

            .announcement-container .announcement-item {
                margin-bottom: 10px;
                padding: 10px;
                background-color: #ffffff;
                box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
                border-radius: 5px;
                overflow-y: auto;
            }

                .announcement-container .announcement-item p {
                    margin: 0;
                }

        #announcementsSection {
            background-color: #f2f2f2;
            padding: 20px;
            border-radius: 8px;
            margin-bottom: 20px;
        }

        #announcementInput {
            margin-bottom: 20px;
        }

        #txtAnnouncement {
            width: 100%;
            padding: 5px;
            box-sizing: border-box;
        }

        #btnPostAnnouncement {
            margin-top: 10px;
            padding: 10px 10px;
            background-color: #000066;
            color: white;
            border: none;
            border-radius: 3px;
            cursor: pointer;
        }

        /* Style for the announcements display area */
        #announcementsContainer {
            max-width: 600px;
            margin: 0 auto;
        }

        .announcement {
            border: 1px solid #ddd;
            padding: 10px;
            margin-bottom: 10px;
            border-radius: 5px;
        }

            .announcement strong {
                color: #333;
                font-size: 14px;
            }

            .announcement hr {
                margin: 10px 0;
                border: none;
                border-top: 1px solid #ddd;
            }


        /* Rest of your styles... */
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <header>
            Course Name - 
  <asp:Literal ID="courseNameLiteral" runat="server" Text=""></asp:Literal>
            <span id="courseIdLabel" style="color: #999;"></span>
        </header>


        <section>
            <div class="left-column">

                <div class="material">
                    <h2>Materials <span class="plus-sign" onclick="openMaterialModal()">&#43;</span></h2>
                    <asp:ListBox ID="materialList" runat="server" BackColor="#E1EAEA" ForeColor="#000066" Height="170px" Width="359px" onclick="openMaterialFile()"></asp:ListBox>

                    <!-- Updated material content here -->
                    <div id="materialModal" class="modal">
                        <div class="modal-content">
                            <span class="close" onclick="closeMaterialModal()">&times;</span>
                            <h2>Add Material</h2>
                            <asp:FileUpload ID="fileMaterial" runat="server" Accept=".pdf" />
                            <!-- Add any additional input fields for material details -->
                            <asp:TextBox ID="txtMaterialName" runat="server" placeholder="Material Name"></asp:TextBox>
                            <asp:Button ID="btnAddMaterial" runat="server" Text="Add Material" OnClick="btnAddMaterial_Click" />
                        </div>
                    </div>
                </div>
                <div class="assignment">
                    <h2>Assignments <span class="plus-sign" onclick="openAssignmentModal()">&#43;</span></h2>
                    <!-- Updated assignment content here -->

                    <asp:ListBox ID="assignmentList" runat="server" BackColor="#E1EAEA" ForeColor="#000066" Height="170px" Width="359px" />


                    <!-- Add the modal for adding new assignments -->
                    <div id="assignmentModal" class="modal">
                        <div class="modal-content">
                            <span class="close" onclick="closeAssignmentModal()">&times;</span>
                            <h2>Add Assignment</h2>
                            <div>
                                <label for="txtAssignmentID">Assignment ID:</label>
                                <asp:TextBox ID="txtAssignmentID" runat="server"></asp:TextBox>
                            </div>
                            <div>
                                <label for="txtAssignmentName">Assignment Name:</label>
                                <asp:TextBox ID="txtAssignmentName" runat="server"></asp:TextBox>
                            </div>
                            <div>
                                <label for="txtAssignmentDescription">Assignment Description:</label>
                                <asp:TextBox ID="txtAssignmentDescription" runat="server" TextMode="MultiLine" Rows="4"></asp:TextBox>
                            </div>
                            <div>
                                <label for="txtDueDate">Due Date:</label>
                                <asp:TextBox ID="txtDueDate" runat="server" TextMode="Date"></asp:TextBox>
                            </div>
                            <div>
                                <label for="fileAssignment">Upload Assignment (PDF):</label>
                                <asp:FileUpload ID="fileAssignment" runat="server" Accept=".pdf" />
                            </div>
                            <asp:Button ID="btnAddAssignment" runat="server" Text="Add Assignment" OnClick="btnAddAssignment_Click" />
                        </div>
                    </div>
                </div>
            </div>
            <div id="customModal" class="modal">
                <div class="modal-content">
                    <span class="close" onclick="closeCustomModal()">&times;</span>
                    <h2>Assignment Details</h2>
                    <div>
                        <label>Assignment Name:</label>
                            <span id="customAssignmentName"></span>
                        </div>
                        <div>
                            <label>Description:</label>
                            <span id="customAssignmentDescription"></span>
                        </div>
                        <div>
                            <label>Due Date:</label>
                            <span id="customDueDate"></span>
                        </div>
                        <div>
                            <label>File Data:</label>
                            <span id="customFileData"></span>
                        </div>
                    </div>
            </div>
            <div class="right-column">

                <div class="announcement">
                    <div class="announcement-form">
                        <h2>Create Announcement</h2>
                        <textarea id="txtAnnouncement" runat="server" placeholder="Type your announcement here" rows="4"></textarea>
                        <br />
                        <asp:Button ID="btnCreateAnnouncement" runat="server" Text="Create Announcement" OnClick="btnCreateAnnouncement_Click" />
                    </div>
                    <h2>Announcements</h2>
                    <!-- Display announcements dynamically -->
                    <asp:Repeater ID="rptAnnouncements" runat="server">
                        <ItemTemplate>
                            <p>
                                <strong><%# Eval("UserName") %>:</strong>
                                <%# Eval("Content") %>
                                <span style="color: #888;">(<%# Eval("Timestamp") %>)</span>
                            </p>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>

            </div>
        </section>
    </form>
    <div id="myModal" class="modal">
        <div class="modal-content">
            <span class="close" onclick="closeModal()">&times;</span>
            <h2>Add Files</h2>
            <!-- Add your file input and other content here -->
            <input type="file" />
            <button>Upload</button>
        </div>
    </div>
    <!-- ... -->

    <script>
        if (window.history.replaceState) {
            window.history.replaceState(null, null, window.location.href);
        }
        function postAnnouncement() {
            var newAnnouncement = document.getElementById('txtNewAnnouncement').value;

            // Validate if the announcement is not empty
            if (newAnnouncement.trim() !== '') {
                // Add the new announcement to the list
                var announcementList = document.getElementById('announcementList');
                var listItem = document.createElement('li');
                listItem.className = 'announcement-item';
                listItem.innerHTML = `<p>${newAnnouncement}</p>`;
                announcementList.appendChild(listItem);

                // Clear the text area
                document.getElementById('txtNewAnnouncement').value = '';
            } else {
                // Handle empty announcement
                alert('Please enter a valid announcement.');
            }
        }
        var strongElement;

        function openAssignmentModal() {
            document.getElementById('assignmentModal').style.display = 'block';
        }

        // Function to close the assignment modal
        function closeAssignmentModal() {
            document.getElementById('assignmentModal').style.display = 'none';
        }

        // Function to display assignment details in the custom modal
        function displayAssignmentDetails(assignmentName, assignmentDescription, dueDate, fileData) {
            document.getElementById('customAssignmentName').innerText = assignmentName;
            document.getElementById('customAssignmentDescription').innerText = assignmentDescription;
            document.getElementById('customDueDate').innerText = dueDate;
            //document.getElementById('customFileData').innerText = fileData;

            // Check if fileData is present and is a valid File object
            if (fileData && fileData instanceof File) {
                // Display file details
                document.getElementById('customFileData').innerText = `File Name: ${fileData.name}, Size: ${fileData.size} bytes, Type: ${fileData.type}`;
            } else {
                // No file selected
                document.getElementById('customFileData').innerText = 'No file selected';
            }

            // Open the custom modal
            openCustomModal();
        }
        // Function to add assignment dynamically
        // Function to add assignment dynamically
        function addAssignment() {
            console.log("Adding assignment");

            // Get assignment details from the modal inputs
            var assignmentID = document.getElementById('txtAssignmentID').value;
            var assignmentName = document.getElementById('txtAssignmentName').value;
            var assignmentDescription = document.getElementById('txtAssignmentDescription').value;
            var dueDate = document.getElementById('txtDueDate').value;
            var fileInput = document.getElementById('fileAssignment');
            var fileData = fileInput.files[0];

            // Ensure that a file is selected
            if (fileData) {
                // Create a new list item to display the assignment
                var assignmentList = document.getElementById('assignmentList');
                var listItem = document.createElement('li');
                listItem.className = "assignment-item";
                listItem.innerHTML = `<strong>${assignmentName}</strong><br>ID: ${assignmentID}<br>Description: ${assignmentDescription}<br>Due Date: ${dueDate}<br>Date Created: ${new Date().toISOString()}<br>`;

                // Add data attributes to the new assignment
                listItem.setAttribute('data-assignmentID', assignmentID);
                listItem.setAttribute('data-assignmentName', assignmentName);
                listItem.setAttribute('data-description', assignmentDescription);
                listItem.setAttribute('data-dueDate', dueDate);

                // Append the new assignment to the list
                assignmentList.appendChild(listItem);

                // Store file details in a separate data attribute
                listItem.setAttribute('data-file-name', JSON.stringify({
                    name: fileData.name,
                    size: fileData.size,
                    type: fileData.type
                }));
                console.log("Assignment added successfully");
            } else {
                console.log("No file selected");
            }

            // Close the assignment modal
            closeAssignmentModal();
        }
        function openMaterialFile() {
            var materialList = document.getElementById('materialList');
            var selectedIndex = materialList.selectedIndex;

            // Check if an item is selected
            if (selectedIndex !== -1) {
                // Get the selected item
                var selectedItem = materialList.options[selectedIndex];

                // Get the material ID and name from data attributes
                var materialID = selectedItem.getAttribute('data-materialID');
                var materialName = selectedItem.getAttribute('data-materialName');
                var fileData = selectedItem.getAttribute('data-fileData');

                // Check if fileData is present
                if (fileData) {
                    // Convert base64 string to byte array
                    var byteCharacters = atob(fileData);
                    var byteNumbers = new Array(byteCharacters.length);
                    for (var i = 0; i < byteCharacters.length; i++) {
                        byteNumbers[i] = byteCharacters.charCodeAt(i);
                    }
                    var byteArray = new Uint8Array(byteNumbers);

                    // Create a blob from the byte array
                    var blob = new Blob([byteArray], { type: 'application/pdf' });

                    // Create a URL for the blob
                    var blobUrl = URL.createObjectURL(blob);

                    // Open the PDF file in a new tab
                    window.open(blobUrl, '_blank');
                } else {
                    // Handle the case where no file is present
                    console.log("No file data available for the selected material.");
                }
            }
        }

        function handleFileInput() {
            var fileInput = document.getElementById('fileAssignment');
            var customLabel = document.getElementById('customFileInputLabel');

            if (fileInput.files.length > 0) {
                var fileName = fileInput.files[0].name;
                customLabel.innerText = `Selected file: ${fileName}`;
            } else {
                customLabel.innerText = 'No file selected';
            }
        }

        // Run this function after the page is loaded to bind click events to existing assignments
        document.addEventListener('DOMContentLoaded', function () {
            var assignmentList = document.getElementById('assignmentList');

            assignmentList.addEventListener('click', function (event) {
                var clickedItem = event.target;
                strongElement = this.querySelector('strong');
                // Extract assignment details from data attributes
                var assignmentID = clickedItem.getAttribute('data-assignmentID') || 'N/A';
                //  var assignmentName = strongElement ? strongElement.innerText : 'N/A';
                var description = clickedItem.getAttribute('data-description') || 'N/A';
                var assignmentName = clickedItem.getAttribute('data-assignmentName') || 'N/A';
                var dueDate = clickedItem.getAttribute('data-dueDate') || 'N/A';
                var fileData = clickedItem.getAttribute('data-file-name') || 'N/A';
                // Display the assignment details in the custom modal
                displayAssignmentDetails(assignmentName, description, dueDate, fileData);
            });
        });
        // Function to open the custom modal
        function openCustomModal() {
            document.getElementById('customModal').style.display = 'block';
        }

        // Function to close the custom modal
        function closeCustomModal() {
            document.getElementById('customModal').style.display = 'none';
        }
        function openMaterialModal() {
            document.getElementById('materialModal').style.display = 'block';
        }

        function closeMaterialModal() {
            document.getElementById('materialModal').style.display = 'none';
        }
    </script>

    <!-- ... -->


</body>
</html>
