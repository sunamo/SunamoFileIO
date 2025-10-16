# NuGet Alternatives to SunamoFileIO

This document lists popular NuGet packages that provide similar functionality to SunamoFileIO.

## Overview

File I/O operations

## Alternative Packages

### System.IO.File
- **NuGet**: System.IO.FileSystem
- **Purpose**: Built-in file operations
- **Key Features**: Read, write, copy, delete, exists checks

### System.IO.Abstractions
- **NuGet**: System.IO.Abstractions
- **Purpose**: Testable file I/O
- **Key Features**: Mockable file system, dependency injection

### Zio
- **NuGet**: Zio
- **Purpose**: Virtual file systems
- **Key Features**: Memory FS, sub FS, aggregated FS

### Polly
- **NuGet**: Polly
- **Purpose**: Resilient file operations
- **Key Features**: Retry on failures, transient error handling

## Comparison Notes

System.IO for standard operations. System.IO.Abstractions for testable code.

## Choosing an Alternative

Consider these alternatives based on your specific needs:
- **System.IO.File**: Built-in file operations
- **System.IO.Abstractions**: Testable file I/O
- **Zio**: Virtual file systems
