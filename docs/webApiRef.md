# Web API documentation

## Usage
To consume the APIs, a client need to obtain a JWT token by calling authentication api. After
the token is obtained, store the token. And all api calls will require the token to be sent with it.
## Domain  
**http://ec2-15-223-82-164.ca-central-1.compute.amazonaws.com**

## API

Authentication
- `POST /auth` (Basic Authentication)

Student 
- `GET /student` get student information
- `GET /student/credhrs` get remaining credit hours
- `POST /student/register/{cid}` register a course

Course
- `GET /course?keywords=[string]` get a list of eligible courses that are filtered with the keywords
- `GET /course?year=[int]` get a list of courses within year range
- `GET /course/recommend` get a list of recommend courses
- `GET /course/eligible` get a list of eligible courses
- `GET /course/taking` get a list of current taking courses
- `GET /course/completed` get a list of completed courses
- `GET /course/{cid}` get information of a course
- `GET /course/{cid}/comments` get a list of comment of the course

Enroll
- `DELETE /enroll/{eid}` drop a enrollment
- `GET /enroll/{eid}` get a enrollment
- `POST /enroll` (contain an `Enroll` object as request data) update the enrollment that contain the same id
- `GET /enroll/all` get a list of all enrollment
- `GET /enroll/current` get a list of current taking enrollment
- `GET /enroll/completed` get a list of completed enrollment

Faculty
- `GET /faculty/{fid}` get information of a faculty