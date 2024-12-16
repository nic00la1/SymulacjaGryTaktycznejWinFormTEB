using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SymulacjaGryTaktycznejWinFormTEB.Classes;

public class Mag : Jednostka
{
    public int MagicznyAtak { get; set; }

    public Mag()
    {
        Zycie = 20;
        MinObrazenia = 10;
        MaxObrazenia = 20;
        Szybkosc = 4;
        MagicznyAtak = 5; // Example value for the additional mechanic
    }

    public override int ObliczObrazenia()
    {
        Random rand = new();
        return rand.Next(MinObrazenia, MaxObrazenia + 1) + MagicznyAtak;
    }
}
