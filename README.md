# Help Desk Ticket Management System 🎫

A complete full-stack enterprise support ticket management solution. This project allows employees to raise, track, and manage software, hardware, and network-related support requests through a responsive web interface.

## 🏗️ Solution Architecture

The solution is built using a decoupled, multi-tier architecture to ensure separation of concerns. Below is the complete repository structure:

```text
HelpDeskManagement/
├── HelpDesk.Api/                   # ASP.NET Core Web API
│   ├── Controllers/
│   │   └── TicketController.cs     # REST API Endpoints (/api/Ticket/...)
│   ├── Data/
│   │   └── HelpDeskDbContext.cs    # EF Core DbContext
│   ├── Migrations/                 # EF Core Migrations
│   ├── Models/
│   │   └── Ticket.cs               # Core Ticket Entity Model
│   ├── Repositories/
│   │   ├── ITicketRepository.cs    # Repository Interface
│   │   └── TicketRepository.cs     # Repository Implementation (EF Core)
│   ├── appsettings.json            # Database connection & config
│   └── Program.cs                  # DI, CORS & Swagger configuration
├── HelpDesk.Mvc/                   # ASP.NET Core MVC Application
│   ├── Controllers/
│   │   ├── HomeController.cs       # Dashboard metrics & analytics
│   │   └── TicketController.cs     # Ticket management UI actions
│   ├── Models/
│   │   ├── DashboardViewModel.cs   # Metric summary view model
│   │   └── Ticket.cs               # Validated Ticket model
│   ├── Services/
│   │   ├── ITicketService.cs       # Service Layer Interface
│   │   └── TicketService.cs        # HttpClient API Consumer
│   ├── Views/
│   │   ├── Home/
│   │   │   └── Index.cshtml        # Dashboard (Total, Open, Closed stats)
│   │   ├── Ticket/
│   │   │   ├── Index.cshtml        # View All Tickets
│   │   │   ├── Details.cshtml      # View Ticket Details
│   │   │   ├── Create.cshtml       # Raise Ticket (Status="Open")
│   │   │   ├── Edit.cshtml         # Edit Ticket (Status/Priority dropdowns)
│   │   │   ├── Delete.cshtml       # Delete Confirmation
│   │   │   └── Filter.cshtml       # Filter Tickets by Status
│   │   └── Shared/
│   │       ├── _Layout.cshtml      # Responsive Bootstrap 5 Layout
│   │       └── _ValidationScriptsPartial.cshtml
│   ├── appsettings.json            # API BaseUrl configuration
│   └── Program.cs                  # Typed HttpClient registration
├── HelpDesk.Tests/                 # xUnit & Moq Unit Test Project
│   └── TicketControllerTests.cs    # Unit tests covering all controller endpoints
├── HelpDeskManagement.sln          # Visual Studio Solution File
├── .gitignore                      # Git ignore rules for .NET
└── README.md                       # System documentation
```

## 🚀 Key Features

*   **Interactive Dashboard:** Displays real-time metrics for Total, Open, and Closed tickets.
*   **Ticket Lifecycle Management:** Create, Read, Update, and Delete (CRUD) support requests.
*   **Dynamic Filtering:** Instantly filter the ticket list by status (Open, In Progress, Closed).
*   **Status Enforcement:** System strictly defaults new tickets to an "Open" status.
*   **Robust Unit Testing:** Validates API responses (Ok, NotFound, BadRequest) using xUnit and Moq.

## 💻 Technologies Used

*   **Framework:** .NET 10.0
*   **Backend:** C#, ASP.NET Core Web API
*   **Frontend:** ASP.NET Core MVC, Razor Pages, HTML5, CSS3, Bootstrap 5
*   **Database ORM:** Entity Framework Core
*   **Database:** MS SQL Server (LocalDB)
*   **Testing:** xUnit, Moq
*   **Version Control:** Git, GitHub

---

## ⚙️ Prerequisites

To run this project locally, ensure you have the following installed:
*   [Visual Studio 2022](https://visualstudio.microsoft.com/) (or newer) with the **ASP.NET and web development** workload.
*   [.NET 10.0 SDK](https://dotnet.microsoft.com/download)
*   SQL Server Express or LocalDB (included with Visual Studio).

---

## 🛠️ Getting Started

Follow these steps to clone the repository, set up the database, and run the application.

### 1. Clone the Repository
Open your terminal or Git Bash and run the following commands:
```bash
git clone https://github.com/your-username/HelpDeskManagement.git
cd HelpDeskManagement
```

### 2. Open the Solution
*   Open Visual Studio.
*   Click **Open a project or solution**.
*   Navigate to the cloned folder and open the `HelpDeskManagement.sln` file.

### 3. Database Setup (Migrations)
The API is configured to use LocalDB by default. To generate the SQL Server database and tables:
1.  In Visual Studio, go to **Tools** > **NuGet Package Manager** > **Package Manager Console**.
2.  Ensure the **Default project** dropdown at the top of the console is set to `HelpDesk.Api`.
3.  Run the following command to apply the migration and create the database:
```powershell
Update-Database
```

### 4. Run the Application
To experience the full system, both the API and MVC projects need to run simultaneously. 
1.  Right-click the **HelpDeskManagement** solution in the Solution Explorer.
2.  Select **Configure Startup Projects...**
3.  Choose **Multiple startup projects**.
4.  Set the action for both `HelpDesk.Api` and `HelpDesk.Mvc` to **Start**.
5.  Click **OK**, then press **F5** (or the green Start button) to launch both applications. 

*(Note: Ensure the API port running in your browser matches the `BaseAddress` configured in your MVC project's `TicketService` or `appsettings.json`).*

---

## 📡 API Endpoints

The `HelpDesk.Api` exposes the following RESTful endpoints:

| HTTP Method | Endpoint | Description |
| :--- | :--- | :--- |
| `GET` | `/api/Ticket/All` | Retrieves all tickets |
| `GET` | `/api/Ticket/{id}` | Retrieves a specific ticket by its ID |
| `POST` | `/api/Ticket` | Creates a new support ticket |
| `PUT` | `/api/Ticket/{id}` | Updates an existing ticket |
| `DELETE` | `/api/Ticket/{id}` | Permanently deletes a ticket |
| `GET` | `/api/Ticket/Status/{status}` | Filters and retrieves tickets by status |