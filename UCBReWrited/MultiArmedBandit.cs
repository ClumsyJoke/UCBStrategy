using System;

namespace UCBReWrited
{

    class MultiArmedBandit
    {
        Random rnd;
        //Инициализация в конструкторе
        double handleCount;
        int[] handleChoise;
        double[] handleWin;
        double[] dispersion;
        //Инициализарованы изначально
        public double[] dSmall = { 1d, -1d }; //Думаю перенести в стратегию,так как в этом классе не хватает значений
        int managmentHorizont;//Как передавать выбор из другого класса?
        public double sqrtManagmentHorizont;

        public double HandleCount { get => handleCount; set => handleCount = value; }
        public int ManagmentHorizont { get => managmentHorizont; set => managmentHorizont = value; }
        public int[] HandeChoise { get => handleChoise; set => handleChoise = value; }
        public double[] HandleWin { get => handleWin; set => handleWin = value; }
        public double[] Dispersion { get => dispersion; set => dispersion = value; }

        public MultiArmedBandit(int count,int horizont) 
        {
            rnd = new Random();
            handleCount = count;
            handleChoise = new int[count];
            handleWin = new double[count];
            Dispersion = new double[count];
            managmentHorizont = horizont;
            sqrtManagmentHorizont = Math.Sqrt(horizont);
        }

        public void ReturnWin(int index,double d = 0)
        {
            handleChoise[index]++;
            handleWin[index] += NormRandom() + d * dSmall[index] / sqrtManagmentHorizont;
        }
        
        public void ReturnWinBinom(int index, double d = 0)
        {

        }
        public void ClearData()
        {
            for (int i = 0;i < handleCount;i++)
            {
                handleChoise[i] = 0;
                handleWin[i] = 0;
            }
        }
        public double NormRandom()
        {
            double s = 0d;
            for (int i = 0; i < 12; i++)
                s += rnd.NextDouble();
            s -= 6;
            return s;
        }
        public double ExpRandom() => -Math.Log(rnd.NextDouble() + double.Epsilon);

    }
}
