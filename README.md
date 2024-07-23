# Movie Search Application JE.OMDb
This is a web application built with ASP.NET Core that allows users to search for movies by title using the OMDB API. The application saves the 5 latest search queries and displays search results with extended movie information when a particular movie is selected.

## Features

- Movie search by title
- Saving the 5 latest search queries
- Displaying search results
- Showing extended movie information (poster, description, IMDb score, etc.)

## Technologies Used

- ASP.NET Core
- C#
- OMDB API
- AutoMapper
- Dependency Injection
- Logging
- NUnit for Unit Testing

## Prerequisites

- .NET Core SDK 3.1 or later
- OMDB API Key (You can obtain one from [OMDB API](http://www.omdbapi.com/apikey.aspx))

## API Endpoints

- **Search Movies By Title:**
    ```
    GET /api/movies/search?query={title}
    ```
    - Returns a list of movies matching the search query.

- **Get Movie Details:**
    ```
    GET /api/movies/{id}
    ```
    - Returns detailed information about the selected movie.

## Improvements

### Security
To improve API security:
- Avoid exposing the OMDB API key in the source code.
- Use environment variables to store sensitive information.
- Validate and sanitize user inputs to prevent injection attacks.

### Logging
Utilize the built-in logging framework to log important events and errors.
