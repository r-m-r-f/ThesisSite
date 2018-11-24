FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["ThesisSite/ThesisSite.csproj", "ThesisSite/"]
RUN dotnet restore "ThesisSite/ThesisSite.csproj"
COPY . .
WORKDIR "/src/ThesisSite"
RUN dotnet build "ThesisSite.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ThesisSite.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ThesisSite.dll"]