using System.Collections.Immutable;

namespace Tuya.CreditCard.Api.Common.Helpers
{
    public static class PaymentHelper
    {
        public static readonly ImmutableList<string> BANK_LIST = ["TUYA", "BANCO 1", "BANCO 2", "BANCO 3"];
        public static readonly ImmutableList<string> FRANCHISE_LIST = ["VISA", "MASTER CARD", "AMERICAN EXPRESS"];
    }
}
