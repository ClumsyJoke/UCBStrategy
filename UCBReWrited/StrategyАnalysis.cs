using System;
using System.IO;


namespace UCBReWrited
{
    class StrategyАnalysis
    {
        UCBBern strat;
        //const int averagingResult = 10000;
        //double startA;


        public StrategyАnalysis(int horizont,StreamWriter ss,double prob,int start) 
        {
            strat = new UCBBern(2,horizont,100000,ss,prob,start);//Количество действий, горизонт,число усреднений, фаил записи

        }
        public void FindLost(double a)
        {
            double lost;
            //double optimalA = 1;
            Console.WriteLine("a = {0}", a);
            strat.sw.WriteLine("a = {0}", a);
            lost = strat.PlayStrategy(a);
            Console.WriteLine("Max lost = {0}", lost);
            Console.WriteLine("Optimal a = {0} with max lost {1}", a, lost);
            strat.sw.Close();
        }
        public void FindOptimalA(double startA)
        {
            double minMaxLost = 10;
            double lost;
            double optimalA = 1;
            for (double a = startA;a < startA + 0.18d; a += 0.03d) 
            {
                Console.WriteLine("a = {0}", a);
                strat.sw.WriteLine("a = {0}", a);
                lost = strat.PlayStrategy(a);
                Console.WriteLine("Max lost = {0}", lost);
                if (minMaxLost > lost)
                {
                    minMaxLost = lost;
                    optimalA = a;
                }
            }
            Console.WriteLine("Optimal a = {0} with max lost {1}", optimalA, minMaxLost);
            strat.sw.Close();
        }

       /* public void FindOptimalM(double startA)
        {
            double minMaxLost = 10;
            double lost;
            double optimalM = 10;
            for (double M = startA; a < startA + 0.2d; a += 0.1)
            {
                Console.WriteLine("a = {0}", a);
                strat.sw.WriteLine("a = {0}", a);
                lost = strat.PlayStrategy(a);
                Console.WriteLine("Max lost = {0}", lost);
                if (minMaxLost > lost)
                {
                    minMaxLost = lost;
                    optimalM = M;
                }
            }
            Console.WriteLine("Optimal a = {0} with max lost {1}", optimalA, minMaxLost);
            strat.sw.Close();
        }
       */
    }
}
