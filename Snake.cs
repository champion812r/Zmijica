using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Zmijica
{
    class Snake
    {
        List<Point> teloZmije = new List<Point>();
        int visinaMatrice, sirinaMatrice, x, y, velicina = 3;
        Point hrana = new Point { X = 5, Y = 3 }; //privremena hrana
        public Snake(int visinaMatrice, int sirinaMatrice, int x, int y)
        {
            this.visinaMatrice = visinaMatrice;
            this.sirinaMatrice = sirinaMatrice;
            this.x = x;
            this.y = y;
            NapraviNovuZmiju(velicina); //nisam sigurna da l ovo treba da stoji ovde

        }


        public List<Instrukcija> lista(int direction)
        {
            List<Instrukcija> Lista = new List<Instrukcija>();
            Point glava = new Point { X = teloZmije[0].X, Y = teloZmije[0].Y };
            if (direction == 1)
            {
                glava.Y--;
                if (glava.Y < 0) glava.Y = visinaMatrice; //ako predje zid
                if (glava == teloZmije[0]) glava.Y++;       //ako ide unazad
                ZmijaUpdate(ref teloZmije, glava, ref Lista);  //menjanje pozicija u listi, dodavanje instrukcija
            }
            else if (direction == 2)
            {
                glava.Y++;
                if (glava.Y > visinaMatrice) glava.Y = 0;
                if (glava == teloZmije[0]) glava.Y--;
                ZmijaUpdate(ref teloZmije, glava, ref Lista);
            }
            else if (direction == 3)
            {
                glava.X--;
                if (glava.X < 0) glava.X = sirinaMatrice;
                if (glava == teloZmije[0]) glava.X++;
                ZmijaUpdate(ref teloZmije, glava, ref Lista);
            }
            else if (direction == 4)
            {
                glava.X++;
                if (glava.Y > sirinaMatrice) glava.Y = 0;
                if (glava == teloZmije[0]) glava.X--;
                ZmijaUpdate(ref teloZmije, glava, ref Lista);
            }

            return Lista;
        }

        private void ZmijaUpdate(ref List<Point> teloZmije, Point glava, ref List<Instrukcija> Lista)
        {
            Instrukcija instrukcija = new Instrukcija();
            if (teloZmije.Contains(glava)) //da li se sudara
            {
                glava.X = -1;
                instrukcija.xy = glava;
                Lista.Add(instrukcija);
                NapraviNovuZmiju(velicina);
            }
            else
            {
                teloZmije.Insert(0, glava);
                instrukcija.xy = glava;
                instrukcija.oboj = true;
                instrukcija.telo = true;
                Lista.Add(instrukcija);

                if (glava == hrana) //ako pokupi hranu
                {
                    //NapraviNovuHranu(ref instrukcija);
                    //Lista.Add(instrukcija);
                }
                else
                {
                    instrukcija.xy = teloZmije[teloZmije.Count - 1];
                    instrukcija.oboj = false;
                    instrukcija.telo = true;
                    Lista.Add(instrukcija);
                    teloZmije.RemoveAt(teloZmije.Count - 1);
                }
            }
        }

        private void NapraviNovuZmiju(int velicina)
        {
            teloZmije = new List<Point>();
            for (int i = 0; i <= velicina; i++)
            {
                Point deo = new Point { X = x - i, Y = y };
                teloZmije.Add(deo);
            }
        }

        public struct Instrukcija
        {
            public Point xy;
            public bool oboj;  // true = oboj, false = obrisi
            public bool telo;  // true = zmija, false = hrana
        }


    }

}