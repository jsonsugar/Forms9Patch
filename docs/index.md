# Forms9Patch 

A suite of elements built to simplify image management and text formatting for your NetStandard, PCL, and Shared Library Xamarin.Forms iOS, Android and UWP applications.

## Overview

Xamarin Forms is great for developing cross platform applications (certainly very sane) but it is missing some important features:

 - Patch scalable images
 - SVG images
 - Single point of use, cross platform, multi-screen / multi-resolution image management
 - Simple, markup formatted text for labels and buttons
 - Single point of use, cross platform, custom font management
 - Segmented button control
 - Pop-ups as a first class layout element

Android developers can use NinePatch bitmaps, the drawable directory naming convention, Html.FromHtml, and a bunch of complex file manipulations to address the image issue.  Likewise, iOS developers can use ResizeableImageWithCapInsets, the @2x, @3x, @4x file naming convention, and some 3rd party libraries for this purpose.  Custom fonts are a bit more complicated and label layouts take things up another notch.   Forms9Patch enhances Xamarin Forms to make multi-resolution / multi-screen image management, custom fonts, and HTML text formatting easy for PCL and Shared Library applications for iOS, Android and UWP.

## So, what exactly is Forms9Patch?

Simply stated, Forms9Patch is three core elements from which five layouts, three buttons, six pop-ups are built upon.  And, to make your life easier, some extensions, effects, and services are added.   The three core elements are:

 - `Forms9Patch.ImageSource`: a free to use derivative of `Xamarin.Forms.ImageSource` to support multi-device / multi-density image management from Embedded Resources.
 - `Forms9Patch.Image`: an enhanced implementation of `Xamarin.Forms.Image` with border (outline) and shadow properties  **_plus_** support of SVG vector images.  Raster images (.png, .jpg, .bmp) images also have patch scaling, tiling, and tinting support.
 - `Forms9Patch.Label`: an enhanced implementation of `Xamarin.Forms.Label` for easy access to single-point-of-use custom fonts, auto-fitting, and text formatting via markup tags.

Forms9Patch's five layouts are derivatives of the five `Xamarin.Forms.Layout` subclasses - enhanced with the ability to use a `Forms9Patch.Image` as a background.

 - `Forms9Patch.Frame`
 - `Forms9Patch.AbsoluteLayout`
 - `Forms9Patch.Grid`
 - `Forms9Patch.RelativeLayout`
 - `Forms9Patch.StackLayout`

Forms9Patch's three button elements (`Forms9Patch.Button`, `Forms9Patch.StateButton`, and `Forms9Patch.SegmentedControl`) are all derived from `Xamarin.Forms.Button` and share the following enhancements:

 - A `BackgroundImage` property: Use a `Forms9Patch.Image` as a button background.
 - An icon:
     - Specified either as markup text (`IconText` property) or as a `Forms9Patch.Image` (`IconImage` property).  `IconText` makes it easy to use custom icon fonts like [Material Icons](https://material.io/icons/).  `IconImage` makes it easy to tint raster images or use `SVG` vector images.  
     - A button's icon can be before (default) or after (`TrailingIcon` property `true`) its text and at the edge of the button (default) or next to the button's text (`TightSpacing` property `true`).  
     - A button's icon or text can be arranged horizontally (default) or vertically (`Orientation` property set to `StackOrientation.Vertical`).
 - A label:
     - Specified either as plain text (`Text` property) or markup text (`HtmlText` property).
     - Autofitting algorithms (see [Autofitting](Autofitting) )
- `LongPressing` and `LongPressed` touch events.

The three Forms9Patch button elements are:

 - `Forms9Patch.Button`
 - `Forms9Patch.StateButton`: a further enhanced implementation of `Forms9Patch.Button` that allow most enhanced properties to be configured by the button's state (Default, Pressing, Selected, Disabled, DisabledAndSelected) 
 - `Forms9Patch.SegmentControl`: a segmented button control that allows for border, separator, and shadow control.  Note that there is currently no support for background images.



Forms9Patch's pop-up elements can be thought of as Forms9Patch layout elements (outline and background image capabilities) that rendere with full page overlay backgrounds and the ability to be canceled by tapping the background.  These popup elements are:

 - `Forms9Patch.ModalPopup`: takes the enhancements (borders; patch-scalable, tintable, tile-able background images; shadows) from `Forms9Patch.Frame` and puts it into a pop-up layout.
 - `Forms9Patch.BubblePopup`: goes a step further than `Forms9Patch.ModalPopup` by pointing to a target `Xamarin.Forms.VisualElement`.  The pointer has `PointerLength`, `PointerTipRadius` and `PointerCornerRadius` properties.
 - `Forms9Patch.ActivityIndicatorPopup`: a convenience element that presents a `Xamarin.Forms.ActivityIndicator` over a page overlay.
 - `Forms9Patch.Toast`: a convenience element that presents a title, a message and an optional confirmation button.
 - `Forms9Patch.PermissionPopup`: a convenience element that presents a title, a message, and an accept and a decline button.
 - `Forms9Patch.TargetedToast`: similar to `Forms9Patch.Toast` but enhanced to point to a `Xamarin.Forms.VisualElement`.

Then there are Forms9Patch's extensions.  A few noteworthy ones are:

 - Color Extensions: Blend two colors, change a color's opacity, convert between Xamarin.Forms.Color and string representations.
 - WebView Extensions: Print the contents of a `Xamarin.Forms.WebView` (currently iOS and Android only).
 - HtmlString Extensions: Convert HTML markup to a PNG file.

And lastly is Forms9Patch's services:

 - Keyboard Service: Want to put away the system keyboard?  Want to be notified when the system keyboard appears or disappears? 
 - Key Clicks Service: Do you want to make a system keyboard sound (and vibration, if available)?
 - Application Info Service: Do you want to quickly know your application's Name, Bundle/Package ID, Build number and Version string?
 - OS Info Service: What is the version of the device's operating system?
 
## Forms9Patch Guides

 - [Getting Started for Windows VisualStudio 2017](guides/GettingStartedWindows.md)
 - [Using ```Forms9Patch.ImageSource```](guides/ImageSource.md)
 - [Using ```Forms9Patch.Image```](guides/Image.md)
 - [Using Forms9Patch's layouts](guides/Layouts.md)
 - Using Forms9Patch's pop-ups
 - Custom Fonts
 - Using ```Forms9Patch.Label```
 - Putting it all together: Using Forms9Patch's buttons

## API 
