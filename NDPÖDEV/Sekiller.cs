using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NDPÖDEV;

namespace NDPÖDEV
{
    internal abstract class Sekil
    {

        public int x_Koordinat { get; set; }

        public int y_Koordinat { get; set; }

        public abstract void Cizdir(Graphics g);

        public abstract bool Carpisma(Sekil digerSekil);


    }

    class Nokta : Sekil
    {

        public int yaricap { get; set; }

        public int x { get; set; }
        public int y { get; set; }

        public Nokta()
        { 
            Random random = new Random();
            x = random.Next(50);
            y = random.Next(50);
            yaricap = 5;
        
        }


        public override bool Carpisma(Sekil digerSekil)
        {
            return true;
        }

        public override void Cizdir(Graphics g)
        {
            g.FillEllipse(Brushes.Red, x, y, yaricap, yaricap);
        }




    }

    class Cember : Sekil
    {
        public int yaricap { get; set; }

        
        public Cember()
        {
            Random random = new Random();
            this.yaricap =100;
            x_Koordinat = random.Next(200);
            y_Koordinat = random.Next(200);
            
        }


        public override void Cizdir(Graphics g)
        {
            g.DrawEllipse(Pens.Red, x_Koordinat, y_Koordinat, yaricap, yaricap);
        }


        public override bool Carpisma(Sekil digerSekil) 
        {
            if (digerSekil is Cember digerCember)
            {
                double merkezlerArasıMesafe = Math.Sqrt(Math.Pow((this.x_Koordinat + this.yaricap / 2), 2) - Math.Pow((digerCember.x_Koordinat + digerCember.yaricap / 2), 2));

                return merkezlerArasıMesafe <= (this.yaricap + digerCember.yaricap) / 2;

            }
            else if (digerSekil is Küre digerKüre)
            {
                double merkezlerArasıMesafe = Math.Sqrt(Math.Pow((this.x_Koordinat + this.yaricap / 2), 2) - Math.Pow((digerKüre.x_Koordinat + digerKüre.yaricap / 2), 2));

                return merkezlerArasıMesafe <= (this.yaricap + digerKüre.yaricap) / 2;

            }
            else if (digerSekil is Nokta digerNokta)
            {
                double merkezlerArasıMesafe = Math.Sqrt(Math.Pow((this.x_Koordinat + this.yaricap / 2), 2) - Math.Pow((digerNokta.x_Koordinat + digerNokta.yaricap / 2), 2));

                return merkezlerArasıMesafe <= (this.yaricap + digerNokta.yaricap) / 2;

            }
            else if  (digerSekil is Kare digerKare)
            {
                bool xEksenindeCarpisma = Math.Abs(this.x_Koordinat + this.yaricap / 2 - digerKare.x_Koordinat - digerKare.kenar / 2) <= (this.yaricap / 2 + digerKare.kenar / 2);
                bool yEksenindeCarpisma = Math.Abs(this.y_Koordinat + this.yaricap / 2 - digerKare.y_Koordinat - digerKare.kenar / 2) <= (this.yaricap / 2 + digerKare.kenar / 2);
                
                return xEksenindeCarpisma && yEksenindeCarpisma;
            }
            else if (digerSekil is Küp digerKüp)
            {
                bool xEksenindeCarpisma = Math.Abs(this.x_Koordinat + this.yaricap / 2 - digerKüp.x_Koordinat - digerKüp.kenar / 2) <= (this.yaricap / 2 + digerKüp.kenar / 2);
                bool yEksenindeCarpisma = Math.Abs(this.y_Koordinat + this.yaricap / 2 - digerKüp.y_Koordinat - digerKüp.kenar / 2) <= (this.yaricap / 2 + digerKüp.kenar / 2);

                return xEksenindeCarpisma && yEksenindeCarpisma;
            }
            else if (digerSekil is Dikdörtgen digerDikdortgen)
            {
                double dikdortgenMinX = digerDikdortgen.x_Koordinat;
                double dikdortgenMaxX = digerDikdortgen.x_Koordinat + digerDikdortgen.kenar;
                double dikdortgenMinY = digerDikdortgen.y_Koordinat;
                double dikdortgenMaxY = digerDikdortgen.y_Koordinat + digerDikdortgen.genislik;

                double closestX = Math.Max(dikdortgenMinX, Math.Min(this.x_Koordinat, dikdortgenMaxX));
                double closestY = Math.Max(dikdortgenMinY, Math.Min(this.y_Koordinat, dikdortgenMaxY));

                double distanceX = this.x_Koordinat - closestX;
                double distanceY = this.y_Koordinat - closestY;
                double distanceSquared = (distanceX * distanceX) + (distanceY * distanceY);

                return distanceSquared <= (this.yaricap * this.yaricap);


            }
            else if (digerSekil is DikdörtgenPrizma digerDikdortgenPrizma)
            {
                double dikdortgenMinX = digerDikdortgenPrizma.x_Koordinat;
                double dikdortgenMaxX = digerDikdortgenPrizma.x_Koordinat + digerDikdortgenPrizma.kenar;
                double dikdortgenMinY = digerDikdortgenPrizma.y_Koordinat;
                double dikdortgenMaxY = digerDikdortgenPrizma.y_Koordinat + digerDikdortgenPrizma.kenar;

                double closestX = Math.Max(dikdortgenMinX, Math.Min(this.x_Koordinat, dikdortgenMaxX));
                double closestY = Math.Max(dikdortgenMinY, Math.Min(this.y_Koordinat, dikdortgenMaxY));

                double distanceX = this.x_Koordinat - closestX;
                double distanceY = this.y_Koordinat - closestY;
                double distanceSquared = (distanceX * distanceX) + (distanceY * distanceY);

                return distanceSquared <= (this.yaricap * this.yaricap);


            }
            else if (digerSekil is Yüzey digerYüzey)
            {
                // Çemberin merkez noktasının yüzeyin sınırları içerisinde olup olmadığını kontrol et
                double closestX = Math.Max(digerYüzey.x_Koordinat, Math.Min(digerYüzey.x_Koordinat, digerYüzey.x_Koordinat + digerYüzey.genislik));
                double closestY = Math.Max(digerYüzey.y_Koordinat, Math.Min(digerYüzey.y_Koordinat, digerYüzey.y_Koordinat + digerYüzey.yukseklik));

                double distanceX = x_Koordinat - closestX;
                double distanceY = y_Koordinat - closestY;
                double distanceSquared = (distanceX * distanceX) + (distanceY * distanceY);

                return distanceSquared <= (yaricap * yaricap);

            }

            return false;
        
        }



    }

