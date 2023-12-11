using System;
using System.Runtime.InteropServices;

namespace UCBReWrited
{
    class BanditBern : MultiArmedBandit
    {
        //readonly Random rnd;

        //Инициализация в конструкторе
        //public double[] dSmall = { 1d, -1d };
       
        public BanditBern(int count, int horizont, double disp, double prob, int start) 
            : base(count, horizont, disp, prob, start) { }
       

        public override void ReturnWin(int index, int packageIndex, double d = 0)
        {
            HandleChoise[index] += PackageSize[packageIndex] / PackageSize[0];
            double di = 0;
            if (index == 0)
                di += d;
            else
                di -= d;
            HandleWin[index] += Random(di, packageIndex);
        }
        /// <summary>
        /// Генератор распределения Бернулли
        /// </summary>
        /// <param name="d"></param>
        /// <param name="packageIndex">Индекс выбора размера пакета: 0 - начальный, 1 - основной </param>
        /// <returns></returns>
        protected override double Random(double d, int packageIndex)
        {
            double prob = Probability + d * sqrtDN;
            int sum = 0;
            for (int i = 0; i < PackageSize[packageIndex]; i++)
                if (rnd.NextDouble() < prob)
                    sum++;
            return sum;
        }
    }
}
