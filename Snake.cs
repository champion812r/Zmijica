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
        int visinaMatrice, sirinaMatrice,x, y;

        public Snake(int visinaMatrice, int sirinaMatrice, int x, int y)
        {
            this.visinaMatrice = visinaMatrice;
            this.sirinaMatrice = sirinaMatrice;
            this.x = x;
            this.y = y;
            for (int i = 0; i < 5; i++)
            {
                Point deo = new Point { X = x-i, Y = y };
                teloZmije.Add(deo);
            }
        }


        private List<Instrukcija> lista (int direction)
        {
            List<Instrukcija> Lista = new List<Instrukcija>();
   

            return Lista;
        }

        private void ZmijaUpdate(ref List<Point> teloZmije)
        {
            teloZmije.RemoveAt(teloZmije.Count - 1);
            Point glava = new Point { X = 0, Y = 1 };
            teloZmije.Insert(0, glava);
            
        }

        struct Instrukcija
        {
            public int x, y;
            public bool oboj;  // true = oboj, false = obrisi
            public bool zmija;  // true = zmija, false = hrana
        }


    }

}
