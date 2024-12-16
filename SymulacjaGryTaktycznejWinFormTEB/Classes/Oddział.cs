using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SymulacjaGryTaktycznejWinFormTEB.Classes;

public class Oddział
{
    public Jednostka Jednostka { get; set; }
    public int Ilosc { get; set; }
    public string Nazwa { get; set; }

    public Oddział(Jednostka jednostka, int ilosc, string nazwa)
    {
        Jednostka = jednostka;
        Ilosc = ilosc;
        Nazwa = nazwa;
    }

    public int ObliczObrazenia()
    {
        return Jednostka.ObliczObrazenia() * Ilosc;
    }

    public int ObliczZycie()
    {
        return Jednostka.Zycie * Ilosc;
    }
}
