using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Zmijica
{
    public struct Instrukcija  //sadrzi podatke o tome sta treba da se uradi sa odredjenim kvadratom
    {
        public Point xy;   // lokacija
        public bool oboj;  // true = oboj, false = obrisi
        public bool telo;  // true = zmija, false = hrana
    }
    public class User
    {
        public int maxScore { get; set; } //najveći postignuti rezultat
        public string username { get; set; } //koristnicko ime
    }
    public class Snake
    {
        int visinaMatrice, sirinaMatrice, x, y, velicina=3, foodMax=3, currentScore;
        bool pokupljeno;
        User currentUser;
        StreamReader sr;
        StreamWriter sw;
        List<Point> teloZmije = new List<Point>();  //lista koordinata delova tela zmijice
        List<Point> hrana = new List<Point>();
        static List<User> korisnici;
        
        
        public Snake(int visinaMatrice, int sirinaMatrice, int x, int y, string username) //pocetna podesavanje 
        {   
            //velicina matrice (grid-a)
            this.visinaMatrice = visinaMatrice; 
            this.sirinaMatrice = sirinaMatrice;
            
            //pocetne koordinate glave
            this.x = x;
            this.y = y;

            //novi korisnik
            currentUser = new User();
            //postavljanje imena
            this.currentUser.username = username;
            //currentUser.username = "blanffn";
            currentScore = 0;
            //pravljenje zmije
            NapraviNovuZmiju(velicina);

            // pravljenje liste korisnika
            korisnici = new List<User>();

            //ako vec postoji .txt file sa ovom listu podaci se preuzimaju iz nje
            if (File.Exists("data.txt"))
            {
                sr = new StreamReader("data.txt");
                User korisnik = new User();
                while (sr.ReadLine() != null)
                {
                    string s = sr.ReadLine();
                    korisnik.username = s.Split()[0];
                    korisnik.maxScore = int.Parse(s.Split()[0]);
                    if (korisnik.username == currentUser.username) currentUser=korisnik;
                    korisnici.Add(korisnik);
                }
            }

            
        }

        //vraca trenutni rezultat igrice
        public int CurrentScore()
        {
            return currentScore;
        }

        //vraca podatke o trenutnom korisniku
        public User CurrentUserData()
        {
            return currentUser;
        }

        //vraca listu korisnika
        public static List<User> AllUsersData()
        {
            return korisnici;
        }

        //vraca niz lista pozicije delova zmijice i hrane
        public List<Point>[] SnakeAndFoodData()
        {
            List<Point>[] SnakeAndFood = new List<Point>[2] { teloZmije, hrana };
            return SnakeAndFood;
        }

        //vraca listu instrukcija u zavisnosti od smera kretanja zmijice
        public List<Instrukcija> lista (int direction)  
        {
            List<Instrukcija> Lista = new List<Instrukcija>(); //pravi se nova lista instrukcija
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
                if (glava.Y > visinaMatrice) glava.Y = 0; //ako predje zid
                if (glava == teloZmije[0]) glava.Y--; //ako ide unazad 
                ZmijaUpdate(ref teloZmije, glava, ref Lista); //menjanje pozicija u listi, dodavanje instrukcija
            }
            else if (direction == 3) //ako ide levo
            {
                glava.X--;
                if (glava.X < 0) glava.X = sirinaMatrice; //ako predje zid
                if (glava == teloZmije[0]) glava.X++; //ako ide unazad
                ZmijaUpdate(ref teloZmije, glava, ref Lista); //menjanje pozicija u listi, dodavanje instrukcija
            }
            else if (direction == 4) //ako ide desno
            {
                glava.X++;
                if (glava.X > sirinaMatrice) glava.X = 0; //ako predje zid
                if (glava == teloZmije[0]) glava.X--; //ako ide unazad
                ZmijaUpdate(ref teloZmije, glava, ref Lista); //menjanje pozicija u listi, dodavanje instrukcija
            }

            return Lista;
        }

        //promene u listi zmijice i desavanja u zavisnosti od njene pozicije, kreiranje instrukcija
        private void ZmijaUpdate(ref List<Point> teloZmije, Point glava, ref List<Instrukcija> Lista) 
        {
            Instrukcija instrukcija = new Instrukcija();

            //da li se sudara (sudara se ako je lokacija glave jednaka sa nekim delom tela zmijice)
            if (teloZmije.Contains(glava)) 
            {
                //kreiranje instrukcije
                glava.X = -1; //nepostojeca lokacija kao znak za kraj igrice
                instrukcija.xy = glava; 
                Lista.Add(instrukcija); //dodaje se u listu instrukcija
                KrajIgrice(); 
            }
            //ako se ne sudara
            else
            {
                //glava se uvek dodaje
                teloZmije.Insert(0, glava);

                //pravi se instrukcija za bojenje lokacije na kojoj je glava
                instrukcija.xy = glava;
                instrukcija.oboj = true;
                instrukcija.telo = true;
                Lista.Add(instrukcija); //dodaje se u listu instrukcija

                //provera da li glava ima isti lokaciju kao neki clan iz liste hrana
                for (int i = 0; i < hrana.Count; i++) 
                {
                    if (hrana[i] == glava) //ako postoji takav clan
                    {
                        currentScore++; //score se povecava
                        pokupljeno = true; 
                        hrana.RemoveAt(i); //hrana se uklanja iz liste
                        break;
                    }
                    else pokupljeno = false; //ako ne postoji
                }

                //rep se brise kada se ne pokupi hrana, ili ako nema hrane, a bool je ostao true
                if (!pokupljeno || hrana.Count==0) 
                {
                    //pravljenje istrukcije za brisanje repa 
                    instrukcija.xy = teloZmije[teloZmije.Count - 1]; //rep je uvek poslednji clan liste
                    instrukcija.oboj = false;
                    instrukcija.telo = true;
                    Lista.Add(instrukcija); //dodaje se u listu instrukcija
                    teloZmije.RemoveAt(teloZmije.Count - 1);  //uklanja se iz liste delova tela zmijice
                }
                
                //nasumicno generisanje hrane po frame-u
                Random r = new Random();
                if (r.Next(0, 5) == 1)
                    NapraviNovuHranu(ref hrana, r.Next(1, foodMax + 1), teloZmije, ref Lista);
            }
        }

        //funkcija koja se poziva na kraju igrice (kada se izgubi, pauzira i zapocne nova igra ili izadje iz igrice)
        public void KrajIgrice()
        {
            //ako je korisnik nov 
            if (currentUser.maxScore == 0)
            {
                currentUser.maxScore = currentScore;
                korisnici.Add(currentUser);
            }
            else if (currentScore > currentUser.maxScore)
            {
                currentUser.maxScore = currentScore;
            }
            NapraviNovuZmiju(velicina); 
        }

        private void NapraviNovuHranu(ref List<Point> hrana, int foodMax, List<Point> zmija, ref List<Instrukcija> Lista)
        {
            Random r = new Random();
            Point food = new Point { X = r.Next(0, sirinaMatrice + 1), Y = r.Next(0, visinaMatrice + 1) }; 
            Instrukcija instrukcija = new Instrukcija();
            for (int i = hrana.Count; i < foodMax; i++)
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


        private void UpisivanjeListe()
        {
            KrajIgrice();
            sr.Close();
            if (File.Exists("data.txt"))
                 File.Delete("data.txt");
            File.Create("data.txt").Close();
            sw = new StreamWriter("data.txt");
            for (int i = 0; i < korisnici.Count; i++)
                sw.WriteLine(korisnici[i].username + " " + korisnici[i].maxScore);
            sw.Close();
        }
    }

}
