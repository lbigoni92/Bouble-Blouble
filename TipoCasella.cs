// See https://aka.ms/new-console-template for more information

/// <summary>
/// Ogni TipoCasella rappresenta lo stato di una cella del campo da gioco.
/// </summary>
internal enum TipoCasella
{
    Esplosa = -1,
    Vuota=0,
    Sgonfia = 1,
    GonfiaAMeta = 2,
    InProcintoDiEsplodere =3
}