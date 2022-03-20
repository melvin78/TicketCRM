FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
RUN apt-get update \
&& apt-get install -y --no-install-recommends libgdiplus libc6-dev \
&& apt-get clean \
&& rm -rf /var/lib/apt/lists/*
RUN cd /usr/lib && ln -s libgdiplus.so gdiplus.dll

WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["IdentityServerAspNetIdentity/IdentityServerAspNetIdentity.csproj", "IdentityServerAspNetIdentity/"]

COPY ["TicketCRM.ApplicationLayer.SeedWork/TicketCRM.ApplicationLayer.SeedWork.csproj", "TicketCRM.ApplicationLayer.SeedWork/"]
COPY ["TicketCRM.Email.Templates/TicketCRM.Email.Templates.csproj" ,"TicketCRM.Email.Templates/"]
COPY ["TicketCRM.DomainLayer.MainBoundedContextDTO/TicketCRM.DomainLayer.MainBoundedContextDTO.csproj", "TicketCRM.DomainLayer.MainBoundedContextDTO/"]
COPY ["TicketCRM.Infrastructure.Utils/TicketCRM.Infrastructure.Utils.csproj", "TicketCRM.Infrastructure.Utils/"]


RUN dotnet restore "IdentityServerAspNetIdentity/IdentityServerAspNetIdentity.csproj"
COPY . .
WORKDIR "/src/IdentityServerAspNetIdentity"
RUN dotnet build "IdentityServerAspNetIdentity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IdentityServerAspNetIdentity.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IdentityServerAspNetIdentity.dll"]
