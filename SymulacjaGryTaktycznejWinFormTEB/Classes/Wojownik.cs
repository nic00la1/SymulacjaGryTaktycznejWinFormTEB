using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SymulacjaGryTaktycznejWinFormTEB.Classes;

public class Wojownik : Jednostka
{
    public int SpecialAttack { get; set; }

    public Wojownik()
    {
        Zycie = 50;
        Atak = 5;
        Obrona = 3;
        MinObrazenia = 2;
        MaxObrazenia = 7;
        Szybkosc = 5;
        SpecialAttack = 10; // Example special attack value
    }

    public override int ObliczObrazenia()
    {
        Random rand = new();
        int obrazenia = rand.Next(MinObrazenia, MaxObrazenia + 1);
        return Math.Max(1, obrazenia + Atak - Obrona);
    }

    public int UseSpecialAttack()
    {
        // Example special attack logic
        return SpecialAttack;
    }

    public void BerserkMode()
    {
        if (Zycie < 20) Atak += 5;
    }

    public void ShieldBlock()
    {
        Obrona += 5;
    }

    public bool DoubleStrike()
    {
        Random rand = new();
        return rand.Next(0, 100) < 20; // 20% chance to attack twice
    }
}
