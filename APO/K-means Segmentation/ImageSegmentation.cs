using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APO.K_means_Segmentation
{
    class ImageSegmentation
    {
        private const Int32 m_Distance = 5;
        private const Int32 m_OffsetClr = 50;

        private static KMCClusters m_Clusters = new KMCClusters();
        public ImageSegmentation() { }
        public Bitmap Compute(Bitmap InputFile)
        {
            // Initialize the code execution timer
            var watch = System.Diagnostics.Stopwatch.StartNew();

            // Initialize the directory info reference object
            DirectoryInfo dir_info = new DirectoryInfo("Clusters");
            // Check if the directory with name "Clusters" is created.
            // If not, create the directory with name "Clusters"
            if (dir_info.Exists == false) dir_info.Create();

            // Initialize the array of clusters by generating an initial cluster
            // containing the original source image associated with the array of super-pixels
            m_Clusters.Init((Bitmap)InputFile.Clone(), m_Distance, m_OffsetClr);

            // Initialize the bitmap object used to store the resulting segmented image
            LockedBitmap ResultBitmap = new LockedBitmap(m_Clusters[0].Frame.Width, m_Clusters[0].Frame.Height);

            Int32 FrameIndex = 0;
            // Iterate throught the array of clusters until we've process all clusters being generated
            for (Int32 Index = 0; Index < m_Clusters.Count(); Index++)
            {
                // For each particular cluster from the array, obtain the values of bitmap object and
                // the List<KMCPoint<int>> object which is the array of centroid super-pixels
                List<KMCPoint<int>> Centroids = m_Clusters[Index].Centroids.ToList();
                LockedBitmap FrameBuffer = new LockedBitmap(m_Clusters[Index].Frame.m_Bitmap);

                // Save the image containg the segmented area associated with the current cluster to the
                // specific file, which name has the following format, for example "D:\Clusters\Cluster_N.jpg"
                FrameBuffer.Save("Clusters\\Cluster_" + FrameIndex + ".jpg");

                FrameBuffer.LockBits();

                // Iterating through the array of centroid super pixels and for each super-pixels
                // perform a linear search to find all those pixel in the current image which distance
                // does not exceed the value of specific boundary parameter.
                for (Int32 Cnt = 0; Cnt < Centroids.Count(); Cnt++)
                {
                    // Obtain the value of Width and Height of the image for the current cluster
                    Int32 Width = FrameBuffer.Width;
                    Int32 Height = FrameBuffer.Height;

                    // Create a bitmap object to store an image for the newly built cluster
                    LockedBitmap TargetFrame = new LockedBitmap(FrameBuffer.Width, FrameBuffer.Height);

                    TargetFrame.LockBits();

                    // Iterate through each element of the matrix of pixels for the current image
                    for (Int32 Row = 0; Row < FrameBuffer.Width; Row++)
                    {
                        for (Int32 Col = 0; Col < Height; Col++)
                        {
                            // For each pixel in this matrix, compute the value of color offset of the current centroid super-pixel
                            double OffsetClr = GetEuclClr(new KMCPoint<int>(Row, Col, FrameBuffer.GetPixel(Row, Col)),
                                                          new KMCPoint<int>(Centroids[Cnt].X, Centroids[Cnt].Y, Centroids[Cnt].Clr));

                            //Perform a check if the color offset value does not exceed the value of boundary parameter
                            if (OffsetClr <= 50)
                            {
                                // Copy the current pixel to the target image for the newly created cluster
                                TargetFrame.SetPixel(Row, Col, Centroids[Cnt].Clr);
                            }

                            // Otherwise, set the color of the current pixel to "white" (R;G;B) => (255;255;255)
                            // in the target bitmap for the newly built cluster
                            else TargetFrame.SetPixel(Row, Col, Color.FromArgb(255, 255, 255));
                        }
                    }

                    TargetFrame.UnlockBits();

                    // Create an array of centroid super-pixels and append 
                    // it the value of current centroid super-pixel retrieved
                    List<KMCPoint<int>> TargetCnts = new List<KMCPoint<int>>();
                    TargetCnts.Add(Centroids[0]);

                    // Compute the "mean" value for the newly created cluster
                    KMCPoint<int> Mean = m_Clusters.GetMean(TargetFrame, TargetCnts);

                    // Perform a check if the "mean" point coordinates of the newly created cluster are
                    // not equal to the coordinates of the current centroid super-pixel (e.g. the centroid
                    // super-pixel has been moved). If so, append a newly built cluster to the array of clusters
                    if (Mean.X != m_Clusters[Index].Center.X && Mean.Y != m_Clusters[Index].Center.Y)
                        m_Clusters.Add(TargetFrame, TargetCnts, Mean);

                    FrameIndex++;
                }

                FrameBuffer.UnlockBits();
            }

            ResultBitmap.LockBits();

            // Iterate through the array of clusters previously obtained
            for (Int32 Index = 0; Index < m_Clusters.Count(); Index++)
            {
                // For each cluster retrieve a specific image containing the segmented area
                LockedBitmap FrameOut = new LockedBitmap(m_Clusters[Index].Frame.m_Bitmap);

                FrameOut.LockBits();

                FrameOut.Save("temp_" + Index + ".jpg");

                // Obtain the dimensions of that image
                int Width = FrameOut.Width, Height = FrameOut.Height;
                // Iterate through the matrix of pixels for the current image and for each
                // pixel perform a check if its color is not equal to "white" (R;G;B) => (255;255;255).
                // If not, copy the pixel data to the target matrix of pixels for the resulting segmented image
                for (Int32 Row = 0; Row < Width; Row++)
                {
                    for (Int32 Col = 0; Col < Height; Col++)
                    {
                        if (FrameOut.GetPixel(Row, Col) != Color.FromArgb(255, 255, 255))
                        {
                            ResultBitmap.SetPixel(Row, Col, FrameOut.GetPixel(Row, Col));
                        }
                    }
                }

                FrameOut.UnlockBits();
            }

            ResultBitmap.UnlockBits();

            // Save the segmented image to file with name which is the value of OutputFile variable
            //ResultBitmap.Save(OutputFile);

            watch.Stop(); // Stop the execution timer
            // Obtain the value of executing time in milliseconds
            var elapsedMs = watch.ElapsedMilliseconds;

            // Create timespan from the elapsed milliseconds value
            TimeSpan ts = TimeSpan.FromMilliseconds(elapsedMs);
            // Print the message "Done" and the formatted execution time
            Console.WriteLine("***Done***\n" + ts.ToString(@"hh\:mm\:ss"));

            return ResultBitmap.m_Bitmap;
        }
        public double GetEuclD(KMCPoint<int> Point1, KMCPoint<int> Point2)
        {
            // Compute the Euclidian distance between two pixel in the 2D-space
            return Math.Sqrt(Math.Pow(Point1.X - Point2.X, 2) +
                             Math.Pow(Point1.Y - Point2.Y, 2));
        }
        public double GetEuclClr(KMCPoint<int> Point1, KMCPoint<int> Point2)
        {
            // Compute the Euclidian distance between two colors in the 3D-space
            return Math.Sqrt(Math.Pow(Math.Abs(Point1.Clr.R - Point2.Clr.R), 2) +
                             Math.Pow(Math.Abs(Point1.Clr.G - Point2.Clr.G), 2) +
                             Math.Pow(Math.Abs(Point1.Clr.B - Point2.Clr.B), 2));
        }
    }
}
