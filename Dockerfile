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

ARG SmtpServer=${SmtpServer}
ARG SmtpUserName=${SmtpUserName}
ARG SmtpPassword=${SmtpPassword}
ARG SmtpServerPort=${SmtpServerPort}
ARG EnableSsl=${EnableSsl}
ARG UseDefaultCredentials=${UseDefaultCredentials}
ARG EmailDisplayName=${EmailDisplayName}
ARG SendersName=${SendersName}
RUN echo $SmtpPassword
RUN echo $SmtpUserName


ENV SmtpServer=${SmtpServer}
ENV SmtpUserName=${SmtpUserName}
ENV SmtpPassword=${SmtpPassword}
ENV SmtpServerPort=${SmtpServerPort}
ENV UseDefaultCredentials=${UseDefaultCredentials}
ENV EmailDisplayName=${EmailDisplayName}
ENV SendersName=${SendersName}
ENV EnableSsl=${EnableSsl}



COPY "TicketCRM.sln" "TicketCRM.sln"

COPY ["TicketCRM.ApplicationLayer.MainBoundedContext/TicketCRM.ApplicationLayer.MainBoundedContext.csproj", "TicketCRM.ApplicationLayer.MainBoundedContext/"]
COPY ["TicketCRM.ApplicationLayer.SeedWork/TicketCRM.ApplicationLayer.SeedWork.csproj", "TicketCRM.ApplicationLayer.SeedWork/"]
COPY ["TicketCRM.DataLayer.DataAccess/TicketCRM.DataLayer.DataAccess.csproj", "TicketCRM.DataLayer.DataAccess/"]
COPY ["TicketCRM.DataLayer.EmailTemplates/TicketCRM.DataLayer.EmailTemplates.csproj" ,"TicketCRM.DataLayer.EmailTemplates/"]
COPY ["TicketCRM.DomainLayer.MainBoundedContext/TicketCRM.DomainLayer.MainBoundedContext.csproj", "TicketCRM.DomainLayer.MainBoundedContext/"]
COPY ["TicketCRM.DomainLayer.MainBoundedContextDTO/TicketCRM.DomainLayer.MainBoundedContextDTO.csproj", "TicketCRM.DomainLayer.MainBoundedContextDTO/"]
COPY ["TicketCRM.Infrastructure.Utilities/TicketCRM.Infrastructure.Utilities.csproj", "TicketCRM.Infrastructure.Utilities/"]
COPY ["IdentityServerAspNetIdentity/IdentityServerAspNetIdentity.csproj", "IdentityServerAspNetIdentity/id"]


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
