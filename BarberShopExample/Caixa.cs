using static BarberShopExample.GlobalVariable;
using System.Threading;
using System;
namespace BarberShopExample
{
    class Caixa
    {
        //---------------------------------------CAIXA-----------------------------------------//
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
                cCliente = queue2.Dequeue();//remove e retorna o primeiro objeto da lista
                mutex3.Release();
                receipt[cCliente].Release();//adicionado na claase cCliente o retorno da queue2
            }
        }
        //---------------------------------------TEMPOPAGAR-----------------------------------------//
        private void ActPag()
        {
            Thread.Sleep(1200);
        }
    }
}
