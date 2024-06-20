# URL Shortener ✂️

## Description 🐙

This is a minimalistic web service that shortens an URL. This service saves the original and shortened URLs in a Redis database for quick retrieval. The project leverages a minimal API style using .NET to ensure a clean and efficient codebase.

<p align="center">
<img src="https://media.giphy.com/media/pCJcExvbKdSeyyv8zP/giphy.gif?cid=790b76117818qyv5iphroju3hm0ddr2gv7lqi5eeif6fyl2i&ep=v1_gifs_search&rid=giphy.gif&ct=g" />
</p>

## Tech Stack 📝

<p align="center">
<img src="https://skillicons.dev/icons?i=dotnet,docker,redis" alt="logos of dotnet, docker and redis" />
</p>

## Instructions to Run 🧑‍💻

### Prerequisites

- [Docker](https://www.docker.com/get-started) installed on your machine.

### Steps to Run

1. **Clone the Repository**

   ```sh
   git clone <repository-url>
   cd url-shortener
   ```

2. **Build and Run the Docker Containers**

   Use Docker Compose to build and start both the URL Shortener API and Redis containers.

   ```sh
   docker-compose up --build
   ```

3. **Access the API**

   Once the containers are up and running, the URL Shortener API will be accessible at:

   ```
   http://localhost:3001
   ```

### API Endpoints

- **Shorten URL**

  ```
  POST /shorten
  ```

  **Request Body:**
  ```json
  {
    "url": "https://example.com"
  }
  ```

  **Response:**
  ```json
  {
    "message": "success",
    "shortUrl": "http://localhost:5000/abc123"
  }
  ```

- **Redirect to Original URL**

  ```
  GET /{shortUrl}
  ```

  Accessing the shortened URL will redirect to the original URL.

### Stopping the Containers

To stop the running containers, use:

```sh
docker-compose down
```
