using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APO
{
    public class Krzywa
    {
        private Pen pen;
        private List<Point> lSkladoweKrzywej;
        private float tension;  //wrazliwosc linii - dla zera bedzie to prosta lamana, dla wiekszego od 0 zaokragla kanty
        private Point[] tSkladoweKrzywej;
        private int zaznaczony; //indeks punktu do usuniecia

        public Krzywa()
        {
            pen = new Pen(Color.Black, 2);
            lSkladoweKrzywej = new List<Point>();
            lSkladoweKrzywej.Add(new Point(0, 255));
            lSkladoweKrzywej.Add(new Point(255, 0));
            tension = 0.0f;
            zaznaczony = -1;
        }
        public Pen Pen { get { return pen; } }
        public float Tension { get { return tension; } set { tension = value; } }
        public List<Point> LSkladoweKrzywej { get { return lSkladoweKrzywej; } set { lSkladoweKrzywej = value; } }
        public Point[] TSkladoweKrzywej { get { return tSkladoweKrzywej; } set { tSkladoweKrzywej = value; } }
        public int Zaznaczony { get { return zaznaczony; } set { zaznaczony = value; } }

        //tworze tablice bo meteda DrawCurve jako argument przyjmuje tablice
        public void TworzTablice()
        {
            tSkladoweKrzywej = new Point[lSkladoweKrzywej.Count];
            lSkladoweKrzywej.Sort(Porownywacz);
            tSkladoweKrzywej = lSkladoweKrzywej.ToArray();
        }


        //Dzieki temu mozemy uzywac metody sort() w Listach by posortowac Punkty(X,Y). Sortowanie po wartosci X
        PorownaniePunktow Porownywacz = new PorownaniePunktow();
        public class PorownaniePunktow : IComparer<Point>
        {
            public int Compare(Point Xa, Point Xb)
            {
                if (Xa.X < Xb.X)
                    return -1;
                else if (Xa.X > Xb.X)
                    return 1;
                else
                {
                    if (Xa.Y > Xb.Y) return 1;
                    if (Xa.Y < Xb.Y) return -1;
                    else return 0;
                }
            }
        }
    }
}
