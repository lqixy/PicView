using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PicView.Models;

namespace PicView.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty] private ImageModel? _currentImage;
    [ObservableProperty] private Bitmap? _currentBitmap;
    [ObservableProperty] private string _statusText = "欢迎使用PicView图片查看器";
    [ObservableProperty] private bool _isLoading;
    
    private List<ImageModel> _images = new();
    private int _currentIndex = -1;
    
    public ICommand OpenFolderCommand { get; }
    public ICommand NextImageCommand { get; }
    public ICommand PreviousImageCommand { get; }
    
    public MainWindowViewModel()
    {
        OpenFolderCommand = new AsyncRelayCommand(OpenFolder);
        NextImageCommand = new RelayCommand(NextImage, () => _images.Count > 0);
        PreviousImageCommand = new RelayCommand(PreviousImage, () => _images.Count > 0);
    }
    
    private async Task OpenFolder()
    {
        try
        {
            IsLoading = true;
            StatusText = "正在加载文件夹...";
            
            // 清理现有图片资源
            foreach (var img in _images)
            {
                img.Dispose();
            }
            _images.Clear();
            CurrentBitmap = null;
            CurrentImage = null;
            _currentIndex = -1;
            
            // 使用新的StorageProvider API选择文件夹
            var storageProvider = App.MainWindow.StorageProvider;
            var folder = await storageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions
            {
                Title = "选择图片文件夹",
                AllowMultiple = false
            });
            
            if (folder == null || folder.Count == 0)
            {
                StatusText = "未选择文件夹";
                return;
            }
            
            var folderPath = folder[0].Path.LocalPath;
            
            if (string.IsNullOrEmpty(folderPath))
            {
                StatusText = "无法访问所选文件夹";
                return;
            }
            
            // 加载图片文件
            var supportedExtensions = new[] { ".jpg", ".jpeg", ".png", ".bmp", ".gif" };
            var imageFiles = Directory.GetFiles(folderPath)
                .Where(file => supportedExtensions.Contains(Path.GetExtension(file).ToLower()))
                .OrderBy(f => f)
                .ToList();
            
            if (imageFiles.Count == 0)
            {
                StatusText = "所选文件夹中没有支持的图片文件";
                return;
            }
            
            foreach (var file in imageFiles)
            {
                _images.Add(new ImageModel { FilePath = file });
            }
            
            StatusText = $"已加载 {_images.Count} 张图片";
            
            // 显示第一张图片
            _currentIndex = 0;
            ShowCurrentImage();
        }
        catch (Exception ex)
        {
            StatusText = $"加载出错: {ex.Message}";
        }
        finally
        {
            IsLoading = false;
        }
    }
    
    private void NextImage()
    {
        if (_images.Count == 0) return;
        
        _currentIndex = (_currentIndex + 1) % _images.Count;
        ShowCurrentImage();
    }
    
    private void PreviousImage()
    {
        if (_images.Count == 0) return;
        
        _currentIndex = (_currentIndex - 1 + _images.Count) % _images.Count;
        ShowCurrentImage();
    }
    
    private void ShowCurrentImage()
    {
        if (_currentIndex >= 0 && _currentIndex < _images.Count)
        {
            CurrentImage = _images[_currentIndex];
            CurrentBitmap = CurrentImage.GetBitmap();
            StatusText = $"图片 {_currentIndex + 1}/{_images.Count}: {CurrentImage.FileName}";
        }
    }
    
    public void CleanupResources()
    {
        // 释放所有图片资源
        foreach (var img in _images)
        {
            img.Dispose();
        }
        _images.Clear();
        
        CurrentBitmap?.Dispose();
        CurrentBitmap = null;
        CurrentImage = null;
    }
}