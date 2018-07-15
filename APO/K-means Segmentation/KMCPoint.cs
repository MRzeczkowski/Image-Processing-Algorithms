using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APO.K_means_Segmentation
{
    class KMCPoint<T>
    {
        // KMCPoint constructor
        public KMCPoint(T X, T Y, Color Clr) { this.X = X; this.Y = Y; this.Clr = Clr; }
        // X coordinate property
        public T X { get { return m_X; } set { m_X = value; } }
        // Y coordinate property
        public T Y { get { return m_Y; } set { m_Y = value; } }
        // Colorref property
        public Color Clr { get { return m_Color; } set { m_Color = value; } }

        private T m_X; // X coord
        private T m_Y; // Y coord
        private Color m_Color; // Colorref (R;G;B)
    }
}
