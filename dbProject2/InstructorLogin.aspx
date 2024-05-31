<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InstructorLogin.aspx.cs" Inherits="dbProject2.InstructorLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Instructor Login</title>
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
            margin-bottom: 50px;
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

            height: 40px;
        }

        #btnLogin {
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

            #btnLogin:hover {
                background-color: #003366; /* Darker shade on hover */
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Instructor Login</h2>
            <div>
                <label for="txtEmail">Email:</label>
                <asp:TextBox ID="txtEmail" runat="server" />
            </div>
            <div>
                <label for="txtPassword">Password:</label>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" />
            </div>
            <div>
                <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" />
            </div>
            <div>
                <asp:Label ID="lblMessage" runat="server" ForeColor="Red" />
            </div>
            <div>
                  <p>dont have account?</p>
                <asp:Button ID="btnSignup" runat="server" Text="Signup" OnClick="btnSignup_Click" />
            </div>
        </div>
    </form>
</body>
</html>
