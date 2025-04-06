# Web Application for Job Search

A sophisticated job search platform built with C# and ASP.NET MVC that connects job seekers with employers through an intuitive and responsive interface.

## Features

### User Authentication System
- Secure registration and login for job seekers and employers
- Role-based authorization with different permissions
- Password recovery and email verification
- Two-factor authentication support
- OAuth integration (Google, LinkedIn, Microsoft)
- Session management and security features
- Account lockout protection against brute force attacks

### Job Seeker Features
- Create and manage comprehensive profiles
  - Personal information and contact details
  - Work experience with detailed history
  - Education background and certifications
  - Skills inventory with proficiency levels
  - Portfolio links and project showcases
- Upload and store multiple resumes/CVs in various formats (PDF, DOCX)
- Set job preferences and receive tailored recommendations
  - Salary expectations
  - Location preferences with remote work options
  - Industry and role preferences
  - Work culture and company size preferences
- Apply to jobs with one-click application
- Track application status in real-time
- Save favorite job listings and create job alerts
- Career development resources and skill assessment tools
- Interview scheduling and calendar integration

### Employer Features
- Company profile management
  - Company description and culture information
  - Upload logo and company images
  - Team member profiles
  - Company benefits and perks
- Post and manage job listings
  - Detailed job descriptions with rich text formatting
  - Custom application questions
  - Screening criteria configuration
  - Job posting templates and drafts
- Review applicants and their resumes
  - Advanced filtering and sorting options
  - Candidate scoring and ranking
  - Collaborative hiring with team comments
- Communication system with candidates
  - Interview scheduling
  - Automated email templates
  - Mass communication options
- Analytics dashboard for job posting performance
  - Applicant demographics
  - Source tracking
  - Conversion rates
  - Time-to-fill metrics

### Search Functionality
- Advanced search filters
  - Location with radius search
  - Industry and job categories
  - Experience level and qualifications
  - Salary range
  - Employment type (full-time, part-time, contract)
  - Remote work options
- Keyword-based search with relevance ranking
  - Boolean search operators
  - Phrase matching
  - Skill-based matching
- Geolocation-based job matching
- Saved searches with notifications
- AI-powered job recommendations based on profile
- Similar job suggestions

### Notification System
- Email alerts for new job matches
- Application status updates
- Interview invitations and reminders
- Custom notification preferences
- In-app notification center
- SMS notifications for urgent communications (optional)

### Administrative Features
- User management dashboard
- Content moderation tools
- System health monitoring
- Analytics and reporting
  - User registration metrics
  - Platform engagement statistics
  - Job posting and application analytics
  - Revenue tracking (for premium features)

## Technology Stack

### Backend
- C# 10.0 with latest language features
- ASP.NET MVC 5 framework
- Entity Framework Core 6.0 for data access
  - Code-first approach
  - Migration management
  - Query optimization
- Identity Framework for authentication and authorization
- SQL Server database (supports 2019/2022)
- RESTful API architecture
- Background services for scheduled tasks
- Caching system (Redis)
- Full-text search engine integration

### Frontend
- Razor views with dynamic rendering
- JavaScript/jQuery for interactive elements
- TypeScript for type-safe client-side code
- Bootstrap 5 for responsive layouts
- AJAX for asynchronous operations
- Modern CSS with SASS/SCSS preprocessors
- Progressive Web App (PWA) capabilities
- Client-side validation
- Rich text editors for job descriptions
- Interactive dashboards with charting libraries

### DevOps & Deployment
- CI/CD pipeline using Azure DevOps
- Containerization with Docker
- Orchestration with Kubernetes
- Cloud hosting on Microsoft Azure
  - App Service for web application
  - Azure SQL for database
  - Blob Storage for file storage
  - Redis Cache for performance
- Application monitoring with Application Insights
- Automated testing in the pipeline
- Infrastructure as Code (IaC) with Azure Resource Manager templates

### Security
- HTTPS enforcement with TLS 1.3
- CSRF protection
- SQL injection prevention
- XSS protection
- Input validation and sanitization
- GDPR compliance features
- Data encryption at rest and in transit
- Regular security audit procedures

## Getting Started

### Prerequisites
- Visual Studio 2022 or later
- .NET 6.0 SDK
- SQL Server (local or cloud instance)
- Git
- Docker Desktop (optional for containerized development)
- Node.js and npm (for frontend asset management)

