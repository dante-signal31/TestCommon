# TestCommon
Common functions useful for C# tests.
____

In this package you can find some functions I use frequently at my tests.

## Modules list
### Fs
Package with filesystem utilities. They are useful to prepare folders and files for your tests.
###### Crypto
Cryptographic functions for your tests. Here you can find hashing functions to check file contents.
###### Filewalker
Iterator to traverse folder trees.
###### Ops
Functions for file operations (copy, delete, etc).
###### Temp
Class to create both temporal files and folders to be removed when a code block ends.
### Random
Utilities to generate random content for your tests. Includes next modules:
###### Strings
Functions to create random strings.
### System
Utilities to deal with your hot operating system. Includes next modules:
###### TemporalEnvironmentVariable
Functions to manipulate environment variables