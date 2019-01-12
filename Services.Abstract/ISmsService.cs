using System.Threading.Tasks;

namespace Services.Abstract
{
    public interface ISmsService
    {
        Task SendSms(string phoneNumber, string message);
    }
}
