namespace Tuya.CreditCard.Api.Common.Helpers
{
    public static class GenericHelper
    {
        public static string GenerateGuidWithoutHyphen()
        {
            var guid = Guid.NewGuid();
            return guid.ToString("N");
        }
    }
}
