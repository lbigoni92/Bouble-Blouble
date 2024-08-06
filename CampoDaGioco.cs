// See https://aka.ms/new-console-template for more information
internal class CampoDaGioco
{
    private const int Righe = 6;
    private const int Colonne = 5;
    /// <summary>
    /// Campo da gioco che è costituito da 6*5 celle ti tipo TipoCasella
    /// </summary>
    private readonly TipoCasella[,] Campo = new TipoCasella[Righe, Colonne];
    public CampoDaGioco()
    {
        //al massimo abbiamo 30 caselle da inizializzare  e andiamo a inizializzare della 5  alle 15 caselle
        int nCelleDaInizializzare = new Random().Next(5, 15);//ci genera un numero casuale tra 5 e 15
        InizializzaCampoDaGioco(nCelleDaInizializzare);
        Console.WriteLine("Campo inizializzato con " + nCelleDaInizializzare + " caselle con la palla");

    }

    public void StampaCampo()
    {
        for (int r = 0; r < Righe; r++)
        {
            for (int c = 0; c < Colonne; c++)
            {
                switch (Campo[r, c])
                {
                    case TipoCasella.Vuota:
                        Console.Write("V ");
                        break;
                    case TipoCasella.Sgonfia:
                        Console.Write("S ");
                        break;
                    case TipoCasella.GonfiaAMeta:
                        Console.Write("G ");
                        break;
                    case TipoCasella.InProcintoDiEsplodere:
                        Console.Write("I ");
                        break;
                    case TipoCasella.Esplosa:
                        Console.Write("E ");
                        break;
                }
            }
            Console.WriteLine();
        }
    }

    /// <summary>
    ///private perche non deve essere visibile ad li fuori della classe; si inizializza la matrice Campo con nCelleDaInizializzare caselle
    /// </summary>
    /// <param name="nCelleDaInizializzare">numero di caselle con la palla</param>
    /// <exception cref="NotImplementedException"></exception>
    private void InizializzaCampoDaGioco(int nCelleDaInizializzare)
    {
        //così avremo un campo con sole celle vuote
        for (int r = 0; r < Righe; r++)
            for (int c = 0; c < Colonne; c++)
                Campo[r, c] = TipoCasella.Vuota;

        //adesso andiamo a inizializzare il campo caselle con la palla
        for (int i = 0; i < nCelleDaInizializzare; i++)
        {
            //genero una [r,c] casuale
            int r;
            int c;
            r = new Random().Next(0, 6);
            c = new Random().Next(0, 5);
            //se la casella è vuota la inizializzo con la palla
            if (Campo[r, c] == TipoCasella.Vuota)
            {
                int palla = new Random().Next(0, 3);
                switch (palla)
                {
                    case 0:
                        Campo[r, c] = TipoCasella.Sgonfia;
                        break;
                    case 1:
                        Campo[r, c] = TipoCasella.GonfiaAMeta;
                        break;
                    case 2:
                        Campo[r, c] = TipoCasella.InProcintoDiEsplodere;
                        break;
                }

            }
            else
            {
                i--;
            }
        }
    }
}