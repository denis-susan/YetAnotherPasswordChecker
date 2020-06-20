FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY YetAnotherPasswordChecker.sln .
COPY YetAnotherPasswordChecker.DAL/YetAnotherPasswordChecker.DAL.csproj ./YetAnotherPasswordChecker.DAL/
COPY YetAnotherPasswordChecker.BLL/YetAnotherPasswordChecker.BLL.csproj ./YetAnotherPasswordChecker.BLL/
COPY YetAnotherPasswordChecker/YetAnotherPasswordChecker.csproj ./YetAnotherPasswordChecker/
RUN dotnet restore
COPY . .
WORKDIR "/src/."
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "YetAnotherPasswordChecker.dll"]