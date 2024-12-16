using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SymulacjaGryTaktycznejWinFormTEB.Classes.Symulacje;

public static class Pojedynek
{
    private static readonly Random random = new();

    public static async Task PojedynekAsync(Jednostka jednostka1,
                                            Jednostka jednostka2,
                                            Action<string> updateUI,
                                            Action<Jednostka, Jednostka>
                                                animateAttack,
                                            Action<string> showVictoryScreen,
                                            Action playBerserkEffect,
                                            Action playShieldBlockEffect,
                                            Action playDoubleStrikeEffect,
                                            Action playFireballEffect,
                                            Action playHealEffect,
                                            Action playManaShieldEffect
    )
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
            // Use special abilities with a probability check
            if (atakujacy is Wojownik wojownik)
            {
                if (random.NextDouble() <
                    0.1) // 10% chance to enter Berserk mode
                {
                    wojownik.BerserkMode();
                    playBerserkEffect();
                    updateUI(
                        $"{wojownik.GetType().Name} wchodzi w tryb Berserk.");
                }

                if (random.NextDouble() <
                    0.1) // 10% chance to perform Double Strike
                    if (wojownik.DoubleStrike())
                    {
                        int doubleStrikeObrazenia = Math.Max(1,
                            wojownik.ObliczObrazenia() - obronca.Obrona);
                        obronca.Zycie -= doubleStrikeObrazenia;
                        updateUI(
                            $"{wojownik.GetType().Name} atakuje za {doubleStrikeObrazenia} pkt obrażeń. {obronca.GetType().Name} pozostaje {obronca.Zycie} HP");
                        animateAttack(wojownik, obronca);
                        playDoubleStrikeEffect();
                    }
            } else if (atakujacy is Mag mag)
            {
                if (random.NextDouble() < 0.1) // 10% chance to cast a spell
                {
                    int spellDamage = mag.CastSpell();
                    if (spellDamage > 0)
                    {
                        obronca.Zycie -= spellDamage;
                        updateUI(
                            $"{mag.GetType().Name} rzuca zaklęcie za {spellDamage} pkt obrażeń. {obronca.GetType().Name} pozostaje {obronca.Zycie} HP");
                        animateAttack(mag, obronca);
                        playFireballEffect();
                    }
                } else if (random.NextDouble() < 0.05) // 5% chance to heal
                {
                    if (mag.Heal())
                    {
                        updateUI(
                            $"{mag.GetType().Name} leczy się za 20 HP. {mag.GetType().Name} ma teraz {mag.Zycie} HP");
                        playHealEffect();
                    }
                } else if
                    (random.NextDouble() < 0.05) // 5% chance to use Mana Shield
                    if (mag.ManaShield())
                    {
                        updateUI(
                            $"{mag.GetType().Name} używa tarczy many. Obrona wzrasta o 10.");
                        playManaShieldEffect();
                    }
            }

            int obrazenia = Math.Max(1,
                atakujacy.ObliczObrazenia() - obronca.Obrona);
            obronca.Zycie -= obrazenia;
            updateUI(
                $"{atakujacy.GetType().Name} atakuje za {obrazenia} pkt obrażeń. {obronca.GetType().Name} pozostaje {obronca.Zycie} HP");

            animateAttack(atakujacy, obronca);

            if (obronca.Zycie <= 0)
            {
                string result =
                    $"{obronca.GetType().Name} ginie. {atakujacy.GetType().Name} wygrywa.";
                updateUI(result);
                showVictoryScreen(result);
                break;
            }

            // Swap roles
            Jednostka temp = atakujacy;
            atakujacy = obronca;
            obronca = temp;

            // Introduce a delay between actions
            await Task.Delay(2000);
        }
    }
}
