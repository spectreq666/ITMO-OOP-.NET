using Itmo.ObjectOrientedProgramming.Lab4.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Controllers;

public interface IFileSystemController
{
    void Connect(string address);

    void Disconnect();

    bool IsConnected { get; }

    bool DirectoryExists(string address);

    void ChangeDirectory(string address);

    IEnumerable<DirectoryNode> GetDirectoryTree(string path, int depth);

    string CurrentDirectory { get; }

    void MoveFile(string sourcePath, string destinationPath);

    bool FileExists(string filePath);

    void DeleteFile(string filePath);

    void RenameFile(string filePath, string newName);

    string GetFullPath(string filePath);

    void CopyFile(string sourcePath, string destinationPath);

    string GetFileContent(string filePath);
}