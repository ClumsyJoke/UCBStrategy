
using System.IO;
using System.Threading;

namespace UCBReWrited
{
    class Program
    {
        static void Main()
        {
            StrategyАnalysis[] arrStrat = new StrategyАnalysis[3];
            Thread[] potok1 = new Thread[1];
            int start = 5;
            int horizon = 1000;
            arrStrat[0] = new StrategyАnalysis(horizon, new StreamWriter("D:/Out25.txt"), 0.5, 25);
            //arrStrat[1] = new StrategyАnalysis(horizon, new StreamWriter("D:/Out20.txt"), 0.5, 20);
            potok1[0] = new Thread(() => arrStrat[0].FindLost(0.88d));
            potok1[0].Start();
            //arrStrat[1].FindOptimalA(0.85d, 1.0d, 0.03d);
            //arrStrat[1].FindLost(0.86d);
        }
        //{
        //arrStrat[0] = new StrategyАnalysis(horizon, new StreamWriter("D:/Out05.txt"),0.05,start);//Передача в конструктор размер горизотна и фаила записи
        //arrStrat[2] = new StrategyАnalysis(horizon, new StreamWriter("D:/Out3200.txt"));
        //arrStrat[0].FindLost(0.94d);
        //Thread[] potok1 = new Thread[1];
        //potok1[0] = new Thread(() => arrStrat[0].FindLost(0.95d));
        //potok1[0].Start();
        //arrStrat[1].FindLost(0.98d);}
    }
}

