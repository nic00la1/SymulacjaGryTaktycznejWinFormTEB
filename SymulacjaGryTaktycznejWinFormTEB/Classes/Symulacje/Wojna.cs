using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SymulacjaGryTaktycznejWinFormTEB.Classes.Symulacje;

public static class Wojna
{
    public static async Task WojnaAsync(Armia armia1,
                                        Armia armia2,
                                        Action<string> updateUI,
                                        Action<Jednostka, Jednostka>
                                            animateAttack,
                                        Action<string> showVictoryScreen
    )
    {
        int totalDamageDealtByArmia1 = 0;
        int totalDamageDealtByArmia2 = 0;

        while (armia1.Oddziały.Any(o => o.Ilosc > 0) &&
               armia2.Oddziały.Any(o => o.Ilosc > 0))
        {
            int obrazenia1 = armia1.ObliczObrazenia();
            int obrazenia2 = armia2.ObliczObrazenia();

            totalDamageDealtByArmia1 += obrazenia1;
            totalDamageDealtByArmia2 += obrazenia2;

            // Podział obrażeń na oddziały wrogiej armii
            int obrazeniaNaOddzial1 = obrazenia1 / armia2.Oddziały.Count;
            int obrazeniaNaOddzial2 = obrazenia2 / armia1.Oddziały.Count;

            // Zastosowanie obrażeń do armia2
            foreach (Oddział oddzial in armia2.Oddziały)
            {
                int remainingDamage = obrazeniaNaOddzial1;
                while (remainingDamage > 0 && oddzial.Ilosc > 0)
                {
                    int currentUnitHp = oddzial.Jednostka.Zycie;
                    if (remainingDamage >= currentUnitHp)
                    {
                        remainingDamage -= currentUnitHp;
                        oddzial.Ilosc--;
                        updateUI(
                            $"DEBUG: {oddzial.Nazwa} Ilosc decremented to {oddzial.Ilosc}");
                    } else
                    {
                        oddzial.Jednostka.Zycie -= remainingDamage;
                        remainingDamage = 0;
                    }
                }

                updateUI(
                    $"{oddzial.Nazwa} ma teraz {oddzial.Ilosc} jednostek.");
                updateUI(
                    $"UPDATE_HP {oddzial.Nazwa} {oddzial.Jednostka.Zycie}");
                updateUI($"UPDATE_UNITS {oddzial.Nazwa} {oddzial.Ilosc}");
            }

            // Zastosowanie obrażeń do armia1
            foreach (Oddział oddzial in armia1.Oddziały)
            {
                int remainingDamage = obrazeniaNaOddzial2;
                while (remainingDamage > 0 && oddzial.Ilosc > 0)
                {
                    int currentUnitHp = oddzial.Jednostka.Zycie;
                    if (remainingDamage >= currentUnitHp)
                    {
                        remainingDamage -= currentUnitHp;
                        oddzial.Ilosc--;
                        updateUI(
                            $"DEBUG: {oddzial.Nazwa} Ilosc decremented to {oddzial.Ilosc}");
                    } else
                    {
                        oddzial.Jednostka.Zycie -= remainingDamage;
                        remainingDamage = 0;
                    }
                }

                updateUI(
                    $"{oddzial.Nazwa} ma teraz {oddzial.Ilosc} jednostek.");
                updateUI(
                    $"UPDATE_HP {oddzial.Nazwa} {oddzial.Jednostka.Zycie}");
                updateUI($"UPDATE_UNITS {oddzial.Nazwa} {oddzial.Ilosc}");
            }

            updateUI($"Armia 1 zadaje {obrazenia1} obrażeń.");
            updateUI($"Armia 2 zadaje {obrazenia2} obrażeń.");

            animateAttack(armia1.Oddziały.First().Jednostka,
                armia2.Oddziały.First().Jednostka);

            // Wprowadzenie opóźnienia między akcjami
            await Task.Delay(2000);
        }

        string result;
        if (armia1.Oddziały.Sum(o => o.Ilosc) > 0)
            result = "Armia 1 wygrywa.";
        else
            result = "Armia 2 wygrywa.";
        updateUI(result);
        showVictoryScreen(result);

        updateUI(
            $"Całkowite obrażenia zadane przez Armię 1: {totalDamageDealtByArmia1}");
        updateUI(
            $"Całkowite obrażenia zadane przez Armię 2: {totalDamageDealtByArmia2}");
    }
}
