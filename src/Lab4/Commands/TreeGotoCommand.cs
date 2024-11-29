﻿using Itmo.ObjectOrientedProgramming.Lab4.Controllers;
using Itmo.ObjectOrientedProgramming.Lab4.Models;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands;

public class TreeGotoCommand : Command
{
    private readonly IFileSystemController _fileSystem;
    private readonly string _filePath;

    public TreeGotoCommand(IFileSystemController fileSystem, string path)
        : base("tree goto")
    {
        _fileSystem = fileSystem;
        _filePath = path;
    }

    public override void Validate()
    {
        if (string.IsNullOrWhiteSpace(_filePath))
            throw new ArgumentException("Путь не может быть пустым.");

        if (!_fileSystem.IsConnected)
            throw new InvalidOperationException("Файловая система не подключена.");

        if (!_fileSystem.DirectoryExists(_filePath))
            throw new DirectoryNotFoundException($"Директории '{_filePath}' не существует.");
    }

    public override void Execute()
    {
        _fileSystem.ChangeDirectory(_filePath);
    }
}