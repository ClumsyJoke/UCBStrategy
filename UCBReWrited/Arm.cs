
using System;

class Arm
{
    public static readonly double Expectation = 0.5d;
    //public static int Horizont;

    //Разные переменные для каждой рукоятки
    private int armChoise = 0;
    private int armWin = 0;

    private double dispersion;
    private double probabitily;

    private int armIndex;
    private int horizont;

    private Random rnd;

    //Конструктор
    public Arm(int horizont, double dispersion, int armIndex)
    {
        Dispersion = dispersion;
        Horizont = horizont;

        SetProbability();

        ArmIndex = armIndex;

        rnd = new Random();
    }

    //Свойства
    public int ArmChoises
    {
        get => armChoise;
        set => armChoise = value;
    }
    public int ArmWin
    {
        get => armWin;
        set => armWin = value;
    }
    public double Dispersion
    {
        get => dispersion;
        set => dispersion = value;
    }
    public int ArmIndex
    {
        get => armIndex;
        set => armIndex = value;
    }
    public double Probabitily { 
        get => probabitily; 
        set => probabitily = value; 
    }
    public int Horizont { 
        get => horizont; 
        set => horizont = value; }

    public void SetProbability(double d = 0) 
    {
        Probabitily =  Expectation + (ArmIndex == 0 ? d : -d) * Math.Sqrt(Dispersion / Horizont);
    }

    public void UseArm(int packSize)
    {
        ArmChoises += packSize;
        while(packSize-- > 0)
        {
            if (rnd.NextDouble() < Probabitily)
            {
                ArmWin++;
            }
        }
    }

    public void ResetArm()
    {
        ArmChoises = 0;
        ArmIndex = 0;
    }

}
