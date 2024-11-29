# clientServerApp

Это кроссплатформенное клиент-серверное приложение, где сервер отправляет текстовые сообщения клиенту в зависимости от текущей даты и времени.

## Зависимости
- Docker
- Docker compose
- git

## Установка

1. Клонировать репозиторий:
    ```bash
    git clone https://github.com/Davabbb/clientServerApp.git
    cd clientServerApp
    ```

## Использование

### Запуск приложения с помощью Docker compose

1. Убедитесь, что Docker и Docker compose установлены.
2. Запустить приложение с помощью DOcker compose:
    ```bash
    docker compose up --build
    ```
    флаг build нужен для того, чтобы Docker images собирались автоматически

## Логи приложения

### Логи сервера

#### Логи при обработке WebSocket-запросов

1. **WebSocket request received**
   - **Уровень**: INFO
   - **Описание**: Этот лог указывает, что сервер получил WebSocket-запрос от клиента.

2. **Client connected**
   - **Уровень**: INFO
   - **Описание**: Этот лог указывает, что клиент успешно подключился к WebSocket-серверу.

3. **Not a WebSocket request**
   - **Уровень**: WARN
   - **Описание**: Этот лог указывает, что полученный запрос не является WebSocket-запросом.

#### Логи при отправке сообщений клиенту

4. **Entering SendMessageAsync method**
   - **Уровень**: INFO
   - **Описание**: Этот лог указывает, что сервер начал выполнение метода `SendMessageAsync`, который отправляет сообщения клиенту.

5. **Sending message to client**
   - **Уровень**: INFO
   - **Описание**: Этот лог указывает, что сервер готовится отправить сообщение клиенту.

6. **Send message: [message]**
   - **Уровень**: INFO
   - **Описание**: Этот лог указывает, что сообщение было успешно отправлено клиенту.

#### Логи при генерации сообщений

7. **Time: [numbers]**
   - **Уровень**: DEBUG
   - **Описание**: Этот лог указывает текущие значения даты и времени, используемые для генерации сообщения.

### Логи клиента

Клиентское приложение генерирует следующие логи:

- **INFO**: Информационные сообщения о подключении к серверу и получении сообщений.

#### Логи при подключении к серверу

1. **Connected to server**
   - **Уровень**: INFO
   - **Описание**: Этот лог указывает, что клиент успешно подключился к WebSocket-серверу.

#### Логи при получении сообщений от сервера

2. **[message]**
   - **Уровень**: INFO
   - **Описание**: Этот лог указывает, что клиент получил сообщение от сервера.

#### Логи при завершении соединения

3. **End connection**
   - **Уровень**: INFO
   - **Описание**: Этот лог указывает, что соединение с сервером завершено.

## Автоматизация с GitHub Actions

GitHub Actions настроены для автоматизации сборки и тестирования.