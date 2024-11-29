FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

COPY ../ServerApp/ServerApp.csproj ./ServerApp/
COPY Tests/Tests.csproj ./Tests/

RUN dotnet restore ./Tests/Tests.csproj

COPY ../ServerApp ./ServerApp
COPY ./Tests ./Tests

RUN dotnet build ./Tests/Tests.csproj -c Release -o out

FROM build-env AS testrunner
WORKDIR /app/Tests

ARG RUN_TESTS
RUN if [ "$RUN_TESTS" = "true" ]; then \
        echo "Список тестов:"; \
        dotnet test Tests.csproj --list; \
        echo "Запуск тестов..."; \
        dotnet test Tests.csproj --logger:console; \
    else \
        echo "Тесты пропущены"; \
    fi

