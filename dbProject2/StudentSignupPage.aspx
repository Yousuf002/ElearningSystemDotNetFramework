<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentSignupPage.aspx.cs" Inherits="dbProject2.StudentSignupPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Instructor Signup</title>
    <style>
         body {
     font-family: Arial, sans-serif;
     background-color: white; /* Set a light background color */
     margin: 0;
     padding: 0;
     display: flex;
     align-items: center;
     justify-content: center;
     height: 100vh;
 }

 form {
     max-width: 600px;
     background-color: #ffffff;
     padding: 20px;
     border-radius: 8px;
     box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
     text-align: center;
 }

 h2 {
     color: #000066;
     margin-bottom:50px;
 }

 label {
     display: block;
     margin: 10px 0 5px;
     color: #000066;
 }

 input {
     width: 80%;
     padding: 8px;
     margin: 0 auto 15px;
     box-sizing: border-box;
     display: block; /* Ensure inputs are on new lines */
   
     height:40px;
 }
 #btnSignup {
     background-color: #000066;
     color: #ffffff;
     padding: 10px;
     border: none;
     border-radius: 4px;
     cursor: pointer;
     width: 40%;
     display: block; /* Ensure button is on a new line */
     margin: 0 auto; /* Center the button */
 }


    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Instructor Signup</h2>
            <div>
                <label for="txName">Name:</label>
                <asp:TextBox ID="txtName" runat="server" required="true"/>
            </div>
            <div>
                <label for="txtEmail">Username:</label>
                <asp:TextBox ID="txtEmail" runat="server" TextMode ="Email" required="true" />
            </div>
            <div>
                <label for="txtPassword">Password:</label>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" required ="true"/>
            </div>
            <div>
                <asp:Button ID="btnSignup" runat="server" Text="Signup" OnClick="btnSignup_Click" />
            </div>
        </div>
    </form>
</body>
</html>
