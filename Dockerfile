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


COPY "TicketCRM.sln" "TicketCRM.sln"

COPY ["TicketCRM.ApplicationLayer.MainBoundedContext/TicketCRM.ApplicationLayer.MainBoundedContext.csproj", "TicketCRM.ApplicationLayer.MainBoundedContext/"]
COPY ["TicketCRM.ApplicationLayer.SeedWork/TicketCRM.ApplicationLayer.SeedWork.csproj", "TicketCRM.ApplicationLayer.SeedWork/"]
COPY ["TicketCRM.Email.Integrations.DataAccess/TicketCRM.Email.Integrations.DataAccess.csproj", "TicketCRM.Email.Integrations.DataAccess/"]
COPY ["TicketCRM.Email.Templates/TicketCRM.Email.Templates.csproj" ,"TicketCRM.Email.Templates/"]
COPY ["TicketCRM.DomainLayer.MainBoundedContext/TicketCRM.DomainLayer.MainBoundedContext.csproj", "TicketCRM.DomainLayer.MainBoundedContext/"]
COPY ["TicketCRM.DomainLayer.MainBoundedContextDTO/TicketCRM.DomainLayer.MainBoundedContextDTO.csproj", "TicketCRM.DomainLayer.MainBoundedContextDTO/"]
COPY ["TicketCRM.Infrastructure.Utils/TicketCRM.Infrastructure.Utils.csproj", "TicketCRM.Infrastructure.Utils/"]
COPY ["IdentityServerAspNetIdentity/IdentityServerAspNetIdentity.csproj", "IdentityServerAspNetIdentity/"]


RUN dotnet restore "TicketCRM.ApplicationLayer.MainBoundedContext/TicketCRM.ApplicationLayer.MainBoundedContext.csproj"
COPY . .
WORKDIR "/src/TicketCRM.ApplicationLayer.MainBoundedContext"
RUN dotnet build "TicketCRM.ApplicationLayer.MainBoundedContext.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TicketCRM.ApplicationLayer.MainBoundedContext.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TicketCRM.ApplicationLayer.MainBoundedContext.dll"]
