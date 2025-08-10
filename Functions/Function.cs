public class Function
{
    public virtual float Evaluate(float x)
    {
        return x;
    }
    public float getZero(float seed)
    {
        int tries = 0;
        float x = seed;
        DerivatedFunction d = new DerivatedFunction(this);
        //Console.WriteLine("s:"+Evaluate(x));
        while (Math.Abs(Evaluate(x)) > 0.001f && tries < 100&&d.Evaluate(x)!=0)
        {
            x -= Evaluate(x) / d.Evaluate(x);
            //Console.WriteLine(x + ", " + Evaluate(x)+", "+d.Evaluate(x));
            tries++;
        }
        return x;
    }
}