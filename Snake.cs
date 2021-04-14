using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Zmijica
{
    public struct Instrukcija  //sadrzi podatke o tome sta treba da se uradi sa odredjenim kvadratom
    {
        public Point xy;   // lokacija
        public bool oboj;  // true = oboj, false = obrisi
        public bool telo;  // true = zmija, false = hrana
    }
    public class Snake
    {
        List<Point> teloZmije = new List<Point>();  //lista koordinata delova tela zmijice
        int visinaMatrice, sirinaMatrice, x, y, velicina=3, foodmax=3;
        List<Point> hrana = new List<Point>();
        bool pokupljeno;

        public Snake(int visinaMatrice, int sirinaMatrice, int x, int y) //pocetna podesavanje 
        {   
            //velicina matrice (grid-a)
            this.visinaMatrice = visinaMatrice; 
            this.sirinaMatrice = sirinaMatrice;
            
            //pocetne koordinate glave
            this.x = x;
            this.y = y; 
            
            //pravljenje zmije i hrane
            NapraviNovuZmiju(velicina);
        }
        

        public List<Instrukcija> lista (int direction)  //vraca listi instrukcija u zavisnosti od smera kretanja zmijice
        {
            List<Instrukcija> Lista = new List<Instrukcija>();
            Point glava = new Point { X = teloZmije[0].X, Y = teloZmije[0].Y }; //trenutna lokacija glave zmijice
    
            if (direction==1) //ako ide gore
            {
                glava.Y--;
                if (glava.Y < 0) glava.Y = visinaMatrice; //ako predje zid
                if (glava==teloZmije[0]) glava.Y++;       //ako ide unazad
                ZmijaUpdate(ref teloZmije, glava, ref Lista);  //menjanje pozicija u listi, dodavanje instrukcija
            }
            else if (direction == 2) //ako ide dole
            {
                glava.Y++;
                if (glava.Y > visinaMatrice) glava.Y = 0;
                if (glava == teloZmije[0]) glava.Y--;
                ZmijaUpdate(ref teloZmije, glava, ref Lista);
            }
            else if (direction == 3) //ako ide levo
            {
                glava.X--;
                if (glava.X < 0) glava.X = sirinaMatrice;
                if (glava == teloZmije[0]) glava.X++;
                ZmijaUpdate(ref teloZmije, glava, ref Lista);
            }
            else if (direction == 4) //ako ide desno
            {
                glava.X++;
                if (glava.X > sirinaMatrice) glava.X = 0;
                if (glava == teloZmije[0]) glava.X--;
                ZmijaUpdate(ref teloZmije, glava, ref Lista);
            }

            return Lista;
        }

        private void ZmijaUpdate(ref List<Point> teloZmije, Point glava, ref List<Instrukcija> Lista) //promene u listi i kreiranje instrukcija
        {
            Instrukcija instrukcija = new Instrukcija();
            if (teloZmije.Contains(glava)) //da li se sudara 
            {
                glava.X = -1; //nepostojeca lokacija kao znak za kraj igrice
                instrukcija.xy = glava; 
                Lista.Add(instrukcija);
                NapraviNovuZmiju(velicina); //restart
            }
            else
            {
                //glava se uvek dodaje
                teloZmije.Insert(0, glava);
                instrukcija.xy = glava;
                instrukcija.oboj = true;
                instrukcija.telo = true;
                Lista.Add(instrukcija);

                for (int i = 0; i < hrana.Count; i++) //provera da li glava ima isti lokaciju kao neki clan iz liste hrana
                {
                    if (hrana[i] == glava) //ako postoji takav clan
                    {
                        pokupljeno = true;
                        hrana.RemoveAt(i); //uklanja se iz liste
                        break;
                    }
                    else pokupljeno = false; //ako ne postoji
                }

                if (!pokupljeno || hrana.Count==0) //rep se brise kada se ne pokupi hrana, ili ako nema hrane, a bool je ostao true
                {
                    //brisanje repa 
                    instrukcija.xy = teloZmije[teloZmije.Count - 1];
                    instrukcija.oboj = false;
                    instrukcija.telo = true;
                    Lista.Add(instrukcija);
                    teloZmije.RemoveAt(teloZmije.Count - 1);    
                }
                
                //nasumicno generisanje hrane po frame-u
                Random r = new Random();
                if (r.Next(0, 10) == 1)
                    NapraviNovuHranu(ref hrana, r.Next(1, foodmax + 1), teloZmije, ref Lista);
            }
        }

        private void NapraviNovuHranu(ref List<Point> hrana, int foodmax, List<Point> zmija, ref List<Instrukcija> Lista)
        {
            Random r = new Random();
            Point food = new Point { X = r.Next(0, sirinaMatrice + 1), Y = r.Next(0, visinaMatrice + 1) }; 
            Instrukcija instrukcija = new Instrukcija();
            for (int i = hrana.Count; i < foodmax; i++)
            {   
                while(hrana.Contains(food) || zmija.Contains(food)) //trazi se random lokacija na kojoj nema ni zmije ni hrane
                food = new Point { X = r.Next(0, sirinaMatrice + 1), Y = r.Next(0, visinaMatrice + 1) };
                hrana.Add(food); //kada se nadje doda se u listu

                //dodavanje instrukcije u listu
                instrukcija.xy = hrana[i];
                instrukcija.oboj = true;
                instrukcija.telo = false;
                Lista.Add(instrukcija);
            }
        }

        private void NapraviNovuZmiju(int velicina)
        {   
            //kreiranje zmijice u pocetnom polozaju
            teloZmije = new List<Point>();
            for (int i = 0; i <= velicina; i++)
            {
                Point deo = new Point { X = x - i, Y = y };
                teloZmije.Add(deo);
            }
        }



    }

}
