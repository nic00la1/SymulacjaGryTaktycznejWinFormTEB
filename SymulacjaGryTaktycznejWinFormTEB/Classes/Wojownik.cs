using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SymulacjaGryTaktycznejWinFormTEB.Classes;

public class Wojownik : Jednostka
{
    public Wojownik()
    {
        Zycie = 50;
        MinObrazenia = 2;
        MaxObrazenia = 7;
        Szybkosc = 5;
    }

    public override int ObliczObrazenia()
    {
        Random rand = new();
        return rand.Next(MinObrazenia, MaxObrazenia + 1);
    }
}
