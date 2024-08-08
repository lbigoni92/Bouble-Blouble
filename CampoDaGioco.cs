// See https://aka.ms/new-console-template for more information
internal class CampoDaGioco
{
    private const int Righe = 6;
    private const int Colonne = 5;
    /// <summary>
    /// Numero di mosse che si possono fare prima di perdere
    /// </summary>
    private int NumeroMosse;
    /// <summary>
    /// Campo da gioco che è costituito da 6*5 celle ti tipo TipoCasella
    /// </summary>
    private readonly TipoCasella[,] Campo = new TipoCasella[Righe, Colonne];
    public CampoDaGioco()
    {
        NumeroMosse = new Random().Next(5, 10);//ci genera un numero di mosse tra 5 e 10
        //al massimo abbiamo 30 caselle da inizializzare  e andiamo a inizializzare della 5  alle 15 caselle
        int nCelleDaInizializzare = new Random().Next(5, 15);//ci genera un numero casuale tra 5 e 15
        InizializzaCampoDaGioco(nCelleDaInizializzare);
        Console.WriteLine("Campo inizializzato con " + nCelleDaInizializzare + " caselle con la palla");

    }
    /// <summary>
    /// Restituisce il numero di mosse rimanenti
    /// </summary>
    /// <returns></returns>
    public int GetNumeroMosse()
    {
        return NumeroMosse;
    }

    /// <summary>
    /// Stampa il campo da gioco
    /// </summary>to do migliorare la grafica
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
    /// sia passa le coordinate di una cella e si verifica se è una bomba e nel caso gli si aumenta il livello fino a farla esplodere
    /// </summary>
    /// <param name="inpRiga"></param>
    /// <param name="inpColonna"></param>
    /// <returns>ritorna false se si termina il numero di mosse, true se abbiamo fatto la giocata, se i valori non sono validi si restituisce un eccezione</returns>
    /// <exception cref="Exception">se i parametri inpRiga o inpColonna non sono numerici si lancia un eccezione</exception>
    public bool AzionaBolla(string inpRiga, string inpColonna)
    {
        int riga = -1;
        int colonna = -1;
        if (!int.TryParse(inpRiga, out riga) || !int.TryParse(inpColonna, out colonna))
        {
            //errore nel caso non si riesca a convertire le coordinate
            throw new Exception("Errore nella conversione delle coordinate");
        }
        AvanzamentoDiStato(riga, colonna);
        GestioneEsplosione();
        //to do gestire i varic casi qua dentro a seconda della tipologia di casella
        // Console.WriteLine("Hai toccato una bomba in posizione " + riga + "," + colonna);
        //abbiamo fatto la mossa e si va a 0, quindi finiamo le Vite a nostra disposizione
        NumeroMosse--;
        if (NumeroMosse == 0)
        {
            Console.WriteLine("Hai perso, hai superato il numero massimo di Mosse a tua disposizione");
            return false;
        }
        return true;
    }


