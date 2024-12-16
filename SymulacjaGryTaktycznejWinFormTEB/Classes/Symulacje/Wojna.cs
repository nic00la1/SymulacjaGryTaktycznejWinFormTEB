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

            updateUI(
                $"Armia 1 ma {armia1.Oddziały.Sum(o => o.Ilosc)} jednostek.");
            updateUI(
                $"Armia 2 ma {armia2.Oddziały.Sum(o => o.Ilosc)} jednostek.");

            animateAttack(armia1.Oddziały.First().Jednostka,
                armia2.Oddziały.First().Jednostka);

            // Introduce a delay between actions
            await Task.Delay(2000);
        }

        string result;
        if (armia1.Oddziały.Sum(o => o.Ilosc) > 0)
            result = "Armia 1 wygrywa.";
        else
            result = "Armia 2 wygrywa.";
        updateUI(result);
        showVictoryScreen(result);
    }
}
