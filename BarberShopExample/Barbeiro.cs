using System.Threading;
using static BarberShopExample.GlobalVariable;

namespace BarberShopExample
{
    class Barbeiro
    {
        //                                    BARBEIRO                               \\
        public void BarberFunction()
        {
            int bCliente;
            while (true)
            {
                custoReady.WaitOne();
                mutex2.WaitOne();
                bCliente = queue1.Dequeue();
                mutex2.Release();
                coord.WaitOne();
                CortarCabelo();
                coord.Release();
                finished[bCliente].Release();
                sairBCadeira[bCliente].WaitOne();
                barbCadeira.Release();
            }
        }
        //                                    CORTAR CABELO                               \\
        private void CortarCabelo()
        {
            Thread.Sleep(7000);
        }
    }
}
