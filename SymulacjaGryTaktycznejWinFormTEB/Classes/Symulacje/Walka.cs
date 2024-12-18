using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SymulacjaGryTaktycznejWinFormTEB.Classes.Symulacje;

public static class Walka
{
    private static readonly Random random = new();

    public static async Task WalkaOddzialowAsync(
        Oddział oddzial1,
        Oddział oddzial2,
        Action<string> updateUI,
        Action<Oddział, Oddział> animateAttack,
        Action<string, Oddział, Oddział> showVictoryScreen
    )
    {
        Oddział atakujacy, obronca;

        if (random.Next(2) == 0)
        {
            atakujacy = oddzial1;
            obronca = oddzial2;
        } else
        {
            atakujacy = oddzial2;
            obronca = oddzial1;
        }

        while (oddzial1.Ilosc > 0 && oddzial2.Ilosc > 0)
        {
            // Oblicz obrażenia atakującego oddziału
            int obrazenia = atakujacy.ObliczObrazenia();
            updateUI($"{atakujacy.Nazwa} zadaje {obrazenia} obrażeń.");
            animateAttack(atakujacy, obronca);

            // Zadaj obrażenia obrońcy
            while (obrazenia > 0 && obronca.Ilosc > 0)
                if (obrazenia >= obronca.Jednostka.Zycie)
                {
                    obrazenia -= obronca.Jednostka.Zycie;
                    obronca.Ilosc--;
                    if (obronca.Ilosc > 0)
                        obronca
                            .ResetZycie(); // Reset Zycie to default value only if there are remaining units
                } else
                {
                    obronca.Jednostka.Zycie -= obrazenia;
                    obrazenia = 0;
                }

            updateUI($"{obronca.Nazwa} ma teraz {obronca.Ilosc} jednostek.");
            updateUI($"UPDATE_UNITS {obronca.Nazwa} {obronca.Ilosc}");
            updateUI(
                $"UPDATE_HP {obronca.Nazwa} {obronca.ObliczCalkowiteZycie()}");

            // Sprawdź, czy obrońca przetrwał
            if (obronca.Ilosc <= 0)
            {
                showVictoryScreen($"{atakujacy.Nazwa} wygrywa!", oddzial1,
                    oddzial2);
                return;
            }

            // Zamień role atakującego i obrońcy
            Oddział temp = atakujacy;
            atakujacy = obronca;
            obronca = temp;

            // Wprowadź opóźnienie między turami
            await Task.Delay(2000);
        }
    }
}
