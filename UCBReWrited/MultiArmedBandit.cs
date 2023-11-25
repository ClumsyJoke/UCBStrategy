using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCBReWrited
{
    public abstract class MultiArmedBandit
    {
        protected readonly Random rnd;

        //Инициализация в конструкторе
        
        double handleCount;
        int[] handleChoise;
        double[] handleWin;

        int managmentHorizont;
        double sqrtManagmentHorizont;

        double dispersion;
        double sqrtDispersion;

        double probability;
        protected readonly double sqrtDN;
        int[] packageSize;

        //public double[] dSmall = { 1d, -1d };


        public MultiArmedBandit(int count, int horizont, double disp, double prob, int start)//Инициализация основного автомата
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
        public int[] HandleChoise { get => handleChoise; set => handleChoise = value; }
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
        public abstract void ReturnWin(int index, int packageIndex, double d = 0);

        /// <summary>
        /// Генератор распределения Бернулли
        /// </summary>
        /// <param name="d"></param>
        /// <param name="packageIndex">Индекс выбора размера пакета: 0 - начальный, 1 - основной </param>
        /// <returns></returns>
        protected abstract double Random(double d, int packageIndex);

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

