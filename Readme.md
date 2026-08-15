# 🚀 ResumeIQ — AI-Powered Resume Analyzer

ResumeIQ is a full-stack AI-powered resume analysis application built with Angular, ASP.NET Core Web API, and a locally hosted Gemma 3 Large Language Model using Ollama.

The application analyzes resume content and generates structured insights including detected skills, strengths, weaknesses, and recommendations for improving the resume.

The project demonstrates how AI/LLM capabilities can be integrated into a real-world full-stack application while keeping the AI infrastructure completely free and locally hosted.

## ✨ Features

- 🤖 AI-powered resume analysis
- 🧠 Local LLM inference using Gemma 3
- 🦙 Ollama integration
- 🔌 ASP.NET Core REST API
- 🅰️ Angular frontend
- 🎨 Bootstrap UI
- 🎯 SCSS styling
- 📦 Structured JSON AI responses
- 💉 Dependency Injection
- 🔄 HttpClientFactory
- 🧩 AI service abstraction using IAiService
- 📋 Automatic skill extraction
- 💪 Strength identification
- ⚠️ Weakness detection
- 💡 AI-generated recommendations
- 📖 Swagger/OpenAPI documentation
- 💰 Completely free and locally runnable

## 🏗️ Architecture

    User
      |
      v
    Angular Frontend
      |
      | HTTP POST
      v
    ASP.NET Core Web API
      |
      v
    ResumeController
      |
      v
    IAiService
      |
      v
    OllamaAiService
      |
      | HTTP
      v
    Ollama
      |
      v
    Gemma 3
      |
      v
    Structured JSON
      |
      v
    ResumeAnalysisResponse
      |
      v
    Angular Analysis Dashboard

## 🛠️ Technology Stack

### Frontend

- Angular
- TypeScript
- Bootstrap
- SCSS
- RxJS
- Angular HttpClient

### Backend

- ASP.NET Core Web API
- C#
- .NET
- REST APIs
- Dependency Injection
- HttpClientFactory
- DTOs
- Swagger / OpenAPI

### AI

- Ollama
- Gemma 3
- Local LLM inference
- Prompt Engineering
- Structured JSON generation

### Development Tools

- Visual Studio / Visual Studio Code
- Git
- GitHub
- Swagger UI

## 📁 Project Structure

    ResumeIQ/
    |
    +-- ResumeIQ.API/
    |   |
    |   +-- Controllers/
    |   |   +-- ResumeController.cs
    |   |
    |   +-- Models/
    |   |   +-- ResumeAnalysisRequest.cs
    |   |   +-- ResumeAnalysisResponse.cs
    |   |
    |   +-- Services/
    |   |   +-- IAiService.cs
    |   |   +-- OllamaAiService.cs
    |   |
    |   +-- Program.cs
    |   +-- appsettings.json
    |   +-- ResumeIQ.API.csproj
    |
    +-- resume-iq-ui/
        |
        +-- src/
        |   +-- app/
        |       |
        |       +-- core/
        |       |   +-- models/
        |       |   +-- services/
        |       |
        |       +-- features/
        |           +-- resume-analyzer/
        |
        +-- angular.json
        +-- package.json
        +-- tsconfig.json

## 🔄 Application Flow

1. User enters resume text in Angular.
2. Angular sends an HTTP POST request.
3. ASP.NET Core receives the request.
4. ResumeController validates the input.
5. IAiService processes the request.
6. OllamaAiService creates the AI prompt.
7. ASP.NET Core sends the request to Ollama.
8. Gemma 3 analyzes the resume.
9. Ollama returns structured JSON.
10. ASP.NET Core deserializes the response.
11. ResumeAnalysisResponse is returned.
12. Angular displays the analysis.

## 🤖 AI Integration

ResumeIQ uses Ollama to run Gemma 3 locally.

The application does not require a paid AI API or cloud AI provider.

    ASP.NET Core
          |
          | HTTP
          v
    Ollama API
          |
          v
       Gemma 3
          |
          v
     JSON Response
          |
          v
    ASP.NET Core
          |
          v
    Angular

The AI inference runs locally on the developer's machine.

This provides a privacy-friendly development setup because resume content does not need to be sent to an external AI API.

## 🧠 AI Analysis

The current AI analysis focuses on four major areas.

### Skills

The AI identifies technical and professional skills found in the resume.

Examples:

- ASP.NET Core
- Angular
- C#
- SQL Server
- REST APIs
- Authentication
- Full Stack Development

### Strengths

