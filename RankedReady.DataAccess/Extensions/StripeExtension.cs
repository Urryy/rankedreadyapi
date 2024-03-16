using RankedReadyApi.Common.Enums;
using Stripe.Checkout;

namespace RankedReady.DataAccess.Extensions;

public static class StripeExtension
{
    public static void CreateSessionOptions(this SessionCreateOptions options, RankedReadyApi.Common.DataTransferObjects.ValorantAccount.AccountFullDto valAccount, RankedReadyApi.Common.DataTransferObjects.LeagueLegendAccount.AccountFullDto leagAccount)
    {
        if (valAccount != null)
        {
            if (!valAccount.IsActiveInShop || valAccount.StateAccount == StateAccount.Inactive.ToString() ||
                valAccount.StateAccount == StateAccount.Soled.ToString())
                throw new ArgumentException("Account isn't active in shop");

            var item = new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmount = (long)100 * (long)valAccount.Price,
                    Currency = "usd",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = "Valorant Account",
                        Description = $"Rank: {valAccount.Rank}"
                    },

                },
                Quantity = 1
            };

            options.LineItems.Add(item);
        }
        else if (leagAccount != null)
        {
            if (!leagAccount.IsActiveInShop || leagAccount.StateAccount == StateAccount.Inactive.ToString() ||
                leagAccount.StateAccount == StateAccount.Soled.ToString())
                throw new ArgumentException("Account isn't active in shop");

            var item = new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmount = (long)100 * (long)leagAccount.Price,
                    Currency = "usd",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = "League Legend Account",
                        Description = $"Rank: {leagAccount.Rank}"
                    },

                },
                Quantity = 1
            };

            options.LineItems.Add(item);
        }
        else
        {
            throw new Exception("Account doesn't exist");
        }
    }
}
