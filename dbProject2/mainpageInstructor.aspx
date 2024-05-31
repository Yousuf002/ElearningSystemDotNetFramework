<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mainpageInstructor.aspx.cs" Inherits="dbProject2.mainpageInstructor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Main Page for Instructor</title>
    <script>
        function redirectToCourseContent(courseID) {
            // Redirect to CourseContentPage.aspx with the selected CourseID
            window.location.href = 'instructorContent.aspx?courseID=' + courseID;
        }
    </script>
    <style>
        /* Your existing styles here */
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
            text-decoration: none;
        }

            nav a {
                text-decoration: none;
                color: #000066;
                margin-left: 10px;
                margin-right: 10px;
            }

        section {
            display: flex;
            justify-content: space-between;
            padding: 20px;
            margin: 20px;
        }

        .course {
            flex: 0.7; /* 70% width for courses */
            padding: 10px;
            margin: 10px;
            background-color: #ffffff;
            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
            border-radius: 5px;
        }

            .course p {
                height: 100px;
                font-size: 30px;
                font-weight: bold;
                padding-top: 50px;
                padding-left: 10px;
                background-color: ghostwhite;
                border-radius: 9px;
            }

                .course p:hover {
                    background-color: aliceblue;
                }

        .add-course {
            flex: 0.3; /* 30% width for the add course section */
            padding: 10px;
            margin: 10px;
            background-color: #ffffff;
            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
            border-radius: 5px;
        }

            .add-course input {
                width: 80%;
                padding: 8px;
                margin-bottom: 10px;
                box-sizing: border-box;
            }

            .add-course button {
                background-color: #000066;
                color: #ffffff;
                padding: 10px;
                border: none;
                border-radius: 4px;
                cursor: pointer;
                width: 80%;
            }

                .add-course button:hover {
                    background-color: #003366; /* Darker shade on hover */
                }
    </style>
</head>
<body>
    <form id="form2" runat="server">
        <header>
            Welcome to E-Learning System
        </header>

        <section>
            <div class="course" runat="server" id="courseDiv">
                <h2>My Courses</h2>
                <!-- Courses will be dynamically added here -->
            </div>
            <div class="add-course">
                <h2>Add New Course</h2>
                <asp:TextBox runat="server" ID="txtNewCourseName" placeholder="Course Name" required="true" />
                <asp:TextBox runat="server" ID="txtNewCourseID" placeholder="Course ID" required="true" />
                <asp:TextBox runat="server" ID="txtNewCourseDescription" placeholder="Course Description" required="true" />

                <asp:Button ID="btnAddCourse" runat="server" Text="Add Course" OnClick="btnAddCourse_Click" />

            </div>
        </section>
    </form>
</body>
</html>
