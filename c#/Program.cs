using System;
using System.IO;
using System.Xml;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length != 1)
        {
            Console.WriteLine("Usage: CSharpProgram.exe <source_folder>");
            Environment.Exit(1);
        }

        // Path of Folder to be Organized
        string sourceFolder = args[0];

        // Path of Folder after Organized
        string targetFolder = sourceFolder;

        Console.WriteLine($"Are you sure you want to Organize {sourceFolder}(Y/any key):");
        char userInput = Console.ReadKey().KeyChar;
        Console.WriteLine();
        if (userInput == 'Y' || userInput == 'y')
        {
            CategorizeFiles(sourceFolder, targetFolder);
            Console.WriteLine("Organized Successfully");

        }
        else
            Console.WriteLine("Did Not Organized, Exiting Program!");
    }

    static void CategorizeFiles(string sourceFolder, string destinationFolder)
    {
        // Create destination folders if they don't exist
        string[] categories = { "images", "videos", "documents", "music", "compressed", "programs", "general", "folder" };
        foreach (var category in categories)
        {
            string folderPath = Path.Combine(destinationFolder, category);
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
        }

        // Rename Folders to desired Name
        RenameFolder(sourceFolder, categories);


        // Get a list of files and folders in the source folder
        string[] items = Directory.GetFileSystemEntries(sourceFolder);

        // Categorize folders and move them to 'folder'
        foreach (var item in items)
        {
            if (Directory.Exists(item))
            {
                string itemName = Path.GetFileName(item);

                if (!categories.Contains(itemName.ToLower()))
                {
                    string destinationPath = Path.Combine(destinationFolder, "Folder", itemName);
                    Directory.Move(item, destinationPath);
                }
            }
        }

        // Get a list of files in the source folder
        string[] files = Directory.GetFiles(sourceFolder);

        // Categorize files based on their extensions
        foreach (var file in files)
        {
            string fileName = Path.GetFileName(file);
            string fileExtension = fileName.Split('.')[^1].ToLower();

            switch (fileExtension)
            {
                case "jpg":
                case "jpeg":
                case "png":
                case "gif":
                    MoveFile(file, destinationFolder, "Images");
                    break;
                case "mp4":
                case "avi":
                case "mkv":
                case "mov":
                    MoveFile(file, destinationFolder, "Videos");
                    break;
                case "pdf":
                case "doc":
                case "docx":
                case "txt":
                case "psd":
                case "html":
                case "htm":
                    MoveFile(file, destinationFolder, "Documents");
                    break;
                case "mp3":
                case "wav":
                case "flac":
                case "aac":
                    MoveFile(file, destinationFolder, "Music");
                    break;
                case "zip":
                case "rar":
                case "7z":
                    MoveFile(file, destinationFolder, "Compressed");
                    break;
                case "exe":
                case "msi":
                    MoveFile(file, destinationFolder, "Programs");
                    break;
                default:
                    MoveFile(file, destinationFolder, "General");
                    break;
            }
        }
    }

    // Move Files from source file path to destination folder
    static void MoveFile(string sourceFilePath, string destinationFolder, string category)
    {
        string fileName = Path.GetFileName(sourceFilePath);
        string destinationPath = Path.Combine(destinationFolder, category, fileName);
        File.Move(sourceFilePath, destinationPath);
    }


    // Rename Folder (example "images" to "Images")
    static void RenameFolder(string basePath, string[] foldersToRename)
    {
        try
        {
            foreach (string folderName in foldersToRename)
            {
                string oldFolderPath = Path.Combine(basePath, folderName);

                // Determine the new folder name with the first letter capitalized
                string newFolderName = char.ToUpper(folderName[0]) + folderName.Substring(1);

                // Combine the old folder path with the new folder name
                string newFolderPath = Path.Combine(basePath, newFolderName);

                // Check if the folder is already in the desired format
                if (!oldFolderPath.Equals(newFolderPath))
                {
                    // Rename the Folder
                    string tempFolder = Path.Combine(basePath, "TEMPORARYFOLDERTORENAMETHEOLDERFOLDER");
                    Directory.Move(oldFolderPath, tempFolder);
                    Directory.Move(tempFolder, newFolderPath);

                    Console.WriteLine($"Folder '{folderName}' renamed successfully to: {newFolderPath}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

}

