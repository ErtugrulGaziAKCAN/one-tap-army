namespace QuickTools.Editor.QuickScreenshots
{
    public class ScreenshotResolution
    {
        public int Width { get; }
        public int Height { get; }

        public string Label { get; }
            
        public ScreenshotResolution(int width, int height, string label)
        {
            Width = width;
            Height = height;
            Label =  $"{width}x{height}_{label}";
        }
    }
}