#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
RUN apt-get update -qq && apt-get -y install libgdiplus libc6-dev
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Binus.BL.CertificateGenerator/Binus.BL.CertificateGenerator.csproj", "Binus.BL.CertificateGenerator/"]
RUN dotnet restore "Binus.BL.CertificateGenerator/Binus.BL.CertificateGenerator.csproj"
COPY . .
WORKDIR "/src/Binus.BL.CertificateGenerator"
RUN dotnet build "Binus.BL.CertificateGenerator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Binus.BL.CertificateGenerator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN chmod 755 /app/Rotativa/Linux/wkhtmltopdf
ENTRYPOINT ["dotnet", "Binus.BL.CertificateGenerator.dll"]