﻿using System.Windows;
using System.Windows.Controls;

namespace BluePrintEditor.Designer.Adorners
{
    public class ResizeChrome : Control
    {
        static ResizeChrome()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(ResizeChrome), 
        	    new FrameworkPropertyMetadata(typeof(ResizeChrome)));
        }
    }
}
