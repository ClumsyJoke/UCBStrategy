using System;
using System.IO;


namespace UCBReWrited
{
    class StrategyАnalysis
    {
        UCBStrategy strat;
        const int averagingResult = 10000;
        //double startA;


        public StrategyАnalysis(StreamWriter ss) 
        {
            strat = new UCBStrategy(2,1000,300000,ss);

        }

        public void FindOptimalA(double startA)
        {
            double minMaxLost = 10;
            double lost;
            double optimalA = 1;
            for (double a = startA;a < startA + 0.2d; a += 0.03) 
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
    }
}
