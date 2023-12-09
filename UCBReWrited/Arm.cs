
using System;

class Arm
{
    public static readonly double Expectation = 0.5d;
    public static int Horizont;

    //Разные переменные для каждой рукоятки
    private int _armChoise;
    private int _armWin;

    private double _dispersion;
    private double _probabitily;

    private int _armIndex;

    private Random _rnd;

    //Конструктор
    public Arm(int choise, int win, int dispersion, int armIndex, double d = 0)
    {
        ArmChoise = choise;
        ArmWin = win;

        Dispersion = dispersion;
        SetProbability(d);

        ArmIndex = armIndex;

        _rnd = new Random();
    }

    //Свойства
    public int ArmChoise
    {
        get => _armChoise;
        set => _armChoise = value;
    }
    public int ArmWin
    {
        get => _armWin;
        set => _armWin = value;
    }
    public double Dispersion
    {
        get => _dispersion;
        set => _dispersion = value;
    }
    public int ArmIndex
    {
        get => _armIndex;
        set => _armIndex = value;
    }
    public double Probabitily { 
        get => _probabitily; 
        set => _probabitily = value; 
    }


    public void SetProbability(double d) 
    {
        double temp = d * Math.Sqrt(Dispersion / Horizont);
        if (ArmIndex == 0)
            Probabitily = Expectation + temp;
        else
            Probabitily = Expectation - temp;
    }

    public void UseArm(int packSize)
    {
        ArmChoise += packSize;
        while(packSize-- > 0)
        {
            if (_rnd.NextDouble() < Probabitily)
            {
                ArmWin++;
            }
        }
    }

    public void ResetArm()
    {
        ArmChoise = 0;
        ArmIndex = 0;
    }

}
