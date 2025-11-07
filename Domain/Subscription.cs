using System;

namespace Domain
{
    public sealed class Subscription
    {
        public Plan Plan { get; private set; }
        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }

        private Subscription(Plan plan, DateTime start, DateTime end)
        {
            Plan = plan;
            Start = start;
            End = end;
        }

        public static Subscription Create(Plan plan, DateTime startUtc)
        {
            var end = startUtc + plan.Duration;
            return new Subscription(plan, startUtc, end);
        }

        public bool IsActive(DateTime nowUtc) => nowUtc >= Start && nowUtc < End;

        public Subscription Renew(DateTime nowUtc)
        {
            var newStart = IsActive(nowUtc) ? End : nowUtc;
            var newEnd = newStart + Plan.Duration;
            return new Subscription(Plan, newStart, newEnd);
        }

        public Subscription ChangePlan(Plan newPlan, DateTime nowUtc)
        {
            var newStart = nowUtc;
            var newEnd = nowUtc + newPlan.Duration;
            return new Subscription(newPlan, newStart, newEnd);
        }
    }
}