The AI identifies areas where the resume demonstrates strong experience.

Examples:

- Backend development
- Full-stack development
- Database experience
- REST API development
- Modern web development

### Weaknesses

The AI identifies areas that could be improved or require more information.

Examples:

- Limited project metrics
- Limited information about project complexity
- Missing technical details
- Lack of measurable achievements

### Recommendations

The AI provides suggestions for improving the resume.

Examples:

- Add measurable achievements
- Describe project scale
- Highlight specific technical implementations
- Add details about project impact

## 🔌 API

### Analyze Resume

Endpoint:

POST /api/Resume/analyze

Request:

    {
      "resumeText": "I have experience developing REST APIs using ASP.NET Core, Angular and SQL Server."
    }

Response:

    {
      "skills": [
        "ASP.NET Core",
        "Angular",
        "SQL Server",
        "REST APIs"
      ],
      "strengths": [
        "Backend development",
        "Full-stack development"
      ],
      "weaknesses": [
        "Limited information about project achievements"
      ],
      "recommendations": [
        "Add measurable achievements to projects",
        "Provide more information about project complexity"
      ]
    }

## 🧩 Backend Design

The AI functionality is abstracted behind an IAiService interface.

    IAiService
        |
        +-- OllamaAiService

The controller depends on IAiService instead of directly depending on OllamaAiService.

    ResumeController
          |
          v
      IAiService
          |
          v
    OllamaAiService
          |
          v
        Ollama

This provides loose coupling and allows another AI implementation to be introduced in the future without changing the controller layer.

## 📦 Strongly Typed AI Response

The AI response is converted into a strongly typed C# model.

    ResumeAnalysisResponse
        |
        +-- Skills
        +-- Strengths
        +-- Weaknesses
        +-- Recommendations

This prevents raw AI output from being passed throughout the application and provides a clear contract between the backend and frontend.

## 🧠 Prompt Engineering

The current AI service uses a structured prompt to instruct Gemma 3 to analyze the resume and return information in a predictable JSON structure.

The prompt focuses on:

1. Skills
2. Strengths
3. Weaknesses
4. Recommendations

The application also requests JSON output from Ollama.

This makes the AI response easier for ASP.NET Core to deserialize and process.

## 💻 Local Development Setup

### Prerequisites

Install:

- .NET SDK
- Node.js
- Angular CLI
- Ollama
- Git

## 🤖 Ollama Setup

Install Ollama from:

https://ollama.com/

Verify the installation:

    ollama --version

Pull Gemma 3:

    ollama pull gemma3

Verify the model:

    ollama list

Test the model:

    ollama run gemma3

## ⚙️ Backend Configuration

The ASP.NET Core backend uses the following Ollama configuration:

    {
      "Ollama": {
        "BaseUrl": "http://localhost:11434",
        "Model": "gemma3"
      }
    }

## ▶️ Run the Backend

Navigate to the backend directory:

    cd ResumeIQ.API

Restore dependencies:

    dotnet restore

Run the API:

    dotnet run

Open Swagger:

    http://localhost:<PORT>/swagger

Use the following endpoint:

    POST /api/Resume/analyze

## 🅰️ Run the Angular Frontend

Open another terminal.

Navigate to the frontend:

    cd resume-iq-ui

Install dependencies:

    npm install

Run Angular:

    ng serve

Open:

    http://localhost:4200

## 🔐 CORS

During local development, Angular runs on:

    http://localhost:4200

while ASP.NET Core runs on a different local port.

The backend allows the Angular development origin through CORS.

## 💰 Cost

ResumeIQ is designed to use free and locally hosted technologies.

| Technology | Cost |
|------------|------|
| Angular | Free |
| ASP.NET Core | Free |
| Bootstrap | Free |
| Ollama | Free |
| Gemma 3 | Free / Local |
| Git | Free |
| GitHub | Free |

No paid cloud AI service is required.

The AI inference is performed locally using Ollama.

## 🚧 Current Status

### Version 1 — Completed

- [x] ASP.NET Core Web API
- [x] Angular frontend
- [x] Bootstrap UI
- [x] SCSS styling
- [x] Ollama integration
- [x] Gemma 3 integration
- [x] AI resume analysis
- [x] Structured JSON response
- [x] AI service abstraction
- [x] Dependency Injection
- [x] HttpClientFactory
- [x] Swagger/OpenAPI
- [x] Angular to ASP.NET Core integration
- [x] ASP.NET Core to Ollama integration
- [x] AI-generated skills analysis
- [x] AI-generated strengths analysis
- [x] AI-generated weaknesses analysis
- [x] AI-generated recommendations

