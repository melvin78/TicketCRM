FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
RUN apt-get update \
&& apt-get install -y --no-install-recommends libgdiplus libc6-dev \
&& apt-get clean \
&& rm -rf /var/lib/apt/lists/*
RUN cd /usr/lib && ln -s libgdiplus.so gdiplus.dll

WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["IdentityServerAspNetIdentity/IdentityServerAspNetIdentity.csproj", "IdentityServerAspNetIdentity/"]

COPY ["Centrino.ApplicationLayer.SeedWork/Centrino.ApplicationLayer.SeedWork.csproj", "Centrino.ApplicationLayer.SeedWork/"]
COPY ["Centrino.Email.Templates/Centrino.Email.Templates.csproj" ,"Centrino.Email.Templates/"]
COPY ["Centrino.DomainLayer.MainBoundedContextDTO/Centrino.DomainLayer.MainBoundedContextDTO.csproj", "Centrino.DomainLayer.MainBoundedContextDTO/"]
COPY ["Centrino.Infrastructure.Utils/Centrino.Infrastructure.Utils.csproj", "Centrino.Infrastructure.Utils/"]


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
