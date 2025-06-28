using System;
using System.IO;
using Avalonia.Media.Imaging;

namespace PicView.Models
{
    public class ImageModel
    {
        public string FilePath { get; set; }
        public string FileName => Path.GetFileName(FilePath);
        
        private Bitmap? _bitmap;
        public Bitmap? GetBitmap()
        {
            if (_bitmap == null && File.Exists(FilePath))
            {
                try
                {
                    _bitmap = new Bitmap(FilePath);
                }
                catch (Exception)
                {
                    // 图片加载失败
                    return null;
                }
            }
            return _bitmap;
        }
        
        public void Dispose()
        {
            _bitmap?.Dispose();
            _bitmap = null;
        }
    }
} 