# Segundo_Prototipo_Empresariales

Descripción corta

Este repositorio contiene el segundo prototipo del proyecto de tienda de instrumentos musicales. Incluye un cliente en C#, un cliente web en JavaScript, un microservicio en Python y un servidor Java (Spring Boot, Maven).

**Estructura del repositorio**
- `GUICliente2/`: Proyecto cliente en C# (.NET). Contiene la solución `Cliente_C#.sln` y el proyecto `Cliente_C#.csproj`.
- `Cliente_JavaScript/`: Frontend estático (HTML/CSS/JS). Páginas para la UI del cliente y scripts para operaciones CRUD en el navegador.
- `Microservicio_Amplificador/`: Microservicio en Python (FastAPI) (archivos: `main.py`, `services.py`, `models.py`, `database.py`, `requirements.txt`).
- `Servidor/`: Aplicación Java con Maven (`pom.xml`, `mvnw`, `src/`).

**Tecnologías**
- C# / .NET (proyecto en `GUICliente2`)
- JavaScript / HTML / CSS (cliente web en `Cliente_JavaScript`)
- Python (microservicio en `Microservicio_Amplificador`)
- Java (Spring Boot) + Maven (servidor en `Servidor`)

**Requisitos**
- Windows (desarrollo probado en Windows)
- Visual Studio (o `dotnet` SDK) para el proyecto C#
- Python 3.8+ y `pip` para el microservicio
- JDK 21+ y Maven (o usar los wrappers `mvnw`, `mvnw.cmd`) para el servidor Java
- Navegador moderno para el cliente JavaScript

**Cómo ejecutar cada módulo**

- `GUICliente2` (C#)
  - Abrir `GUICliente2\Cliente_C#.sln` con Visual Studio.
  - Compilar y ejecutar desde Visual Studio (Debug o Release).
  - Alternativa: usar `dotnet` desde la línea de comandos si el proyecto es SDK-style:
    - `dotnet build "GUICliente2\Cliente_C#.csproj"`
    - `dotnet run --project "GUICliente2\Cliente_C#.csproj"`

- `Cliente_JavaScript` (Frontend estático)
  - Abrir `Cliente_JavaScript\pages\inicio.html` (u otras páginas) en el navegador.
  - Recomendado (desde PowerShell) servir los archivos estáticos:
    - `cd Cliente_JavaScript; python -m http.server 5000`  # luego abrir http://localhost:5000/pages/inicio.html
  - O usar un servidor estático como `npx serve` si tiene Node.js.

- `Microservicio_Amplificador` (Python + FastAPI)
  - Crear y activar un entorno virtual (PowerShell):
    - `python -m venv .venv`
    - `.\.venv\Scripts\Activate.ps1`
  - Instalar dependencias:
    - `pip install -r requirements.txt`
  - Ejecutar el microservicio (recomendado con `uvicorn`):
    - `uvicorn main:app --reload --host 0.0.0.0 --port 8000`


- `Servidor` (Java + Maven)
  - Desde PowerShell, dentro de `Servidor` ejecutar (usa wrapper incluido):
    - `cd Servidor`
    - `.\mvnw.cmd clean package`  # compila y empaqueta
    - `.\mvnw.cmd spring-boot:run`  # si es Spring Boot
  - Alternativa: `mvnw.cmd` y luego ejecutar el JAR en `target/` con `java -jar`.
