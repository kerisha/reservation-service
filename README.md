# booking-microservice

# Stack

## .net7 webapp - frontend
## .net7 minimal api - backend
## rabbitmq - queueing structure
## sql database - data
## .net7 console app - SMS sender

### Concept
Guest House Mangement System for making reservations

### Flow
- User registers and logs in and makes a reservation via frontend
- Reservation is sent to backend API
- Backend API save reservation to the database
- Backend API sends a reservation completion notice to a queue (RabbitMQ)
- A console application listens to new messages on queue. On message received, it sends an SMS notifying user that reservation has been successful