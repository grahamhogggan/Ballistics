public class Quadratic : Function
{
    public float q;
    public float l;
    public float c;
    public override float Evaluate(float x)
    {
        return q * x * x + l * x + c;
    }
    public Quadratic(float a, float b, float c)
    {
        q = a;
        l = b;
        this.c = c;
    }
}