    /// <summary>
    /// Se si seleziona una bolla in procinto di esplodere questa esploderà e 
    /// i) scompare dalla griglia, 
    /// ii) propaga l'esplosione nelle direzioni verticali e orizzontali
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    private void GestioneEsplosione()
    {//un esplosione può provocare oltre che l'avanzamento di stato anche l'innesco di ulteriori bombe questo fa si che si debba usare una funzione ricorsiva

        bool esplosione = false;//parto dal presupposto che non ci siano esplosioni
        for (int r = 0; r < Righe; r++)
            for (int c = 0; c < Colonne; c++)
                if (Campo[r, c] == TipoCasella.Esplosa)// se trovo una cella esplosa
                {
                    esplosione= true;//me lo segno
                    Campo[r, c] = TipoCasella.Vuota;//la imposto a vuota
                    PropagazioneEsplosione(r, c);//propago l'esplosione
                }
        if (esplosione)//se ho propagato un'esplosione posso avere ulteriori esplosioni
            GestioneEsplosione();//quindi riparto con il controllo
    }
    /// <summary>
    /// Gestisco le propagazioni nelle 4 direzioni
    /// </summary>
    /// <param name="rEsplosione"></param>
    /// <param name="cEsplosione"></param>
    private void PropagazioneEsplosione(int rEsplosione, int cEsplosione)
    {
        //propagazione verso l'alto
        for (int r = rEsplosione; r >= 0; r--)
            if (Campo[r, cEsplosione] != TipoCasella.Vuota)
            {
                AvanzamentoDiStato(r, cEsplosione);
                break;//dopo aver avanzato di stato non posso più propagare l'esplosione
            }
        //propagazione verso il basso
        for (int r = rEsplosione; r < Righe; r++)
            if (Campo[r, cEsplosione] != TipoCasella.Vuota)
            {
                AvanzamentoDiStato(r, cEsplosione);
                break;//dopo aver avanzato di stato non posso più propagare l'esplosione
            }
        //propagazione verso sinistra
        for (int c = cEsplosione; c >= 0; c--)
            if (Campo[rEsplosione, c] != TipoCasella.Vuota)
            {
                AvanzamentoDiStato(rEsplosione, c);
                break;//dopo aver avanzato di stato non posso più propagare l'esplosione
            }
        //propagazione verso destra
        for (int c = cEsplosione; c < Colonne; c++)
            if (Campo[rEsplosione, c] != TipoCasella.Vuota)
            {
                AvanzamentoDiStato(rEsplosione, c);
                break;//dopo aver avanzato di stato non posso più propagare l'esplosione
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

    /// <summary>
    /// Qua andiamo a gestire l'avanzamento di stato di una casella
    /// </summary>
    /// <param name="riga"></param>
    /// <param name="colonna"></param>
    private void AvanzamentoDiStato(int riga, int colonna)
    {
        switch (Campo[riga, colonna])
        {
            case TipoCasella.Vuota:
                //  Console.WriteLine("Hai toccato una casella Vuota");
                break;
            case TipoCasella.Sgonfia:
                Campo[riga, colonna] = TipoCasella.GonfiaAMeta;
                //  Console.WriteLine("Hai toccato una casella 'Sgonfia' è diventata 'Gonfia a Metà'");
                break;
            case TipoCasella.GonfiaAMeta:
                Campo[riga, colonna] = TipoCasella.InProcintoDiEsplodere;
                // Console.WriteLine("Hai toccato una casella 'Gonfia a Metà' è diventata 'In Procinto di Esplodere'");
                break;
            case TipoCasella.InProcintoDiEsplodere:
                Campo[riga, colonna] = TipoCasella.Esplosa;
                // Console.WriteLine("Hai toccato una casella Vuota");
                break;
        }
    }
    /// <summary>
    /// Partendo dalla griglia iniziale, simulare l'esplosione di ogni bolla a partire da quelle in procinto di esplodere, fino a calare di stato. 
    /// vedere quante mosse ci vogliono
    /// </summary>
    /// <returns></returns>
    private int NumeroMinimoDiMosse()
    {
        int contaNMosse = 0;
        CercaEdEsplodi(contaNMosse);
        return 0;
    }
    //nota se si fa questa simulazione non si deve lavorare sulla griglia originale ma su una copia sennò si modifica la griglia originale e non si può più fare la simulazione
    private void CercaEdEsplodi(int contaNMosse)
    {
        for (int r = 0; r < Righe; r++)
            for (int c = 0; c < Colonne; c++)
                if (Campo[r, c] == TipoCasella.InProcintoDiEsplodere)
                {
                    Campo[r, c] = TipoCasella.Esplosa;
                    contaNMosse++;
                    PropagazioneEsplosione(r, c);
                    CercaEdEsplodi(contaNMosse);
                }
    }

    //to do
    //1) migliorare la grafica
    //2) gestire la fine del gioco
    //3) gestire il numero di mosse minime a finché si possa vincere


    //2)creare una funzione che verifica se ci sono solamente caselle vuote e attaccare quella funzione nella funzione AzionaBolla prima di fare il decremento di mosse  e far restituire un bool alla funzione
}