### Installation

1. Clone the repository
   ```
   git clone https://github.com/yourusername/web-application-for-job-search.git
   ```

2. Open the solution file in Visual Studio
   ```
   WebApplicationForJobSearch.sln
   ```

3. Install frontend dependencies
   ```
   cd WebApplicationForJobSearch/wwwroot
   npm install
   ```

4. Update the connection string in `appsettings.json` to point to your SQL Server instance
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=JobSearchDb;Trusted_Connection=True;MultipleActiveResultSets=true"
   }
   ```

5. Run database migrations
   ```
   Update-Database
   ```

6. Seed initial data (optional)
   ```
   dotnet run seeddata
   ```

7. Build and run the application
   ```
   F5 or Ctrl+F5
   ```

8. Access the application at `https://localhost:5001`

### Docker Deployment
```
docker-compose up -d
```

## Project Structure

```
WebApplicationForJobSearch/
├── Controllers/              # MVC Controllers
│   ├── AccountController.cs  # User authentication
│   ├── JobsController.cs     # Job listing operations
│   ├── ApplicationController.cs # Job applications
│   └── ...
├── Models/                   # Data models and ViewModels
│   ├── Domain/               # Entity models
│   ├── ViewModels/           # View-specific models
│   └── DTOs/                 # Data transfer objects
├── Views/                    # Razor views
│   ├── Account/              # Authentication views
│   ├── Jobs/                 # Job listing views
│   ├── Shared/               # Layouts and partials
│   └── ...
├── Data/                     # Database context and migrations
│   ├── ApplicationDbContext.cs
│   ├── Migrations/
│   └── Repositories/         # Data access implementations
├── Services/                 # Business logic services
│   ├── Interfaces/           # Service contracts
│   ├── Implementations/      # Service implementations
│   └── BackgroundServices/   # Background processing
├── Repositories/             # Data access layer
│   ├── Interfaces/           # Repository contracts
│   └── Implementations/      # Repository implementations
├── Helpers/                  # Utility classes
│   ├── Extensions/           # Extension methods
│   ├── Filters/              # Action filters
│   └── Utilities/            # General utilities
├── wwwroot/                  # Static files (CSS, JS, images)
│   ├── css/                  # Stylesheets
│   ├── js/                   # JavaScript files
│   ├── lib/                  # Third-party libraries
│   └── images/               # Image assets
├── Areas/                    # Feature areas
│   ├── Admin/                # Admin dashboard
│   ├── Employer/             # Employer portal
│   └── JobSeeker/            # Job seeker portal
├── Api/                      # API endpoints
│   ├── Controllers/          # API controllers
│   └── Middleware/           # API-specific middleware
├── Infrastructure/           # Cross-cutting concerns
│   ├── Logging/              # Logging configuration
│   ├── Caching/              # Cache implementations
│   └── Security/             # Security configurations
└── Configurations/           # App configuration
    ├── AutoMapperProfile.cs  # Object mapping profiles
    ├── DependencyInjection.cs # Service registration
    └── AppSettings.cs        # Strong-typed settings
```

## Configuration

The application uses the following configuration files:

- `appsettings.json` - Main configuration file
  ```json
  {
    "ConnectionStrings": {
      "DefaultConnection": "Server=.;Database=JobSearchDb;Trusted_Connection=True;MultipleActiveResultSets=true"
    },
    "Logging": {
      "LogLevel": {
        "Default": "Information",
        "Microsoft": "Warning",
        "Microsoft.Hosting.Lifetime": "Information"
      }
    },
    "AllowedHosts": "*",
    "EmailSettings": {
      "SmtpServer": "smtp.example.com",
      "SmtpPort": 587,
      "EnableSsl": true,
      "SenderEmail": "noreply@jobsearch.com",
      "SenderName": "Job Search Platform"
    },
    "ApplicationSettings": {
      "JobExpirationDays": 30,
      "MaxResumeFileSizeMb": 10,
      "AllowedFileExtensions": [".pdf", ".docx", ".doc"]
    }
  }
  ```
- `appsettings.Development.json` - Development-specific settings
- `web.config` - IIS configuration
- `launchSettings.json` - Application launch profiles

## Database Schema

The database follows a normalized relational design with the following key tables:

