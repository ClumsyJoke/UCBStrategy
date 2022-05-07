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
            Thread[] potok1 = new Thread[2];

            arrStrat[0] = new StrategyАnalysis(new StreamWriter("D:/Out11000.txt"));
            arrStrat[1] = new StrategyАnalysis(new StreamWriter("D:/Out21000.txt"));
            arrStrat[2] = new StrategyАnalysis(new StreamWriter("D:/Out31000.txt"));

            potok1[0] = new Thread(() => arrStrat[0].FindOptimalA(0.6d));
            potok1[1] = new Thread(() => arrStrat[1].FindOptimalA(0.8d));
            potok1[0].Start();
            potok1[1].Start();
            arrStrat[2].FindOptimalA(1d);
        }
    }
}
