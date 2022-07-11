using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCBReWrited
{
    class MultiArmedBanditBern
    {
        Random rnd;
        //Инициализация в конструкторе
        double handleCount;
        int[] handleChoise;
        double[] handleWin;

        int managmentHorizont;
        double sqrtManagmentHorizont;

        double dispersion;
        double sqrtDispersion;

        double probability;
        
        //Инициализарованы изначально
        
        public double[] dSmall = { 1d, -1d };
        
        int packageSize = 100;

        public double HandleCount { get => handleCount; set => handleCount = value; }
        public int[] HandeChoise { get => handleChoise; set => handleChoise = value; }
        public double[] HandleWin { get => handleWin; set => handleWin = value; }

        public int ManagmentHorizont { get => managmentHorizont; set => managmentHorizont = value; }
        public double SqrtManagmentHorizont { get => sqrtManagmentHorizont; set => sqrtManagmentHorizont = value; }

        public double Dispersion { get => dispersion; set => dispersion = value; }
        public double SqrtDispersion { get => sqrtDispersion; set => sqrtDispersion = value; }

        public int PackageSize { get => packageSize; set => packageSize = value; }
        public double Probability { get => probability; set => probability = value; }

        public MultiArmedBanditBern(int count, int horizont ,double disp,double prob)//Инициализация основного автомата
        {
            rnd = new Random();

            handleCount = count;
            handleChoise = new int[count];
            handleWin = new double[count];

            managmentHorizont = horizont;
            SqrtManagmentHorizont = Math.Sqrt(horizont);

            dispersion = disp;
            SqrtDispersion = Math.Sqrt(dispersion);
            
            Probability = prob;
        }

        public void ReturnWin(int index, double d = 0)
        {
            handleChoise[index]++;
            double di=0;
            if (index == 0)
                di += d;
            else
                di -= d;
            handleWin[index] += BernRandom(index, di);
        }

        private double BernRandom(int index, double d)
        {
            double prob = Probability + d * SqrtDispersion / SqrtManagmentHorizont;
            int sum = 0;
            for (int i = 1; i <= packageSize; i++)
                if (rnd.NextDouble() < prob)
                    sum++;
            return sum;
        }

        public void ClearData()
        {
            for (int i = 0; i < handleCount; i++)
            {
                handleChoise[i] = 0;
                handleWin[i] = 0;
            }
        }
        
       
        
    }
}
