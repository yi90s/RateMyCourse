# cReg™
cReg™ is an application that enables university students to efficiently and effectively register for courses and view their courses over the internet.

# Progress
- Right now, we are focusing on building our website. You can check out our progress in [here](https://github.com/MQuizzle/Gr8Group/projects/1)
- [Milestone-1](https://github.com/MQuizzle/Gr8Group/milestone/1)
- [Milestone-2](https://github.com/MQuizzle/Gr8Group/milestone/2)

# Design
- MVC pattern
- [ER Diagram](https://github.com/MQuizzle/Gr8Group/blob/master/Documents/entity%20diagram.PNG)
- [DFD](https://github.com/MQuizzle/Gr8Group/blob/master/Documents/DFD.png)
- [Page Diagram](https://github.com/MQuizzle/Gr8Group/blob/master/Documents/page_diagram.png)
- [System Diagram](https://github.com/MQuizzle/Gr8Group/blob/master/Documents/system_diagram.png)

# Code File Route
- Controllers
     - Logic
         - [Advisor.cs](https://github.com/MQuizzle/Gr8Group/blob/master/Controllers/Logic/Advisor.cs)
     - [CourseController.cs](https://github.com/MQuizzle/Gr8Group/blob/master/Controllers/CourseController.cs)
     - [HomeController.cs](https://github.com/MQuizzle/Gr8Group/blob/master/Controllers/HomeController.cs)
- Models
     - ViewModels
         - [ErrorViewModel.cs](https://github.com/MQuizzle/Gr8Group/blob/master/Models/ViewModels/ErrorViewModel.cs)
	       - [ViewModelBase.cs](https://github.com/MQuizzle/Gr8Group/blob/master/Models/ViewModels/ViewModelBase.cs)
	       - [rateCourseViewModel.cs](https://github.com/MQuizzle/Gr8Group/blob/master/Models/ViewModels/rateCourseViewModel.cs)
     - entities
         - [Course.cs](https://github.com/MQuizzle/Gr8Group/blob/master/Models/entities/Course.cs)
	       - [Enrolled.cs](https://github.com/MQuizzle/Gr8Group/blob/master/Models/entities/Enrolled.cs)
	       - [Faculty.cs](https://github.com/MQuizzle/Gr8Group/blob/master/Models/entities/Faculty.cs)
	       - [Prerequisite.cs](https://github.com/MQuizzle/Gr8Group/blob/master/Models/entities/Prerequisite.cs)
	       - [Required.cs](https://github.com/MQuizzle/Gr8Group/blob/master/Models/entities/Required.cs)
	       - [Student.cs](https://github.com/MQuizzle/Gr8Group/blob/master/Models/entities/Student.cs)
- Persistence/Contexts
     - [DataContext.cs] (https://github.com/MQuizzle/Gr8Group/blob/master/Persistence/Contexts/DataContext.cs)
- Test
     - Infrastructure
         - [DataContextTest.cs](https://github.com/MQuizzle/Gr8Group/blob/master/Tests/Infrastructure/DataContextTest.cs)
	       - [TestBase.cs](https://github.com/MQuizzle/Gr8Group/blob/master/Tests/Infrastructure/TestBase.cs)
     - UnitTests/Controller
         - [HomeControllerTests.cs](https://github.com/MQuizzle/Gr8Group/blob/master/Tests/UnitTests/Controller/HomeControllerTests.cs)
	 
  
# Installing Environment
- [.NET Core 3.1](https://dotnet.microsoft.com/download)

# Tools:
- Technologies
  - ASP.NET Core
  - AWS EC2
  - AWS RDS
  
- Testing Tools
  - Unit Test: xUnit
  - Intergration Test: In Progress
  - System Test: In Progress

# Login Information
- Website Link: http://cregwebapp-prod.us-east-2.elasticbeanstalk.com/
- Username: **1**  
- Password: **password**
  
# Related Documents
- [Vision Statement](https://github.com/MQuizzle/Gr8Group/blob/master/Documents/Vision-Statement.md)
- [Function Documents](https://github.com/MQuizzle/Gr8Group/blob/master/Documents/FunctionDoc.md)
- [Server Information](https://github.com/MQuizzle/Gr8Group/blob/master/Documents/Server-README.md)
- [Proposal Document](https://github.com/MQuizzle/Gr8Group/blob/master/Documents/Proposal%20Document.pdf)
