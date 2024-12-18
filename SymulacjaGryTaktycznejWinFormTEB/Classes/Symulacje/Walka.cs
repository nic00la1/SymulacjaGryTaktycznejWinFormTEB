using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        Action<string, Oddział, Oddział> showVictoryScreen,
        Action<Oddział> playBattleCryEffect,
        Action<Oddział> playArrowRainEffect,
        Action<Oddział> playWarDrumEffect
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

        Stopwatch stopwatch = Stopwatch.StartNew();

        while (oddzial1.Ilosc > 0 && oddzial2.Ilosc > 0)
        {
            // Play a random effect
            int effect = random.Next(3);
            switch (effect)
            {
                case 0:
                    playBattleCryEffect(atakujacy);
                    break;
                case 1:
                    playArrowRainEffect(obronca);
                    break;
                case 2:
                    playWarDrumEffect(atakujacy);
                    break;
            }

            // Oblicz obrażenia atakującego oddziału
            int obrazenia = atakujacy.ObliczObrazenia();
            updateUI($"{atakujacy.Nazwa} zadaje {obrazenia} obrażeń.");
            animateAttack(atakujacy, obronca);

            // Zadaj obrażenia obrońcy
            int unitsLost = 0;
            while (obrazenia > 0 && obronca.Ilosc > 0)
                if (obrazenia >= obronca.Jednostka.Zycie)
                {
                    obrazenia -= obronca.Jednostka.Zycie;
                    unitsLost++;
                    obronca.Ilosc--;
                    if (obronca.Ilosc > 0)
                        obronca
                            .ResetZycie(); // Reset Zycie to default value only if there are remaining units
                } else
                {
                    obronca.Jednostka.Zycie -= obrazenia;
                    obrazenia = 0;
                }

            if (unitsLost > 0)
                updateUI(
                    $"DEBUG: {obronca.Nazwa} Ilosc decremented to {obronca.Ilosc}");

            updateUI($"{obronca.Nazwa} ma teraz {obronca.Ilosc} jednostek.");
            updateUI($"UPDATE_UNITS {obronca.Nazwa} {obronca.Ilosc}");
            updateUI(
                $"UPDATE_HP {obronca.Nazwa} {obronca.ObliczCalkowiteZycie()}");

            // Sprawdź, czy obrońca przetrwał
            if (obronca.Ilosc <= 0)
            {
                stopwatch.Stop();
                updateUI($"Czas rozgrywki {stopwatch.Elapsed.Seconds} sekundy");
                showVictoryScreen($"{atakujacy.Nazwa} wygrywa!", oddzial1,
                    oddzial2);
                return;
            }

            // Zamień role atakującego i obrońcy
            (atakujacy, obronca) = (obronca, atakujacy);

            // Wprowadź opóźnienie między turami
            await Task.Delay(2000);
        }
    }
}
