# PicView 图片查看器

PicView是一个简洁、高效的图片查看器应用程序，基于Avalonia UI框架开发，支持macOS ARM架构。

## 功能特点

- 简洁的用户界面
- 支持使用左右方向键快速浏览图片
- 支持多种图片格式（JPG、JPEG、PNG、BMP、GIF）
- 显示图片信息和状态
- 针对Apple Silicon芯片优化

## 系统要求

- macOS 10.15或更高版本
- Apple Silicon芯片（ARM架构）或Intel芯片

## 安装方法

### 方法一：使用DMG安装

1. 下载PicView.dmg文件
2. 双击打开DMG文件
3. 将PicView应用程序拖到Applications文件夹
4. 从启动台或Applications文件夹启动PicView

### 方法二：直接运行

1. 下载并解压PicView.zip文件
2. 双击PicView.app运行应用程序

## 使用说明

1. 启动应用程序
2. 点击"打开文件夹"按钮，选择包含图片的文件夹
3. 使用左右方向键浏览图片
4. 底部状态栏显示当前图片信息

## 快捷键

- **左方向键**：查看上一张图片
- **右方向键**：查看下一张图片

## 开发技术

- .NET 9.0
- Avalonia UI 11.2.1
- CommunityToolkit.Mvvm 8.2.1
- MVVM架构模式

## 项目结构

```
PicView/
  ├── Models/             # 数据模型
  │   └── ImageModel.cs   # 图片模型
  ├── ViewModels/         # 视图模型
  │   ├── ViewModelBase.cs
  │   └── MainWindowViewModel.cs
  ├── Views/              # 视图
  │   ├── MainWindow.axaml
  │   └── MainWindow.axaml.cs
  └── Assets/             # 资源文件
```

## 构建说明

### 先决条件

- .NET SDK 9.0或更高版本
- Visual Studio 2022、JetBrains Rider或Visual Studio Code

### 构建步骤

1. 克隆仓库：
   ```
   git clone https://github.com/yourusername/PicView.git
   cd PicView
   ```

2. 构建项目：
   ```
   dotnet build
   ```

3. 运行应用程序：
   ```
   dotnet run --project PicView
   ```

4. 发布为macOS ARM应用程序：
   ```
   cd PicView
   dotnet publish -c Release -r osx-arm64 --self-contained true -p:PublishSingleFile=true -p:PublishTrimmed=true
   ./create-app-bundle.sh
   ```

## 许可证

[MIT License](LICENSE)
