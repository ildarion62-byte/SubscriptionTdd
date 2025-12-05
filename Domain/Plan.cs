using System;

namespace Domain
{
    public sealed class Plan : IEquatable<Plan>
    {
        public string Name { get; }
        public TimeSpan Duration { get; }

        private Plan(string name, TimeSpan duration)
        {
            Name = name;
            Duration = duration;
        }

        public static Plan Weekly() => new("Weekly", TimeSpan.FromDays(7));
        public static Plan Monthly() => new("Monthly", TimeSpan.FromDays(30)); 
        public static Plan Annual() => new("Annual", TimeSpan.FromDays(365)); 

        public bool Equals(Plan? other)
            => other is not null && Name == other.Name && Duration == other.Duration;

        public override bool Equals(object? obj) => Equals(obj as Plan);
        public override int GetHashCode() => HashCode.Combine(Name, Duration);
        public override string ToString() => $"{Name} ({Duration.TotalDays}d)";
    }
}
