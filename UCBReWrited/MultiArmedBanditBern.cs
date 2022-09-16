using System;

namespace UCBReWrited
{
    class MultiArmedBanditBern
    {
        readonly Random rnd;
        
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
        int[] packageSize;
        
        //public double[] dSmall = { 1d, -1d };


        public MultiArmedBanditBern(int count, int horizont, double disp, double prob, int start)//Инициализация основного автомата
        {
            rnd = new Random();

            handleCount = count;
            handleChoise = new int[count];
            handleWin = new double[count];

            managmentHorizont = horizont;
            SqrtManagmentHorizont = Math.Sqrt(horizont);
            PackageSize = new int[] { start, 100 };

            dispersion = disp;
            SqrtDispersion = Math.Sqrt(dispersion);
            sqrtDN = sqrtDispersion / sqrtManagmentHorizont;
            Probability = prob;

        }
        //Свойства на используемые переменные
        public double HandleCount { get => handleCount; set => handleCount = value; }
        public int[] HandeChoise { get => handleChoise; set => handleChoise = value; }
        public double[] HandleWin { get => handleWin; set => handleWin = value; }

        public int ManagmentHorizont { get => managmentHorizont; set => managmentHorizont = value; }
        public double SqrtManagmentHorizont { get => sqrtManagmentHorizont; set => sqrtManagmentHorizont = value; }

        public double Dispersion { get => dispersion; set => dispersion = value; }
        public double SqrtDispersion { get => sqrtDispersion; set => sqrtDispersion = value; }
        public double Probability { get => probability; set => probability = value; }
        public int[] PackageSize { get => packageSize; set => packageSize = value; }


        /// <summary>
        /// Изменение значения HandleWin и HandleChoise
        /// </summary>
        /// <remarks>HandleWin: в зависимости от генератора Бернулли, 
        /// HandleChoise: + 1
        /// </remarks>
        /// <param name="index">Вбор ручки автомата: 0 - первая, 1 - вторая , ...</param>
        /// <param name="packageIndex">Индекс выбора размера пакета: 0 - начальный, 1 - основной</param>
        /// <param name="d"></param>
        public void ReturnWin(int index, int packageIndex, double d = 0)
        {
            handleChoise[index]++;
            double di = 0;
            if (index == 0)
                di += d;
            else
                di -= d;
            handleWin[index] += BernRandom(di, packageIndex);
        }
        /// <summary>
        /// Генератор распределения Бернулли
        /// </summary>
        /// <param name="d"></param>
        /// <param name="packageIndex">Индекс выбора размера пакета: 0 - начальный, 1 - основной </param>
        /// <returns></returns>
        private double BernRandom(double d, int packageIndex)
        {
            double prob = Probability + d * sqrtDN;
            int sum = 0;
            for (int i = 1; i <= PackageSize[packageIndex]; i++)
                if (rnd.NextDouble() < prob)
                    sum++;
            return sum;
        }
        /// <summary>
        /// Очистка handleChoise и handleWin
        /// </summary>
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
