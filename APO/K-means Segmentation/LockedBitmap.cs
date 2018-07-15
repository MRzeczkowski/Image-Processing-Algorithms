using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APO.K_means_Segmentation
{
    class LockedBitmap
    {
            // LockedBitmap class constructors
            public LockedBitmap(string filename)
            {
                // If the m_Bitmap internal object is unitilialized
                if (m_Bitmap == null)
                {
                    // Initialize the bitmap object and copy source bitmap into
                    // the object being constructed
                    m_Bitmap = new Bitmap(filename);
                    // Initilaize the rectange area object having size equal to size of the bitmap
                    m_rect = new Rectangle(new Point(0, 0), m_Bitmap.Size);
                }
            }
            public LockedBitmap(Int32 Width, Int32 Height)
            {
                // If the m_Bitmap internal object is unitilialized
                if (m_Bitmap == null)
                {
                    // Initialize the bitmap object and copy source bitmap into
                    // the object being constructed
                    m_Bitmap = new Bitmap(Width, Height);
                    // Initilaize the rectange area object having size equal to size of the bitmap
                    m_rect = new Rectangle(new Point(0, 0), m_Bitmap.Size);
                }
            }
            public LockedBitmap(Bitmap bitmap)
            {
                // If the m_Bitmap internal object is unitilialized
                if (m_Bitmap == null)
                {
                    // Initialize the bitmap object and copy source bitmap into
                    // the object being constructed
                    m_Bitmap = new Bitmap(bitmap);
                    // Initilaize the rectange area object having size equal to size of the bitmap
                    m_rect = new Rectangle(new Point(0, 0), m_Bitmap.Size);
                }
            }

            // Assignment operator overloading for LockedBitmap class object
            public static implicit operator LockedBitmap(Bitmap bitmap)
            {
                // Construct the LockedBitmap class object from the value of generic Bitmap object
                return new LockedBitmap(bitmap);
            }

            // Call this method to lock the bitmap pixels buffer
            public void LockBits()
            {
                // Lock bitmap's bits and obtain the BitmapData object
                m_BitmapInfo = m_Bitmap.LockBits(m_rect, System.Drawing.Imaging.
                    ImageLockMode.ReadWrite, m_Bitmap.PixelFormat);

                // Obtain the value of pointer of the first line of pixels
                m_BitmapPtr = m_BitmapInfo.Scan0;
                // Allocating the array of pixels having size equal to BitmapInfo.Stride x Bitmap.Height
                m_Pixels = new byte[Math.Abs(m_BitmapInfo.Stride) * m_Bitmap.Height]; // Use marshal copy routines to copy the entire buffer of pixels
                // to the previously allocated array m_Pixels
                System.Runtime.InteropServices.Marshal.Copy(m_BitmapPtr, m_Pixels, 0, Math.Abs(m_BitmapInfo.Stride) * m_Bitmap.Height);
            }

            // Call this method to unlock and reflect changes to the pixels buffer
            public void UnlockBits()
            {
                // Obtain the value of pointer of the first line of pixels
                m_BitmapPtr = m_BitmapInfo.Scan0;
                // Use marshal copy routines to copy all elements
                // in the array m_Pixels back to the pixels buffer
                System.Runtime.InteropServices.Marshal.Copy(m_Pixels, 0,
                    m_BitmapPtr, Math.Abs(m_BitmapInfo.Stride) * m_Bitmap.Height);

                // Unlock the bits.
                m_Bitmap.UnlockBits(m_BitmapInfo);
            }

            // Call this method to retrieve the color reference of pixel with coordinates (Row;Col)
            public Color GetPixel(Int32 Row, Int32 Col)
            {
                // Obtain the value of color depth
                Int32 Channel = System.Drawing.Bitmap.GetPixelFormatSize(m_BitmapInfo.PixelFormat);
                // Compute the value of index of pixel in the m_Pixels buffer
                Int32 Pixel = (Row + Col * m_Bitmap.Width) * (Channel / 8);

                // Declaring three variables to be assigned the values of colors (R;G;B)
                Int32 Red = 0, Green = 0, Blue = 0, Alpha = 0;

                if (Pixel > m_Pixels.Length - 4)
                    Pixel = m_Pixels.Length - 4;

                // Check if the bitmap is 32-bit color depth
                if (Channel == 32)
                {
                    // Assign the values of specific elements in the
                    // array of pixels to corresponding variables
                    Blue = m_Pixels[Pixel];
                    Green = m_Pixels[Pixel + 1];
                    Red = m_Pixels[Pixel + 2];
                    Alpha = m_Pixels[Pixel + 3];
                }

                // Check if the bitmap is 24-bit color depth
                else if (Channel == 24)
                {
                    // Assign the values of specific elements in the
                    // array of pixels to corresponding variables
                    Blue = m_Pixels[Pixel];
                    Green = m_Pixels[Pixel + 1];
                    Red = m_Pixels[Pixel + 2];
                }

                // Check if the bitmap is 16-bit color depth
                else if (Channel == 16)
                {
                    // Assign the values of specific elements in the
                    // array of pixels to corresponding variables
                    Blue = m_Pixels[Pixel];
                    Green = m_Pixels[Pixel + 1];
                }

                // Check if the bitmap is 8-bit color depth
                else if (Channel == 8)
                {
                    // Assign the value of specific element in the
                    // array of pixels to corresponding variables
                    Blue = m_Pixels[Pixel];
                }

                // Construct the color reference object from the value of variables
                // assigned the specific values of colors
                return (Channel != 8) ? Color.FromArgb(Red, Green, Blue) : Color.FromArgb(Blue, Blue, Blue);
            }

            // Call this method to set the color of pixel Clr with coordinates (Row;Col)
            public void SetPixel(Int32 Row, Int32 Col, Color Clr)
            {
                // Obtain the value of color depth
                Int32 Channel = System.Drawing.Bitmap.GetPixelFormatSize(m_BitmapInfo.PixelFormat);
                // Compute the value of index of pixel in the m_Pixels buffer
                Int32 Pixel = (Row + Col * m_Bitmap.Width) * (Channel / 8);

            if (Pixel > m_Pixels.Length - 4)
                Pixel = m_Pixels.Length - 4;

            // Check if the bitmap is 32-bit color depth
            if (Channel == 32)
                {
                    // If so, assign the value of each color (R;G;B;A) to the specific
                    // elements in the array of pixels
                    m_Pixels[Pixel] = Clr.B;
                    m_Pixels[Pixel + 1] = Clr.G;
                    m_Pixels[Pixel + 2] = Clr.R;
                    m_Pixels[Pixel + 3] = Clr.A;
                }

                // Check if the bitmap is 24-bit color depth
                else if (Channel == 24)
                {
                    // If so, assign the value of each color (R;G;B) to the specific
                    // elements in the array of pixels
                    m_Pixels[Pixel] = Clr.B;
                    m_Pixels[Pixel + 1] = Clr.G;
                    m_Pixels[Pixel + 2] = Clr.R;
                }

                // Check if the bitmap is 16-bit color depth
                else if (Channel == 16)
                {
                    // If so, assign the value of each color (B;G) to the specific
                    // elements in the array of pixels
                    m_Pixels[Pixel] = Clr.B;
                    m_Pixels[Pixel + 1] = Clr.G;
                }

                // Check if the bitmap is 8-bit color depth
                else if (Channel == 8)
                {
                    // If so, assign the value of each color (B) to the specific
                    // element in the array of pixels
                    m_Pixels[Pixel] = Clr.B;
                }
            }

            // Use this property to retrieve the width and height of an image locked
            public Int32 Width { get { return m_Bitmap.Width; } }
            public Int32 Height { get { return m_Bitmap.Height; } }

            // Use this method to save the bitmap to file
            public void Save(string filename)
            {
                // Calling Save(filename) method to save the bitmap to a file
                m_Bitmap.Save(filename);
            }

            // Declaring Bitmap class object
            public Bitmap m_Bitmap = null;

            // Declaring Rectangle class Object
            private Rectangle m_rect;
            // Declaring point to bitmap pixels buffer
            private IntPtr m_BitmapPtr;
            // Declaring array of pixels;
            private byte[] m_Pixels = null;
            // Declaring BitmaData object
            private BitmapData m_BitmapInfo = null;
        }
    }

