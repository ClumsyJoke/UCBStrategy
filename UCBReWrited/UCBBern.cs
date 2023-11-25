using System;
using System.IO;

namespace UCBReWrited
{
    class UCBBern
    {
        BanditBern bandit;
        //Инициализарованы изначально
        public StreamWriter sw;
        //Изменяемые в процессе работы
        double[] confidentialInterval;
        readonly int averagingNumber;

        public UCBBern(int count, int horizont, int aver, StreamWriter ss,double prob,int start)
        {
            bandit = new BanditBern(count, horizont, 0.25d, prob, start);//Число действий, горизонт, дисперсия, вероятность
            confidentialInterval = new double[count];
            averagingNumber = aver;
            sw = ss;
        }

        public double PlayStrategy(double a = 1)
        {
            double lost;
            double maxLost = 0;
            //int n = (bandit.ManagmentHorizont - (2 * bandit.PackageSize[0])) / bandit.PackageSize[1];//Число пакетов
            double logK = Math.Log(2);// Не считать несколько раз для начальных действий
            int relation = bandit.PackageSize[1] / bandit.PackageSize[0];
            int horizont = bandit.ManagmentHorizont / bandit.PackageSize[0];
            for (double d = 0.0d; d < 45.0d; d += 0.3d)
            {
                lost = 0;//Обнуление потерь после изменения d
                for (int avg = 0;avg < averagingNumber; avg++)
                {
                    //Начальные данные
                    bandit.ClearData();
                    //Первые 2 действия
                    for (int start = 0; start < bandit.HandleCount; start++) 
                    {
                        bandit.ReturnWin(start, 0, d);
                        confidentialInterval[start] = bandit.HandleWin[start] / bandit.HandleChoise[start] + a * Math.Sqrt(bandit.PackageSize[0] * bandit.Dispersion * logK  / bandit.HandleChoise[start]);
                    }
                    for (int size = bandit.HandleChoise[0] + bandit.HandleChoise[1]; size % relation != 0; size += 1)
                    {
                        //NewMethod(a, d, size);
                        OneStep(a, 1, d, size,0);
                    }
                    //Остальные 48 действий
                    for (int k = bandit.HandleChoise[0] + bandit.HandleChoise[1]; k < horizont; k += relation)
                    {
                        OneStep(a, relation, d, k,1);
                    }
                    //Учёт потерь
                    lost += (bandit.Probability * bandit.ManagmentHorizont + d * bandit.SqrtDispersion * bandit.SqrtManagmentHorizont) ;    
                    lost -= bandit.HandleWin[0]  + bandit.HandleWin[1] ;
                    
                }
                lost /= averagingNumber * bandit.SqrtDispersion * bandit.SqrtManagmentHorizont;
                //Вывод для отладки
                Console.WriteLine(lost + " | " + d);
                sw.WriteLine(lost + " | " + d);

                if (lost > maxLost)
                    maxLost = lost;
            }
            return maxLost;
        }

        private void NewMethod(double a, double d, int size)
        {
            int maxIndex = (confidentialInterval[0] > confidentialInterval[1] ? 0 : 1);//Максимальный доверительный
            bandit.ReturnWin(maxIndex, 0, d);//Действие над максимальным доверительным интервалом
            double aSqrtZ = a * Math.Sqrt(bandit.PackageSize[0] * Math.Log(size + 1)) * bandit.SqrtDispersion;//Чтоб не считать 2 раза общую часть в UCB
            for (int i = 0; i < bandit.HandleCount; i++)
                confidentialInterval[i] = bandit.HandleWin[i] / bandit.HandleChoise[i] + aSqrtZ / Math.Sqrt(bandit.HandleChoise[i]);
        }

        private void OneStep(double a, int relation, double d, int k,int pack)
        {
            int maxIndex = (confidentialInterval[0] > confidentialInterval[1] ? 0 : 1);//Максимальный доверительный
            bandit.ReturnWin(maxIndex, pack, d);//Действие над максимальным доверительным интервалом
            double aSqrtZ = a * Math.Sqrt(bandit.PackageSize[0] * Math.Log(k + relation)) * bandit.SqrtDispersion;//Чтоб не считать 2 раза общую часть в UCB
            for (int i = 0; i < bandit.HandleCount; i++)
                //Доверительный интервал
                confidentialInterval[i] = bandit.HandleWin[i] / bandit.HandleChoise[i] + aSqrtZ / Math.Sqrt(bandit.HandleChoise[i]);
        }

    }
}
