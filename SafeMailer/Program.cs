namespace SafeMailer;

class Program
{
    static async Task Main(string[] args)
    {
        var emailServices = new List<IEmailService>
        {
            new SendGridEmailService(),
            new GmailEmailService(),
            new YahooEmailService()
        };

        var emailManager = new EmailServiceManager(emailServices);
        await emailManager.SendEmail("example@example.com", "Test Subject", "Test Body");
    }
}

public interface IEmailService
{
    Task<bool> SendEmail(string to, string subject, string body);
}

public class EmailServiceManager
{
    private readonly List<IEmailService> services;

    public EmailServiceManager(List<IEmailService> services)
    {
        this.services = services;
    }

    public async Task<bool> SendEmail(string to, string subject, string body)
    {
        foreach (var service in services)
        {
            var result = await TrySendEmail(service, to, subject, body);

            if (result) return true;
        }

        return false;
    }

    private async Task<bool> TrySendEmail(IEmailService service, string to, string subject, string body)
    {
        var retries = 3;
        while (retries-- > 0)
        {
            var result = await service.SendEmail(to, subject, body);

            if (result) return true;
        }

        return false;
    }
}

public class SendGridEmailService : IEmailService
{
    public async Task<bool> SendEmail(string to, string subject, string body)
    {
        await Task.Delay(5000);
        return true;
    }
}

public class GmailEmailService : IEmailService
{
    public async Task<bool> SendEmail(string to, string subject, string body)
    {
        await Task.Delay(5000);
        return true;
    }
}

public class YahooEmailService : IEmailService
{
    public async Task<bool> SendEmail(string to, string subject, string body)
    {
        await Task.Delay(5000);
        return true;
    }
}