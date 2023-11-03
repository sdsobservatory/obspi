FROM mcr.microsoft.com/dotnet/aspnet:7.0-bullseye-slim AS base
LABEL authors="Alex Helms"
LABEL organization="SDSO"
WORKDIR /app
EXPOSE 80

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/nightly/sdk:8.0 AS build
ARG TARGETARCH
WORKDIR /src
COPY . ./
RUN dotnet restore "Obspi/Obspi.csproj" -a $TARGETARCH
WORKDIR /src/Obspi
RUN dotnet publish "Obspi.csproj" -c Release -a $TARGETARCH --no-restore -o /app/publish

FROM node:20.9.0-bullseye-slim AS client
WORKDIR /app
ENV PATH /app/node_modules/.bin:$PATH
COPY "Obspi/client/" ./
RUN npm ci --silent
RUN npm run build

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
COPY --from=client /app/dist ./wwwroot
ENTRYPOINT ["dotnet", "Obspi.dll"]