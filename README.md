# Hotel Listing Application Setup Guide

## (Development Environment Setup)
### 1. IDEs (Code Editors)

You can use either of the following:
* **Visual Studio 2022 (for .NET Backend)**

  * [Download Visual Studio](https://visualstudio.microsoft.com/downloads/)
  * Select the **ASP.NET and web development** workload during installation.

* **Visual Studio Code (for Angular Frontend)**

  * [Download VS Code](https://code.visualstudio.com/)

---

### 2. .NET SDK (Version 6.0 or Later)

* Download and install from:
  [https://dotnet.microsoft.com/en-us/download](https://dotnet.microsoft.com/en-us/download)

---

### 3. Node.js and npm

* Download and install from:
  [https://nodejs.org/](https://nodejs.org/)

* This also includes **npm** (Node Package Manager), required to install Angular dependencies.

---

### 4. Angular CLI (Install Globally)

Install Angular CLI globally by running the following command in the terminal or command prompt:

```bash
npm install -g @angular/cli
```

---

## How to Run the Application

### Step 1: Run the Backend API (ASP.NET Core)

1. Open **Visual Studio** or terminal.

2. Open the backend solution/project folder.

3. Restore packages (if required) and run the application:

   **Via Terminal:**

   ```bash
   dotnet restore
   dotnet run
   ```

4. The API will be available at `https://localhost:8000` or `http://localhost:8000`.

---

### Step 2: Run the Angular Frontend

1. Open the Angular project folder using **VS Code** or terminal.
2. Install frontend dependencies (Important: `node_modules` is **not included**):
   ```bash
   npm install
   ```

3. Run the frontend Angular app:
   ```bash
   ng serve 
   ```
   or
   ```bash
   npm start
   ```

4. Visit the app in your browser at:
   [http://localhost:4200](http://localhost:4200)

---

## Notes
* Do **not include or share `node_modules`** folder. It can be regenerated using `npm install`.
* Make sure the backend API is running **before** starting the frontend app.
---

## Quick Command Reference

| Task                     | Command          | Location               |
| ------------------------ | ---------------- | ---------------------- |
| Restore .NET packages    | `dotnet restore` | Backend project folder |
| Run .NET API             | `dotnet run`     | Backend project folder |
| Install Angular packages | `npm install`    | Angular project folder |
| Run Angular app          | `ng serve`       | Angular project folder |

---
# Questions/Answers
---
### Q: How long did you spend on the coding test and how would you improve your solution if you had more time? If you were unable to spend as much time as you would have liked on the coding test?
### Answer
I spent around **3 hours** building the hotel search application.
Within this limited timeframe, I developed a well-rounded, performance-optimized, and user-friendly application featuring:
- Viewing available hotels with details such as **name**, **description**, **location**, and **rating**.
- Searching by hotel name with **case-insensitive** support and **partial matching**.
- Filtering hotels by **rating**, **location**, and **Name**.
- Sorting hotels by **rating in descending order**.
- Pagination for smooth navigation through hotel listings.
- Visual improvements for a clean and modern user experience.

### What I Would Add with More Time
If given more time, I would focus on the following enhancements:
1. Integrate a **backend API** to fetch hotel data dynamically.
2. Implement **user reviews** and **booking functionality**.
3. Add **automated testing** to cover various search, filter, and sorting scenarios.
---

### Q: Describe the tooling/ libriries/ packages you chose to use for your development process and the reasons why?**
### Answer
This application is designed following **Clean Architecture** principles combined with the **CQRS** pattern to ensure scalability, maintainability, and clear separation of concerns. Below is a description of the tools and libraries chosen, with the reasons behind their selection:

### Backend (ASP.NET Core with Clean Architecture & CQRS)

| Tool/Library              | Purpose                                          | Why We Chose It                                                                                   |
| ------------------------- | ------------------------------------------------ | ------------------------------------------------------------------------------------------------ |
| **ASP.NET Core (6.0+)**   | Web API framework for building RESTful services  | Cross-platform, high performance, scalable, and supported by Microsoft                            |
| **MediatR**               | Implements the CQRS pattern                        | Decouples commands and queries, enabling clear separation between write and read operations       |
| **Swashbuckle / Swagger** | API documentation and testing interface           | Provides interactive API documentation for easier development and testing                         |
| **AutoMapper**            | Mapping between domain models and DTOs            | Simplifies transformation of data between layers without manual mapping                           |
| **JSON File Data Source** | Reads hotel data from JSON files                    | Lightweight and simple solution for data storage without the complexity of a database            |

### Frontend (Angular)

| Tool/Library            | Purpose                              | Why We Chose It                                                               |
| ----------------------- | ------------------------------------ | ----------------------------------------------------------------------------- |
| **Angular 15+**         | Frontend SPA framework               | Robust TypeScript-based framework with strong community support and tooling  |
| **Bootstrap 5**         | Styling and responsive design        | Mobile-first, consistent UI components and responsive grid system             |
| **ngModel (Forms)**     | Two-way data binding                 | Easy and declarative user input handling                                     |
| **RxJS**                | Reactive programming                 | Handles asynchronous data streams and state management cleanly               |
| **provideHttpClient**   | HTTP communication with backend API | Angular standalone function to configure HttpClient in a modular way       |

### Development Tools

| Tool                   | Purpose                         | Why We Chose It                             |
| ---------------------- | ------------------------------- | ------------------------------------------- |
| **Visual Studio**      | Backend IDE                    | Excellent support for .NET Core and debugging tools                             |
| **Visual Studio Code** | Frontend editor                 | Lightweight, extensible, and perfect for Angular development                    |
| **Swagger UI**         | API documentation              | Interactive API explorer aiding frontend-backend collaboration                  |
| **Git**                | Version control                | Source control and collaboration across teams                                  |

### Architecture Highlights

- **Clean Architecture:** Separates domain, application, infrastructure, and presentation layers for easier maintainability and testability.
- **CQRS Pattern:** Segregates command (write) and query (read) responsibilities to simplify complexity and improve performance.
- **File-based Data Source:** Uses JSON files to simulate data storage, making the app lightweight and easy to deploy without database dependencies.

---

### Q: Describe how this solution would be deployed and run in your chosen cloud provider and any impact this may have on its development?**
### Answer
This application consists of two parts:

- **Backend:** ASP.NET Core API serving hotel data from local JSON files.
- **Frontend:** Angular application consuming the API.

The solution can be deployed on Azure using containerization technologies like Docker and orchestrated with Kubernetes, with CI/CD automation via Azure DevOps.

### Backend Deployment (ASP.NET Core API)
- The backend reads hotel data directly from local JSON files (no external database).
- The JSON files are included inside the container image or mounted as persistent volumes in Kubernetes.

### Frontend Deployment (Angular)
- The Angular app is built into static files and served either via a web server container or a static hosting service.
- It consumes the backend API endpoints.

### Dockerization

1. **Docker Images:**
   - Create a Dockerfile for the backend API, which copies the application and the JSON data files into the image.
   - Create a Dockerfile for the Angular frontend, which builds the Angular app and serves it using a lightweight web server (e.g., Nginx).

2. **Container Images:**
   - Build and tag container images for backend and frontend.
   - Push these images to Azure Container Registry (ACR) or any container registry.

### Kubernetes Deployment on Azure Kubernetes Service (AKS)

1. **Cluster Setup:**
   - Create an AKS cluster via Azure Portal or CLI.
   - Connect to AKS using `kubectl`.

2. **Deployments and Services:**
   - Create Kubernetes Deployment manifests for backend and frontend containers.
   - Configure Services for exposing APIs internally and frontend externally.
   - Use Persistent Volume Claims if JSON files need to be updated or shared across pods.

3. **ConfigMaps / Secrets:**
   - Use ConfigMaps for API URLs or environment variables.
   - Use Secrets for sensitive data if required.

### Azure DevOps CI/CD Pipeline

1. **Build Pipeline:**
   - Build backend API and frontend Angular projects.
   - Build Docker images for both.
   - Push Docker images to Azure Container Registry.

2. **Release Pipeline:**
   - Deploy Docker images to AKS using `kubectl apply` or Helm charts.
   - Automate rollbacks and zero-downtime deployments.

3. **Triggers:**
   - Set pipeline triggers on code commits for automated build and deploy.
---

### Q: If the application was enhanced to contain business sensitive data what considerations and possible solutions would you consider for securing it?**
### Answer
1. **Authentication and Authorization**
   - Implement strong user authentication mechanisms, such as OAuth2/OpenID Connect or JWT tokens.

2. **Data Encryption**
   - Encrypt sensitive data at rest using industry-standard algorithms.
   - Use TLS/SSL to encrypt data in transit between client or man-in-the-middle attacks.

3. **Secure Storage and Access to Secrets**
   - Avoid hardcoding sensitive credentials or secrets in source code.
   - Use secure vault solutions (Like: Azure Key Vault) to store and manage sensitive configuration data.

4. **Input Validation and Sanitization**
   - Validate and sanitize all user inputs to protect against injection attacks (SQL injection, NoSQL injection).
   - Use validation libraries like FluentValidation to enforce strict rules.

5. **Audit Logging and Monitoring**
   - Implement detailed audit logs for sensitive operations and access.
   - Monitor logs actively for suspicious activities.

6. **Secure Development Practices**
   - Follow secure coding standards and perform regular security code reviews.
   - Integrate static code analysis and vulnerability scanning tools into CI/CD pipelines.

7. **Data Minimization and Anonymization**
   - Collect and store only necessary sensitive data.
   - Anonymize or mask sensitive information where full details are not required.

8. **API Security**
   - Protect APIs with rate limiting, throttling, and input validation.
   - Use API gateways or firewalls to control and monitor traffic.

---

### Q: How would you track down a performance issue in production and what was your last experience of this?**
### Answer
When there is a slowdown or performance problem in a live application, I start by carefully monitoring the system to find where the problem is happening whether it's in the user interface, the server, or the database. I then try to recreate the problem in a safe environment to better understand it. Using special tools, I check how the different parts of the application are working and identify any delays or inefficiencies. After finding the cause, I improve the code and data handling to make the application faster and smoother. Throughout this process, I keep the team informed and document the steps taken to prevent future issues. For example, in my recent project at Nagarro, I improved the applications loading speed by 25% by making both the server and frontend work more efficiently, which led to a better experience for users.

