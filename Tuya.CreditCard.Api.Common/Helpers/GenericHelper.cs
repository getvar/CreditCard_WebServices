namespace Tuya.CreditCard.Api.Common.Helpers
{
    public static class GenericHelper
    {
        public static string GenerateGuidWithoutHyphen()
        {
            var guid = Guid.NewGuid();
            return guid.ToString("N");
        }

        public static string GenerateRandomStringValueFromList(List<string> list)
        {
            Random rdn = new Random();
            return list[rdn.Next(list.Count - 1)];
        }
    }
}
