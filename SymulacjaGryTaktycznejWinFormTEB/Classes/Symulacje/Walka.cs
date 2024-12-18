using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SymulacjaGryTaktycznejWinFormTEB.Classes.Symulacje;

public static class Walka
{
    public static async Task WalkaAsync(Oddział oddzial1,
                                        Oddział oddzial2,
                                        Action<string> updateUI,
                                        Action<Jednostka, Jednostka>
                                            animateAttack,
                                        Action<string> showVictoryScreen
    )
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

            updateUI(
                $"Oddział {oddzial1.Nazwa} ma {oddzial1.Ilosc} jednostek.");
            updateUI(
                $"Oddział {oddzial2.Nazwa} ma {oddzial2.Ilosc} jednostek.");

            animateAttack(oddzial1.Jednostka, oddzial2.Jednostka);

            // Introduce a delay between actions
            await Task.Delay(2000);
        }

        string result;
        if (oddzial1.Ilosc > 0)
            result = $"Oddział {oddzial1.Nazwa} wygrywa.";
        else
            result = $"Oddział {oddzial2.Nazwa} wygrywa.";
        updateUI(result);
        showVictoryScreen(result);
    }

    public static async Task WalkaOddzialowAsync(Oddział oddzial1,
                                                 Oddział oddzial2,
                                                 Action<string> updateUI,
                                                 Action<Jednostka, Jednostka>
                                                     animateAttack,
                                                 Action<string, Oddział,
                                                     Oddział> showVictoryScreen
    )
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

            updateUI(
                $"Oddział {oddzial1.Nazwa} ma {oddzial1.Ilosc} jednostek.");
            updateUI(
                $"Oddział {oddzial2.Nazwa} ma {oddzial2.Ilosc} jednostek.");

            animateAttack(oddzial1.Jednostka, oddzial2.Jednostka);

            // Introduce a delay between actions
            await Task.Delay(2000);
        }

        string result;
        if (oddzial1.Ilosc > 0)
            result = $"Oddział {oddzial1.Nazwa} wygrywa.";
        else
            result = $"Oddział {oddzial2.Nazwa} wygrywa.";
        updateUI(result);
        showVictoryScreen(result, oddzial1, oddzial2);
    }
}
