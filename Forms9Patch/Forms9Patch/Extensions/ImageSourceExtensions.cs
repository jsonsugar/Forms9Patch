﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forms9Patch
{
    public static class ImageSourceExtensions
    {
        public static bool SameAs(this Xamarin.Forms.ImageSource thisSource, Xamarin.Forms.ImageSource otherSource)
        {
            if (thisSource == otherSource)
                return true;
            var thisStreamSource = thisSource as Xamarin.Forms.StreamImageSource;
            var otherStreamSource = otherSource as Xamarin.Forms.StreamImageSource;
            if (thisStreamSource != null && otherStreamSource != null)
            {
                if (thisStreamSource.GetValue(ImageSource.AssemblyProperty) != otherStreamSource.GetValue(ImageSource.AssemblyProperty))
                    return false;
                return thisStreamSource.GetValue(ImageSource.PathProperty) != otherStreamSource.GetValue(ImageSource.PathProperty) ? false : true;
            }
            var thisFileSource = thisSource as Xamarin.Forms.FileImageSource;
            var otherFileSource = otherSource as Xamarin.Forms.FileImageSource;
            return thisFileSource != null && otherFileSource != null && thisFileSource.File == otherFileSource.File;
        }

    }
}