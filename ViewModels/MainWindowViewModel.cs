using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TresEnRaya.ViewModels
{

    class NodoJuego
    {
        public NodoJuego nodoPadre = null;
        public List<NodoJuego> nodos;
        public int[] estado = new int[10] { -1, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public int[,] estadosGanadores = new int[8,3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 }, { 1, 5, 9 }, { 3, 5, 7 }, { 1, 4, 7 }, { 2, 5, 6 }, { 3, 6, 9 } };
        public int nivel;

        public NodoJuego(NodoJuego nPadre)
        {
            nodoPadre = nPadre;
            nodos = new List<NodoJuego>();
        }
    }

    public class MainWindowViewModel : BindableBase
    {
        int[] estado = new int[10]  {0, 1, 2, 0, 0, 2, 1, 0, 1, 0  };

        private string[] _celdas = new string[10];
        public string C1 { get { return _celdas[1]; } set { SetProperty(ref _celdas[1], value); } }
        public string C2 { get { return _celdas[2]; } set { SetProperty(ref _celdas[2], value); } }
        public string C3 { get { return _celdas[3]; } set { SetProperty(ref _celdas[3], value); } }
        public string C4 { get { return _celdas[4]; } set { SetProperty(ref _celdas[4], value); } }
        public string C5 { get { return _celdas[5]; } set { SetProperty(ref _celdas[5], value); } }
        public string C6 { get { return _celdas[6]; } set { SetProperty(ref _celdas[6], value); } }
        public string C7 { get { return _celdas[7]; } set { SetProperty(ref _celdas[7], value); } }
        public string C8 { get { return _celdas[8]; } set { SetProperty(ref _celdas[8], value); } }
        public string C9 { get { return _celdas[9]; } set { SetProperty(ref _celdas[9], value); } }


        int _numeroNodos = 0;
        public int NumeroNodos { get { return _numeroNodos; } set { SetProperty(ref _numeroNodos, value); } }

        public MainWindowViewModel()
        {
            NodoJuego mainNodo = new NodoJuego(null);
            mainNodo.nivel = 0;
            Task.Run(() => CrearArbolJugadas(mainNodo));
            //CrearArbolJugadas(mainNodo);
                        
        }


        private int CrearArbolJugadas(NodoJuego nodo)
        {
            int nJugadasNivel = 9 - nodo.nivel;
            int celda = 1;
            //Thread.Sleep(1);
            while (nJugadasNivel > 0)
            {
                if (nodo.estado[celda] == 0)
                {
                                        
                    NodoJuego nodoHijo = new NodoJuego(nodo);
                    nodo.estado.CopyTo(nodoHijo.estado, 0);
                    nodoHijo.nivel = nodo.nivel + 1;
                    nodoHijo.estado[celda] = nodo.nivel % 2 == 0 ? 1 : 2;
                    AsignarCeldas(nodoHijo.estado);
                    nodo.nodos.Add(nodoHijo);
                    NumeroNodos++;
                    nJugadasNivel--;
                    CrearArbolJugadas(nodoHijo);
                    
                }
                celda++;

            }
            


            return 0;
        }

        private void AsignarCeldas (int [] valores)
        {
            string GetValorCelda(int v)
            {

                if (v == 1) return "X";
                if (v == 2) return "O";
                return " ";

            }
            C1 = GetValorCelda(valores[1]);
            C2 = GetValorCelda(valores[2]);
            C3 = GetValorCelda(valores[3]);
            C4 = GetValorCelda(valores[4]);
            C5 = GetValorCelda(valores[5]);
            C6 = GetValorCelda(valores[6]);
            C7 = GetValorCelda(valores[7]);
            C8 = GetValorCelda(valores[8]);
            C9 = GetValorCelda(valores[9]);

        }

        
    }
}
