using System;
using System.IO;

namespace UCBReWrited
{
    class UCBBern
    {
        MultiArmedBanditBern bandit;
        //Инициализарованы изначально
        public StreamWriter sw;
        //Изменяемые в процессе работы
        double[] confidentialInterval;
        readonly int averagingNumber;

        public UCBBern(int count, int horizont, int aver, StreamWriter ss,double prob,int start)
        {
            bandit = new MultiArmedBanditBern(count, horizont, 0.25d, prob, start);//Число действий, горизонт, дисперсия, вероятность
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
            for (double d = 0.0d;d < 2.5.1d; d += 0.3d)
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
                        confidentialInterval[start] = bandit.HandleWin[start] / bandit.HandeChoise[start] + a * Math.Sqrt(bandit.Dispersion * logK / bandit.HandeChoise[start]);
                    }
                    for (int size = bandit.HandeChoise[0] + bandit.HandeChoise[1]; size  % relation != 0; size += 1)
                    {
                        int maxIndex = (confidentialInterval[0] > confidentialInterval[1] ? 0 : 1);//Максимальный доверительный
                        bandit.ReturnWin(maxIndex, 0, d);//Действие над максимальным доверительным интервалом
                        double aSqrtZ = a * Math.Sqrt(Math.Log(size + 1) * bandit.PackageSize[0]) * bandit.SqrtDispersion;//Чтоб не считать 2 раза общую часть в UCB
                        for (int i = 0; i < bandit.HandleCount; i++)
                            //Доверительный интервал
                            confidentialInterval[i] = bandit.HandleWin[i] / bandit.HandeChoise[i] + aSqrtZ / Math.Sqrt(bandit.HandeChoise[i]);
                    }
                    //Остальные 48 действий
                    for (int k = bandit.HandeChoise[0] + bandit.HandeChoise[1] ; k < horizont; k += relation)
                    {
                        int maxIndex = (confidentialInterval[0] > confidentialInterval[1] ? 0 : 1);//Максимальный доверительный
                        bandit.ReturnWin(maxIndex, 1, d);//Действие над максимальным доверительным интервалом
                        double aSqrtZ = a * Math.Sqrt(relation * Math.Log(k + relation) * bandit.PackageSize[0]) * bandit.SqrtDispersion;//Чтоб не считать 2 раза общую часть в UCB
                        for( int i = 0; i < bandit.HandleCount; i++)
                            //Доверительный интервал
                            confidentialInterval[i] = bandit.HandleWin[i] / bandit.HandeChoise[i] + aSqrtZ / Math.Sqrt(bandit.HandeChoise[i]);
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

    }
}
