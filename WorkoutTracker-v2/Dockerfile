#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["WorkoutTracker-v2/WorkoutTracker-v2.csproj", "WorkoutTracker-v2/"]
RUN dotnet restore "WorkoutTracker-v2/WorkoutTracker-v2.csproj"
COPY . .
WORKDIR "/src/WorkoutTracker-v2"
RUN dotnet build "WorkoutTracker-v2.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WorkoutTracker-v2.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WorkoutTracker-v2.dll"]