using BookTaxiEntyties.Entyties;

namespace BookTaxiEntyties.Contracts;

public interface IPaymentRepository
{
    Task<Payments> Update(Payments entity);
    Task<Payments> Delete(Payments entity);
    Task AddPayment(Payments entity);
    Task<List<Payments>> GetAllPayments();
    Task<Payments> GetByIdPayment(int id);
    Task<Payments> GetByAmount(string amount);
    Task<Payments> GetByStatus(string status);
    Task<Payments> GetByPaymentDate(DateTime paymentDate);
}