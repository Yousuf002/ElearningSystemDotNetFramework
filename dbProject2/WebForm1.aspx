<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="dbProject2.WebForm1" %>

<<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title></title>
    <style>
        .card {
            width: 400px;
            padding: 20px;
            border-radius: 10px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
            background-color: #fff;
            display: flex;
            flex-direction: column;
            align-items: center;
            justify-content: center;
            text-align: center;
            font-family: Arial, sans-serif;
            margin: auto;
            margin-top: 100px;
        }

        .buttonStyle {
            padding: 15px;
            margin: 10px;
            background-color: #000066;
            color: white;
            border: none;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 16px;
            cursor: pointer;
            font-weight: bold;
        }

        #label1{
            font-weight:bold;
            font-size:24px;
        }
        /* Change the background color on hover */
        .buttonStyle:hover {
            background-color: #f5f5f5;
            color: black;   
            border-style: solid;
            border-width: 1px;
            border-color: black;
        }
    </style>
</head>

<body style="background-color: #f2f2f2;">
    <form id="form1" runat="server">
        <div class="card">
            <asp:Label ID="Label1" runat="server" BackColor="White"  ForeColor="#000066" Text="E-Learning System"></asp:Label>
            <asp:Label ID="Label2" runat="server" BackColor="White" ForeColor="#000066" Text="powered by Datasoft Solutions"></asp:Label>
            <asp:Label ID="Label3" runat="server" BackColor="White" ForeColor="#000066" Text="Select Your Role"></asp:Label>

            <div style="margin-top: 20px;">
                <asp:Button ID="Button2" runat="server" Text="Instructor" CssClass="buttonStyle" OnClick="Button2_Click" />
                <asp:Button ID="Button3" runat="server" Text="Student" CssClass="buttonStyle" OnClick="Button3_Click" />
            </div>
        </div>
    </form>
</body>

</html>

