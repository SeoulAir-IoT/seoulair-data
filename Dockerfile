FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app

# copy of csproj and restore  as distinct layers
COPY *.sln .
COPY ./src/SeoulAir.Data.Api/*.csproj ./src/SeoulAir.Data.Api/
COPY ./src/SeoulAir.Data.Domain/*.csproj ./src/SeoulAir.Data.Domain/
COPY ./src/SeoulAir.Data.Domain.Services/*.csproj ./src/SeoulAir.Data.Domain.Services/
COPY ./src/SeoulAir.Data.Repositories/*.csproj ./src/SeoulAir.Data.Repositories/

RUN dotnet restore

# copy everything else and build app
COPY *.sln .
COPY ./src/SeoulAir.Data.Api/. ./src/SeoulAir.Data.Api/
COPY ./src/SeoulAir.Data.Domain/. ./src/SeoulAir.Data.Domain/
COPY ./src/SeoulAir.Data.Domain.Services/. ./src/SeoulAir.Data.Domain.Services/
COPY ./src/SeoulAir.Data.Repositories/. ./src/SeoulAir.Data.Repositories/

WORKDIR /app/src/SeoulAir.Data.Api
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app

COPY --from=build /app/src/SeoulAir.Data.Api/out ./
ENV ASPNETCORE_URLS=http://+:5600
ENTRYPOINT ["dotnet","SeoulAir.Data.Api.dll"]