## 🔮 Future Roadmap

### Phase 2 — Resume File Processing

- [ ] PDF upload
- [ ] DOCX upload
- [ ] PDF text extraction
- [ ] DOCX text extraction
- [ ] File validation
- [ ] File size validation
- [ ] Resume preview

Planned flow:

    PDF / DOCX
        |
        v
    ASP.NET Core
        |
        v
    Text Extraction
        |
        v
    Resume Text
        |
        v
    Gemma 3
        |
        v
    Resume Analysis

### Phase 3 — Database

Introduce SQLite and Entity Framework Core.

Planned features:

- [ ] SQLite
- [ ] Entity Framework Core
- [ ] Resume storage
- [ ] Analysis history
- [ ] Job description storage
- [ ] Persistent data

### Phase 4 — Job Description Matching

Allow users to provide a job description along with their resume.

    Resume
       +
    Job Description
       |
       v
    ResumeIQ
       |
       v
    Job Match Analysis

Planned features:

- [ ] Skill matching
- [ ] Missing skill detection
- [ ] Keyword matching
- [ ] Job compatibility score
- [ ] Resume recommendations
- [ ] Required vs optional skill detection

### Phase 5 — Deterministic Resume Scoring

The application will combine traditional software logic with AI analysis.

    Resume
       |
       +----------------------+
       |                      |
       v                      v
    Deterministic          AI Engine
    Engine                    |
       |                      v
       |                   Gemma 3
       |                      |
       +----------+-----------+
                  |
                  v
             Final Analysis

Deterministic logic can handle:

- Exact skill matching
- Keyword matching
- Skill scoring
- Section validation
- Job requirement matching

AI can handle:

- Experience analysis
- Project relevance
- Resume quality
- Contextual recommendations

### Phase 6 — Authentication

Planned features:

- [ ] User registration
- [ ] User login
- [ ] JWT authentication
- [ ] Password hashing
- [ ] Angular route guards
- [ ] HTTP interceptor
- [ ] User-specific resume history

### Phase 7 — Advanced AI

Future versions may introduce:

- [ ] Embeddings
- [ ] Semantic skill matching
- [ ] Vector similarity
- [ ] Local vector database
- [ ] Retrieval-Augmented Generation
- [ ] Context-aware recommendations
- [ ] AI response evaluation
- [ ] Prompt versioning

### Phase 8 — Production Engineering

Planned backend improvements:

- [ ] Global exception handling
- [ ] Structured logging
- [ ] Request validation
- [ ] Rate limiting
- [ ] Caching
- [ ] Unit testing
- [ ] Integration testing
- [ ] API performance optimization
- [ ] Docker support
- [ ] CI/CD

## 🎯 Learning Objectives

This project provides practical experience with full-stack development and AI engineering.

### Full-Stack Development

    Angular
       +
    ASP.NET Core
       +
    REST APIs
       +
    Bootstrap

### Backend Engineering

- Dependency Injection
- HttpClientFactory
- DTOs
- REST API design
- Configuration
- Validation
- Error handling
- Service abstraction

### AI Engineering

- LLM integration
- Ollama
- Gemma 3
- Prompt engineering
- Structured AI output
- JSON deserialization
- Local LLM inference
- AI service abstraction

### Future AI Concepts

- Embeddings
- Semantic search
- Vector databases
- RAG
- AI evaluation
- Prompt optimization

## 🌟 Project Vision

ResumeIQ is not intended to remain a simple prompt-engineering demo.

The long-term goal is to evolve it into a complete AI-powered resume intelligence platform.

    LLM Integration
          |
          v
    Structured AI Output
          |
          v
    Resume File Processing
          |
          v
    Deterministic Skill Matching
          |
          v
    Semantic Skill Matching
          |
          v
    Embeddings
          |
          v
    Vector Search
          |
          v
    RAG
          |
          v
    AI-Powered Resume Intelligence

## 📸 Screenshots

Screenshots can be added after completing the frontend.

Recommended screenshots:

- Resume analyzer page
- AI analysis results
- Swagger API
- Skills analysis
- Recommendations section

## 👨‍💻 Author

Tapan Ray

Software Engineer | ASP.NET Core | Angular | SQL Server | AI

## 📄 License

This project is created for educational, learning, and portfolio purposes.

---

Built with Angular, ASP.NET Core, Ollama and Gemma 3.