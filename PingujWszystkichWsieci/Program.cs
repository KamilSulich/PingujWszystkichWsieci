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
            Console.WriteLine("Nastapilo uruchomienie programu. Logi z programu są dostępne w pliku wynikpinga.txt w folderze bin solucji");
            var informacjeProcesu = new System.Diagnostics.ProcessStartInfo();
            informacjeProcesu.UseShellExecute = false;
            informacjeProcesu.CreateNoWindow = false;
            informacjeProcesu.FileName = @"C:\Windows\System32\cmd.exe";
            informacjeProcesu.Verb = "runas";
            string wynikPinga;
            for (int i = 0; i < 256; i++)
            {
                informacjeProcesu.Arguments = "/C ping 192.168.0." + Convert.ToString(i) + ">wynikpinga.txt ";
                System.Diagnostics.Process.Start(informacjeProcesu);
                Thread.Sleep(30000);
                try
                {
                     wynikPinga = System.IO.File.ReadAllText(@"C:\gry i filmy\backup slowik1647 google drive\moje własne projekty\programy\PingujWszystkichWsieci\PingujWszystkichWsieci\bin\Debug\wynikpinga.txt");
                }
                catch (Exception e)
                { wynikPinga = e.Message; }

                if (wynikPinga.Contains("Utracone = 0") || wynikPinga.Contains("Lost = 0"))//jezeli nie utracono jakis pakietow podczas pinga
                {
                    if (!wynikPinga.Contains("Destination host unreachable."))
                    {
                        File.AppendAllText(path, wynikPinga + Environment.NewLine);
                    }
                }
            }
        }
    }
}
