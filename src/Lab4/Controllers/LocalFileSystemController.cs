using Itmo.ObjectOrientedProgramming.Lab4.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Controllers;

public class LocalFileSystemController : IFileSystemController
{
    private string? _currentPath;

    public bool IsConnected => _currentPath != null;

    public string CurrentDirectory => _currentPath ?? throw new InvalidOperationException("Файловая система не подключена.");

    public void Connect(string address)
    {
        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentException("Путь не может быть пустым.", nameof(address));

        if (!Directory.Exists(address))
            throw new DirectoryNotFoundException($"Директория '{address}' не существует.");

        _currentPath = address;
    }

    public void Disconnect()
    {
        if (!IsConnected)
            throw new InvalidOperationException("Файловая система уже отключена.");

        _currentPath = null;
    }

    public bool DirectoryExists(string address)
    {
        string fullPath = Path.Combine(_currentPath ?? throw new InvalidOperationException("Файловая система не подключена."), address);
        return Directory.Exists(fullPath);
    }

    public bool FileExists(string filePath)
    {
        string fullPath = Path.Combine(_currentPath ?? throw new InvalidOperationException("Файловая система не подключена."), filePath);
        return File.Exists(fullPath);
    }

    public void ChangeDirectory(string address)
    {
        string fullPath = Path.Combine(_currentPath ?? throw new InvalidOperationException("Файловая система не подключена."), address);

        if (!Directory.Exists(fullPath))
            throw new DirectoryNotFoundException($"Директория '{fullPath}' не существует.");

        _currentPath = fullPath;
    }

    public IEnumerable<DirectoryNode> GetDirectoryTree(string path, int depth)
    {
        return GetDirectoryTreeRecursive(path, depth);
    }

    public void MoveFile(string sourcePath, string destinationPath)
    {
        string sourceFullPath = Path.Combine(_currentPath ?? throw new InvalidOperationException("Файловая система не подключена."), sourcePath);

        if (!File.Exists(sourceFullPath))
            throw new FileNotFoundException($"Файл по пути '{sourceFullPath}' не найден.");

        string? destinationDirectory;

        if (Path.IsPathRooted(destinationPath))
        {
            destinationDirectory = Path.GetDirectoryName(destinationPath);
        }
        else
        {
            destinationDirectory = Path.Combine(_currentPath, destinationPath);
        }

        if (destinationDirectory == null || !Directory.Exists(destinationDirectory))
            throw new DirectoryNotFoundException($"Целевая директория '{destinationDirectory}' не существует.");

        if (Directory.Exists(destinationPath))
        {
            destinationPath = Path.Combine(destinationPath, Path.GetFileName(sourcePath));
        }

        string destinationFileName = Path.GetFileName(destinationPath);
        string destinationFullPath = Path.Combine(destinationDirectory, destinationFileName);
        Console.WriteLine("Путь назначения: " + destinationFullPath);

        if (File.Exists(destinationFullPath))
        {
            int counter = 1;
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(destinationFileName);
            string fileExtension = Path.GetExtension(destinationFileName);

            do
            {
                string newFileName = $"{fileNameWithoutExtension} ({counter}){fileExtension}";
                destinationFullPath = Path.Combine(destinationDirectory, newFileName);
                counter++;
            }
            while (File.Exists(destinationFullPath));
        }

        File.Move(sourceFullPath, destinationFullPath);
    }

    public void DeleteFile(string filePath)
    {
        string fullFilePath = Path.Combine(_currentPath ?? throw new InvalidOperationException("Файловая система не подключена."), filePath);
        if (!File.Exists(fullFilePath))
            throw new FileNotFoundException($"Файл по пути '{fullFilePath}' не найден.");

        File.Delete(fullFilePath);
    }

    public void RenameFile(string filePath, string newName)
    {
        string fullFilePath = Path.Combine(_currentPath ?? throw new InvalidOperationException("Файловая система не подключена."), filePath);
        if (!File.Exists(fullFilePath))
            throw new FileNotFoundException($"Файл по пути '{fullFilePath}' не найден.");

        string directory = Path.GetDirectoryName(fullFilePath) ?? throw new InvalidOperationException("Не удалось получить директорию файла.");
        string newFullPath = Path.Combine(directory, newName);

        File.Move(fullFilePath, newFullPath);
    }

    public void CopyFile(string sourcePath, string destinationPath)
    {
        string sourceFullPath = Path.Combine(CurrentDirectory, sourcePath);
        string destinationFullPath = Path.Combine(CurrentDirectory, destinationPath, Path.GetFileName(sourcePath));
        string destinationFilePath = Path.Combine(sourceFullPath, destinationFullPath);

        File.Copy(sourceFullPath, destinationFilePath, overwrite: true);
    }

    public string GetFullPath(string filePath)
    {
        return Path.IsPathRooted(filePath) ? filePath : Path.Combine(CurrentDirectory, filePath);
    }

    public void ShowFileContent(string filePath)
    {
        try
        {
            string fileContents = File.ReadAllText(filePath);
            Console.WriteLine(fileContents);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при чтении файла: {ex.Message}");
        }
    }

    private IEnumerable<DirectoryNode> GetDirectoryTreeRecursive(string path, int depth)
    {
        foreach (string dir in Directory.GetDirectories(path))
        {
            var node = new DirectoryNode(Path.GetFileName(dir), false);
            foreach (DirectoryNode child in GetDirectoryTreeRecursive(dir, depth - 1))
            {
                node.AddChild(child);
            }

            yield return node;
        }

        foreach (string file in Directory.GetFiles(path))
        {
            yield return new DirectoryNode(Path.GetFileName(file), true);
        }
    }
}
