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

    public static void Walka(Oddział oddzial1, Oddział oddzial2)
    {
        while (oddzial1.Ilosc > 0 && oddzial2.Ilosc > 0)
        {
            int obrazenia1 = oddzial1.ObliczObrazenia();
            int obrazenia2 = oddzial2.ObliczObrazenia();

            // Apply damage to oddzial2
            int remainingDamage = obrazenia1;
            while (remainingDamage > 0 && oddzial2.Ilosc > 0)
            {
                int currentUnitHp = oddzial2.Jednostka.Zycie;
                if (remainingDamage >= currentUnitHp)
                {
                    remainingDamage -= currentUnitHp;
                    oddzial2.Ilosc--;
                } else
                {
                    oddzial2.Jednostka.Zycie -= remainingDamage;
                    remainingDamage = 0;
                }
            }

            // Apply damage to oddzial1
            remainingDamage = obrazenia2;
            while (remainingDamage > 0 && oddzial1.Ilosc > 0)
            {
                int currentUnitHp = oddzial1.Jednostka.Zycie;
                if (remainingDamage >= currentUnitHp)
                {
                    remainingDamage -= currentUnitHp;
                    oddzial1.Ilosc--;
                } else
                {
                    oddzial1.Jednostka.Zycie -= remainingDamage;
                    remainingDamage = 0;
                }
            }

            Console.WriteLine(
                $"Oddział {oddzial1.Nazwa} ma {oddzial1.Ilosc} jednostek.");
            Console.WriteLine(
                $"Oddział {oddzial2.Nazwa} ma {oddzial2.Ilosc} jednostek.");
        }

        if (oddzial1.Ilosc > 0)
            Console.WriteLine($"Oddział {oddzial1.Nazwa} wygrywa.");
        else
            Console.WriteLine($"Oddział {oddzial2.Nazwa} wygrywa.");
    }

    public static void Wojna(Armia armia1, Armia armia2)
    {
        while (armia1.Oddziały.Any(o => o.Ilosc > 0) &&
               armia2.Oddziały.Any(o => o.Ilosc > 0))
        {
            int obrazenia1 = armia1.ObliczObrazenia();
            int obrazenia2 = armia2.ObliczObrazenia();

            // Apply damage to armia2
            int remainingDamage = obrazenia1;
            foreach (Oddział oddzial in armia2.Oddziały)
            {
                if (remainingDamage <= 0) break;

                while (remainingDamage > 0 && oddzial.Ilosc > 0)
                {
                    int currentUnitHp = oddzial.Jednostka.Zycie;
                    if (remainingDamage >= currentUnitHp)
                    {
                        remainingDamage -= currentUnitHp;
                        oddzial.Ilosc--;
                    } else
                    {
                        oddzial.Jednostka.Zycie -= remainingDamage;
                        remainingDamage = 0;
                    }
                }
            }

            // Apply damage to armia1
            remainingDamage = obrazenia2;
            foreach (Oddział oddzial in armia1.Oddziały)
            {
                if (remainingDamage <= 0) break;

                while (remainingDamage > 0 && oddzial.Ilosc > 0)
                {
                    int currentUnitHp = oddzial.Jednostka.Zycie;
                    if (remainingDamage >= currentUnitHp)
                    {
                        remainingDamage -= currentUnitHp;
                        oddzial.Ilosc--;
                    } else
                    {
                        oddzial.Jednostka.Zycie -= remainingDamage;
                        remainingDamage = 0;
                    }
                }
            }

            Console.WriteLine(
                $"Armia 1 ma {armia1.Oddziały.Sum(o => o.Ilosc)} jednostek.");
            Console.WriteLine(
                $"Armia 2 ma {armia2.Oddziały.Sum(o => o.Ilosc)} jednostek.");
        }

        if (armia1.Oddziały.Sum(o => o.Ilosc) > 0)
            Console.WriteLine("Armia 1 wygrywa.");
        else
            Console.WriteLine("Armia 2 wygrywa.");
    }
}
