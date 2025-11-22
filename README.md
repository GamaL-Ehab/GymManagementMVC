# 🏋️‍♂️ Gym Management System (ASP.NET MVC)

A scalable **Gym Management System** built using **ASP.NET MVC** with a clean 3-Layer Architecture (Presentation, Business Logic, and Data Access Layers), applying **Repository**, **Unit of Work**, and **Dependency Injection** patterns for maintainability and testability.  
Includes user authentication using **ASP.NET Identity**, role-based access (SuperAdmin, Admin), and modular features for managing members, plans, trainers, memberships, and workout sessions.

---

## 📋 Table of Contents
1. [Overview](#overview)
2. [System Features](#system-features)
3. [Architecture](#architecture)
4. [Design Patterns Used](#design-patterns-used)
5. [Project Structure](#project-structure)
6. [Technology Stack](#technology-stack)
7. [Setup Instructions](#setup-instructions)
8. [Database Schema](#database-schema)
9. [How the System Works](#how-the-system-works)
10. [Contributing](#contributing)
11. [License](#license)
12. [Author](#author)

---

## 🚀 Overview

The **GymManagementMVC** project is designed to provide gym owners with an efficient management solution for:

📌 Managing Members and Membership Plans  
📌 Tracking Trainers and Assigned Sessions  
📌 Handling Subscriptions, Sessions, Health Profiles  
📌 Monitoring Attendance and Session Availability  
📌 Providing secure authentication and authorization  

Built using **ASP.NET MVC**, it follows **3-layer architecture** to ensure modular development and maintainability, and uses **ASP.NET Identity** to manage authentication, roles, and permissions.  

---

## ✨ System Features

### 👤 Member Management
- Create, edit, delete members
- Health data (height, weight, blood type) management
- Membership details: plan, duration, expiry
- Upload member photos (Attachment Service)

### 💳 Membership Plans
- Manage plan information (duration, price, description)
- Track plan status (active/inactive)
- Toggle activation with one click

### 👨‍🏫 Trainer Management
- Manage trainer profiles and assigned sessions
- Contact info, specialization, availability

### 🗓️ Session Management
- Create and manage gym sessions
- Assign trainer, capacity, dates, duration
- Track available slots, time, and session categories.

### 🔒 Authentication & Authorization
- Users authenticate via login page using ASP.NET Identity
- Supports roles: SuperAdmin and Admin
- Authorization controls access to specific actions/pages

### 📎 Attachment Service
- Validates file extensions, size, creates unique names (GUID), stores securely, and removes on demand

---

## 🏗️ Architecture

The system follows the **3-Layer Architecture**: Presentation (MVC), Business Logic (BLL), and Data Access Layer (DAL).  
Each layer performs **distinct responsibilities**, ensuring loose coupling.

User → Presentation (UI) → Business Logic (Services) → Data Access → Database

- **Presentation Layer (ASP.NET MVC)**: Manages UI, view rendering, and controllers. No business logic here. 
- **Business Logic Layer (BLL)**: Core processing, validation, transformations, and service definitions.
- **Data Access Layer (DAL)**: Handles Entity Framework, DbContexts, repositories, CRUD operations. No business rules.

---

## 🔄 Design Patterns Used

### 📦 Repository Pattern  
Abstracts data operations and avoids duplicate data access code. Improves testability & maintainability.

### 🧾 Unit of Work  
Ensures multiple operations execute in a **single database transaction**. Improves performance & consistency.

### 💉 Dependency Injection  
Used for services, repositories, and controllers for loose coupling and testability.

---

## 📂 Project Structure

GymManagementMVC/
│
├── GymManagementPL/ → MVC Controllers, Views, ViewModels, wwwroot
│
├── GymManagementBLL/ → Services, DTOs, Business Logic, Validation
│
├── GymManagementDAL/ → DbContext, Entities, Repositories, Migrations
│
└── GymManagementMVC.sln

---

## 🛠️ Technology Stack

| Category | Technology |
|----------|------------|
| UI | ASP.NET MVC 8, Razor Views, Bootstrap 5, HTML5/CSS3 |
| Business Layer | C#, LINQ, Services, DTOs |
| Data Layer | Entity Framework Core, SQL Server |
| Security | ASP.NET Identity, Authentication/Authorization |
| Tools | Visual Studio 2026, GitHub |

---

## ⚙️ Setup Instructions

### 1️⃣ Clone the Repository
git clone https://github.com/GamaL-Ehab/GymManagementMVC.git
2️⃣ Configure the Database (Web.config / appsettings.json)
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=GymDB;Trusted_Connection=True;"
}
3️⃣ Run Migrations
Update-Database
4️⃣ Build and Run the Project
Press ▶️ (F5) in Visual Studio
Open https://localhost:xxxx

## 📊 Database Schema Overview

| Table        | Description                                      |
|--------------|--------------------------------------------------|
| Members      | Basic and health details of gym members          |
| Plans        | Pricing, duration, and description of gym plans  |
| Trainers     | Trainer information and specialization           |
| Sessions     | Date, time, trainer, and capacity allocation     |
| AspNetUsers  | User login and credentials (Identity)            |

🔍 How the System Works
🔐 Authentication Flow:
User enters credentials (email/password)

Identity validates against AspNetUsers

On success → Authentication cookie is created 🪙 
ASP MVC

Authorization checks role for page access (Admin, Trainer)

🤝 Contributing
Fork the project

Create your feature branch (feature/xyz)

Commit changes and push

Open a Pull Request 🚀

👨‍💻 Author
GamaL Ehab
Full Stack Developer | ASP.NET | Angular
📧 Contact: gamalehabg@gmail.com
🌐 GitHub: GamaL-Ehab

📌 If you like this project, give it a ⭐ on GitHub!
