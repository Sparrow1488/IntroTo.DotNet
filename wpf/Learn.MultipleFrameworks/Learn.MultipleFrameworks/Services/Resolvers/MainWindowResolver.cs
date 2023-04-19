using System;
using System.Windows;

namespace Learn.MultipleFrameworks.Services.Resolvers;

public class MainWindowResolver
{
    private readonly Window? _window;

    public MainWindowResolver(Window? window)
    {
        _window = window;
    }

    public Window GetRequiredWindow()
    {
        return _window ?? throw new Exception("Window not set to be resolved");
    }
}