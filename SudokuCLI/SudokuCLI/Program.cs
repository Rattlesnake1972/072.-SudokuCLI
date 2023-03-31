using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;    // Ha fájl beolvasásról, vagy írásról van szó, ezt a névteret mindenképpen ide be kell írnunk.

namespace SudokuCLI
{
    class Feladvany
    {
        public string Kezdo { get; private set; }
        public int Meret { get; private set; }

        public Feladvany(string sor)    // Ez egy konstruktor, ami átvesz egy sor nevű változót, amit a feladvanyok.txt fájlunkból kapunk. Ezt a sor nevű változót, mint látni, odaadja a francia zárójelben lévő Kezdo nevű mezőnek. A feladvanyok.txt fájlból érkező sor egy kezdő állapot, azért is lett Kezdo a neve, amit el kell tudnunk tárolni egy változóban, ami a sor nevű változó lesz, ott fog tárolódni az az adatsor, ami a fájlból jön. Azonkívül itt ezzel a konstruktorral egy példányt készítünk a feladvanyok.txt fájlból, példányosítunk, hogy a felhasználó számára is hozzáférhetővé váljanak a benne szereplő adatok, hogy lássa magyarul a sudoku feladványait kirajzolva a képernyőre.
        {
            Kezdo = sor;
            Meret = Convert.ToInt32(Math.Sqrt(sor.Length));    // Itt az történik, hogy a fájlból beolvasott sor hosszából (sor.Length) négyzetgyököt vonunk (Math.Sqrt), átkonvertáljuk egész számmá (Convert.ToInt32) és ez lesz a sudoku mérete. Tehát, ha a fájlban az adott sorunk 16 karakterből áll, akkor abból vonunk négyzetgyököt, ami ugye 4, ez lesz a sudoku mérete, tehát egy 4x4-es négyzetben fog kirajzolódni. Ha 64 karakterből áll a sor, annak a négyzetgyöke 8, tehát 8x8-as lesz a sudoku mérete, ekkora négyzetet fog kirajzolni.
        }

        public void Kirajzol()    // Ez a Kirajzol rész végzi gyakorlatilag a sudoku négyzeteinek kirajzolását. Mit is csinál? Végigmegy egy for ciklussal a fájlban található adott soron (Kezdo.Length), ha 0-át talál a sorban, akkor annak a helyére pontot rajzol (if(Kezdo[i] == 0) {Console.Write(".");}), ha nem nullát talál, akkor meg kiírja az ott szereplő számot (Console.Write(Kezdo[i]);). Kicsit másképp fogalmazva: Ha a Kezdo állapot i-edik, azaz akármelyik eleme 0, akkor pontot rajzol, különben ha a Kezdo i-edik, azaz akármelyik eleme nem nulla, akkor kiírja ezt az i-edik elemet, ami egy nullától különböző szám lesz, ami a fájlban szerepel.
        {
            for (int i = 0; i < Kezdo.Length; i++)
            {
                if (Kezdo[i] == '0')
                {
                    Console.Write(".");
                }
                else
                {
                    Console.Write(Kezdo[i]);
                }
                if (i % Meret == Meret - 1)
                {
                    Console.WriteLine();
                }
            }
        }
    }


    class Program
    {
        static List<Feladvany> lista = new List<Feladvany>();    // Itt egy listát készítünk a Feladvany osztályban létrejött adatoknak a tárolására.
        static void Main(string[] args)
        {
            // beolvassuk a progiba a feladvanyok.txt fájlt UTF8-as kódolással, hogy az ékezetes betűket is kezelni tudja és, hogy képes legyen a program dolgozni az adatokkal. Ha ez nem történik meg, akkor a progi nem tud mivel dolgozni. Ezért ezt a fájlt be kell illeszteni a program mappájába, a bin --> Debug mappába.

            StreamReader sr = new StreamReader("feladvanyok.txt", Encoding.UTF8);

            string sor = "";
            while(!sr.EndOfStream)
            {
                sor = sr.ReadLine();
                Feladvany f = new Feladvany(sor);            // A fenti Feladvany nevű osztályból egy példány. Jobban mondva a feladvanyok.txt file egy sorából készített példány, ami a Feladvany nevű osztályban szerepel. Példányosítás. Ezt az új példányt f-nek neveztük el, ami már nem ugyanaz, mint a fenti Feladvany sor az f betű miatt. Mivel a fenti Feladvany osztály konstruktora egy sor nevű változót vár, az új példány zárójelébe a sor-t kell írni.

                lista.Add(f);    // Az f nevű Feladvany osztály példányainak elemeit a listámhoz adom.
            }

            sr.Close();    // Bezárom a beolvasott fájlomat, hogy az abban szereplő adatokat ne tudja átírni, piszkálni senki.

            Console.WriteLine("3. feladat");
            Console.WriteLine("Feladványok száma: " + lista.Count);    // Ez a sor összeszámolja a beolvasott fájlban szereplő sorokat, amik a feladványok darabszáma lesz és azt írja ki. Jelen esetben 98.

            Console.WriteLine("4. feladat");

            int meret = 0;

            do
            {
                Console.Write("Kérem a sudoku méretét!: ");

                meret = Convert.ToInt32(Console.ReadLine());
            }
            while (meret<4 || meret>9);

            int meretDB = 0;
            for (int i = 0; i < lista.Count; i++)
            {
                if(lista[i].Meret==meret)
                {
                    meretDB++;
                }
            }

            Console.WriteLine("Ennyi van a megadott méretből: " +meretDB);
            Console.WriteLine("{0}x{0} méretű feladványból {1} darab van tárolva. ", meret, meretDB);

            Console.WriteLine("5. feladat");
            Random r = new Random();

            int kivalasztottIndex = 0;

            do
            {
                kivalasztottIndex = r.Next(0, lista.Count);
            }
            while (lista[kivalasztottIndex].Meret != meret);

            Console.WriteLine("A kiválasztott feladvány kezdő állapota: " + lista[kivalasztottIndex].Kezdo);

            Console.WriteLine("6. feladat");

            int nemNullaDB = 0;
            int hossz = lista[kivalasztottIndex].Kezdo.Length;

            for (int i = 0; i < hossz; i++)
            {
                if(lista[kivalasztottIndex].Kezdo[i]!='0')
                {
                    nemNullaDB++;
                }
            }

            double kitoltottseg = (double)nemNullaDB / hossz * 100;

            Console.WriteLine("A kiválasztott feladvány kitöltöttsége: " + kitoltottseg + "%");

            Console.WriteLine("7. feladat");
            Console.WriteLine("A kiválasztott feladvány kirajzolva: ");
            lista[kivalasztottIndex].Kirajzol();

            Console.WriteLine("8. feladat");

            string fajlneve = "sudoku" + meret + ".txt";
            StreamWriter sw = new StreamWriter(fajlneve);

            for (int i = 0; i < lista.Count; i++)
            {
                if(lista[i].Meret==meret)
                {
                    sw.WriteLine(lista[i].Kezdo);
                }
            }

            sw.Close();
            Console.WriteLine("A kiválasztott méretűek kiírva a fájlba.");
            Console.WriteLine("A " + fajlneve + " állomány létrehozva ennyi sorral: " +meretDB);
            
            


            Console.ReadKey();
        }
    }
}
