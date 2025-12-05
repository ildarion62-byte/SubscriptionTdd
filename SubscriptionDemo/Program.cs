using Domain;
using System.Numerics;

Console.OutputEncoding = System.Text.Encoding.UTF8;

Console.WriteLine("=== Демонстрація роботи бібліотеки SubscriptionTdd ===");
Console.WriteLine();

// Поточний момент часу
var now = DateTime.UtcNow;

// 1. Обираємо план
var plan = Plan.Monthly();
Console.WriteLine($"Створено план: {plan}");

// 2. Створюємо підписку, яка почалася 5 днів тому
var start = now.AddDays(-5);
var subscription = Subscription.Create(plan, start);

Console.WriteLine("Підписка створена:");
Console.WriteLine($"  План: {subscription.Plan.Name}");
Console.WriteLine($"  Початок: {subscription.Start}");
Console.WriteLine($"  Кінець: {subscription.End}");
Console.WriteLine($"  Чи активна? {subscription.IsActive(now)}");

Console.WriteLine();

// 3. Продовжуємо підписку
var renewed = subscription.Renew(now);

Console.WriteLine("Після продовження:");
Console.WriteLine($"  Новий початок: {renewed.Start}");
Console.WriteLine($"  Новий кінець: {renewed.End}");
Console.WriteLine($"  Чи активна? {renewed.IsActive(now)}");

Console.WriteLine();

// 4. Зміна плану
var newPlan = Plan.Annual();
var changed = renewed.ChangePlan(newPlan, now);

Console.WriteLine("Після зміни плану:");
Console.WriteLine($"  Новий план: {changed.Plan.Name}");
Console.WriteLine($"  Новий початок: {changed.Start}");
Console.WriteLine($"  Новий кінець: {changed.End}");
Console.WriteLine($"  Чи активна? {changed.IsActive(now)}");

Console.WriteLine();
Console.WriteLine("Демонстрацію завершено. Натисніть будь-яку клавішу...");
Console.ReadKey();
