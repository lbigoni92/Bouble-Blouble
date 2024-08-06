// See https://aka.ms/new-console-template for more information
CampoDaGioco cdg = new CampoDaGioco();



do
{
    try//se c'è un errore nel codice all'interno del blocco try allora viene eseguito il blocco catch
    {

        Console.WriteLine("Menu:");
        Console.WriteLine("1. Stampa campo");
        Console.WriteLine("2. Tocca una bomba");
        Console.WriteLine("3. Fine");
        Console.WriteLine("Inserisci la scelta:");
        string inp = Console.ReadLine() ?? "";
        int scelta = -1;
        //il tryparse restituisce true se la conversione è andata a buon fine altrimenti false e popola la variabile scelta con il valore corretto
        if (!int.TryParse(inp, out scelta))
        {
            //errore nel caso non si riesca la scelta
            throw new Exception("Errore scelta non valida");
        }
        switch (scelta)
        {
            case 1:
                cdg.StampaCampo();
                break;
            case 2:
                //??"" significa che se il valore restituito dalla Console.ReadLine() è null allora assegna una stringa vuota e quindi il controlo delle posizioni da errore
                Console.WriteLine("Inserisci la riga:");
                string inpRiga = Console.ReadLine() ?? "";
                Console.WriteLine("Inserisci la colonna:");
                string inpColonna = Console.ReadLine() ?? "";
                bool ris = cdg.AzionaBolla(inpRiga, inpColonna);
                break;
            case 3:
                return;
            default:
                Console.WriteLine("Scelta non valida");
                break;
        }
    }
    //catch cattura l'eccezione e la memorizza nella variabile ex
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);//si stampa il messaggio dell'eccezione
    }
} while (true);