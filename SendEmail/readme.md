# Email Sending Service

This project provides a simple email sending service using C# and .NET. It includes an `EmailService` class for sending emails and a basic web API endpoint to trigger email sending.

## Features

- Send emails using SMTP
- Configurable SMTP settings
- Basic error handling and logging
- Web API endpoint for sending emails

## Prerequisites

- SMTP server access

## Configuration

Before using the email service, you need to configure the following settings in the `EmailService` class:

```csharp
private static readonly string Sender = "YOUR_EMAIL";
private static readonly string Password = "YOUR_PASSWORD";
private static readonly string Username = "YOUR_USERNAME";
private static readonly string SmptServer = "SMPT_SERVER";
private static readonly int PortSMTP = 0; // PORT_SMTP
```

Replace the placeholder values with your actual SMTP server details.

## Usage

### Sending an email programmatically

To send an email using the `EmailService` class:

```csharp
using SendEmail.Service;

EmailService.SendEmail("recipient@example.com", "Subject", "Email body");
```

### Using the Web API

The project includes a simple web API with a single endpoint for sending emails. To use it:

1. Start the application.
2. Send a GET request to the root URL ("/").
3. The API will send a predefined email and return a success message.

## API Endpoint

- **URL**: `/`
- **Method**: `GET`
- **Response**: 
  - Success: "E-mail foi enviado!" (Email was sent!)

## Error Handling

The `EmailService` includes basic error handling. If an error occurs while sending an email, it will be logged to the console.

## Customization

To customize the email content sent via the API, modify the `User` object creation in the `Program.cs` file:

```csharp
var user = new SendEmailRequest("to@example.com", "Hello world", "<h1>Hello World</h1>");
```

## Security Considerations

- Ensure that you keep your SMTP credentials secure and don't commit them to version control.
- Consider using environment variables or a secure configuration management system for storing sensitive information.
- Use HTTPS in production to protect data in transit.

## Contributing

[Add information about how others can contribute to this project]

## License

[Add your license information here]