    class Küre :Sekil
    {
        public int yaricap { get; set; }

        public Küre()
        {
            Random random = new Random();
            this.yaricap = random.Next(100);
            x_Koordinat = random.Next(200);
            y_Koordinat = random.Next(200);

        }

        public override void Cizdir(Graphics g)
        {
            g.FillEllipse(Brushes.Red, x_Koordinat, y_Koordinat, yaricap, yaricap);
        }


        public override bool Carpisma(Sekil digerSekil)
        {


            if (digerSekil is Cember digerCember)
            {
                double merkezlerArasıMesafe = Math.Sqrt(Math.Pow((this.x_Koordinat + this.yaricap / 2), 2) - Math.Pow((digerCember.x_Koordinat + digerCember.yaricap / 2), 2));

                return merkezlerArasıMesafe <= (this.yaricap + digerCember.yaricap) / 2;

            }
            else if (digerSekil is Küre digerKüre)
            {
                double merkezlerArasıMesafe = Math.Sqrt(Math.Pow((this.x_Koordinat + this.yaricap / 2), 2) - Math.Pow((digerKüre.x_Koordinat + digerKüre.yaricap / 2), 2));

                return merkezlerArasıMesafe <= (this.yaricap + digerKüre.yaricap) / 2;

            }
            else if (digerSekil is Nokta digerNokta)
            {
                double merkezlerArasıMesafe = Math.Sqrt(Math.Pow((this.x_Koordinat + this.yaricap / 2), 2) - Math.Pow((digerNokta.x_Koordinat + digerNokta.yaricap / 2), 2));

                return merkezlerArasıMesafe <= (this.yaricap + digerNokta.yaricap) / 2;

            }
            else if (digerSekil is Kare digerKare)
            {
                bool xEksenindeCarpisma = Math.Abs(this.x_Koordinat + this.yaricap / 2 - digerKare.x_Koordinat - digerKare.kenar / 2) <= (this.yaricap / 2 + digerKare.kenar / 2);
                bool yEksenindeCarpisma = Math.Abs(this.y_Koordinat + this.yaricap / 2 - digerKare.y_Koordinat - digerKare.kenar / 2) <= (this.yaricap / 2 + digerKare.kenar / 2);

                return xEksenindeCarpisma && yEksenindeCarpisma;
            }
            else if (digerSekil is Küp digerKüp)
            {
                bool xEksenindeCarpisma = Math.Abs(this.x_Koordinat + this.yaricap / 2 - digerKüp.x_Koordinat - digerKüp.kenar / 2) <= (this.yaricap / 2 + digerKüp.kenar / 2);
                bool yEksenindeCarpisma = Math.Abs(this.y_Koordinat + this.yaricap / 2 - digerKüp.y_Koordinat - digerKüp.kenar / 2) <= (this.yaricap / 2 + digerKüp.kenar / 2);

                return xEksenindeCarpisma && yEksenindeCarpisma;
            }
            else if (digerSekil is Dikdörtgen digerDikdortgen)
            {
                double dikdortgenMinX = digerDikdortgen.x_Koordinat;
                double dikdortgenMaxX = digerDikdortgen.x_Koordinat + digerDikdortgen.kenar;
                double dikdortgenMinY = digerDikdortgen.y_Koordinat;
                double dikdortgenMaxY = digerDikdortgen.y_Koordinat + digerDikdortgen.genislik;

                double closestX = Math.Max(dikdortgenMinX, Math.Min(this.x_Koordinat, dikdortgenMaxX));
                double closestY = Math.Max(dikdortgenMinY, Math.Min(this.y_Koordinat, dikdortgenMaxY));

                double distanceX = this.x_Koordinat - closestX;
                double distanceY = this.y_Koordinat - closestY;
                double distanceSquared = (distanceX * distanceX) + (distanceY * distanceY);

                return distanceSquared <= (this.yaricap * this.yaricap);


            }
            else if (digerSekil is DikdörtgenPrizma digerDikdortgenPrizma)
            {
                double dikdortgenMinX = digerDikdortgenPrizma.x_Koordinat;
                double dikdortgenMaxX = digerDikdortgenPrizma.x_Koordinat + digerDikdortgenPrizma.kenar;
                double dikdortgenMinY = digerDikdortgenPrizma.y_Koordinat;
                double dikdortgenMaxY = digerDikdortgenPrizma.y_Koordinat + digerDikdortgenPrizma.kenar;

                double closestX = Math.Max(dikdortgenMinX, Math.Min(this.x_Koordinat, dikdortgenMaxX));
                double closestY = Math.Max(dikdortgenMinY, Math.Min(this.y_Koordinat, dikdortgenMaxY));

                double distanceX = this.x_Koordinat - closestX;
                double distanceY = this.y_Koordinat - closestY;
                double distanceSquared = (distanceX * distanceX) + (distanceY * distanceY);

                return distanceSquared <= (this.yaricap * this.yaricap);


            }
            else if (digerSekil is Yüzey digerYüzey)
            {
                // Çemberin merkez noktasının yüzeyin sınırları içerisinde olup olmadığını kontrol et
                double closestX = Math.Max(digerYüzey.x_Koordinat, Math.Min(digerYüzey.x_Koordinat, digerYüzey.x_Koordinat + digerYüzey.genislik));
                double closestY = Math.Max(digerYüzey.y_Koordinat, Math.Min(digerYüzey.y_Koordinat, digerYüzey.y_Koordinat + digerYüzey.yukseklik));

                double distanceX = x_Koordinat - closestX;
                double distanceY = y_Koordinat - closestY;
                double distanceSquared = (distanceX * distanceX) + (distanceY * distanceY);

                return distanceSquared <= (yaricap * yaricap);

            }

            return false;
        }

    }

