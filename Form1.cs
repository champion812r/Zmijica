using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Zmijica
{
    public struct GameSettings
    {
        /// <summary>
        /// 
        /// Struktura koja čuva podatke o brzini zmije i imenu igrača
        /// koji se unose pre početka gejma
        /// 
        /// </summary>
        public int speed;
        public string name;
        public bool controls;
    }
    public partial class SnakeGraphics : Form
    {
        /// <summary>
        /// 
        /// definisanje boja za: telo zmije, hranu i dve vrste polja GameGrid-a
        /// 
        /// </summary>
        SolidBrush snakeBodyBrush = new(Color.FromArgb(164, 254, 99));
        SolidBrush foodBrush = new(Color.FromArgb(255, 153, 0));
        SolidBrush gridCellOne = new(Color.FromArgb(72, 85, 107));
        SolidBrush gridCellTwo = new(Color.FromArgb(87, 107, 121));
        SolidBrush snakeEye = new(Color.Black);
        Graphics g;

        Snake snake;
        Options o;

        int snakeBodySize=20; /// dimenzija tela zmije
        int eyeSize, eyeDist; /// velicina oka i razmak izmedju oka
        int direction = 4; /// smer kretanja zmije: {1 2 3 4}={gore, dole, levo, desno}
        int glavaX = 6, glavaY = 6; /// startna pozicija zmije
        int gridWidth, gridHeight; /// dimenzije GameGrid-a
        Point lastHeadPosition = new(-1,-1);
        Keys[] controlKeys; /// dugmici koji se koriste za upravljanje zmijom
        Timer timer= new(); /// tajmer koji ce otkucavati za prikaz novih frejmova
        int interval; /// interval nakon kojeg ce tajmer otkucavati - brzina kretanja zmije

        bool playing = false; ///oznacava da li je u toku igra

        int speedMin = 500, speedMax = 100; ///minimalna i maximalna vrednost intervala, izrazeno u milisekundama

        public SnakeGraphics()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen; ///pozicioniranje forme na centar ekrana

            this.Enabled = false;
            g = gameGrid.CreateGraphics(); /// pomocu 'g' se iscrtavaju objekti po GameGrid-u
            
            gridWidth = gameGrid.Width;
            gridHeight = gameGrid.Height;

            eyeSize = (snakeBodySize * 2) / 5;
            eyeDist = snakeBodySize / 5;

            this.Show(); //rucno prikazujem osnovnu formu da se ne bi Options forma prikazala prva
            MessageBox.Show("Control snake using arrow keys. Pause game by pressing 'P'", "Welcome to game Zmijica!",MessageBoxButtons.OK,MessageBoxIcon.Information);
            StartNewGame();
        }

        void ResetGridBackground()
        {
            /// <summary>
            /// 
            /// Prikazivanje pozadine na GameGrid-u. 
            /// x i y predstavljaju piksele u GameGrid-u
            /// 
            /// </summary>
            for (int y = 0; y <= gridHeight - snakeBodySize; y += snakeBodySize)
            {
                for (int x = 0; x <= gridWidth - snakeBodySize; x += snakeBodySize)
                {
                    FillGridCell(x, y);///popunjavanje pravougaonog polja ciji se gornji levi ugao nalazi na poziciji (x,y) 
                }
            }
        }
        void resetSnake()
        {
            /// <summary>
            /// 
            /// Postavljanje zmije na pocetne vrednosti
            /// Startni smer je desno (4)
            /// 
            /// </summary>
            direction = 4;
            lastHeadPosition = new Point(-1, -1);
            snake = new Snake(gridHeight / snakeBodySize -1, gridWidth / snakeBodySize -1, glavaX, glavaY,o.name); ///pravim novu zmiju i saljem potrebne podatke
        }

        void launchTimer()
        {
            timer = new Timer() { Interval = interval };
            timer.Tick += Frame; /// govorimo tajmeru koju funkciju treba da izvrsi pri svakom otkucaju odnosno za svaki frejm
            timer.Start();
        }
        private void Frame(object sender, EventArgs e)
        {
            makeMove(); /// u svakom frejmu trazicamo da zmija nacini sledeci korak
        }


        private void Stop()
        {
            /// <summary>
            /// 
            /// Funkcija koja zaustavlja igru, odnosno zaustavlja timer koji je sam po sebi pokretac igre
            /// 
            /// </summary>

            timer.Stop();
            playing = false;
        }

        void GameOver()
        {
            ///
            /// Funkcija koja se poziva kada zmija dodirne samu sebe
            ///

            Stop();

            ///
            /// Obavestava se igrac da je igra zavrsena i upitan je
            /// da li zeli da sacuva rezultat
            ///
            int score =snake.CurrentScore(); 
            string message = "Game Over :(( \nScore: "+score.ToString();
            string title = "Game Over";
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            MessageBox.Show(message, title, buttons, MessageBoxIcon.Information);

            //snake.UpisivanjeListe(); /// ukoliko zeli, vrsi se upisivanje rezultata

            ResetGridBackground(); /// stanje GameGrid-a se vraca na pocetno
            StartNewGame(); /// zapocinje se odmah nova igra
        }
        bool makeMove()
        {
            ///
            /// Funkcija u kojoj se prave promene na GameGrid-u
            /// odnosno prikazuje novi pokret zmije i pojava hrane
            /// 
            /// Funkcija vraca vrednst da li je izmena nacinjena
            ///

            label1.Text = "Score: " + snake.CurrentScore().ToString();

            List<Instrukcija> instrukckije = snake.lista(direction,timer.Interval); /// saljemo pravac kretanja klasi Snake i dobijamo natrag instrukcije
            foreach(Instrukcija i in instrukckije)
            {
                int X = i.xy.X, Y = i.xy.Y;
                if(X==-1)
                {
                    /// ukoliko je X==-1, to je znak da je zmija dotakla
                    /// samu sebe pa se igra zavrsava
                    GameOver();
                    return false;
                }

                ///
                /// Kako je za klasu Snake merna jedinica GameGrid-a jedno polje,
                /// a za grafiku jedan pixel, moramo da konvertujemo
                /// jednostavnim mnozenjem velicinom dela zmije
                ///
                int x = X * snakeBodySize, y = Y * snakeBodySize;

                if (i.oboj)
                {
                    if (i.telo)
                    {
                        /// ukoliko treba crtati zmiju
                        DrawHead(x, y);
                    }
                    else
                    {
                        /// ukoliko treba crtati hranu
                        DrawFood(x, y);
                    }
                }
                else
                {
                    /// ukoliko treba obrisati nesto,
                    /// bilo da je to rep zmije ili hrana
                    if(i.telo) Erase(x, y);

                    if(i.telo==false)
                    {
                        timer.Interval -= (int)Math.Ceiling((double)timer.Interval / 100);
                        Debug.WriteLine(timer.Interval);
                    }
                }
            }
            return true;
        }

        void FillGridCell(int x, int y)
        {
            ///
            /// Funkcija koja boji polje GameGrid-a 
            /// na lokaciji (x,y)
            ///

            /// 'i' i 'j' predstavljaju vrstu i kolonu polja
            int i = x / snakeBodySize, j = y / snakeBodySize;
            if ((i + j) % 2 == 0) /// parnost zbira vrste i kolone odredjuje boju polja, cime grid dobija izgled sahovske table
            {
                g.FillRectangle(gridCellOne, new Rectangle(x, y, snakeBodySize, snakeBodySize)); /// bojenje jednom bojom
            }
            else g.FillRectangle(gridCellTwo, new Rectangle(x, y, snakeBodySize, snakeBodySize)); /// bojenje drugom bojom
        }
        void Erase(int x, int y)
        {
            ///
            /// Funkcija koja brise hranu ili deo zmije sa nekog polja,
            /// odnosno samo iscrtava prazno polje na toj poziciji
            ///
            FillGridCell(x, y);
        }
        void DrawBody(int x, int y)
        {
            ///
            /// Funkcija koja iscrtava deo tela zmije
            ///
            g.FillRectangle(snakeBodyBrush, new Rectangle(x, y, snakeBodySize, snakeBodySize));
        }
        void DrawFood(int x, int y)
        {
            ///
            /// Funkcija koja iscrtava hranu
            ///
            g.FillEllipse(foodBrush, new Rectangle(x, y, snakeBodySize, snakeBodySize));
        }
        void DrawHead(int x, int y)
        {
            ///
            /// Funkcija koja iscrtava glavu zmije
            /// Glava sadrzi dva oka koja su usmerena
            /// kao smer kretanja
            ///

            DrawBody(x, y); /// glava je prvenstveno deo tela, pa cemo pokrenuti funkciju koja ga iscrtava

            if(lastHeadPosition.X!=-1)
            {
                ///
                /// Ukoliko postoji informacija o mestu na kome je poslednji put bila glava,
                /// na tom mestu cemo iscrtati samo deo tela cime cemo zapravo
                /// ukloniti oci koje su tu bile ucrtane
                ///
                DrawBody(lastHeadPosition.X,lastHeadPosition.Y);
            }

            ///
            /// U nastavku crtamo oči zmije
            ///

            int x1, x2, y1, y2; /// koordinate dva kruga koji predstavljaju oci zmije
            

            if(direction==1)
            {
                /// smer gore, jedno oko gore levo, drugo gore desno
                x1 = x; y1 = y;
                x2 = x + eyeSize + eyeDist; y2 = y;
            }
            else if (direction==2)
            {
                /// smer dole, jedno oko dole levo, drugo dole desno
                x1 = x; y1 = y+eyeSize + eyeDist;
                x2 = x + eyeSize + eyeDist; y2 = y + eyeSize + eyeDist;
            }
            else if(direction==3)
            {
                /// smer levo, jedno oko gore levo, drugo dole levo
                x1 = x; y1 = y;
                x2 = x; y2 = y + eyeSize + eyeDist;
            }
            else
            {
                /// smer desno, jedno oko gore desno, drugo dole desno
                x1 = x + eyeSize + eyeDist; y1=y;
                x2 = x + eyeSize + eyeDist; y2 = y + eyeSize + eyeDist;
            }

            /// iscrtavamo oba oka
            g.FillEllipse(snakeEye, new Rectangle(x1, y1, 8, 8));
            g.FillEllipse(snakeEye, new Rectangle(x2, y2, 8, 8));

            lastHeadPosition = new Point(x, y); /// belezimo poslednju poziciju glave

        }

        void Pause()
        {
            ///
            /// Funkcija koja pauzira igru 
            ///

            timer.Stop(); /// pauziramo tajmer

            Options o = new Options(playing);
            o.ShowDialog(); /// otvaramo prozor sa opcijama

            timer.Start(); /// prozor sa opcijama je sada zatvoren pa startujemo ponovo tajmer kako bi se igra nastavila
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            ///
            /// Funkcija koja procesuira dugme pritisnuto na tastaturi
            ///

            if (keyData==Keys.P)
            {
                /// Ukoliko je dugme 'P' pritisnuto, pauzira se igra
                Pause();
            }
            if (!playing) return base.ProcessCmdKey(ref msg, keyData); /// ukoliko se ne igra, ne treba procesuirati dugmice

            if(controlKeys.Contains(keyData)) /// ukoliko je strelica pritisnuta, provericemo koja je u pitanju
            {
                bool directionChanged = false; /// indikator koji pokazuje da li je doslo do promene smera kretanja zmije
                /// ukoliko je unesen smer isti kao i trenutni, to se ne belezi kao promena
                /// ukoliko je unesen smer suprotan trenutnom, takodje se ne belezi kao promena
                if (keyData==controlKeys[1] && direction!=1 && direction!=2)
                {
                    direction = 2;
                    directionChanged = true;
                }
                else if (keyData == controlKeys[0] && direction != 2 && direction != 1)
                {
                    direction = 1;
                    directionChanged = true;
                }
                else if (keyData == controlKeys[2] && direction != 4 && direction != 3)
                {
                    direction = 3;
                    directionChanged = true;
                }
                else if (keyData == controlKeys[3] && direction != 3 && direction != 4)
                {
                    direction = 4;
                    directionChanged = true;
                }

                if(directionChanged) 
                {
                    ///
                    /// Ukoliko je smer promenjen, zaustavicemo tajmer na kratko
                    /// dok se unese ta promena i onda ga pustiti da nastavi
                    /// ukoliko je promena nacinjena
                    ///

                    timer.Stop();
                    if(makeMove()) timer.Start();
                }
            }

            return base.ProcessCmdKey(ref msg, keyData); 
        }

        void StartNewGame()
        {
            ///
            /// Funkcija koja zapocinje novu igru
            ///

            o = new Options(false);
            o.ShowDialog(); /// prikazujemo prozor sa opcijama kako bi igrac uneo ime i brzinu

            GameSettings gs = o.getSettings(); /// nakon sto se prozor sa Opcijama zatvorio, pokupicemo unete vrednosti

            if(gs.controls) controlKeys = new Keys[] { Keys.Up, Keys.Down, Keys.Left, Keys.Right };
            else controlKeys = new Keys[] { Keys.W, Keys.S, Keys.A, Keys.D };

            resetSnake(); /// postavljamo zmiju na pocetne vrednosti
            interval = speedMin - gs.speed * speedMax; /// racunamo interval na osnovu odabrane vrednosti na trackSpeed-u
            launchTimer(); /// pokrecemo tajmer kako bi igrica zapocela
            playing = true;
        }

        private void RedrawGrid()
        {
            ///
            /// Funkcija koje je uvedena da nakon spustanja ili switchovanja prozora
            /// prikaze GameGrid onakav kakav je bio pre toga
            ///

            ResetGridBackground(); /// prvo crtamo prazan grid

            if (!playing) return; /// ukoliko se ne igra, nema potrebe nista drugo da radimo

            List<Point>[] data = snake.SnakeAndFoodData(); /// uzimam pozicije na kojima se nalazila zmija i hrana
            foreach(Point p in data[0]) /// prolazimo kroz sve delove zmije i crtamo ih
            {
                int X = p.X, Y = p.Y;
                int x = X * snakeBodySize, y = Y * snakeBodySize;
                DrawBody(x, y);
            }
            foreach(Point p in data[1]) /// prolazimo kroz svu hranu i crtamo je
            {
                int X = p.X, Y = p.Y;
                int x = X * snakeBodySize, y = Y * snakeBodySize;
                DrawFood(x, y);
            }
        }

        private void gameGrid_Paint(object sender, PaintEventArgs e)
        {
            ///
            /// Funkcija koja se poziva svaki put kada se GameGrid 
            /// iznova iscrtava
            /// Zato pozivamo funkciju za ponovno crtanje grid-a
            ///
            RedrawGrid();
        }
    }
}
