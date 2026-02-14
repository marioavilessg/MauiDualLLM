# LLMDebate

Este repositorio contiene dos proyectos desarrollados con .NET MAUI:

- **MauiApp1**
- **MauiApp2**

Ambos proyectos están diseñados para servir como aplicaciones multiplataforma (Android, iOS, Windows, MacCatalyst, Tizen) y comparten una estructura similar, incluyendo modelos, servicios, vistas, y viewmodels.

## Estructura General

Cada proyecto contiene las siguientes carpetas principales:

- **Models/**: Clases de datos como `AppSettings` y `ChatMessage`.
- **Services/**: Servicios para lógica de negocio y comunicación, como `LlmService`, `RabbitMqService` y `SettingsService`.
- **ViewModels/**: Lógica de presentación para las vistas, como `ChatViewModel` y `ConfigViewModels`.
- **Views/**: Vistas de la aplicación (páginas XAML y su code-behind).
- **Resources/**: Recursos como imágenes, fuentes y estilos.
- **Platforms/**: Archivos específicos de cada plataforma soportada.

## Descripción de los Proyectos

### MauiApp1

Aplicación principal que implementa funcionalidades de chat y configuración, utilizando servicios para interactuar con modelos de lenguaje (LLM) y mensajería RabbitMQ.

### MauiApp2

Idéntica a la primera aplicación salvo en configuraciones como las colas de rabbitMQ y modelo.

## Requisitos

- .NET 8.0 o superior
- Visual Studio 2022 o superior con soporte para MAUI

## Ejecución

Nota: Debes tener apis de modelos activas y ponerlas en Models/AppSettings, además de rabbitMQ.

1. Abre la solución `LLMDebate.sln` en Visual Studio.
3. Selecciona el proyecto que deseas ejecutar (`MauiApp1` o `MauiApp2`).
4. Elige la plataforma de destino (Android, iOS, Windows, etc.).
5. Ejecuta la aplicación.

## Notas

- Los servicios y modelos pueden ser reutilizados o adaptados entre ambos proyectos.
- La carpeta `Resources/Raw` contiene archivos adicionales de referencia.
