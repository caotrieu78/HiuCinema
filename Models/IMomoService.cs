using BookingCinema.Models.MoMo;
using BookingCinema.Models.Oder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace BookingCinema.Models
{
    public interface IMomoService
    {
        Task<MomoCreatePaymentResponseModel> CreatePaymentAsync(OrderInfoModel model);
        MomoExecuteResponseModel PaymentExecuteAsync(IQueryCollection collection);
    }
}
