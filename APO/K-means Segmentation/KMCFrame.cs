using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APO.K_means_Segmentation
{
    class KMCFrame
    {
        // KMCFrame Constructor
        public KMCFrame(LockedBitmap Frame, List<KMCPoint<Int32>> Centroids, KMCPoint<Int32> Center)
        {
            this.Frame = Frame;
            this.m_Centroids = Centroids; this.Center = Center;
        }

        // Bitmap Frame Property
        public LockedBitmap Frame
        {
            get { return m_Frame; }
            set { m_Frame = value; }
        }
        // Centroids List Property
        public List<KMCPoint<Int32>> Centroids
        {
            get { return m_Centroids; }
            set { m_Centroids = value; }
        }
        // Central Super-Pixel Property
        public KMCPoint<Int32> Center
        {
            get { return m_Center; }
            set { m_Center = value; }
        }
        // Bitmap Frame Object
        private LockedBitmap m_Frame = null;
        // Central Super-Pixel Point Object
        private KMCPoint<Int32> m_Center;
        // Array of Super-Pixel Objects (i.e. Centroids)
        private List<KMCPoint<Int32>> m_Centroids = null;
    }
}
