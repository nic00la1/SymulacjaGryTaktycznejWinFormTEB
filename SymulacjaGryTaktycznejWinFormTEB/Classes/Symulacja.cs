using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SymulacjaGryTaktycznejWinFormTEB.Classes;

public static class Symulacja
{
    public static void Pojedynek(Jednostka jednostka1, Jednostka jednostka2)
    {
        Jednostka atakujacy, obronca;

        if (jednostka1.Szybkosc >= jednostka2.Szybkosc)
        {
            atakujacy = jednostka1;
            obronca = jednostka2;
        } else
        {
            atakujacy = jednostka2;
            obronca = jednostka1;
        }

        while (jednostka1.Zycie > 0 && jednostka2.Zycie > 0)
        {
            int obrazenia = Math.Max(1,
                atakujacy.ObliczObrazenia() - obronca.Obrona);
            obronca.Zycie -= obrazenia;
            Console.WriteLine(
                $"{atakujacy.GetType().Name} atakuje za {obrazenia} pkt obrażeń. {obronca.GetType().Name} pozostaje {obronca.Zycie} HP");

            if (obronca.Zycie <= 0)
            {
                Console.WriteLine(
                    $"{obronca.GetType().Name} ginie. {atakujacy.GetType().Name} wygrywa.");
                break;
            }

            // Swap roles
            Jednostka temp = atakujacy;
            atakujacy = obronca;
            obronca = temp;
        }
    }
}
