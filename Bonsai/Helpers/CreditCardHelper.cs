namespace Bonsai.Helpers
{
	public static class CreditCardHelper
	{
		public static string MaskCreditCardNumber(string Number)
        {
			int NumberQuantityToNotMask = 4;
			int NumberOfCreditCardCaracters = 16;
            string EndNumbers = Number.Substring(Number.Length - NumberQuantityToNotMask);
			string MaskedNumbers = string.Concat(Enumerable.Repeat("*", NumberOfCreditCardCaracters - NumberQuantityToNotMask));
			return MaskedNumbers + EndNumbers;
        }
	}
}

