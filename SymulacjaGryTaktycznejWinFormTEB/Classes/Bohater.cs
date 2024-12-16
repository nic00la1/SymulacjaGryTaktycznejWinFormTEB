using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SymulacjaGryTaktycznejWinFormTEB.Classes;

public class Bohater
{
    public int Atak { get; set; }
    public int Obrona { get; set; }

    public Bohater(int atak, int obrona)
    {
        Atak = atak;
        Obrona = obrona;
    }
}
