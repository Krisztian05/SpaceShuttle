using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShuttle
{
    struct Ursiklo 
    {
        public string Kod;
        public string Datum;
        public string Nev;
        public int Nap;
        public int Ora;
        public string Land;
        public int Legenyseg;

        public Ursiklo(string sor)
        {
            var dbok = sor.Split(';');
            this.Kod = dbok[0];
            this.Datum = dbok[1];
            this.Nev = dbok[2];
            this.Nap = int.Parse(dbok[3]);
            this.Ora = int.Parse(dbok[4]);
            this.Land = dbok[5];
            this.Legenyseg = int.Parse(dbok[6]);
        }
    }
    internal class Program
    {
        static List<Ursiklo> Ursiklo_List = new List<Ursiklo>() { };
        static void Main(string[] args)
        {
            Feladat23();
            Feladat4();
            Feladat5();
            Feladat6();
            Feladat7();
            Feladat8();
            Feladat9();
            Feladat10();
            Console.ReadKey();
        }
        private static void Feladat10() 
        {
            List<string> Ursiklo_Nev = new List<string>() { };
            foreach (var U in Ursiklo_List)
            {
                if (!Ursiklo_Nev.Contains(U.Nev))
                {
                    Ursiklo_Nev.Add(U.Nev);
                }
            }
            double[] Ursiklo_Nap = new double[Ursiklo_Nev.Count];
            for (int i = 0; i < Ursiklo_List.Count; i++)
            {
                for (int j = 0; j < Ursiklo_Nev.Count; j++)
                {
                    if (Ursiklo_List[i].Nev == Ursiklo_Nev[j])
                    {
                        Ursiklo_Nap[j] += Ursiklo_List[i].Nap + (double) Ursiklo_List[i].Ora / 24;
                    }
                }
            }

            var sw = new StreamWriter(@"ursiklok.txt", false, Encoding.UTF8);
            for (int i = 0; i < Ursiklo_Nev.Count; i++)
            {
                sw.WriteLine("{0}\t{1:0.00}", Ursiklo_Nev[i], Ursiklo_Nap[i]);
            }
            sw.Close();
        }
        private static void Feladat9()
        {
            Console.WriteLine("9. feladat:");
            double Teljesitett = 0;
            foreach (var U in Ursiklo_List)
            {
                if (U.Land == "Kennedy")
                {
                    Teljesitett++;
                }
            }
            Teljesitett = Teljesitett / Ursiklo_List.Count * 100;
            Console.WriteLine("\tA küldetések {0:0.00}%-a fejeződött be a Kennedy űrközpontban.", Teljesitett);
        }
        private static void Feladat8()
        {
            int Evszam;
            Console.WriteLine("8. feladat:");
            do
            {
                Console.Write("\tÉvszám: ");
                Evszam = int.Parse(Console.ReadLine());
            } while (Evszam < 1980 && Evszam < 2012);
            int Szamlalo = 0, Szamol = 0;
            while (Szamlalo < Ursiklo_List.Count)
            {
                if (int.Parse(new string(Ursiklo_List[Szamlalo].Datum.Take(4).ToArray())) == Evszam)
                {
                    Szamol++;
                }
                Szamlalo++;
            }
            if (Szamol == 0)
            {
                Console.WriteLine("\t Ebben az évben nem volt küldetés");
            }
            else
            {
                Console.WriteLine("\t Ebben az évben {0} küldetés volt.", Szamol);
            }
        }
        private static void Feladat7()
        {
            Console.WriteLine("7. feladat:");
            int MaxIndex = 0;
            int Max = 0;
            for (int i = 0; i < Ursiklo_List.Count; i++)
            {
                if (Ursiklo_List[MaxIndex].Nap * 24 + Ursiklo_List[MaxIndex].Ora < Ursiklo_List[i].Nap * 24 + Ursiklo_List[i].Ora)
                {
                    MaxIndex = i;
                    Max = Ursiklo_List[i].Nap * 24 + Ursiklo_List[i].Ora;
                }
            }
            Console.WriteLine("\tA leghosszabb ideig a {0} volt az űrben a {1} küldetés során", Ursiklo_List[MaxIndex].Nev, Ursiklo_List[MaxIndex].Kod);
            Console.WriteLine("\tösszesen {0} órát volt távol a földtől", Max);
        }
        private static void Feladat6()
        {
            Console.WriteLine("6. feladat:");
            int Szamlalo = 0;
            while (Szamlalo < Ursiklo_List.Count && Ursiklo_List[Szamlalo].Land == "nem landolt" || Ursiklo_List[Szamlalo].Nev.ToLower() != "columbia")
            {
                Szamlalo++;
            }
            Console.WriteLine("\t{0} asztronauta volt a Columbia fedélzetén annak utolsó útján.", Ursiklo_List[Szamlalo].Legenyseg);
        }
        private static void Feladat5()
        {
            Console.WriteLine("5. feladat:");
            int OtKisebb = 0;
            foreach (var U in Ursiklo_List)
            {
                if (U.Legenyseg < 5)
                {
                    OtKisebb++;
                }
            }
            Console.WriteLine("\tÖsszesen {0} alkalommal küldtek kevesebb, mint 5 embert az űrbe.", OtKisebb);
        }
        private static void Feladat4()
        {
            Console.WriteLine("4. feladat:");
            int OsszUtas = 0;
            foreach (var U in Ursiklo_List)
            {
                OsszUtas += U.Legenyseg;
            }
            Console.WriteLine("\t{0} utas indult az űrbe összesen.", OsszUtas);
        }
        private static void Feladat23()
        {
            var sr = new StreamReader(@"kuldetesek.csv", Encoding.UTF8);
            int Szamlalo = 0;
            while (!sr.EndOfStream)
            {
                Ursiklo_List.Add(new Ursiklo(sr.ReadLine()));
                Szamlalo++;
            }
            sr.Close();
            Console.WriteLine("3. feladat:\n\tÖsszesen {0} alkalommal indítottal űrhajót", Szamlalo);
        }
    }
}
