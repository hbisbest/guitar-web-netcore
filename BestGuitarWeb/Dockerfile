FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["BestGuitarWeb/BestGuitarWeb.csproj", "BestGuitarWeb/"]
RUN dotnet restore "BestGuitarWeb/BestGuitarWeb.csproj"
COPY . .
WORKDIR "/src/BestGuitarWeb"
RUN dotnet build "BestGuitarWeb.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "BestGuitarWeb.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "BestGuitarWeb.dll"]