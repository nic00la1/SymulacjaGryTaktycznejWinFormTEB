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
        Random rand = new();
        int minObrazenia = Jednostka.MinObrazenia * Ilosc;
        int maxObrazenia = Jednostka.MaxObrazenia * Ilosc;
        return rand.Next(minObrazenia, maxObrazenia + 1);
    }

    public int ObliczCalkowiteZycie()
    {
        return Jednostka.Zycie * Ilosc;
    }

    public void ResetZycie()
    {
        Jednostka.Zycie =
            Jednostka.MaxObrazenia; // Reset Zycie to default value
    }
}
