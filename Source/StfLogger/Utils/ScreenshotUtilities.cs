// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScreenshotUtilities.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace Mir.Stf.Utilities.Utils
{
    /// <summary>
    /// The screenshot utilities.
    /// </summary>
    internal class ScreenshotUtilities
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScreenshotUtilities"/> class.
        /// </summary>
        /// <param name="logger">
        /// The logger.
        /// </param>
        public ScreenshotUtilities(StfLogger logger)
        {
            Logger = logger;
        }

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        public StfLogger Logger { get; set; }

        /// <summary>
        /// The get window rect.
        /// </summary>
        /// <param name="hwnd">
        /// The hwnd.
        /// </param>
        /// <param name="lpRect">
        /// The lp rect.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1305:FieldNamesMustNotUseHungarianNotation", Justification = "Reviewed. Suppression is OK here."), DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

        /// <summary>
        /// The print window.
        /// </summary>
        /// <param name="hwnd">
        /// The hwnd.
        /// </param>
        /// <param name="hdcBlt">
        /// The hdc blt.
        /// </param>
        /// <param name="nFlags">
        /// The n flags.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1305:FieldNamesMustNotUseHungarianNotation", Justification = "Reviewed. Suppression is OK here."), DllImport("user32.dll")]
        public static extern bool PrintWindow(IntPtr hwnd, IntPtr hdcBlt, int nFlags);

        /// <summary>
        /// The get images of all windows.
        /// </summary>
        /// <returns>
        /// The <see><cref>Dictionary</cref></see>
        /// Dictionary of main window titles and base64 encoded images of windows (if not minimized)
        /// </returns>
        public Dictionary<string, string> GetImagesOfAllWindows()
        {
            var windowImages = new Dictionary<string, string>();
            var imageNo = 1;

            try
            {
                var procsWithWindows = Process.GetProcesses().Where(p => p.MainWindowHandle != IntPtr.Zero && !string.IsNullOrEmpty(p.MainWindowTitle));
                foreach (var process in procsWithWindows)
                {
                    var base64Image = GetWindowImage(process.MainWindowHandle);
                    windowImages.Add($"{imageNo}: {process.MainWindowTitle}", base64Image);
                    imageNo++;
                }
            }
            catch (Exception exception)
            {
                Logger.LogInternal($"Getting screenshots of all windows failed: {exception.Message}");
            }

            return windowImages;
        }

        /// <summary>
        /// The get window image.
        /// </summary>
        /// <param name="handle">
        /// The handle.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetWindowImage(IntPtr handle)
        {
            var imageString = string.Empty;

            // TODO: this is quite similar to DoScreenshot(), refactor into one method
            try
            {
                RECT rectangle;
                GetWindowRect(handle, out rectangle);

                using (var bitmap = new Bitmap(rectangle.Width, rectangle.Height, PixelFormat.DontCare))
                {
                    using (var graphics = Graphics.FromImage(bitmap))
                    {
                        var bitmapHdc = graphics.GetHdc();
                        PrintWindow(handle, bitmapHdc, 0);

                        using (var memoryStream = new MemoryStream())
                        {
                            bitmap.Save(memoryStream, ImageFormat.Png);
                            imageString = Convert.ToBase64String(memoryStream.ToArray());
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Logger.LogInternal($"Unable to grab image of window. Error message: {exception.Message}");
            }

            return imageString;
        }

        /// <summary>
        /// The take one screenshot.
        /// </summary>
        /// <param name="bounds">
        /// The bounds.
        /// </param>
        /// <returns>
        /// Base64 encoded image <see cref="string"/>
        /// </returns>
        public string DoScreenshot(Rectangle bounds)
        {
            var imageString = string.Empty;
            var startingPoint = new Point(bounds.X, bounds.Y);

            try
            {
                using (var bitmap = new Bitmap(bounds.Width, bounds.Height, PixelFormat.Format16bppRgb565))
                {
                    using (var graphics = Graphics.FromImage(bitmap))
                    {
                        graphics.CopyFromScreen(startingPoint, Point.Empty, bounds.Size);

                        using (var memoryStream = new MemoryStream())
                        {
                            bitmap.Save(memoryStream, ImageFormat.Png);
                            imageString = Convert.ToBase64String(memoryStream.ToArray());
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                // TODO: Handle specific exceptions
                Logger.LogInternal($"Failed to grab screen shot. Error message: {exception.Message}"); 
            }

            return imageString;
        }

        /// <summary>
        /// The rect. Taken and adapted from http://pinvoke.net/default.aspx/Structures/RECT.html
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]

        // ReSharper disable once InconsistentNaming
        public struct RECT
        {
            /// <summary>
            /// The left.
            /// </summary>
            public int Left;

            /// <summary>
            /// The left.
            /// </summary>
            public int Top;

            /// <summary>
            /// The left.
            /// </summary>
            public int Right;

            /// <summary>
            /// The left.
            /// </summary>
            public int Bottom;

            /// <summary>
            /// Initializes a new instance of the <see cref="RECT"/> struct.
            /// </summary>
            /// <param name="left">
            /// The left.
            /// </param>
            /// <param name="top">
            /// The top.
            /// </param>
            /// <param name="right">
            /// The right.
            /// </param>
            /// <param name="bottom">
            /// The bottom.
            /// </param>
            public RECT(int left, int top, int right, int bottom)
            {
                Left = left;
                Top = top;
                Right = right;
                Bottom = bottom;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="RECT"/> struct.
            /// </summary>
            /// <param name="r">
            /// The r.
            /// </param>
            public RECT(Rectangle r)
                : this(r.Left, r.Top, r.Right, r.Bottom)
            {
            }

            /// <summary>
            /// Gets or sets the x.
            /// </summary>
            public int X
            {
                get
                {
                    return Left;
                }

                set
                {
                    Right -= Left - value;
                    Left = value;
                }
            }

            /// <summary>
            /// Gets or sets the y.
            /// </summary>
            public int Y
            {
                get
                {
                    return Top;
                }

                set
                {
                    Bottom -= Top - value;
                    Top = value;
                }
            }

            /// <summary>
            /// Gets or sets the height.
            /// </summary>
            public int Height
            {
                get
                {
                    return Bottom - Top;
                }

                set
                {
                    Bottom = value + Top;
                }
            }

            /// <summary>
            /// Gets or sets the width.
            /// </summary>
            public int Width
            {
                get
                {
                    return Right - Left;
                }

                set
                {
                    Right = value + Left;
                }
            }

            /// <summary>
            /// Gets or sets the location.
            /// </summary>
            public Point Location
            {
                get
                {
                    return new Point(Left, Top);
                }

                set
                {
                    X = value.X;
                    Y = value.Y;
                }
            }

            /// <summary>
            /// Gets or sets the size.
            /// </summary>
            public Size Size
            {
                get
                {
                    return new Size(Width, Height);
                }

                set
                {
                    Width = value.Width;
                    Height = value.Height;
                }
            }

            /// <summary>
            /// The op_ implicit.
            /// </summary>
            /// <param name="r">
            /// The r.
            /// </param>
            /// <returns>
            /// </returns>
            public static implicit operator Rectangle(RECT r)
            {
                return new Rectangle(r.Left, r.Top, r.Width, r.Height);
            }

            /// <summary>
            /// The op_ implicit.
            /// </summary>
            /// <param name="r">
            /// The r.
            /// </param>
            /// <returns>
            /// </returns>
            public static implicit operator RECT(Rectangle r)
            {
                return new RECT(r);
            }

            /// <summary>
            /// The ==.
            /// </summary>
            /// <param name="r1">
            /// The r 1.
            /// </param>
            /// <param name="r2">
            /// The r 2.
            /// </param>
            /// <returns>
            /// The <see cref="bool"/>.
            /// </returns>
            public static bool operator ==(RECT r1, RECT r2)
            {
                return r1.Equals(r2);
            }

            /// <summary>
            /// The !=.
            /// </summary>
            /// <param name="r1">
            /// The r 1.
            /// </param>
            /// <param name="r2">
            /// The r 2.
            /// </param>
            /// <returns>
            /// The <see cref="bool"/>.
            /// </returns>
            public static bool operator !=(RECT r1, RECT r2)
            {
                return !r1.Equals(r2);
            }

            /// <summary>
            /// The equals.
            /// </summary>
            /// <param name="r">
            /// The r.
            /// </param>
            /// <returns>
            /// The <see cref="bool"/>.
            /// </returns>
            public bool Equals(RECT r)
            {
                return r.Left == Left && r.Top == Top && r.Right == Right && r.Bottom == Bottom;
            }

            /// <summary>
            /// The equals.
            /// </summary>
            /// <param name="obj">
            /// The obj.
            /// </param>
            /// <returns>
            /// The <see cref="bool"/>.
            /// </returns>
            public override bool Equals(object obj)
            {
                if (obj is RECT)
                {
                    return Equals((RECT)obj);
                }

                if (obj is Rectangle)
                {
                    return Equals(new RECT((Rectangle)obj));
                }

                return false;
            }

            /// <summary>
            /// The get hash code.
            /// </summary>
            /// <returns>
            /// The <see cref="int"/>.
            /// </returns>
            public override int GetHashCode()
            {
                return ((Rectangle)this).GetHashCode();
            }

            /// <summary>
            /// The to string.
            /// </summary>
            /// <returns>
            /// The <see cref="string"/>.
            /// </returns>
            public override string ToString()
            {
                return string.Format(
                    CultureInfo.CurrentCulture, 
                    "{{Left={0},Top={1},Right={2},Bottom={3}}}", 
                    Left, 
                    Top, 
                    Right, 
                    Bottom);
            }
        }
    }
}
