# Test3dEYE

Tools for creating and sorting a large text file.

The task is described in the file "Test Task Sorting v.03" placed in the top folder.
FileGenerator - generates an unsorted file of a given size at a given path.
Sorter - sorts the file received from FileGenerator and saves the result to a new file with given name.

The project uses a simple benchmark in the form of measuring the execution time of key parts of the logic.
Unit tests nUnit are used (Test project).
Efficient use of resources when processing large files: processing a 5 GB file consumes about 200 MB of RAM.

## Instructions for use
The project is built for Windows. Therefore, it is recommended to use PowerShell for commands
Use <global_path> - global path to folder  <global_path_to_project_folder>/Build/net8.0


### FileGenerator

```bash
<global_path>/FileGenerator.exe <global_path>/<unsorted_file_name.txt> <file_size>
```
file size can be entered in format:

nB (Bytes)  
nKB (KiloBytes)  
nMB (MegaBytes)  
nGB (GigaBytes)

where "n" is any amount.

For example if your project is located in the D:/FileSorter folder and you need to generate 100Mb file use this command:

```bash
D:/FileSorter/Build/net8.0/FileGenerator.exe D:/FileSorter/Build/net8.0/unsorted.txt 100Mb
```

### Sorter 

```bash
<global_path>/Sorter.exe <global_path>/<unsorted_file_name.txt> <global_path>/<sorted_file_name.txt><file_size>
```
For example use this command:

```bash
D:/FileSorter/Build/net8.0/Sorter.exe D:/FileSorter/Build/net8.0/unsorted.txt D:/FileSorter/Build/net8.0/sorted.txt
```

You could avoid to add global path in commands by adding <global_path_to_project_folder>/Build/net8.0 folder to your system Path variables.