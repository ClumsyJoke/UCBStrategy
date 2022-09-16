using System;

namespace UCBReWrited
{
    class MultiArmedBanditBern
    {
        readonly Random rnd;
        Geterator gen;
        //Инициализация в конструкторе
        double handleCount;
        int[] handleChoise;
        double[] handleWin;

        int managmentHorizont;
        double sqrtManagmentHorizont;

        double dispersion;
        double sqrtDispersion;

        double probability;
        double sqrtDN;

        int startPackageSize;
        
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
        public int StartPackageSize { get => startPackageSize; set => startPackageSize = value; }

        public MultiArmedBanditBern(int count, int horizont ,double disp,double prob,int start)//Инициализация основного автомата
        {
            rnd = new Random();

            handleCount = count;
            handleChoise = new int[count];
            handleWin = new double[count];

            managmentHorizont = horizont;
            SqrtManagmentHorizont = Math.Sqrt(horizont);

            StartPackageSize = start;

            dispersion = disp;
            SqrtDispersion = Math.Sqrt(dispersion);
            sqrtDN = sqrtDispersion / sqrtManagmentHorizont;
            Probability = prob;


            //gen = new Geterator(prob, sqrtDN);
        }

        public void ReturnWin(int index, double d = 0)
        {
            handleChoise[index]++;
            double di=0;
            if (index == 0)
                di += d;
            else
                di -= d;
            handleWin[index] += BernRandom(di);
        }

        public void ReturnFirstWin(int index, double d = 0)
        {
            handleChoise[index]++;
            double di = 0;
            if (index == 0)
                di += d;
            else
                di -= d;
            handleWin[index] += BernRandomStart(di);
        }
        private double BernRandomStart(double d)
        {
            double prob = Probability + d * sqrtDN;
            int sum = 0;
            for (int i = 1; i <= startPackageSize; i++)
                if (rnd.NextDouble() < prob)
                    sum++;
            return sum;
        }
        private double BernRandom(double d)
        {
            double prob = Probability + d * sqrtDN;
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
