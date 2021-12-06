using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using static BarberShopExample.GlobalVariable;
namespace BarberShopExample
{
    public partial class barberForm : Form
    {
        public barberForm()
        {
            InitializeComponent();
        }
        public Point GetSofaLocation(int i)
        {
            Point pf = new Point();
            switch (i)
            {
                case 1:
                    pf = sofa1Label.Location;
                    break;
                case 2:
                    pf = sofa2Label.Location;
                    break;
                case 3:
                    pf = sofa3Label.Location;
                    break;
                case 4:
                    pf = sofa4Label.Location;
                    break;
                default:
                    break;
            }
            return pf;
        }
        public PointF GetBarberChairLocation(int i)
        {
            PointF pf = new PointF();
            switch (i)
            {
                case 1:
                    pf = barberChair1Label.Location;
                    break;
                case 2:
                    pf = barberChair2Label.Location;
                    break;
                case 3:
                    pf = barberChair3Label.Location;
                    break;
                default:
                    break;
            }
            return pf;
        }
        private void exitButton_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
        private void startButton_Click(object sender, EventArgs e)
        {
            GlobalVariable glv = new GlobalVariable();
            Clientes[] myCustomer = new Clientes[25];
            Barbeiro[] myBarber = new Barbeiro[3];
            Caixa myCashier = new Caixa();
            for (int i = 0; i < 25; i++)
                myCustomer[i] = new Clientes(this);
            for (int i = 0; i < 3; i++)
                myBarber[i] = new Barbeiro();
            Thread[] customerThreads = new Thread[25];
            Thread[] barberThreads = new Thread[3];
            Thread cashierThread = new Thread(new ThreadStart(myCashier.CaixaFunc));
            Thread[] firstRunThread = new Thread[GlobalVariable.numeroDeClientes];
            Objects[] myObjects = new Objects[GlobalVariable.numeroDeClientes];
            for (int i = 0; i < GlobalVariable.numeroDeClientes; i++)
            {
                myObjects[i] = new Objects(this);
                firstRunThread[i] = new Thread(new ThreadStart(myObjects[i].Run));
                firstRunThread[i].Name = (i + 1).ToString();
                customerThreads[i] = new Thread(new ThreadStart(myCustomer[i].ClientesFunc));
                customerThreads[i].Name = (i + 1).ToString();
            }
            for (int i = 0; i < 3; i++)
                barberThreads[i] = new Thread(new ThreadStart(myBarber[i].BarberFunction));
            for (int i = 0; i < GlobalVariable.numeroDeClientes; i++)
            {
                firstRunThread[i].Start();
                Thread.Sleep(1);
            }
            for (int i = 0; i < GlobalVariable.numeroDeClientes; i++)
            {
                customerThreads[i].Start();
                Thread.Sleep(800);
                if (i < 3)
                {
                    barberThreads[i].Start();
                    if (i == 0)
                        cashierThread.Start();
                }
            }
        }
    }
}