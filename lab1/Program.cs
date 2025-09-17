using System;

abstract class Figure
{
    private string _name;

    public string Name
    {
        get => _name;
        set => _name = value;
    }

    public Figure(string name)
    {
        _name = name;
        Console.WriteLine("Figure created.");
    }

    ~Figure()
    {
        Console.WriteLine("Figure destroyed.");
    }

    public virtual double GetArea()
    {
        return 0;
    }
}

class Circle : Figure
{
    private double _radius;

    public Circle(string name, double radius) : base(name)
    {
        _radius = radius;
    }

    public override double GetArea()
    {
        return Math.PI * _radius * _radius;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Circle circle = new Circle("MyCircle", 5.0);
        Console.WriteLine($"Name: {circle.Name}");
        Console.WriteLine($"Area: {circle.GetArea():F2}");
    }
}
