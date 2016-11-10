using System.Drawing;

namespace ImgComparer
{
    internal interface IRecogn
    {
        bool ImgRecogn(Bitmap img, string semplePath);
    }
}