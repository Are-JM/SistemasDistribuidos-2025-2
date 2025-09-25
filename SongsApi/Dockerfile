FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080


ENV ASPNETCORE_URL=http://+:8080
ENV MINOMBRE_ES=Areli

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["SongsApi.csproj","./"]
RUN dotnet restore "SongsApi.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "SongsApi.csproj" -c ${configuration} -o /app/build

RUN ls

FROM build AS publish
ARG configuration=Realese
RUN dotnet publish "SongsApi.csproj" -c ${configuration} -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT [ "dotnet", "SongsApi.dll" ]