    class Kare : Sekil 
    {
        public int kenar { get; set; }

        public Kare()
        { 
            Random random = new Random();

            x_Koordinat = random.Next(200);
            y_Koordinat = random.Next(200);
        
            kenar = random.Next(20,200);
        
        }

        public override bool Carpisma(Sekil digerSekil)
        {
            if (digerSekil is Cember digerCember)
            {
                // Çember ile çarpışma kontrolü
                double dikdortgenMinX = x_Koordinat;
                double dikdortgenMaxX = x_Koordinat + kenar;
                double dikdortgenMinY = y_Koordinat;
                double dikdortgenMaxY = y_Koordinat + kenar;

                double closestX = Math.Max(digerCember.x_Koordinat, Math.Min(x_Koordinat, digerCember.x_Koordinat + digerCember.yaricap));
                double closestY = Math.Max(digerCember.y_Koordinat, Math.Min(y_Koordinat, digerCember.y_Koordinat + digerCember.yaricap));

                double distanceX = x_Koordinat - closestX;
                double distanceY = y_Koordinat - closestY;
                double distanceSquared = (distanceX * distanceX) + (distanceY * distanceY);

                return distanceSquared <= (digerCember.yaricap * digerCember.yaricap);
            }
            else if (digerSekil is Küre digerKüre)
            {
                // Çember ile çarpışma kontrolü
                double dikdortgenMinX = x_Koordinat;
                double dikdortgenMaxX = x_Koordinat + kenar;
                double dikdortgenMinY = y_Koordinat;
                double dikdortgenMaxY = y_Koordinat + kenar;

                double closestX = Math.Max(digerKüre.x_Koordinat, Math.Min(x_Koordinat, digerKüre.x_Koordinat + digerKüre.yaricap));
                double closestY = Math.Max(digerKüre.y_Koordinat, Math.Min(y_Koordinat, digerKüre.y_Koordinat + digerKüre.yaricap));

                double distanceX = x_Koordinat - closestX;
                double distanceY = y_Koordinat - closestY;
                double distanceSquared = (distanceX * distanceX) + (distanceY * distanceY);

                return distanceSquared <= (digerKüre.yaricap * digerKüre.yaricap);
            }
            else if (digerSekil is Nokta digerNokta)
            {
                // Çember ile çarpışma kontrolü
                double dikdortgenMinX = x_Koordinat;
                double dikdortgenMaxX = x_Koordinat + kenar;
                double dikdortgenMinY = y_Koordinat;
                double dikdortgenMaxY = y_Koordinat + kenar;

                double closestX = Math.Max(digerNokta.x_Koordinat, Math.Min(x_Koordinat, digerNokta.x_Koordinat + digerNokta.yaricap));
                double closestY = Math.Max(digerNokta.y_Koordinat, Math.Min(y_Koordinat, digerNokta.y_Koordinat + digerNokta.yaricap));

                double distanceX = x_Koordinat - closestX;
                double distanceY = y_Koordinat - closestY;
                double distanceSquared = (distanceX * distanceX) + (distanceY * distanceY);

                return distanceSquared <= (digerNokta.yaricap * digerNokta.yaricap);
            }
            else if (digerSekil is Kare digerKare)
            {
                bool xEksenindeCarpisma = x_Koordinat + kenar >= digerKare.x_Koordinat && digerKare.x_Koordinat + digerKare.kenar >= x_Koordinat;
                bool yEksenindeCarpisma = y_Koordinat + kenar >= digerKare.y_Koordinat && digerKare.y_Koordinat + digerKare.kenar >= y_Koordinat;

                return xEksenindeCarpisma && yEksenindeCarpisma;
            }
            else if (digerSekil is Küp digerKüp)
            {
                bool xEksenindeCarpisma = x_Koordinat + kenar >= digerKüp.x_Koordinat && digerKüp.x_Koordinat + digerKüp.kenar >= x_Koordinat;
                bool yEksenindeCarpisma = y_Koordinat + kenar >= digerKüp.y_Koordinat && digerKüp.y_Koordinat + digerKüp.kenar >= y_Koordinat;

                return xEksenindeCarpisma && yEksenindeCarpisma;
            }
            else if (digerSekil is Dikdörtgen digerDikdortgen)
            {
                // Kare ve dikdörtgen çarpışma denetimi

                bool xEksenindeCarpisma = x_Koordinat + kenar >= digerDikdortgen.x_Koordinat && digerDikdortgen.x_Koordinat + digerDikdortgen.genislik >= x_Koordinat;
                bool yEksenindeCarpisma = y_Koordinat + kenar >= digerDikdortgen.y_Koordinat && digerDikdortgen.y_Koordinat + digerDikdortgen.en >= y_Koordinat;

                return xEksenindeCarpisma && yEksenindeCarpisma;



            }
            else if (digerSekil is DikdörtgenPrizma digerDikdortgenPrizma)
            {
                // Kare ve dikdörtgen çarpışma denetimi

                bool xEksenindeCarpisma = x_Koordinat + kenar >= digerDikdortgenPrizma.x_Koordinat && digerDikdortgenPrizma.x_Koordinat + digerDikdortgenPrizma.genislik >= x_Koordinat;
                bool yEksenindeCarpisma = y_Koordinat + kenar >= digerDikdortgenPrizma.y_Koordinat && digerDikdortgenPrizma.y_Koordinat + digerDikdortgenPrizma.en >= y_Koordinat;

                return xEksenindeCarpisma && yEksenindeCarpisma;



            }
            else if (digerSekil is Yüzey digerYüzey)
            {
                int kareX2 = x_Koordinat + kenar;
                int kareY2 = y_Koordinat + kenar;
                
                return !(kareX2 < digerYüzey.x_Koordinat || x_Koordinat > digerYüzey.x_Koordinat + digerYüzey.genislik ||
                         kareY2 < digerYüzey.y_Koordinat || y_Koordinat > digerYüzey.y_Koordinat + digerYüzey.yukseklik);


            }


            return false;
        }

