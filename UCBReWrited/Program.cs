using System;
using System.IO;
using System.Threading;

namespace UCBReWrited
{
    class Program
    {
        static void Main(string[] args)
        {
            StrategyАnalysis[] arrStrat = new StrategyАnalysis[3];

            int horizon = 5000;
            arrStrat[0] = new StrategyАnalysis(horizon, new StreamWriter("D:/Out1200.txt"));//Передача в конструктор размер горизотна и фаила записи
            arrStrat[1] = new StrategyАnalysis(horizon, new StreamWriter("D:/Out2200.txt"));
            //arrStrat[2] = new StrategyАnalysis(horizon, new StreamWriter("D:/Out3200.txt"));
            //arrStrat[0].FindLost(1.1d);
            //{ //Для определения оптимального а
            Thread[] potok1 = new Thread[1];
            potok1[0] = new Thread(() => arrStrat[0].FindOptimalA(0.9d));
            //potok1[1] = new Thread(() => arrStrat[1].FindOptimalA(1.0d));

            potok1[0].Start();
            //potok1[1].Start();
            arrStrat[1].FindOptimalA(0.95d);
        }

    }
}

