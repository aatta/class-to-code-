using System;

namespace DiagramDesigner.Helpers
{
    public class ToolBoxData
    {
        public string ImageUrl { get; private set; }
        public Type Type { get; private set; }

        public string ToolTip { get; private set; }
        public double Height { get; private set; }
        public double Width { get; private set; }

        public ToolBoxData(string imageUrl, Type type, double width, double height)
        {
            this.ImageUrl = imageUrl;
            this.Type = type;
            this.Width = width;
            this.Height = height;
        }
    }
}