        public override void Cizdir(Graphics g)
        {
            Point[] points = new Point[4];

            points[0] = new Point(x_Koordinat,y_Koordinat);
            points[1] = new Point(x_Koordinat + kenar, y_Koordinat);
            points[2] = new Point(x_Koordinat + kenar, y_Koordinat+kenar);
            points[3] = new Point(x_Koordinat, y_Koordinat+kenar);


            g.DrawPolygon(Pens.Red, points);

        }


    }

    class Küp : Sekil
    {

        public int kenar { get; set; }

        public Küp()
        {
            Random random = new Random();

            x_Koordinat = random.Next(200);
            y_Koordinat = random.Next(200);

            kenar = random.Next(20, 200);

        }

        public override bool Carpisma(Sekil digerSekil)
        {

            if (digerSekil is Cember digerCember)
            {
                // Çember ile çarpışma kontrolü
                double dikdortgenMinX = x_Koordinat;
                double dikdortgenMaxX = x_Koordinat + kenar;
                double dikdortgenMinY = y_Koordinat;
                double dikdortgenMaxY = y_Koordinat + kenar;

                double closestX = Math.Max(digerCember.x_Koordinat, Math.Min(x_Koordinat, digerCember.x_Koordinat + digerCember.yaricap));
                double closestY = Math.Max(digerCember.y_Koordinat, Math.Min(y_Koordinat, digerCember.y_Koordinat + digerCember.yaricap));

                double distanceX = x_Koordinat - closestX;
                double distanceY = y_Koordinat - closestY;
                double distanceSquared = (distanceX * distanceX) + (distanceY * distanceY);

                return distanceSquared <= (digerCember.yaricap * digerCember.yaricap);
            }
            else if (digerSekil is Küre digerKüre)
            {
                // Çember ile çarpışma kontrolü
                double dikdortgenMinX = x_Koordinat;
                double dikdortgenMaxX = x_Koordinat + kenar;
                double dikdortgenMinY = y_Koordinat;
                double dikdortgenMaxY = y_Koordinat + kenar;

                double closestX = Math.Max(digerKüre.x_Koordinat, Math.Min(x_Koordinat, digerKüre.x_Koordinat + digerKüre.yaricap));
                double closestY = Math.Max(digerKüre.y_Koordinat, Math.Min(y_Koordinat, digerKüre.y_Koordinat + digerKüre.yaricap));

                double distanceX = x_Koordinat - closestX;
                double distanceY = y_Koordinat - closestY;
                double distanceSquared = (distanceX * distanceX) + (distanceY * distanceY);

                return distanceSquared <= (digerKüre.yaricap * digerKüre.yaricap);
            }
            else if (digerSekil is Nokta digerNokta)
            {
                // Çember ile çarpışma kontrolü
                double dikdortgenMinX = x_Koordinat;
                double dikdortgenMaxX = x_Koordinat + kenar;
                double dikdortgenMinY = y_Koordinat;
                double dikdortgenMaxY = y_Koordinat + kenar;

                double closestX = Math.Max(digerNokta.x_Koordinat, Math.Min(x_Koordinat, digerNokta.x_Koordinat + digerNokta.yaricap));
                double closestY = Math.Max(digerNokta.y_Koordinat, Math.Min(y_Koordinat, digerNokta.y_Koordinat + digerNokta.yaricap));

                double distanceX = x_Koordinat - closestX;
                double distanceY = y_Koordinat - closestY;
                double distanceSquared = (distanceX * distanceX) + (distanceY * distanceY);

                return distanceSquared <= (digerNokta.yaricap * digerNokta.yaricap);
            }
            else if (digerSekil is Kare digerKare)
            {
                bool xEksenindeCarpisma = x_Koordinat + kenar >= digerKare.x_Koordinat && digerKare.x_Koordinat + digerKare.kenar >= x_Koordinat;
                bool yEksenindeCarpisma = y_Koordinat + kenar >= digerKare.y_Koordinat && digerKare.y_Koordinat + digerKare.kenar >= y_Koordinat;

                return xEksenindeCarpisma && yEksenindeCarpisma;
            }
            else if (digerSekil is Küp digerKüp)
            {
                bool xEksenindeCarpisma = x_Koordinat + kenar >= digerKüp.x_Koordinat && digerKüp.x_Koordinat + digerKüp.kenar >= x_Koordinat;
                bool yEksenindeCarpisma = y_Koordinat + kenar >= digerKüp.y_Koordinat && digerKüp.y_Koordinat + digerKüp.kenar >= y_Koordinat;

                return xEksenindeCarpisma && yEksenindeCarpisma;
            }
            else if (digerSekil is Dikdörtgen digerDikdortgen)
            {
                // Kare ve dikdörtgen çarpışma denetimi

                bool xEksenindeCarpisma = x_Koordinat + kenar >= digerDikdortgen.x_Koordinat && digerDikdortgen.x_Koordinat + digerDikdortgen.genislik >= x_Koordinat;
                bool yEksenindeCarpisma = y_Koordinat + kenar >= digerDikdortgen.y_Koordinat && digerDikdortgen.y_Koordinat + digerDikdortgen.en >= y_Koordinat;

                return xEksenindeCarpisma && yEksenindeCarpisma;



            }
            else if (digerSekil is DikdörtgenPrizma digerDikdortgenPrizma)
            {
                // Kare ve dikdörtgen çarpışma denetimi

                bool xEksenindeCarpisma = x_Koordinat + kenar >= digerDikdortgenPrizma.x_Koordinat && digerDikdortgenPrizma.x_Koordinat + digerDikdortgenPrizma.genislik >= x_Koordinat;
                bool yEksenindeCarpisma = y_Koordinat + kenar >= digerDikdortgenPrizma.y_Koordinat && digerDikdortgenPrizma.y_Koordinat + digerDikdortgenPrizma.en >= y_Koordinat;

                return xEksenindeCarpisma && yEksenindeCarpisma;



            }
            else if (digerSekil is Yüzey digerYüzey)
            {
                int kareX2 = x_Koordinat + kenar;
                int kareY2 = y_Koordinat + kenar;

                return !(kareX2 < digerYüzey.x_Koordinat || x_Koordinat > digerYüzey.x_Koordinat + digerYüzey.genislik ||
                         kareY2 < digerYüzey.y_Koordinat || y_Koordinat > digerYüzey.y_Koordinat + digerYüzey.yukseklik);


            }

            return false;
        }

