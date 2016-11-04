using System.ComponentModel;

namespace Core.Core
{
    public enum ImageFileFormat
    {
        [Description("Tiff")]
        tiff,
        [Description("Bmp")]
        bmp,
        [Description("Jpeg")]
        jpeg,
        [Description("Png")]
        png
    }
}
