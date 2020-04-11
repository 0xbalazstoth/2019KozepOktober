using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _2019KozepOktober
{
    class Radio
    {
        public static List<Radio> Adat = new List<Radio>();
        public string ElsoSor;
        public int Ora;
        public int Perc;
        public int AdasDb;
        public string Nev;

        public Radio(string elsoSor, int ora, int perc, int adasDb, string nev)
        {
            ElsoSor = elsoSor;
            Ora = ora;
            Perc = perc;
            AdasDb = adasDb;
            Nev = nev;
        }

        public static void MasodikFeladat(string fajl)
        {

            using (StreamReader olvas = new StreamReader(fajl))
            {
                string elsoSor = olvas.ReadLine();

                while (!olvas.EndOfStream)
                {
                    string[] split = olvas.ReadLine().Split(';');
                    int ora = Convert.ToInt32(split[0]);
                    int perc = Convert.ToInt32(split[1]);
                    int db = Convert.ToInt32(split[2]);
                    string nev = split[3];

                    Radio radio = new Radio(elsoSor, ora, perc, db, nev);

                    Adat.Add(radio);
                }
            }
        }

        public static void HarmadikFeladat() => Console.WriteLine($"3. feladat: Bejegyzések száma: {Adat.Count} db");
        public static void NegyedikFeladat()
        {
            int volte = 0;

            for (int i = 0; i < Adat.Count; i++)
            {
                if (Adat[i].Perc <= 1)
                    if (Adat[i].AdasDb == 4)
                        volte++;
            }

            string eldontes = (volte >= 1) ? "4. feladat: Volt négy adást indító soffőr." : "4. feladat: Nem Volt négy adást indító soffőr.";

            Console.WriteLine(eldontes);
        }

        public static void OtodikFeladat()
        {
            Console.Write("5. feladat: Kérek egy nevet: ");
            string megadottNev = Console.ReadLine();

            int ossz = 0;

            for (int i = 0; i < Adat.Count; i++)
            {
                if (Adat[i].Nev == megadottNev)
                {
                    ossz += Adat[i].AdasDb;
                }
            }

            string eldontes = (ossz >= 1) ? $"\t{megadottNev} {ossz}x használta a CB-rádiót." : "\tNincs ilyen nevű soffőr!";

            Console.WriteLine(eldontes);
        }

        public static int AtszamolPercre(int ora, int perc)
        {
            return (ora * 60) + perc;
        }

        public static void HetedikFeladat()
        {
            using (StreamWriter ki = new StreamWriter(@"cb2.txt"))
            {
                ki.WriteLine("Kezdes;Nev;AdasDb");

                for (int i = 0; i < Adat.Count; i++)
                {
                    ki.WriteLine($"{AtszamolPercre(Adat[i].Ora, Adat[i].Perc)};{Adat[i].Nev};{Adat[i].AdasDb}");
                    ki.Flush();
                }
            }
        }

        public static void NyolcadikFeladat() => Console.WriteLine($"8. feladat: Sofőrök száma: {Adat.GroupBy(x => x.Nev).Count()} fő");
        public static void KilencedikFeladat()
        {
            string nev = "";
            int max = 0;
            int ossz = 0;

            var rendezett = new SortedDictionary<string, int>();
            for (int i = 0; i < Adat.Count; i++)
            {
                if (rendezett.ContainsKey(Adat[i].Nev))
                    rendezett[Adat[i].Nev]++;
                else
                    rendezett[Adat[i].Nev] = 1;
            }

            foreach (var item in rendezett)
            {
                if (item.Value > max)
                {
                    max = item.Value;
                    nev = item.Key;
                }
            }

            for (int i = 0; i < Adat.Count; i++)
            {
                if (Adat[i].Nev == nev)
                    ossz += Adat[i].AdasDb;
            }

            Console.WriteLine($"9. feladat: Legtöbb adást indító sofőr \r\n\tNév: {nev}\r\n\tAdások száma: {ossz} alkalom");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Feladatok();

            Console.ReadKey();
        }

        private static void Feladatok()
        {
            Radio.MasodikFeladat("cb.txt");
            Radio.HarmadikFeladat();
            Radio.NegyedikFeladat();
            Radio.OtodikFeladat();
            Radio.HetedikFeladat();
            Radio.NyolcadikFeladat();
            Radio.KilencedikFeladat();
        }
    }
}
