namespace BookShopping.Repositories
{
    public interface IVnPayRepositories
    {
        string CreatePaymentUrl(HttpContext context, VnPaymentResquestModel model);
        VnpayResponsementModel PaymentExecute(IQueryCollection collection);
    }
}