- `Users` - User account information
- `UserProfiles` - Extended user profile data
- `Companies` - Employer company information
- `Jobs` - Job listings and details
- `Applications` - Job applications
- `Resumes` - Job seeker resume storage
- `Skills` - Skill taxonomy
- `UserSkills` - Skills associated with users
- `JobSkills` - Skills required for jobs

## Testing

The project includes a comprehensive test suite:

- Unit tests using xUnit
  - Service layer tests
  - Controller tests with mocked dependencies
  - Model validation tests
- Integration tests for controllers and services
  - Database integration tests
  - API endpoint tests
- Automated UI tests using Selenium
- Load and performance tests using JMeter

Run tests using the Test Explorer in Visual Studio or with the following command:
```
dotnet test
```

### Test Project Structure
```
WebApplicationForJobSearch.Tests/
├── UnitTests/
│   ├── Controllers/
│   ├── Services/
│   └── Models/
├── IntegrationTests/
│   ├── Api/
│   ├── Database/
│   └── Services/
└── UITests/
    └── PageObjects/
```

## Deployment

### Development Environment
```
dotnet publish -c Debug
```

### Staging Environment
```
dotnet publish -c Release -o ./staging
```

### Production Environment
1. Publish the application using Visual Studio or command line:
   ```
   dotnet publish -c Release -o ./publish
   ```

2. Deploy to Azure App Service through Visual Studio or Azure DevOps pipeline
   ```yaml
   # azure-pipelines.yml example
   trigger:
     - main
   
   pool:
     vmImage: 'windows-latest'
   
   variables:
     buildConfiguration: 'Release'
   
   steps:
   - task: DotNetCoreCLI@2
     inputs:
       command: 'restore'
       projects: '**/*.csproj'
   
   - task: DotNetCoreCLI@2
     inputs:
       command: 'build'
       projects: '**/*.csproj'
       arguments: '--configuration $(buildConfiguration)'
   
   - task: DotNetCoreCLI@2
     inputs:
       command: 'test'
       projects: '**/*Tests.csproj'
       arguments: '--configuration $(buildConfiguration)'
   
   - task: DotNetCoreCLI@2
     inputs:
       command: 'publish'
       publishWebProjects: true
       arguments: '--configuration $(buildConfiguration) --output $(Build.ArtifactStagingDirectory)'
   
   - task: AzureWebApp@1
     inputs:
       azureSubscription: 'Your-Azure-Subscription'
       appType: 'webApp'
       appName: 'job-search-webapp'
       package: '$(Build.ArtifactStagingDirectory)/**/*.zip'
       deploymentMethod: 'auto'
   ```

3. Configure environment variables for the production environment
4. Set up monitoring and alerts
5. Configure backup and disaster recovery procedures

## Performance Optimization

- Response caching for frequently accessed data
- CDN integration for static assets
- Database indexing strategy
- Query optimization techniques
- Server-side pagination for large data sets
- Lazy loading for resource-intensive components
- Image optimization and compression
- Minification of CSS and JavaScript

## Localization and Globalization

The application supports multiple languages and regions:

- Resource files for UI text
- Culture-specific formatting for dates, numbers, and currencies
- Right-to-left (RTL) support for appropriate languages
- Region-specific content filtering

## Accessibility

The application follows WCAG 2.1 AA standards:

- Semantic HTML structure
- ARIA attributes for enhanced screen reader support
- Keyboard navigation support
- Color contrast compliance
- Focus indication for interactive elements
- Alt text for images

## Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

### Coding Standards
- Follow Microsoft's C# coding conventions
- Use meaningful names for variables, methods, and classes
- Add XML documentation comments for public APIs
- Write unit tests for new features
- Use dependency injection for better testability
- Follow SOLID principles

## Documentation

- API documentation using Swagger/OpenAPI
- User manuals for job seekers and employers
- Administration guide
- Developer documentation in the `/docs` directory

## License

This project is licensed under the MIT License - see the LICENSE file for details

## Acknowledgments

- [ASP.NET MVC Documentation](https://docs.microsoft.com/en-us/aspnet/mvc/)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
- [Bootstrap Documentation](https://getbootstrap.com/docs/)
- [Azure DevOps](https://docs.microsoft.com/en-us/azure/devops/)
- [SQL Server](https://docs.microsoft.com/en-us/sql/sql-server/)

## Support and Contact

For questions and support, please open an issue in the GitHub repository or contact the maintainers at support@jobsearchapp.com.
