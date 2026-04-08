# SignalR Sample APP - .NET 10.0

The purpose of this sample is to demonstrate how to use SignalR in a .NET 10.0 application.
It includes a simple web application that allows users to send messages to each other in real-time.
User can open multiple browser windows to see the real-time communication in action.
Additionally, it includes a simple API that allows users to send messages to the SignalR hub via HTTP requests and receive responses in real-time.
API documentation is available at http://localhost:5009/scalar/v1 when the application is running.

## Setup and Run
To update certficates, run  on terminal to update the development certificates for HTTPS:
dotnet dev-certs https --clean
dotnet dev-certs https --trust

Web App UI:
http://localhost:5009/test-client.html

Scalar API UI:
http://localhost:5009/scalar/v1

## Notes on SignalR. 
SignalR is a library for ASP.NET that allows you to add real-time web functionality to your applications.
It enables server-side code to push content to clients instantly as it becomes available, rather than having the server wait for a client to request new data. 
This is particularly useful for applications that require high-frequency updates, such as chat applications, live dashboards, or gaming.

SignalR supports WebSockets, which is the most efficient transport method for real-time communication.
However, if WebSockets are not available, SignalR can fall back to other techniques such as Server-Sent Events or Long Polling to ensure that real-time communication still works.

SignalR abstracts away the complexities of managing connections and allows developers to focus on the application logic rather than the underlying communication protocols.
There is no need to worry about connection management, reconnection logic, or scaling out across multiple servers, as SignalR can handle these aspects for you.