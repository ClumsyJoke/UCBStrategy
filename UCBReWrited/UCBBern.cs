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
            bandit = new MultiArmedBanditBern(count, horizont, 0.25d,prob,start);//Число действий, горизонт, дисперсия, вероятность
            confidentialInterval = new double[count];
            averagingNumber = aver;
            sw = ss;
            //new StreamWriter("D:/Out2.txt");
        }

        public double PlayStrategy(double a = 1)
        {
            double lost;
            double maxLost = 0;
            int n = (bandit.ManagmentHorizont - (2 * bandit.StartPackageSize)) / bandit.PackageSize;//Число пакетов
            double logK = Math.Log(bandit.HandleCount);
            for (double d = 0d;d < 9.5d; d += 0.3d)
            {
                lost = 0;//Обнуление потерь после изменения d
                for (int avg = 0;avg < averagingNumber; avg++)
                {
                    //Начальные данные
                    bandit.ClearData();
                    //Первые 2 действия
                    for (int start = 0; start< bandit.HandleCount; start++) 
                    {
                        bandit.ReturnFirstWin(start, d);
                        confidentialInterval[start] = bandit.HandleWin[start] + a * Math.Sqrt(bandit.Dispersion * bandit.StartPackageSize * logK);
                    }
                    //Остальные 48 действий
                    for(int k = 1; k <= n; k++)
                    {
                        int maxIndex = (confidentialInterval[0] > confidentialInterval[1] ? 0 : 1);//Максимальный доверительный
                        bandit.ReturnWin(maxIndex,d);//Выбор этого действия
                        double asqrtZ = a * Math.Sqrt(bandit.PackageSize * Math.Log(k + 2));
                        confidentialInterval[0] = bandit.HandleWin[0] / bandit.HandeChoise[0] +  asqrtZ * bandit.SqrtDispersion / Math.Sqrt(bandit.HandeChoise[0]);
                        confidentialInterval[1] = bandit.HandleWin[1] / bandit.HandeChoise[1] +  asqrtZ * bandit.SqrtDispersion / Math.Sqrt(bandit.HandeChoise[1]);
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
      
        public void ClearAll()
        {
            bandit.ClearData();
        }
       
    }
}
