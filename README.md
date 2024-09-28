# CommentSense

## Overview

Welcome to **CommentSense**, an intelligent comment categorization system designed with a microservice architecture. This solution is engineered for filtering and categorizing user comments based on various criteria such as content, city, location, date, and interests using tags. It provides a scalable, efficient way to process and analyze large volumes of feedback, making it easier to extract valuable insights.

## Features

- **Advanced Comment Filtering:**
  - Categorizes user comments based on tags, city, location, date, and other metadata.
  - Streamlines data analysis by filtering feedback according to specified parameters.
  
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
