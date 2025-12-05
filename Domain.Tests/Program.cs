using Domain;

Console.OutputEncoding = System.Text.Encoding.UTF8;

Console.WriteLine("=== Демонстрація роботи бібліотеки SubscriptionTdd ===");
Console.WriteLine();

// 1. Обираємо план
var plan = Plan.Monthly();
Console.WriteLine($"Створено план: {plan}");

// 2. Створюємо підписку
var start = DateTime.UtcNow.AddDays(-5);
var subscription = Subscription.Create(plan, start);

Console.WriteLine($"Підписка створена:");
Console.WriteLine($"  План: {subscription.Plan.Name}");
Console.WriteLine($"  Початок: {subscription.Start}");
Console.WriteLine($"  Кінець: {subscription.End}");
Console.WriteLine($"  Чи активна? {subscription.IsActive()}");

Console.WriteLine();

// 3. Продовжуємо підписку
var now = DateTime.UtcNow;
var renewed = subscription.Renew(now);

Console.WriteLine("Після продовження:");
Console.WriteLine($"  Новий початок: {renewed.Start}");
Console.WriteLine($"  Новий кінець: {renewed.End}");
Console.WriteLine($"  Чи активна? {renewed.IsActive()}");

Console.WriteLine();

// 4. Зміна плану
var newPlan = Plan.Annual();
var changed = renewed.ChangePlan(newPlan, now);

Console.WriteLine("Після зміни плану:");
Console.WriteLine($"  Новий план: {changed.Plan.Name}");
Console.WriteLine($"  Новий початок: {changed.Start}");
Console.WriteLine($"  Новий кінець: {changed.End}");
Console.WriteLine($"  Чи активна? {changed.IsActive()}");

Console.WriteLine();
Console.WriteLine("Демонстрацію завершено. Натисніть будь-яку клавішу...");
Console.ReadKey();
