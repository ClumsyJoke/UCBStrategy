using System;
using System.IO;


namespace UCBReWrited
{
    class UCBStrategy
    {
        MultiArmedBandit bandit;
        //Инициализарованы изначально
        public StreamWriter sw;
        //Изменяемые в процессе работы
        double[] confidentialInterval;
        int averagingNumber;
        
        public UCBStrategy(int index, int horizont,int aver,StreamWriter ss)
        {
            bandit = new MultiArmedBandit(index,horizont);
            confidentialInterval = new double[index];
            averagingNumber = aver;
            sw = ss;
            //new StreamWriter("D:/Out2.txt");
        }

        public double PlayStrategy(double a = 1)
        {
            double lost = 0;
            double maxLost = 0;
            for (double d = 0.3d;d< 8d; d += 0.3d)
            {
                lost = 0;
                for (int avg = 0; avg < averagingNumber; avg++)
                {
                    bandit.ClearData();
                    for (int i = 0; i < bandit.HandleCount; i++) 
                    {
                        double sqrtLog = Math.Sqrt(Math.Log(bandit.HandleCount));
                        bandit.ReturnWin(i, d);
                        confidentialInterval[i] = bandit.HandleWin[i] + a * sqrtLog;
                            //Math.Sqrt(Math.Log(2)/bandit.HandeChoise[i]);
                    }
                    for (int k = 3; k <= bandit.ManagmentHorizont; k++)
                    {
                        int maxIndex = (confidentialInterval[0] > confidentialInterval[1] ? 0 : 1);
                        bandit.ReturnWin(maxIndex, d);
                        double logChoise = Math.Log(k);
                        for (int temp = 0; temp < bandit.HandleCount; temp++)
                            confidentialInterval[temp] = bandit.HandleWin[temp] / bandit.HandeChoise[temp] + a * Math.Sqrt(bandit.Dispersion[temp] *  logChoise / bandit.HandeChoise[temp]);
                        
                    }
                    lost += bandit.ManagmentHorizont * d * bandit.dSmall[0] / bandit.sqrtManagmentHorizont;
                    lost -= bandit.HandleWin[0] + bandit.HandleWin[1];
                }

                lost /= averagingNumber * bandit.sqrtManagmentHorizont;
                //Console.WriteLine(lost + " | " + d);
                sw.WriteLine(lost + " | " + d);
                if (lost > maxLost)
                    maxLost = lost;
            }
            return maxLost;
        }
        //Переписать что бы d была в тестах

        public void ClearAll()
        {
            bandit.ClearData();
        }
        public void FindWinAndInterval(int i,double d,double a)
        {
            bandit.ReturnWin(i, d);
            confidentialInterval[i] = bandit.HandleWin[i] / bandit.HandeChoise[i] + a * (2d + bandit.ExpRandom()) / bandit.HandeChoise[i]; 

        }
         
        //public void FindDispersion(double d,double a)
        //{
            
        //    bandit.ReturnWin(maxIndex, d);
        //    double logChoise = Math.Log(k);
        //    for (int temp = 0; temp < bandit.HandleCount; temp++)
        //        confidentialInterval[temp] = bandit.HandleWin[temp] / bandit.HandeChoise[temp] + a * Math.Sqrt(bandit.Dispersion[temp] * logChoise / bandit.HandeChoise[temp]);

        //}
    }
}
