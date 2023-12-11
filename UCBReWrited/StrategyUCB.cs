

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection.Metadata;
using UCBReWrited;

class StrategyUCB
{
    
    private Arm[] arms = null; //ToDo: Добавить индексатор
    private UpperBounds[] upperBounds = null;
    private int packSize;

    private int totalChoises;
    private int simAmount;
    public StrategyUCB(int horizont, double[] dispersion, int packSize, int armCount, int simAmount)
    {
        arms = new Arm[armCount];
        upperBounds = new UpperBounds[armCount];

        PackSize = packSize;
        SimAmount = simAmount;

        for (int i = 0; i < armCount; i++)
        {
            arms[i] = new Arm(horizont, dispersion[i], i);
            upperBounds[i] = new UpperBounds(i); 
        }
    }

    public int PackSize { get => packSize; set => packSize = value; }
    public int SimAmount { get => simAmount; set => simAmount = value; }


    public double[] PlayStrategy(double a, double startD, double endD, double stepD)
    {
        double maxR = 0;
        List<double> regrets = new List<double>();
        for (double d = startD; d < endD; d += stepD)
        {
            foreach (Arm arm in arms)
                arm.SetProbability(d);
            double currentRegret = 0;
            for (int sim = 0; sim < SimAmount; sim++)
            {
                for (int i = 0; i < arms.Length; i++)
                {
                    arms[i].ResetArm();
                    arms[i].UseArm(PackSize);
                }
                UpdateTotal();
                for (int n = totalChoises; n < arms.Length; n += PackSize)
                {
                    CalculateUCB(a);
                    arms[FindBestArm()].UseArm(PackSize);
                }
                int indexBest = FindBestArm();
                currentRegret += arms[indexBest].Probabitily;
                foreach (Arm ar in arms)
                {
                    currentRegret -= ar.ArmWin;
                }
            }
            currentRegret /= SimAmount * Math.Sqrt(arms[0].Dispersion * arms[0].Horizont);//ToDo: добавить MaxDispersion
            regrets.Add(currentRegret);
        }
        return regrets.ToArray();
    }

    public void CalculateUCB(double a)
    {
        for (int i = 0; i < arms.Length;i++)
            upperBounds[i].Bounds = arms[i].ArmWin / arms[i].ArmChoises + a * Math.Sqrt(PackSize * arms[i].Dispersion * Math.Log(totalChoises) / arms[i].ArmChoises);
    }

    private void UpdateTotal()
    {
        totalChoises = 0;
        for (int i = 0; i < arms.Length; i++)
            totalChoises += arms[i].ArmChoises;
    }

    private int FindBestArm()
    {
        return upperBounds.OrderByDescending(x => x.Bounds).ToArray()[0].Index;
    }
    
}

