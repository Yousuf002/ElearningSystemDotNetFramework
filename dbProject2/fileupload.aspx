<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="fileupload.aspx.cs" Inherits="dbProject2.fileupload" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Upload Assignment</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            margin: 20px;
        }

        h2 {
            color: #333;
        }

        div {
            margin-bottom: 15px;
        }

        label {
            display: inline-block;
            width: 150px;
            margin-bottom: 5px;
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
            background-color: rgb(0,0,0);
            background-color: rgba(0,0,0,0.4);
            padding-top: 60px;
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
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Upload Assignment</h2>
            
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

            <!-- Listbox to display uploaded assignments -->
            <asp:ListBox ID="assignmentList" runat="server" BackColor="#E1EAEA" ForeColor="#000066" Height="170px" Width="359px" OnSelectedIndexChanged="assignmentList_SelectedIndexChanged" AutoPostBack="true" />

            <!-- Add the modal for displaying assignment details -->
            <div id="assignmentDetailsModal" class="modal">
                <div class="modal-content">
                    <span class="close" onclick="closeAssignmentDetailsModal()">&times;</span>
                    <h2>Assignment Details</h2>
                    <div>
                        <label>Assignment Name:</label>
                        <span id="assignmentDetailsName"></span>
                    </div>
                    <div>
                        <label>Description:</label>
                        <span id="assignmentDetailsDescription"></span>
                    </div>
                    <div>
                        <label>Due Date:</label>
                        <span id="assignmentDetailsDueDate"></span>
                    </div>
                    <div>
                        <label>File Data:</label>
                        <span id="assignmentDetailsFileData"></span>
                    </div>
                </div>
            </div>

        </div>
    </form>

    <script>
        // JavaScript functions for opening/closing the assignment details modal
        function openAssignmentDetailsModal() {
            document.getElementById('assignmentDetailsModal').style.display = 'block';
        }

        function closeAssignmentDetailsModal() {
            document.getElementById('assignmentDetailsModal').style.display = 'none';
        }
    </script>
</body>
</html>