        public override void Cizdir(Graphics g)
        {
            Point[] points = new Point[4];

            points[0] = new Point(x_Koordinat, y_Koordinat);
            points[1] = new Point(x_Koordinat + kenar, y_Koordinat);
            points[2] = new Point(x_Koordinat + kenar, y_Koordinat + kenar);
            points[3] = new Point(x_Koordinat, y_Koordinat + kenar);

            g.FillPolygon(Brushes.Red, points);

        }


    }

    class Dikdörtgen : Sekil
    {
        public int en { get; set; }

        public int genislik { get; set; }

        public int kenar { get; set; }

        public Dikdörtgen()
        { 
        
            Random random = new Random();
            
            en = 50;
            genislik = 100;
            kenar = 100;
        
            x_Koordinat = random.Next(200);

            y_Koordinat = random.Next(200);
        
        }

        public override void Cizdir(Graphics g)
        {
            g.DrawRectangle(Pens.Red, x_Koordinat, y_Koordinat, en, genislik);
        }

        public override bool Carpisma(Sekil digerSekil)
        {

            if (digerSekil is Dikdörtgen digerDikdortgen)

            {
                bool xEksenindeCarpisma = Math.Abs(this.x_Koordinat + this.en / 2 - digerDikdortgen.x_Koordinat - digerDikdortgen.en / 2) <= (this.en / 2 + digerDikdortgen.en / 2);
                bool yEksenindeCarpisma = Math.Abs(this.y_Koordinat + this.genislik / 2 - digerDikdortgen.y_Koordinat - digerDikdortgen.genislik / 2) <= (this.genislik / 2 + digerDikdortgen.genislik / 2);

                return xEksenindeCarpisma && yEksenindeCarpisma;
            }
            else if (digerSekil is DikdörtgenPrizma digerDikdortgenPrizma)

            {
                bool xEksenindeCarpisma = Math.Abs(this.x_Koordinat + this.en / 2 - digerDikdortgenPrizma.x_Koordinat - digerDikdortgenPrizma.en / 2) <= (this.en / 2 + digerDikdortgenPrizma.en / 2);
                bool yEksenindeCarpisma = Math.Abs(this.y_Koordinat + this.genislik / 2 - digerDikdortgenPrizma.y_Koordinat - digerDikdortgenPrizma.genislik / 2) <= (this.genislik / 2 + digerDikdortgenPrizma.genislik / 2);

                return xEksenindeCarpisma && yEksenindeCarpisma;
            }
            else if (digerSekil is Kare digerKare)
            {
                bool xEksenindeCarpisma = Math.Abs(this.x_Koordinat + this.kenar / 2 - digerKare.x_Koordinat - digerKare.kenar / 2) <= (this.kenar / 2 + digerKare.kenar / 2);
                bool yEksenindeCarpisma = Math.Abs(this.y_Koordinat + this.kenar / 2 - digerKare.y_Koordinat - digerKare.kenar / 2) <= (this.kenar / 2 + digerKare.kenar / 2);
                return xEksenindeCarpisma && yEksenindeCarpisma;

            }
            else if (digerSekil is Küp digerKüp)
            {
                bool xEksenindeCarpisma = Math.Abs(this.x_Koordinat + this.kenar / 2 - digerKüp.x_Koordinat - digerKüp.kenar / 2) <= (this.kenar / 2 + digerKüp.kenar / 2);
                bool yEksenindeCarpisma = Math.Abs(this.y_Koordinat + this.kenar / 2 - digerKüp.y_Koordinat - digerKüp.kenar / 2) <= (this.kenar / 2 + digerKüp.kenar / 2);
                return xEksenindeCarpisma && yEksenindeCarpisma;

            }
            else if (digerSekil is Cember digerCember)
            {
                double dikdortgenMerkezX = this.x_Koordinat + this.kenar / 2;
                double dikdortgenMerkezY = this.y_Koordinat + this.en / 2;
                double cemberMerkezX = digerCember.x_Koordinat + digerCember.yaricap;
                double cemberMerkezY = digerCember.y_Koordinat + digerCember.yaricap;

                double merkezlerArasiMesafe = Math.Sqrt(Math.Pow(dikdortgenMerkezX - cemberMerkezX, 2) + Math.Pow(dikdortgenMerkezY - cemberMerkezY, 2));
                double minMesafe = (this.kenar / 2) + digerCember.yaricap;

                return merkezlerArasiMesafe <= minMesafe;

            }
            else if (digerSekil is Küre digerKüre)
            {
                double dikdortgenMerkezX = this.x_Koordinat + this.kenar / 2;
                double dikdortgenMerkezY = this.y_Koordinat + this.en / 2;
                double cemberMerkezX = digerKüre.x_Koordinat + digerKüre.yaricap;
                double cemberMerkezY = digerKüre.y_Koordinat + digerKüre.yaricap;

                double merkezlerArasiMesafe = Math.Sqrt(Math.Pow(dikdortgenMerkezX - cemberMerkezX, 2) + Math.Pow(dikdortgenMerkezY - cemberMerkezY, 2));
                double minMesafe = (this.kenar / 2) + digerKüre.yaricap;

                return merkezlerArasiMesafe <= minMesafe;

            }
            else if (digerSekil is Nokta digerNokta)
            {
                double dikdortgenMerkezX = this.x_Koordinat + this.kenar / 2;
                double dikdortgenMerkezY = this.y_Koordinat + this.en / 2;
                double cemberMerkezX = digerNokta.x_Koordinat + digerNokta.yaricap;
                double cemberMerkezY = digerNokta.y_Koordinat + digerNokta.yaricap;

                double merkezlerArasiMesafe = Math.Sqrt(Math.Pow(dikdortgenMerkezX - cemberMerkezX, 2) + Math.Pow(dikdortgenMerkezY - cemberMerkezY, 2));
                double minMesafe = (this.kenar / 2) + digerNokta.yaricap;

                return merkezlerArasiMesafe <= minMesafe;

            }
            else if (digerSekil is Yüzey digerYüzey)
            {

                int dikdortgenX2 = x_Koordinat + genislik;
                int dikdortgenY2 = y_Koordinat + en;

                return !(dikdortgenX2 < digerYüzey.x_Koordinat || x_Koordinat > digerYüzey.x_Koordinat + digerYüzey.genislik ||
                     dikdortgenY2 < digerYüzey.y_Koordinat || y_Koordinat > digerYüzey.y_Koordinat + digerYüzey.yukseklik);



            }

            return false;
        }

    }

