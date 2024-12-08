
# VideoChangeTitle

**VideoChangeTitle** es una herramienta automatizada para actualizar los títulos en los metadatos de archivos de series de televisión basándose en nombres de archivo y búsquedas en bases de datos externas.

## Características

-   Compatibilidad con formatos `.mkv`, `.avi`, y `.mp4`.
-   Normaliza nombres de archivo eliminando puntos innecesarios.
-   Recupera títulos desde bases de datos como **The Movie Database (TMDb)**.
-   Escribe nuevos títulos en los metadatos y registra la fecha de actualización.

## Requisitos

-   **Sistema operativo**: Windows
-   **.NET Framework**: 4.6 o superior
-   **Librerías necesarias**: `TagLib` para la manipulación de metadatos.

## Instalación

1.  Clona el repositorio:
2.  Abre el proyecto en Visual Studio.
3.  Compílalo y genera el ejecutable.

## Uso

1. Previamente, renombra los videos de las series con el siguiente format: [nombre] - [temporada]x[episodio].[extensión].
	Por ejemplo para un video con el capítulo 1 de la primera temporada de "mi serie favorita", el nombre del archivo ha de ser: mi serie favorita - 1x01.mp4
2. Coloca el ejecutable en el directorio de videos o pásalo como argumento al programa.
    
    cmd
    
    Copiar código
    
    `VideoChangeTitle.exe [ruta-del-directorio]` 
    
4.  La aplicación procesará todos los videos compatibles en el directorio especificado.

## Contribuciones

Se aceptan contribuciones en forma de **issues** o **pull requests**.

## Licencia

Este proyecto está bajo la licencia **Apache 2.0**.
