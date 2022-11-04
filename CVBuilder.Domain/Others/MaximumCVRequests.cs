using CVBuilder.Domain.Enums;

namespace CVBuilder.Domain.Others
{
    public static class MaximumCVRequests
    {
        public static int GetCVRequestLimit(SubscriptionType subscriptionType)
        {
            if (subscriptionType == SubscriptionType.Silver)
                return 5;

            else if (subscriptionType == SubscriptionType.Gold)
            {
                return 10;
            }

            else if (subscriptionType == SubscriptionType.Platinum)
                return 15;

            else
                return 0;
        }
    }
}
