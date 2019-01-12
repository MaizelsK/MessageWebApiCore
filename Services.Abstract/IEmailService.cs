using System.Threading.Tasks;

namespace Services.Abstract
{
    public interface IEmailService
    {
        Task SendEmail(string to, string title, string body);
    }
}
