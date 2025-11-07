using System;
using Xunit;
using Domain;
using System.Numerics;

namespace Domain.Tests
{
    public class Subscription_Tests
    {
        [Fact]
        public void IsActive_ReturnsTrue_WhenNowBetweenStartAndEnd()
        {
            var plan = Plan.Monthly();
            var now = new DateTime(2025, 11, 07, 12, 0, 0, DateTimeKind.Utc);
            var start = now.AddDays(-1);

            var sub = Subscription.Create(plan, start);

            Assert.True(sub.IsActive(now));
        }

        [Fact]
        public void IsActive_ReturnsFalse_WhenExpired()
        {
            var plan = Plan.Monthly();
            var now = new DateTime(2025, 11, 07, 12, 0, 0, DateTimeKind.Utc);
            var start = now.AddMonths(-2);

            var sub = Subscription.Create(plan, start);

            Assert.False(sub.IsActive(now));
        }

        [Fact]
        public void Renew_ExtendsFromEnd_WhenActive()
        {
            var plan = Plan.Weekly();
            var now = new DateTime(2025, 11, 07, 12, 0, 0, DateTimeKind.Utc);
            var start = now.AddDays(-1);

            var sub = Subscription.Create(plan, start);

            var renewed = sub.Renew(now);
            Assert.Equal(sub.End.Add(plan.Duration), renewed.End);
        }

        [Fact]
        public void Renew_StartsFromNow_WhenExpired()
        {
            var plan = Plan.Weekly();
            var now = new DateTime(2025, 11, 07, 12, 0, 0, DateTimeKind.Utc);
            var start = now.AddDays(-14);

            var sub = Subscription.Create(plan, start);

            var renewed = sub.Renew(now);
            Assert.Equal(now.Add(plan.Duration), renewed.End);
        }

        [Fact]
        public void ChangePlan_TakesEffectNow()
        {
            var monthly = Plan.Monthly();
            var annual = Plan.Annual();
            var now = new DateTime(2025, 11, 07, 12, 0, 0, DateTimeKind.Utc);

            var sub = Subscription.Create(monthly, now.AddDays(-10));
            var changed = sub.ChangePlan(annual, now);

            Assert.Equal(annual, changed.Plan);
            Assert.Equal(now.Add(annual.Duration), changed.End);
            Assert.True(changed.IsActive(now));
        }
    }
}
