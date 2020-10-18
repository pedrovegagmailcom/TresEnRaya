using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
        public int nivel;
        

        public NodoJuego(NodoJuego nPadre)
        {
            nodoPadre = nPadre;
            nodos = new List<NodoJuego>();
        }
    }

    public class MainWindowViewModel : BindableBase
    {
        NodoJuego mainNodo = new NodoJuego(null);
        NodoJuego currentNodo;
        int[] estado = new int[10]  {-1, 0, 0, 0, 0, 0, 0, 0, 0, 0  };
        public int[][] estadosGanadores = new int[][] { 
            new int[]   { 1, 2, 3 },
            new int[]   { 4, 5, 6 },
            new int[]   { 7, 8, 9 },
            new int[]   { 1, 5, 9 },
            new int[]   { 3, 5, 7 },
            new int[]   { 1, 4, 7 },
            new int[]   { 2, 5, 8 },
            new int[]   { 3, 6, 9 } };
        int _numeroNodos = 0;
        int Turno = 0;
        int turno = 1;
        public int NumeroNodos { get { return _numeroNodos; } set { SetProperty(ref _numeroNodos, value); } }

        #region Celdas
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
        #endregion

        #region Botones
        public DelegateCommand C1Command { get; set; }
        public DelegateCommand C2Command { get; set; }
        public DelegateCommand C3Command { get; set; }
        public DelegateCommand C4Command { get; set; }
        public DelegateCommand C5Command { get; set; }
        public DelegateCommand C6Command { get; set; }
        public DelegateCommand C7Command { get; set; }
        public DelegateCommand C8Command { get; set; }
        public DelegateCommand C9Command { get; set; }

        
        private void ProcesarEstadoMaquina1()
        {
            ProcesarEstadoMaquina(1);
        }

        private void ProcesarEstadoMaquina2()
        {
            ProcesarEstadoMaquina(2);
        }
        private void ProcesarEstadoMaquina3()
        {
            ProcesarEstadoMaquina(3);
        }
        private void ProcesarEstadoMaquina4()
        {
            ProcesarEstadoMaquina(4);
        }
        private void ProcesarEstadoMaquina5()
        {
            ProcesarEstadoMaquina(5);
        }
        private void ProcesarEstadoMaquina6()
        {
            ProcesarEstadoMaquina(6);
        }
        private void ProcesarEstadoMaquina7()
        {
            ProcesarEstadoMaquina(7);
        }
        private void ProcesarEstadoMaquina8()
        {
            ProcesarEstadoMaquina(8);
        }
        private void ProcesarEstadoMaquina9()
        {
            ProcesarEstadoMaquina(9);
        } 
        #endregion


        private void ProcesarEstadoMaquina(int v)
        {
            int jugador = Turno % 2 == 0 ? 1 : 2;

            if (jugador == 1) // Humano
            {
                if (estado[v] == 0)
                {

                    estado[v] = jugador;
                    
                    Turno++;
                }
                else
                {
                    return;
                }
                if (Turno == 9) 
                {
                    AsignarCeldas(estado);
                    return;
                }
            }
            // IA
            {
                foreach (var nodo in currentNodo.nodos)
                {
                    var c = estado.SequenceEqual(nodo.estado);
                    if (c)
                    {
                        currentNodo = nodo.nodos.First();
                        estado = currentNodo.estado;
                        break;
                    }
                }
                Turno++;

            }

            AsignarCeldas(estado);

        }

        

        public MainWindowViewModel()
        {
            C1Command = new DelegateCommand(ProcesarEstadoMaquina1);
            C2Command = new DelegateCommand(ProcesarEstadoMaquina2);
            C3Command = new DelegateCommand(ProcesarEstadoMaquina3);
            C4Command = new DelegateCommand(ProcesarEstadoMaquina4);
            C5Command = new DelegateCommand(ProcesarEstadoMaquina5);
            C6Command = new DelegateCommand(ProcesarEstadoMaquina6);
            C7Command = new DelegateCommand(ProcesarEstadoMaquina7);
            C8Command = new DelegateCommand(ProcesarEstadoMaquina8);
            C9Command = new DelegateCommand(ProcesarEstadoMaquina9);

            
            mainNodo.nivel = 0;
            currentNodo = mainNodo;
            Task.Run(() => CrearArbolJugadas(mainNodo));
            
                        
        }


        private int CrearArbolJugadas(NodoJuego nodo)
        {
            int nJugadasNivel = 9 - nodo.nivel;
            int celda = 1;
            //Thread.Sleep(500);
            while (nJugadasNivel > 0)
            {
                if (nodo.estado[celda] == 0)
                {
                                        
                    NodoJuego nodoHijo = new NodoJuego(nodo);
                    nodo.estado.CopyTo(nodoHijo.estado, 0);
                    nodoHijo.nivel = nodo.nivel + 1;
                    int jugador = nodo.nivel % 2 == 0 ? 1 : 2;
                    nodoHijo.estado[celda] = jugador;
                    int res = EvaluarEstado(nodoHijo.estado, jugador);
                    if (res == 3)
                    {
                        //return 1;
                    }
                    //AsignarCeldas(nodoHijo.estado);
                    nodo.nodos.Add(nodoHijo);
                    NumeroNodos++;
                   
                    nJugadasNivel--;
                    CrearArbolJugadas(nodoHijo);
                    
                }
                celda++;

            }
            


            return 0;
        }

        private int EvaluarEstado(int[] estado, int jugador)
        {
            int[] estado_jugador = new int[5];
            int index = 0;
            for (int i = 1; i < 10; i++)
            {
                
                if (estado[i] == jugador)
                {
                    estado_jugador[index++] = i;
                    if (index == 5)
                    {
                        break;
                    }
                }
            }

            var res = 0;
            for (int i = 0; i < 8; i++)
            {
                res = estado_jugador.Intersect(estadosGanadores[i]).Count();
                if (res == 3)
                {
                    break;
                }
            }
            
            return res;

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
