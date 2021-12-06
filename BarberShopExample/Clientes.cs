using System.Threading;
using System.Drawing;
using System.Windows.Forms;
using System;
using static BarberShopExample.GlobalVariable;
using static BarberShopExample.Objects;

namespace BarberShopExample
{
    class Clientes
    {
        //------------------------------------------------------------------------------------------//
        static Semaphore entrarShop = new Semaphore(1, 1);
        static Semaphore sofaSemaphore = new Semaphore(1, 1);
        static Semaphore bCadeiraSemaphore = new Semaphore(1, 1);
        static Semaphore pagSemaphore = new Semaphore(1, 1);
        static int caixaX = 745, entrarShopY = 370, entrarShopX = 770;
        int tempEnterShopX, tempEnterShopY, tempCaixaX, caixaY = 120, barbCadeiraNum, sofaNum;
        readonly int width = 20, height = 20;
        static int count = 0;
        int leftBeforeSitOnSofa, upBeforeExitShop;
        PointF tempPF, pfName, sofaCoordinate, barberChairCoordinate;
        Graphics g;
        Form f1;
        barberForm bf;
        Brush formColor, objectColor, objectName, respond;
        //-----------------------------------------FUNÇÕES----------------------------------------//
        public Clientes(Form f)
        {
            bf = new barberForm();
            f1 = f;
            g = f1.CreateGraphics();
            objectName = new SolidBrush(Color.Black);
            formColor = new SolidBrush(f1.BackColor);
            objectColor = new SolidBrush(Color.LightSteelBlue);
            respond = new SolidBrush(Color.LimeGreen);
        }
        //-----------------------------------------CLIENTES----------------------------------------//
        public void ClientesFunc()
        {
            int custnr;
            maxCap.WaitOne();
            Entrar();
            mutex1.WaitOne();
            custnr = count;
            count++;
            mutex1.Release();
            sofa.WaitOne();
            SitOnSofa();
            barbCadeira.WaitOne();
            LevantarDoSofa();
            sofa.Release();
            SentarNaCadeira();
            mutex2.WaitOne();
            queue1.Enqueue(custnr);
            custoReady.Release();
            mutex2.Release();
            finished[custnr].WaitOne();
            SairDaCadeira();
            sairBCadeira[custnr].Release();
            Pagar();
            mutex3.WaitOne();
            queue2.Enqueue(custnr);
            mutex3.Release();
            pagamento.Release();
            receipt[custnr].WaitOne();
            Sair();
            maxCap.Release();
        }
        //-----------------------------------------ENTRAR----------------------------------------//
        public void Entrar()
        {
            entrarShop.WaitOne();
            if (Convert.ToInt32(Thread.CurrentThread.Name) > 1)
                entrarShopX -= 60;
            if (entrarShopX <= 280)
            {
                entrarShopX = 770;
                entrarShopY += 30;
            }
            if (entrarShopY > 430)
                entrarShopY = 370;
            tempEnterShopX = entrarShopX;
            tempEnterShopY = entrarShopY;
            tempPF = myCoordinate[Convert.ToInt32(Thread.CurrentThread.Name)];
            entrarShop.Release();
            g.FillEllipse(formColor, tempPF.X, tempPF.Y, width, height);
            tempPF.X = 200;
            g.FillEllipse(objectColor, tempPF.X, tempPF.Y, width, height);
            pfName = tempPF;
            pfName.X += 2;
            pfName.Y += 2;
            g.DrawString(Thread.CurrentThread.Name, f1.Font, objectName, pfName);
            while (tempPF.Y < 370)
            {
                Thread.Sleep(3);
                g.FillEllipse(formColor, tempPF.X, tempPF.Y, width, height);
                tempPF.Y += 1;
                g.FillEllipse(objectColor, tempPF.X, tempPF.Y, width, height);
                pfName = tempPF;
                pfName.X += 2;
                pfName.Y += 2;
                g.DrawString(Thread.CurrentThread.Name, f1.Font, objectName, pfName);
            }
            while (tempPF.X < tempEnterShopX)
            {
                Thread.Sleep(3);
                g.FillEllipse(formColor, tempPF.X, tempPF.Y, width, height);
                tempPF.X += 1;
                g.FillEllipse(objectColor, tempPF.X, tempPF.Y, width, height);
                pfName = tempPF;
                pfName.X += 2;
                pfName.Y += 2;
                g.DrawString(Thread.CurrentThread.Name, f1.Font, objectName, pfName);
                if (tempPF.X > 260)
                    while (tempPF.Y < tempEnterShopY)
                    {
                        Thread.Sleep(3);
                        g.FillEllipse(formColor, tempPF.X, tempPF.Y, width, height);
                        tempPF.Y += 1;
                        g.FillEllipse(objectColor, tempPF.X, tempPF.Y, width, height);
                        pfName = tempPF;
                        pfName.X += 2;
                        pfName.Y += 2;
                        g.DrawString(Thread.CurrentThread.Name, f1.Font, objectName, pfName);
                    }
            }
        }
        //-----------------------------------------SENTAR----------------------------------------//
        public void SitOnSofa()
        {
            sofaSemaphore.WaitOne();
            sofaNum = sofaQueue.Dequeue();
            sofaSemaphore.Release();
            sofaCoordinate = (PointF)bf.GetSofaLocation(sofaNum);
            leftBeforeSitOnSofa = (int)tempPF.X - 25;
            while (tempPF.X > leftBeforeSitOnSofa)
            {
                Thread.Sleep(3);
                g.FillEllipse(formColor, tempPF.X, tempPF.Y, width, height);
                tempPF.X -= 1;
                g.FillEllipse(objectColor, tempPF.X, tempPF.Y, width, height);
                pfName = tempPF;
                pfName.X += 2;
                pfName.Y += 2;
                g.DrawString(Thread.CurrentThread.Name, f1.Font, objectName, pfName);
            }
            while (tempPF.Y > 336)
            {
                Thread.Sleep(3);
                g.FillEllipse(formColor, tempPF.X, tempPF.Y, width, height);
                tempPF.Y -= 1;
                g.FillEllipse(objectColor, tempPF.X, tempPF.Y, width, height);
                pfName = tempPF;
                pfName.X += 2;
                pfName.Y += 2;
                g.DrawString(Thread.CurrentThread.Name, f1.Font, objectName, pfName);
            }
            while (tempPF.X > 520)
            {
                Thread.Sleep(3);
                g.FillEllipse(formColor, tempPF.X, tempPF.Y, width, height);
                tempPF.X -= 1;
                g.FillEllipse(objectColor, tempPF.X, tempPF.Y, width, height);
                pfName = tempPF;
                pfName.X += 2;
                pfName.Y += 2;
                g.DrawString(Thread.CurrentThread.Name, f1.Font, objectName, pfName);
            }
            while (tempPF.X < 520)
            {
                Thread.Sleep(3);
                g.FillEllipse(formColor, tempPF.X, tempPF.Y, width, height);
                tempPF.X += 1;
                g.FillEllipse(objectColor, tempPF.X, tempPF.Y, width, height);
                pfName = tempPF;
                pfName.X += 2;
                pfName.Y += 2;
                g.DrawString(Thread.CurrentThread.Name, f1.Font, objectName, pfName);
            }
            while (tempPF.Y > 280)
            {
                Thread.Sleep(3);
                g.FillEllipse(formColor, tempPF.X, tempPF.Y, width, height);
                tempPF.Y -= 1;
                g.FillEllipse(objectColor, tempPF.X, tempPF.Y, width, height);
                pfName = tempPF;
                pfName.X += 2;
                pfName.Y += 2;
                g.DrawString(Thread.CurrentThread.Name, f1.Font, objectName, pfName);
            }
            while (tempPF.X > sofaCoordinate.X + 15)
            {
                Thread.Sleep(3);
                g.FillEllipse(formColor, tempPF.X, tempPF.Y, width, height);
                tempPF.X -= 1;
                g.FillEllipse(objectColor, tempPF.X, tempPF.Y, width, height);
                pfName = tempPF;
                pfName.X += 2;
                pfName.Y += 2;
                g.DrawString(Thread.CurrentThread.Name, f1.Font, objectName, pfName);
            }
            while (tempPF.X < sofaCoordinate.X + 15)
            {
                Thread.Sleep(3);
                g.FillEllipse(formColor, tempPF.X, tempPF.Y, width, height);
                tempPF.X += 1;
                g.FillEllipse(objectColor, tempPF.X, tempPF.Y, width, height);
                pfName = tempPF;
                pfName.X += 2;
                pfName.Y += 2;
                g.DrawString(Thread.CurrentThread.Name, f1.Font, objectName, pfName);
            }
            while (tempPF.Y > sofaCoordinate.Y - 20)
            {
                Thread.Sleep(3);
                g.FillEllipse(formColor, tempPF.X, tempPF.Y, width, height);
                tempPF.Y -= 1;
                g.FillEllipse(objectColor, tempPF.X, tempPF.Y, width, height);
                pfName = tempPF;
                pfName.X += 2;
                pfName.Y += 2;
                g.DrawString(Thread.CurrentThread.Name, f1.Font, objectName, pfName);
            }
        }
        //-----------------------------------------LEVANTAR----------------------------------------//
        public void LevantarDoSofa()
        {
            sofaSemaphore.WaitOne();
            sofaQueue.Enqueue(sofaNum);
            sofaSemaphore.Release();
            g.FillEllipse(formColor, tempPF.X, tempPF.Y, width, height);
            tempPF.Y -= 1;
            g.FillEllipse(respond, tempPF.X, tempPF.Y, width, height);
            pfName = tempPF;
            pfName.X += 2;
            pfName.Y += 2;
            g.DrawString(Thread.CurrentThread.Name, f1.Font, objectName, pfName);
            Thread.Sleep(800);
        }
        //-----------------------------------------ATENDIMENTO----------------------------------------//
        public void SentarNaCadeira()
        {
            bCadeiraSemaphore.WaitOne();
            barbCadeiraNum = barbCadeiraQueue.Dequeue();
            bCadeiraSemaphore.Release();
            barberChairCoordinate = bf.GetBarberChairLocation(barbCadeiraNum);
            while (tempPF.Y > 156)
            {
                Thread.Sleep(3);
                g.FillEllipse(formColor, tempPF.X, tempPF.Y, width, height);
                tempPF.Y -= 1;
                g.FillEllipse(objectColor, tempPF.X, tempPF.Y, width, height);
                pfName = tempPF;
                pfName.X += 2;
                pfName.Y += 2;
                g.DrawString(Thread.CurrentThread.Name, f1.Font, objectName, pfName);
            }
            while (tempPF.X > barberChairCoordinate.X + 15)
            {
                Thread.Sleep(3);
                g.FillEllipse(formColor, tempPF.X, tempPF.Y, width, height);
                tempPF.X -= 1;
                g.FillEllipse(objectColor, tempPF.X, tempPF.Y, width, height);
                pfName = tempPF;
                pfName.X += 2;
                pfName.Y += 2;
                g.DrawString(Thread.CurrentThread.Name, f1.Font, objectName, pfName);
            }
            while (tempPF.X < barberChairCoordinate.X + 15)
            {
                Thread.Sleep(3);
                g.FillEllipse(formColor, tempPF.X, tempPF.Y, width, height);
                tempPF.X += 1;
                g.FillEllipse(objectColor, tempPF.X, tempPF.Y, width, height);
                pfName = tempPF;
                pfName.X += 2;
                pfName.Y += 2;
                g.DrawString(Thread.CurrentThread.Name, f1.Font, objectName, pfName);
            }
            while (tempPF.Y > barberChairCoordinate.Y - 20)
            {
                Thread.Sleep(3);
                g.FillEllipse(formColor, tempPF.X, tempPF.Y, width, height);
                tempPF.Y -= 1;
                g.FillEllipse(objectColor, tempPF.X, tempPF.Y, width, height);
                pfName = tempPF;
                pfName.X += 2;
                pfName.Y += 2;
                g.DrawString(Thread.CurrentThread.Name, f1.Font, objectName, pfName);
            }
        }
        //--------------------------------------SAIR-DO-ATENDIMENTO----------------------------------//
        public void SairDaCadeira()
        {
            bCadeiraSemaphore.WaitOne();
            barbCadeiraQueue.Enqueue(barbCadeiraNum);
            bCadeiraSemaphore.Release();
            g.FillEllipse(formColor, tempPF.X, tempPF.Y, width, height);
            tempPF.Y -= 1;
            g.FillEllipse(respond, tempPF.X, tempPF.Y, width, height);
            pfName = tempPF;
            pfName.X += 2;
            pfName.Y += 2;
            g.DrawString(Thread.CurrentThread.Name, f1.Font, objectName, pfName);
            Thread.Sleep(800);
        }
        //-----------------------------------------PAGAR----------------------------------------//
        public void Pagar()
        {
            pagSemaphore.WaitOne();
            if (Convert.ToInt32(Thread.CurrentThread.Name) > 1)
                caixaX -= 20;
            if (caixaX <= 660)
                caixaX = 745;
            tempCaixaX = caixaX;
            pagSemaphore.Release();
            while (tempPF.X < barberChairCoordinate.X + 50)
            {
                Thread.Sleep(8);
                g.FillEllipse(formColor, tempPF.X, tempPF.Y, width, height);
                tempPF.X += 1;
                g.FillEllipse(objectColor, tempPF.X, tempPF.Y, width, height);
                pfName = tempPF;
                pfName.X += 2;
                pfName.Y += 2;
                g.DrawString(Thread.CurrentThread.Name, f1.Font, objectName, pfName);
            }
            while (tempPF.Y < barberChairCoordinate.Y + 60)
            {
                Thread.Sleep(8);
                g.FillEllipse(formColor, tempPF.X, tempPF.Y, width, height);
                tempPF.Y += 1;
                g.FillEllipse(objectColor, tempPF.X, tempPF.Y, width, height);
                pfName = tempPF;
                pfName.X += 2;
                pfName.Y += 2;
                g.DrawString(Thread.CurrentThread.Name, f1.Font, objectName, pfName);
            }
            while (tempPF.X < tempCaixaX)
            {
                Thread.Sleep(8);
                g.FillEllipse(formColor, tempPF.X, tempPF.Y, width, height);
                tempPF.X += 1;
                g.FillEllipse(objectColor, tempPF.X, tempPF.Y, width, height);
                pfName = tempPF;
                pfName.X += 2;
                pfName.Y += 2;
                g.DrawString(Thread.CurrentThread.Name, f1.Font, objectName, pfName);
            }
            while (tempPF.Y < caixaY)
            {
                Thread.Sleep(8);
                g.FillEllipse(formColor, tempPF.X, tempPF.Y, width, height);
                tempPF.Y += 1;
                g.FillEllipse(objectColor, tempPF.X, tempPF.Y, width, height);
                pfName = tempPF;
                pfName.X += 2;
                pfName.Y += 2;
                g.DrawString(Thread.CurrentThread.Name, f1.Font, objectName, pfName);
            }
        }
        //-----------------------------------------SAIR----------------------------------------//
        public void Sair()
        {
            g.FillEllipse(formColor, tempPF.X, tempPF.Y, width, height);
            tempPF.Y -= 1;
            g.FillEllipse(respond, tempPF.X, tempPF.Y, width, height);
            pfName = tempPF;
            pfName.X += 2;
            pfName.Y += 2;
            g.DrawString(Thread.CurrentThread.Name, f1.Font, objectName, pfName);
            Thread.Sleep(800);
            upBeforeExitShop = (int)tempPF.Y - 30;
            while (tempPF.Y > upBeforeExitShop)
            {
                Thread.Sleep(3);
                g.FillEllipse(formColor, tempPF.X, tempPF.Y, width, height);
                tempPF.Y -= 1;
                g.FillEllipse(objectColor, tempPF.X, tempPF.Y, width, height);
                pfName = tempPF;
                pfName.X += 2;
                pfName.Y += 2;
                g.DrawString(Thread.CurrentThread.Name, f1.Font, objectName, pfName);
            }
            while (tempPF.X <= 820)
            {
                Thread.Sleep(3);
                g.FillEllipse(formColor, tempPF.X, tempPF.Y, width, height);
                tempPF.X += 1;
                g.FillEllipse(objectColor, tempPF.X, tempPF.Y, width, height);
                pfName = tempPF;
                pfName.X += 2;
                pfName.Y += 2;
                g.DrawString(Thread.CurrentThread.Name, f1.Font, objectName, pfName);
            }
            g.FillEllipse(formColor, tempPF.X, tempPF.Y, width, height);
        }
        //---------------------------------------------------------------------------------------//
    }
}