using System;

namespace UCBReWrited
{
    public struct Geterator
    {
        readonly Random rnd;
        double sqrtD;
        double probability;
            //= Math.Sqrt(horizont);

        public Geterator(double prob,double sqrtDN)
        {
            probability = prob;
            sqrtD = sqrtDN;
            rnd = new Random();
        }
        public  int BernRandom(double d)
        {
            double prob = probability + d * sqrtD;
            int sum = 0;
            for (int i = 1; i <= 100; i++)
                if (rnd.NextDouble() < prob)
                    sum++;
            return sum;
        }
    }
}
