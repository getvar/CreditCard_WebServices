namespace Tuya.CreditCard.Api.Common.Contracts
{
    public interface IApiAccessorUserData
    {
        string GetUserName();
        Guid GetUserId();
    }
}
