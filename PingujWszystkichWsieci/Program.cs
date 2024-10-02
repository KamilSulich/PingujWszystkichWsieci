using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.IO;
using System.Diagnostics;
using System.ComponentModel;
namespace PingujWszystkichWsieci
{
    class Program
    {
        static void Main(string[] args)
        {
            string folder = @"C:\dane\";//Tu wpisujemy własną ścieżkę do folderu w którym będzie zapisywany czas kiedy zerwano połączenie z internetem. Wpisujemy między znaki " ".
            string nazwapliku = @"PingowanieWszystkichIPsieci.txt";//Tu wpisujemy nazwę pliku w którym będzie zapisywany czas kiedy to nastąpiło. Wpisujemy między znaki " ".
            string path = folder + nazwapliku;
            if (File.Exists(path))
                File.Delete(path);
            Console.WriteLine("Nastapilo uruchomienie programu. Bierzące Logi z programu są dostępne w pliku wynikpinga.txt w folderze bin solucji. Ogólny log jest w "+ path);
            var informacjeProcesu = new System.Diagnostics.ProcessStartInfo();
            informacjeProcesu.UseShellExecute = false;
            informacjeProcesu.CreateNoWindow = false;
            informacjeProcesu.FileName = @"C:\Windows\System32\cmd.exe";
            informacjeProcesu.Verb = "runas";
            string wynikPinga;
            string PrefixAdresu;

            
            PrefixAdresu = "192.168.0.";
            wynikPinga = Pinguj(path, informacjeProcesu, PrefixAdresu);
            PrefixAdresu = "192.168.1.";
            wynikPinga = Pinguj(path, informacjeProcesu, PrefixAdresu);
            //pingowanie prefixów 192.168.0. i 192.168.1. zajmie około 4,5 godziny

            Console.WriteLine("koniec działania programu. Wciśnij cokolwiek by wyjść.");
            Console.ReadKey();
        }

        private static string Pinguj(string path, ProcessStartInfo informacjeProcesu, string PrefixAdresu)
        {
            string wynikPinga="";
            for (int i = 0; i < 256; i++)
            {
                
                Console.WriteLine("pinguje adres " + PrefixAdresu + i);
                informacjeProcesu.Arguments = "/C ping " + PrefixAdresu + Convert.ToString(i) + ">wynikpinga.txt ";
                System.Diagnostics.Process.Start(informacjeProcesu);
                Thread.Sleep(30000);
                try
                {
                    wynikPinga = System.IO.File.ReadAllText(@"C:\gry i filmy\backup slowik1647 google drive\moje własne projekty\programy\PingujWszystkichWsieci\PingujWszystkichWsieci\bin\Debug\wynikpinga.txt");
                }
                catch (Exception e)
                { wynikPinga = e.Message; }
                Console.WriteLine(wynikPinga);

                if (wynikPinga.Contains("Utracone = 0") || wynikPinga.Contains("Lost = 0"))//jezeli nie utracono jakis pakietow podczas pinga to zapisz wynik do pliku z wynikami tego co pomyślnie zpingowano
                {
                    if (!wynikPinga.Contains("Destination host unreachable."))
                    {
                        File.AppendAllText(path, wynikPinga + Environment.NewLine);
                    }
                }
            }
            return wynikPinga;
        }


        //for (int i = 0; i < 256; i++)
        //{
        //    Console.WriteLine("pinguje adres 192.168.0."+i);
        //    informacjeProcesu.Arguments = "/C ping 192.168.0." + Convert.ToString(i) + ">wynikpinga.txt ";
        //    System.Diagnostics.Process.Start(informacjeProcesu);
        //    Thread.Sleep(30000);
        //    try
        //    {
        //         wynikPinga = System.IO.File.ReadAllText(@"C:\gry i filmy\backup slowik1647 google drive\moje własne projekty\programy\PingujWszystkichWsieci\PingujWszystkichWsieci\bin\Debug\wynikpinga.txt");
        //    }
        //    catch (Exception e)
        //    { wynikPinga = e.Message; }

        //    if (wynikPinga.Contains("Utracone = 0") || wynikPinga.Contains("Lost = 0"))//jezeli nie utracono jakis pakietow podczas pinga
        //    {
        //        if (!wynikPinga.Contains("Destination host unreachable."))
        //        {
        //            File.AppendAllText(path, wynikPinga + Environment.NewLine);
        //        }
        //    }
        //}
    }
}
