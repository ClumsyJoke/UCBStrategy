
using System.IO;
using System.Threading;

namespace UCBReWrited
{
    class Program
    {
        static void Main()
        {
            StrategyАnalysis[] arrStrat = new StrategyАnalysis[3];
            int start = 95;
            int horizon = 4800 + start * 2 ;
            //arrStrat[0] = new StrategyАnalysis(horizon, new StreamWriter("D:/Out05.txt"),0.05,start);//Передача в конструктор размер горизотна и фаила записи
            arrStrat[1] = new StrategyАnalysis(horizon, new StreamWriter("D:/Out9i5.txt"),0.5,start);
            //arrStrat[2] = new StrategyАnalysis(horizon, new StreamWriter("D:/Out3200.txt"));
            //arrStrat[0].FindLost(0.94d);
            //Thread[] potok1 = new Thread[1];
            //potok1[0] = new Thread(() => arrStrat[0].FindLost(0.95d));
            //potok1[0].Start();
            arrStrat[1].FindOptimalA(1.16d);
            //arrStrat[1].FindLost(0.98d);
        }

    }
}

