// See https://aka.ms/new-console-template for more information
using System;


public abstract class Veicolo
{
    public string Marca { get; set; }
    public string Modello { get; set; }
    public int AnnoProduzione { get; set; }
    public string Targa { get; set; }

    public Veicolo(string marca, string modello, int anno, string targa)
    {
        Marca = marca;
        Modello = modello;
        AnnoProduzione = anno;
        Targa = targa;
    }

    public abstract decimal CalcolaCostoAssicurazione();
}


public abstract class VeicoloTerrestre : Veicolo
{
    public int NumeroRuote { get; set; }
    public int Cilindrata { get; set; }

    public VeicoloTerrestre(string marca, string modello, int anno, string targa,
                            int ruote, int cilindrata)
        : base(marca, modello, anno, targa)
    {
        NumeroRuote = ruote;
        Cilindrata = cilindrata;
    }
}

// Classe derivata per veicoli acquatici
public abstract class VeicoloAcquatico : Veicolo
{
    public double Lunghezza { get; set; }
    public string TipoMotore { get; set; }

    public VeicoloAcquatico(string marca, string modello, int anno, string targa,
                             double lunghezza, string tipoMotore)
        : base(marca, modello, anno, targa)
    {
        Lunghezza = lunghezza;
        TipoMotore = tipoMotore;
    }
}

// ------------------------------
// Classi specifiche terrestri
// ------------------------------

public class Auto : VeicoloTerrestre
{
    public Auto(string marca, string modello, int anno, string targa,
                int ruote, int cilindrata)
        : base(marca, modello, anno, targa, ruote, cilindrata) { }

    public override decimal CalcolaCostoAssicurazione()
    {
        return 300 + (Cilindrata * 0.1m);
    }
}

public class Moto : VeicoloTerrestre
{
    public Moto(string marca, string modello, int anno, string targa,
                int ruote, int cilindrata)
        : base(marca, modello, anno, targa, ruote, cilindrata) { }

    public override decimal CalcolaCostoAssicurazione()
    {
        return 150 + (Cilindrata * 0.05m);
    }
}

public class Camion : VeicoloTerrestre
{
    public Camion(string marca, string modello, int anno, string targa,
                  int ruote, int cilindrata)
        : base(marca, modello, anno, targa, ruote, cilindrata) { }

    public override decimal CalcolaCostoAssicurazione()
    {
        return 500 + (Cilindrata * 0.2m);
    }
}



public class Barca : VeicoloAcquatico
{
    public Barca(string marca, string modello, int anno, string targa,
                 double lunghezza, string tipoMotore)
        : base(marca, modello, anno, targa, lunghezza, tipoMotore) { }

    public override decimal CalcolaCostoAssicurazione()
    {
        return 400 + (decimal)(Lunghezza * 10);
    }
}

public class Nave : VeicoloAcquatico
{
    public Nave(string marca, string modello, int anno, string targa,
                double lunghezza, string tipoMotore)
        : base(marca, modello, anno, targa, lunghezza, tipoMotore) { }

    public override decimal CalcolaCostoAssicurazione()
    {
        return 1000 + (decimal)(Lunghezza * 20);
    }
}



public class Program
{
    public static void Main()
    {
        Veicolo v1 = new Auto("Fiat", "Panda", 2020, "AB123CD", 4, 1200);
        Veicolo v2 = new Barca("Yamaha", "WaveRunner", 2018, "ACQUA99", 5.5, "Fuoribordo");

        Console.WriteLine($"Assicurazione Auto: {v1.CalcolaCostoAssicurazione()} €");
        Console.WriteLine($"Assicurazione Barca: {v2.CalcolaCostoAssicurazione()} €");
    }
}

