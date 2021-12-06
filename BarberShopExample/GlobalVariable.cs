using System.Collections.Generic;
using System.Threading;
using System.Drawing;
namespace BarberShopExample
{
    /// <summary>
    /// Classe contendo as variavés utilizadas na aplicação
    /// </summary>
    public class GlobalVariable
    {
        public static Semaphore maxCap = new Semaphore(20, 20); //capidade máxima de clientes fora da barbearia
        public static Semaphore sofa = new Semaphore(4, 4);
        public static Semaphore barbCadeira = new Semaphore(3, 3);
        public static Semaphore coord = new Semaphore(3, 3);
        public static Semaphore mutex1 = new Semaphore(1, 1);
        public static Semaphore mutex2 = new Semaphore(1, 1);
        public static Semaphore mutex3 = new Semaphore(1, 1);
        public static Semaphore custoReady = new Semaphore(0, 25);
        public static Semaphore pagamento = new Semaphore(0, 25);
        public static Semaphore[] finished = new Semaphore[25];
        public static Semaphore[] sairBCadeira = new Semaphore[25];
        public static Semaphore[] receipt = new Semaphore[25];
        public static int numeroDeClientes = 25;
        public static Queue<int> queue1 = new Queue<int>(25);//classe queue representa entra e saída de objetos
        public static Queue<int> queue2 = new Queue<int>(25);
        public static Queue<int> sofaQueue = new Queue<int>(4);
        public static Queue<int> barbCadeiraQueue = new Queue<int>(3);
        static int x = 15, y = 15;
        static Semaphore getPlaceSemaphore = new Semaphore(1, 1);
        public static PointF GetPlace()
        {
            getPlaceSemaphore.WaitOne();
            PointF pf = new PointF();
            if (y >= 340)
            {
                x += 30;
                y = 15;
            }
            pf.X = x;
            pf.Y = y;
            y += 30;
            if (x == 135)
                if (y > 180)
                {
                    x = 15;
                    y = 15;
                }
            getPlaceSemaphore.Release();
            return pf;
        }
        public GlobalVariable()
        {
            for (int i = 0; i < 25; i++)
            {
                finished[i] = new Semaphore(0, 1);
                sairBCadeira[i] = new Semaphore(0, 1);
                receipt[i] = new Semaphore(0, 1);
            }
            for (int i = 1; i <= 4; i++)
                sofaQueue.Enqueue(i);
            for (int i = 1; i <= 3; i++)
                barbCadeiraQueue.Enqueue(i);
        }
    }
}