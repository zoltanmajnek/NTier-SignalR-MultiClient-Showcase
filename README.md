# NTier-CRUD-Architecture

> **About this project:** This repository showcases an advanced, multi-tier C# .NET application demonstrating clean architecture, data separation, and comprehensive testing methodologies. It was originally developed in 2023 as a capstone project for the *Advanced Development Techniques* course at **Óbudai Egyetem** (Óbuda University).

## 📖 Project Overview

This project serves as an example of an **N-Tier (Multi-Tier) Architecture** built with C# and .NET. It highlights the implementation of the Repository Pattern, RESTful API endpoints, and a separated client-server communication model. 

A key feature of this repository is its demonstration of backend reusability and real-time capabilities: the core API is consumed by **three distinct client applications** (Console, WPF Desktop, and Web/JS). Furthermore, it utilizes **SignalR** to push live updates to connected clients, proving the flexibility and true decoupling of the presentation layer from the business logic.

## 🏗️ Architecture & Project Structure

The solution is divided into distinct layers to enforce the separation of concerns, ensuring that each component has a single responsibility:

### Core & Backend
* **`.Models`**: Contains the core domain entities and data structures. This layer has no dependencies on other layers.
* **`.Repository`**: Handles all direct data access logic. It abstracts the underlying database or data storage mechanism, providing a clean interface for data manipulation (CRUD).
* **`.Logic`**: The business logic layer. It acts as an intermediary between the Repository and the Endpoints, enforcing business rules and data validation.
* **`.Endpoint`**: An ASP.NET Core Web API project exposing the business logic via RESTful HTTP endpoints. It also hosts **SignalR Hubs** to broadcast real-time data changes across all connected clients.

### Presentation Layer (Clients)
* **`.Client` (Console)**: A console-based application demonstrating standard synchronous web communication with the REST API.
* **`.WpfClient` (Desktop)**: A rich desktop application built with Windows Presentation Foundation (WPF), providing a graphical user interface and data binding to consume the backend services.
* **`.JSClient` (Web)**: A frontend web client utilizing JavaScript, HTML, and CSS to interact with the backend, demonstrating cross-platform accessibility and DOM manipulation based on API data.

### Quality Assurance
* **`.Test`**: Contains comprehensive unit tests for the business logic. It utilizes mocking frameworks to isolate components and ensure reliable, independent testing without requiring a live database connection.

## 💻 Technologies Used

* **Backend:** C#, .NET (Core), ASP.NET Core Web API, SignalR (Real-time web functionality)
* **Desktop Client:** WPF (Windows Presentation Foundation), XAML
* **Web Client:** JavaScript (Vanilla), HTML, CSS
* **Design Patterns:** N-Tier Architecture, Repository Pattern, Dependency Injection, MVVM (in WPF)
* **Testing:** Unit Testing, Moq (for mocking repository interfaces)

## 🚀 Getting Started

Follow these instructions to get a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

* [.NET SDK](https://dotnet.microsoft.com/download) (Version compatible with the 2023 release environment)
* An IDE such as [Visual Studio](https://visualstudio.microsoft.com/) (Recommended for WPF support)

### Installation & Running

1. **Clone the repository:**
   ```bash
   git clone https://github.com/your-username/NTier-CRUD-Architecture.git
   ```

2. **Open the Solution:**
   Open the `.sln` file in Visual Studio. Visual Studio will automatically restore the required NuGet packages.

3. **Start the Backend:**
   Set the **`.Endpoint`** project as your startup project and run it. Note the local port it is running on (e.g., `http://localhost:5000`), as the clients will need to connect to this address.

4. **Start a Client:**
   While the Endpoint is running, you can start any of the client applications to interact with the database:
   * **For Console:** Run the `.Client` project.
   * **For Desktop:** Run the `.WpfClient` project.
   * **For Web:** Set the JSClient project as the startup project in Visual Studio and run it. The built-in ASP.NET Core web server will host the static frontend files from the wwwroot folder and open the application in your default browser.

## 🧪 Testing

The solution includes a comprehensive test suite utilizing Mocking to isolate the business logic. To execute the tests, use the Test Explorer in Visual Studio, or run the following command in the solution root:

```bash
dotnet test
```