    class DikdörtgenPrizma : Sekil
    {
        public int en { get; set; }

        public int genislik { get; set; }

        public int kenar { get; set; }

        public DikdörtgenPrizma()
        {

            Random random = new Random();

            en = 50;
            genislik = 100;
            kenar = 100;

            x_Koordinat = random.Next(200);

            y_Koordinat = random.Next(200);

        }

        public override void Cizdir(Graphics g)
        {
            g.FillRectangle(Brushes.Red, x_Koordinat, y_Koordinat, en, genislik);
        }

        public override bool Carpisma(Sekil digerSekil)
        {
            if (digerSekil is Dikdörtgen digerDikdortgen)

            {
                bool xEksenindeCarpisma = Math.Abs(this.x_Koordinat + this.en / 2 - digerDikdortgen.x_Koordinat - digerDikdortgen.en / 2) <= (this.en / 2 + digerDikdortgen.en / 2);
                bool yEksenindeCarpisma = Math.Abs(this.y_Koordinat + this.genislik / 2 - digerDikdortgen.y_Koordinat - digerDikdortgen.genislik / 2) <= (this.genislik / 2 + digerDikdortgen.genislik / 2);

                return xEksenindeCarpisma && yEksenindeCarpisma;
            }
            else if (digerSekil is DikdörtgenPrizma digerDikdortgenPrizma)

            {
                bool xEksenindeCarpisma = Math.Abs(this.x_Koordinat + this.en / 2 - digerDikdortgenPrizma.x_Koordinat - digerDikdortgenPrizma.en / 2) <= (this.en / 2 + digerDikdortgenPrizma.en / 2);
                bool yEksenindeCarpisma = Math.Abs(this.y_Koordinat + this.genislik / 2 - digerDikdortgenPrizma.y_Koordinat - digerDikdortgenPrizma.genislik / 2) <= (this.genislik / 2 + digerDikdortgenPrizma.genislik / 2);

                return xEksenindeCarpisma && yEksenindeCarpisma;
            }
            else if (digerSekil is Kare digerKare)
            {
                bool xEksenindeCarpisma = Math.Abs(this.x_Koordinat + this.kenar / 2 - digerKare.x_Koordinat - digerKare.kenar / 2) <= (this.kenar / 2 + digerKare.kenar / 2);
                bool yEksenindeCarpisma = Math.Abs(this.y_Koordinat + this.kenar / 2 - digerKare.y_Koordinat - digerKare.kenar / 2) <= (this.kenar / 2 + digerKare.kenar / 2);
                return xEksenindeCarpisma && yEksenindeCarpisma;

            }
            else if (digerSekil is Küp digerKüp)
            {
                bool xEksenindeCarpisma = Math.Abs(this.x_Koordinat + this.kenar / 2 - digerKüp.x_Koordinat - digerKüp.kenar / 2) <= (this.kenar / 2 + digerKüp.kenar / 2);
                bool yEksenindeCarpisma = Math.Abs(this.y_Koordinat + this.kenar / 2 - digerKüp.y_Koordinat - digerKüp.kenar / 2) <= (this.kenar / 2 + digerKüp.kenar / 2);
                return xEksenindeCarpisma && yEksenindeCarpisma;

            }
            else if (digerSekil is Cember digerCember)
            {
                double dikdortgenMerkezX = this.x_Koordinat + this.kenar / 2;
                double dikdortgenMerkezY = this.y_Koordinat + this.en / 2;
                double cemberMerkezX = digerCember.x_Koordinat + digerCember.yaricap;
                double cemberMerkezY = digerCember.y_Koordinat + digerCember.yaricap;

                double merkezlerArasiMesafe = Math.Sqrt(Math.Pow(dikdortgenMerkezX - cemberMerkezX, 2) + Math.Pow(dikdortgenMerkezY - cemberMerkezY, 2));
                double minMesafe = (this.kenar / 2) + digerCember.yaricap;

                return merkezlerArasiMesafe <= minMesafe;

            }
            else if (digerSekil is Küre digerKüre)
            {
                double dikdortgenMerkezX = this.x_Koordinat + this.kenar / 2;
                double dikdortgenMerkezY = this.y_Koordinat + this.en / 2;
                double cemberMerkezX = digerKüre.x_Koordinat + digerKüre.yaricap;
                double cemberMerkezY = digerKüre.y_Koordinat + digerKüre.yaricap;

                double merkezlerArasiMesafe = Math.Sqrt(Math.Pow(dikdortgenMerkezX - cemberMerkezX, 2) + Math.Pow(dikdortgenMerkezY - cemberMerkezY, 2));
                double minMesafe = (this.kenar / 2) + digerKüre.yaricap;

                return merkezlerArasiMesafe <= minMesafe;

            }
            else if (digerSekil is Nokta digerNokta)
            {
                double dikdortgenMerkezX = this.x_Koordinat + this.kenar / 2;
                double dikdortgenMerkezY = this.y_Koordinat + this.en / 2;
                double cemberMerkezX = digerNokta.x_Koordinat + digerNokta.yaricap;
                double cemberMerkezY = digerNokta.y_Koordinat + digerNokta.yaricap;

                double merkezlerArasiMesafe = Math.Sqrt(Math.Pow(dikdortgenMerkezX - cemberMerkezX, 2) + Math.Pow(dikdortgenMerkezY - cemberMerkezY, 2));
                double minMesafe = (this.kenar / 2) + digerNokta.yaricap;

                return merkezlerArasiMesafe <= minMesafe;

            }
            else if (digerSekil is Yüzey digerYüzey)
            {

                int dikdortgenX2 = x_Koordinat + genislik;
                int dikdortgenY2 = y_Koordinat + en;

                return !(dikdortgenX2 < digerYüzey.x_Koordinat || x_Koordinat > digerYüzey.x_Koordinat + digerYüzey.genislik ||
                     dikdortgenY2 < digerYüzey.y_Koordinat || y_Koordinat > digerYüzey.y_Koordinat + digerYüzey.yukseklik);



            }

            return false;
        }

    }

    class Yüzey : Sekil
    {
        public int genislik { get; set; }

        public int yukseklik { get; set; }

        public Yüzey() 
        {
            Random random = new Random();

            genislik = random.Next(100);
            
            yukseklik = random.Next(100);

            x_Koordinat = random.Next(200);

            y_Koordinat = random.Next(200);
        
        
        }
            
        public override void Cizdir(Graphics g)
        {
            g.DrawRectangle(Pens.Black, x_Koordinat, y_Koordinat, genislik, yukseklik);
        }

        public override bool Carpisma(Sekil digerSekil)
        {
            if (digerSekil is Cember digerCember)
            {
                
                   
                    double closestX = Math.Max(x_Koordinat, Math.Min(digerCember.x_Koordinat, x_Koordinat + genislik));
                    double closestY = Math.Max(y_Koordinat, Math.Min(digerCember.y_Koordinat, y_Koordinat + yukseklik));

                    double distanceX = digerCember.x_Koordinat - closestX;
                    double distanceY = digerCember.y_Koordinat - closestY;
                    double distanceSquared = (distanceX * distanceX) + (distanceY * distanceY);

                    return distanceSquared <= (digerCember.yaricap * digerCember.yaricap);


            }

            return false;
        }
    }

   

}
