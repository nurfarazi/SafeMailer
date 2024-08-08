# MailRelay Skeleton

MailRelay is a robust email delivery system designed to ensure high reliability and fault tolerance by managing multiple third-party email services. It integrates seamlessly with providers like SendGrid, Gmail, and Yahoo, offering a fallback mechanism to handle failures and ensure emails are always sent without disruptions.

### Features

1.  Define Interface for Email Services: 
     
The `IEmailService` interface defines the contract for email services. Supports multiple email service providers including SendGrid, Gmail, and Yahoo.

2. Implement Service-Specific Adapters:

For each email service, implement an adapter that conforms to IEmailService. This can include SendGridEmailService, GmailEmailService, and YahooEmailService.

3. Service Retry and Fallback Mechanism:

Implement a mechanism to retry a failed email send operation with the same service a configurable number of times.
If retries fail, the system should automatically fall back to the next available service.

4. Service Orchestrator/Manager:

Create a service manager that holds a list of IEmailService instances and manages the process of sending emails, handling retries, and switching between services.

5. Logging and Monitoring:

Integrate logging to track each attempt to send an email, noting failures and successful sends.
Monitoring systems should be in place to alert on repeated failures or degraded performance.

6. Configuration Management:

Use a configuration system to manage the list of services and their priorities, retry limits, and other operational parameters.

### Design Patterns and OOP Principles Used

**Strategy Pattern**:

The `IEmailService` interface and its concrete implementations (like `SendGridEmailService`, `GmailEmailService`, etc.) represent the Strategy pattern. This pattern allows the algorithm's behavior to be selected at runtime. The different strategies encapsulate different email services, which can be switched out seamlessly without affecting the clients that use them.

**Facade Pattern**:

The `EmailServiceManager` acts as a facade that simplifies the interface for clients. Clients don’t need to manage or know about the different email services and their error-handling mechanisms.

**Adapter Pattern**:

If the third-party services have different APIs, each specific service implementation (`SendGridEmailService`, `GmailEmailService`, etc.) might also act as an Adapter. This adapts the third-party email service's API to the common `IEmailService` interface expected by your system.

**Dependency Injection**:

Providing the `EmailServiceManager` with a list of `IEmailService` instances (either through the constructor or a setter method) is an example of Dependency Injection. This promotes loose coupling and greater flexibility in the configuration of email services.

**Singleton Pattern**:

If there should only be a single instance of the `EmailServiceManager` throughout the application, implementing it as a Singleton could be considered. This ensures that email sending operations are centralized and easily manageable.

### SOLID Principles Applied

**Single Responsibility Principle (SRP)**:

Each class has a clear responsibility: service classes handle sending emails, while the manager orchestrates when and how these services are used.

**Open/Closed Principle (OCP)**:

The system is open for extension but closed for modification. You can add new email services by simply creating new classes that implement the `IEmailService` interface without modifying the existing manager logic.

**Liskov Substitution Principle (LSP)**:

Subtypes (different email service implementations) must be substitutable for their base type (`IEmailService`). This is ensured by adhering to the interface contract.

**Interface Segregation Principle (ISP)**:

The `IEmailService` interface is likely small and client-specific, meaning clients will only have to know about the `SendEmail` method, nothing more.

**Dependency Inversion Principle (DIP)**:

The design depends on abstractions (the `IEmailService` interface), not concretions (specific email services), which is crucial for maintaining the flexibility and decoupling of the system.