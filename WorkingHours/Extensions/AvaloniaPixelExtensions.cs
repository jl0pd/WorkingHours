using Avalonia;

namespace WorkingHours.Extensions
{
    internal static class AvaloniaPixelExtensions
    {
        public static PixelPoint ToPixelPoint(this Point point)
            => new PixelPoint((int)point.X, (int)point.Y);

        public static Point ToPoint(this PixelPoint pixelPoint)
            => new Point(pixelPoint.X, pixelPoint.Y);
    }
}
