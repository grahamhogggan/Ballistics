using System.Collections.Generic;
public class IntegratedFunction : Function
{
    private Function f;
    public float initialValue;

    private Dictionary<float, float> tableOfValues;
    public override float Evaluate(float x)
    {
        float start = Closest(x);
        float sum = tableOfValues[start];
        float stepSize = (float)Math.Max(Math.Abs(start -x) / 1000, 0.01);
        if (x < start)
        {
            for (float i = x; i < start; i += stepSize)
            {
                sum -= stepSize * f.Evaluate(i);
            }
        }
        else
        {
            for (float i = start; i < x; i += stepSize)
            {
                sum += stepSize * f.Evaluate(i);
            }
        }
        return sum;

    }
    public IntegratedFunction(Function f)
    {
        this.f = f;
        tableOfValues = new Dictionary<float, float>();
        tableOfValues.Add(0, initialValue);
        for (int i = -300; i < 300; i++)
        {
            if(i!=0)
            tableOfValues.Add(i, Evaluate(i));
        }
    }
    private float Closest(float x)
    {
        float c = 0;
        foreach (float f in tableOfValues.Keys)
        {
            if (Math.Abs(f - x) < Math.Abs(c - x))
            {
                c = f;
            }
        }
        //if(x%1==0)
        //Console.WriteLine("Closest to " + x + " is " + c+", which was found to return "+tableOfValues[c]);
        return c;
    }
}