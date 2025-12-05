# Етап збірки
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Копіюємо всі файли рішення в контейнер
COPY . .

# Публікуємо консольний проєкт SubscriptionDemo
RUN dotnet publish SubscriptionDemo/SubscriptionDemo.csproj -c Release -o /app/publish

# Етап рантайму
FROM mcr.microsoft.com/dotnet/runtime:9.0 AS runtime
WORKDIR /app

# Копіюємо надруковані файли з попереднього етапу
COPY --from=build /app/publish .

# Запуск додатку
ENTRYPOINT ["dotnet", "SubscriptionDemo.dll"]
