# CommentSense

## Welcome to CommentSense
CommentSense is an intelligent comment categorization system built on a microservice architecture, designed for filtering and categorizing user comments efficiently. It processes and analyzes large volumes of feedback based on various criteria such as content, city, location, date, and interests using tags, making it easier to extract actionable insights.

## Key Features:

**Advanced Categorization:** Filters comments using structured tags for better organization and analysis.
**Scalability & Performance:** Designed to handle large datasets efficiently.
**Pagination Support:** Ensures seamless browsing and retrieval of comments in a structured manner.
**PostCommentDetailsFromCSV:** Allows bulk uploading of comment data from CSV files, making integration and processing faster.
This system provides a scalable, high-performance solution for businesses looking to gain deeper insights from customer feedback. 

## Other Features

- **Microservice Architecture:**
  - Modular and scalable architecture, ensuring flexibility for future enhancements.
  - Efficient processing of comment data using Azure Function Apps.

- **Real-Time Monitoring:**
  - Integrated with Application Insights for tracking performance and monitoring system health.

- **Efficient Data Storage:**
  - SQL Server used as the backend database for reliable data management and query performance.

- **API Gateway:**
  - Uses Azure API Management (APIM) as a reverse proxy for secure and efficient API interactions.

## Technology Stack

- **Framework:** [.NET](https://dotnet.microsoft.com/) - Core framework for building backend logic.
- **Microservices:** [Azure Function Apps](https://azure.microsoft.com/en-us/services/functions/) - Enables serverless computing and scalable processing.
- **Programming Language:** [C#](https://docs.microsoft.com/en-us/dotnet/csharp/) - Primary language for backend development.
- **Database:** [Azure SQL Server](https://www.microsoft.com/en-us/sql-server) - Manages persistent data storage.
- **Monitoring:** [Azure Application Insights](https://docs.microsoft.com/en-us/azure/azure-monitor/app/app-insights-overview) - Provides real-time system monitoring and logging.
- **API Management:** [Azure APIM](https://azure.microsoft.com/en-us/services/api-management/) - Handles secure API gateway and reverse proxy.

## Getting Started

To set up and run **CommentSense** locally, follow these steps:

1. **Clone the Repository**

   ```bash
   git clone https://github.com/yourusername/commentsense.git
