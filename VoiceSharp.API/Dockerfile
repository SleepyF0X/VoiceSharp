FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["VoiceSharp/VoiceSharp.csproj", "VoiceSharp/"]
RUN dotnet restore "VoiceSharp/VoiceSharp.csproj"
COPY . .
WORKDIR "/src/VoiceSharp"
RUN dotnet build "VoiceSharp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "VoiceSharp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VoiceSharp.dll"]
