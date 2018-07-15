using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APO.K_means_Segmentation
{
    class KMCClusters : IEnumerable<KMCFrame>
    {
        private readonly System.Random rand = new System.Random();
        private static HashSet<KMCFrame> m_Clusters = new HashSet<KMCFrame>();
        public void Init(Bitmap Filename, Int32 Distance, Int32 Offset)
        {
            // Declare a bitmap object to load and use the original image to be segmented
            LockedBitmap FrameBuffer = new LockedBitmap(Filename);

            // Initialize the array of super-pixels by creating List<KMCPoint<int>> class object
            List<KMCPoint<int>> Centroids = new List<KMCPoint<int>>();
            // Generate an initial array of super-pixels of the original source image
            // stored in the FrameBuffer bitmap object
            this.Generate(ref Centroids, FrameBuffer, Distance, Offset);

            // Compute the value of the centeral super-pixel coordinates and assign it
            // to the Mean local variable
            KMCPoint<int> Mean = this.GetMean(FrameBuffer, Centroids);

            // Append an initial cluster being initialized to the array of clusters
            m_Clusters.Add(new KMCFrame(FrameBuffer, Centroids, Mean));
        }
        public void Generate(ref List<KMCPoint<int>> Centroids, LockedBitmap ImageFrame, Int32 Distance, Int32 Offset)
        {
            // Compute the number of iterations performed by the main loop equal to image W * H
            // The following value is the maximum possible number of random super-pixel being generated
            Int32 Size = ImageFrame.Width * ImageFrame.Height;
            ImageFrame.LockBits();
            // Performing Size - iterations of the following loop to generate a specific amount of super-pixels
            for (Int32 IterCount = 0; IterCount < Size; IterCount++)
            {
                // Obtain a random value of X - coordinate of the current super-pixel
                Int32 Rand_X = rand.Next(0, ImageFrame.Width);
                // Obtain a random value of Y - coordinate of the current super-pixel
                Int32 Rand_Y = rand.Next(0, ImageFrame.Height);

                // Create and instantinate a point object by using the values of
                // Rand_X, Rand_Y and Colorref parameters. The value of colorref is
                // retrieved by using the GetPixel method for the current bitmap object
                KMCPoint<int> RandPoint = new KMCPoint<int>(Rand_X,
                              Rand_Y, ImageFrame.GetPixel(Rand_X, Rand_Y));

                // Performing a validity check if none of those super-pixel previously
                // selected don't exceed the distance and color offset boundary to the 
                // currently generated super-pixel with coordinates Rand_X and Rand_Y and
                // specific color stored as a parameter value of Clr variable
                if (!this.IsValidColor(Centroids, RandPoint, Offset) &&
                    !this.IsValidDistance(Centroids, RandPoint, Distance))
                {
                    // If not, check if the super-pixel with the following coordinates and color
                    // already exists in the array of centroid super-pixels being generated.
                    if (!Centroids.Contains(RandPoint))
                    {
                        // If not, append the object RandPoint to the array of super-pixel objects
                        Centroids.Add(RandPoint);
                    }
                }
            }

            ImageFrame.UnlockBits();
        }
        private bool IsValidDistance(List<KMCPoint<int>> Points, KMCPoint<int> Target, Int32 Distance)
        {
            Int32 Index = -1; bool Exists = false;
            // Iterate through the array of super-pixels until we've found the super-pixel which
            // distance to the target super-pixel is less than or equals to the specified boundary
            while (++Index < Points.Count() && !Exists)
                // For each super-pixel from the array we compute the value of distance and
                // perform a check if the following value is less than or equals to 
                // the value of specific boundary parameter.
                Exists = ((Math.Abs(Target.X - Points.ElementAt(Index).X) <= Distance) ||
                          (Math.Abs(Target.Y - Points.ElementAt(Index).Y) <= Distance)) ? true : false;

            return Exists;
        }
        private bool IsValidColor(List<KMCPoint<int>> Points, KMCPoint<int> Target, Int32 Offset)
        {
            Int32 Index = -1; bool Exists = false;
            // Iterate through the array of super-pixels until we've found the super-pixel which
            // color offset to the target super-pixel is less than or equals to the specified boundary
            while (++Index < Points.Count() && !Exists)
                // For each super-pixel from the array we compute the value of color offset and
                // perform a check if the following value is less than or equals to 
                // the value of specific boundary parameter.
                Exists = (Math.Sqrt(Math.Pow(Math.Abs(Points[Index].Clr.R - Target.Clr.R), 2) +
                                    Math.Pow(Math.Abs(Points[Index].Clr.G - Target.Clr.G), 2) +
                                    Math.Pow(Math.Abs(Points[Index].Clr.B - Target.Clr.B), 2))) <= Offset ? true : false;

            return Exists;
        }
        public KMCPoint<int> GetMean(LockedBitmap FrameBuffer, List<KMCPoint<int>> Centroids)
        {
            // Declaring two variables to assign the value of the "mean" of 
            // the sets of coordinates (X;Y) of each super-pixel
            double Mean_X = 0, Mean_Y = 0;
            // Iterating through the array of super-pixels and for each
            // super-pixel retrieve its X and Y coordinates and divide it
            // by the overall amount of super-pixels. After that, sum up
            // each value with the values of Mean_X and Mean_Y variables
            for (Int32 Index = 0; Index < Centroids.Count(); Index++)
            {
                Mean_X += Centroids[Index].X / (double)Centroids.Count();
                Mean_Y += Centroids[Index].Y / (double)Centroids.Count();
            }

            // Convert the values of Mean_X and Mean_Y to Int32 datatype
            Int32 X = Convert.ToInt32(Mean_X);
            Int32 Y = Convert.ToInt32(Mean_Y);

            FrameBuffer.LockBits();
            Color Clr = FrameBuffer.GetPixel(X, Y);
            FrameBuffer.UnlockBits();

            // Constructing KMCPoint<int> object and return its value
            return new KMCPoint<int>(X, Y, Clr);
        }
        public void Add(LockedBitmap FrameImage, List<KMCPoint<int>> Centroids, KMCPoint<int> Center)
        {
            m_Clusters.Add(new KMCFrame(FrameImage, Centroids, Center));
        }

        public KMCFrame this[Int32 Index]
        {
            get { return m_Clusters.ElementAt(Index); }
        }

        public IEnumerator<KMCFrame> GetEnumerator()
        {
            return m_Clusters.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
