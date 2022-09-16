using System;
using System.IO;


namespace UCBReWrited
{
    class StrategyАnalysis
    {
        UCBBern strat;
        public StrategyАnalysis(int horizont,StreamWriter ss,double prob,int start) 
        {
            strat = new UCBBern(2,horizont,100000,ss,prob,start);//Количество действий, горизонт,число усреднений, фаил записи

        }
        /// <summary>
        /// Поиск потерь для одного "a"
        /// </summary>
        /// <param name="a"></param>
        public void FindLost(double a)
        {
            double lost;
            Console.WriteLine("a = {0}", a);
            strat.sw.WriteLine("a = {0}", a);
            lost = strat.PlayStrategy(a);
            Console.WriteLine("Max lost = {0}", lost);
            Console.WriteLine("Optimal a = {0} with max lost {1}", a, lost);
            strat.sw.Close();
        }

        /// <summary>
        /// Поиск minmax потерь для диапазона "a"
        /// </summary>
        /// <param name="startA">Начало диапазона поиска</param>
        public void FindOptimalA(double startA)
        {
            double minMaxLost = 10;
            double lost;
            double optimalA = 1;
            for (double a = startA;a < startA + 0.18d; a += 0.03d) 
            {
                //Вывод для отладки
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
