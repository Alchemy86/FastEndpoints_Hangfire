# FastEndpoints_Hangfire
Demo app - Makes use of Fastendpoints and Hangfire to create jobs in memory, trigger, pause and continue with jobs one after another. 

Hangfire Endpoint: http//localhost:5006/hangfire
Swagger: http//localhost:5006/swagger

- All jobs paused by default
- Creates a primary job
- Creates 10 jobs to follow once complete
- Executes in sequnce confirmed
- All jobs unpaused at once when submitted

Tests the execution of hangfire jobs if fires by hangfire but paused within the method itself. Works fine.
