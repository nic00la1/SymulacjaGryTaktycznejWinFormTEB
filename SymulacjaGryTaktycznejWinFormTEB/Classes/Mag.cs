using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SymulacjaGryTaktycznejWinFormTEB.Classes;

public class Mag : Jednostka
{
    public int MagicznyAtak { get; set; }
    public int Mana { get; set; }

    public Mag()
    {
        Zycie = 20;
        Atak = 3;
        Obrona = 2;
        MinObrazenia = 10;
        MaxObrazenia = 20;
        Szybkosc = 4;
        MagicznyAtak = 5; // Example magical attack value
        Mana = 100; // Example mana value
    }

    public override int ObliczObrazenia()
    {
        Random rand = new();
        int obrazenia = rand.Next(MinObrazenia, MaxObrazenia + 1);
        return Math.Max(1, obrazenia + Atak - Obrona + MagicznyAtak);
    }

    public int CastSpell()
    {
        // Example spell casting logic
        if (Mana >= 20)
        {
            Mana -= 20;
            return MagicznyAtak * 2; // Example spell damage
        }

        return 0; // Not enough mana to cast spell
    }

    public bool Heal()
    {
        if (Mana >= 30)
        {
            Mana -= 30;
            Zycie += 20; // Example heal amount
            return true; // Healing was successful
        }

        return false; // Not enough mana to heal
    }

    public bool ManaShield()
    {
        if (Mana >= 50)
        {
            Mana -= 50;
            Obrona += 10; // Example shield amount
            return true; // Mana shield was activated
        }

        return false; // Not enough mana to activate mana shield
    }
}
