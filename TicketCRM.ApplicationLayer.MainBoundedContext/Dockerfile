FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ARG SmtpServer=${SmtpServer}
ARG SmtpUserName=${SmtpUserName}
ARG SmtpPassword=${SmtpPassword}
ARG SmtpServerPort=${SmtpServerPort}
ARG EnableSsl=${EnableSsl}
ARG UseDefaultCredentials=${UseDefaultCredentials}
ARG EmailDisplayName=${EmailDisplayName}
ARG SendersName=${SendersName}



ENV SmtpServer=${SmtpServer}
ENV SmtpUserName=${SmtpUserName}
ENV SmtpPassword=${SmtpPassword}
ENV SmtpServerPort=${SmtpServerPort}
ENV UseDefaultCredentials=${UseDefaultCredentials}
ENV EmailDisplayName=${EmailDisplayName}
ENV SendersName=${SendersName}
ENV EnableSsl=${EnableSsl}


FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TicketCRM.ApplicationLayer.MainBoundedContext/TicketCRM.ApplicationLayer.MainBoundedContext.csproj", "TicketCRM.ApplicationLayer.MainBoundedContext/"]

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
