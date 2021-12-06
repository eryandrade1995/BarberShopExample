using static BarberShopExample.GlobalVariable;
using System.Threading;
using System;
namespace BarberShopExample
{
    class Caixa
    {
        ////////////////////////////////Cashier//////////////////////////////////////////////////
        public void CaixaFunc()
        {
            int cCliente;
            while (true)
            {
                pagamento.WaitOne();
                coord.WaitOne();
                ActPag();
                coord.Release();
                mutex3.WaitOne();
                cCliente = queue2.Dequeue();
                mutex3.Release();
                receipt[cCliente].Release();
            }
        }
        /////////////////////////////////AcceptPay///////////////////////////////////////////////
        private void ActPag()
        {
            Thread.Sleep(1200);
        }
    }
}
