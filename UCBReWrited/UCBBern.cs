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
        int averagingNumber;

        public UCBBern(int count, int horizont, int aver, StreamWriter ss)
        {
            bandit = new MultiArmedBanditBern(count, horizont, 0.25d,0.5d);//Число действий, горизонт, дисперсия, вероятность
            confidentialInterval = new double[count];
            averagingNumber = aver;
            sw = ss;
            //new StreamWriter("D:/Out2.txt");
        }

        public double PlayStrategy(double a = 1)
        {
            double lost;
            double maxLost = 0;
            int n = bandit.ManagmentHorizont / bandit.PackageSize;//Число пакетов

            for (double d = 0d;d < 8d; d += 0.3d)
            {
                lost = 0;//Обнуление потерь после изменения d
                
                for (int avg = 0;avg < averagingNumber; avg++)
                {
                    //Начальные данные
                    bandit.ClearData();
                    //Первые 2 действия
                    for ( int start = 0; start< bandit.HandleCount; start++) 
                    {
                        double logK = Math.Log(bandit.HandleCount);//Что бы не считать логарифм k 2 раза
                        bandit.ReturnWin(start, d);
                        confidentialInterval[start] = bandit.HandleWin[start] + a * Math.Sqrt(bandit.Dispersion * bandit.PackageSize * logK);
                    }
                    //Остальные 48 действий
                    for(int k = 3; k <= n; k++)
                    {
                        int maxIndex = (confidentialInterval[0] > confidentialInterval[1] ? 0 : 1);//Максимальный доверительный
                        bandit.ReturnWin(maxIndex,d);//Выбор этого действия
                        //double logChoise = Math.Log(k);//Чтоб не считать 2 раза
                        double sqrtZ = Math.Sqrt(bandit.PackageSize * Math.Log(k));
                        //Доверительный интрвал в момент времени k
                        confidentialInterval[0] = bandit.HandleWin[0] / bandit.HandeChoise[0] + a * sqrtZ * bandit.SqrtDispersion / Math.Sqrt(bandit.HandeChoise[0]);
                        confidentialInterval[1] = bandit.HandleWin[1] / bandit.HandeChoise[1] + a * sqrtZ * bandit.SqrtDispersion / Math.Sqrt(bandit.HandeChoise[1]);
                        
                    }
                    
                    lost += (bandit.Probability * bandit.ManagmentHorizont + d * bandit.SqrtDispersion * bandit.SqrtManagmentHorizont) ;    
                    lost -= bandit.HandleWin[0]  + bandit.HandleWin[1] ;
                    
                }
                
                lost /= averagingNumber * bandit.SqrtDispersion * bandit.SqrtManagmentHorizont;
                Console.WriteLine(lost + " | " + d);
                sw.WriteLine(lost + " | " + d);
                if (lost > maxLost)
                    maxLost = lost;
            }
            return maxLost;
        }
        //public double PlayStrategy(double a = 1)
        //{
        //    double lost = 0;
        //    double maxLost = 0;
        //    for (double d = 0.3d; d < 8d; d += 0.3d)
        //    {
        //        lost = 0;
        //        for (int avg = 0; avg < averagingNumber; avg++)
        //        {
        //            bandit.ClearData();
        //            for (int i = 0; i < bandit.HandleCount; i++)
        //            {
        //                double sqrtLog = Math.Sqrt(Math.Log(bandit.HandleCount));
        //                bandit.ReturnWin(i, d);
        //                confidentialInterval[i] = bandit.HandleWin[i] / 2 + a * Math.Sqrt(bandit.Dispersion * bandit.PackageSize / 2);
        //                //Math.Sqrt(Math.Log(2)/bandit.HandeChoise[i]);
        //            }
        //            for (int k = 2; k < bandit.ManagmentHorizont / bandit.PackageSize; k++)
        //            {
        //                int maxIndex = (confidentialInterval[0] > confidentialInterval[1] ? 0 : 1);
        //                bandit.ReturnWin(maxIndex, d);
        //                double logChoise = Math.Log(k);
        //                for (int temp = 0; temp < bandit.HandleCount; temp++)
        //                    confidentialInterval[temp] = bandit.HandleWin[temp] / bandit.HandeChoise[temp] + a * Math.Sqrt(bandit.Dispersion * bandit.PackageSize* logChoise / bandit.HandeChoise[temp]);

        //            }
        //            lost +=  d * bandit.dSmall[0] * bandit.sqrtManagmentHorizont;
        //            //lost -= bandit.HandleWin[0] + bandit.HandleWin[1];
        //        }

        //        lost /= averagingNumber * bandit.sqrtManagmentHorizont * bandit.PackageSize;
        //        Console.WriteLine(lost + " | " + d);
        //        sw.WriteLine(lost + " | " + d);
        //        if (lost > maxLost)
        //            maxLost = lost;
        //    }
        //    return maxLost;
        //}
        //Переписать что бы d была в тестах

        public void ClearAll()
        {
            bandit.ClearData();
        }
       
    }
}
