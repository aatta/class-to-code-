﻿using System;
using System.Windows;

namespace DiagramDesigner
{
    // Wraps info of the dragged object into a class
    public class DragObject
    {
        public Size? DesiredSize { get; set; }
        public Type ContentType { get; set; }

    }
}
