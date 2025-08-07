public class IntegratedFunction : Function
{
    private Function f;
    public float initialValue;
    public override float Evaluate(float x)
    {
        if(x<0)
        {
            float sum = initialValue;
            for(float i = x; i<0; i+=-x/1000)
            {
                sum+=x/1000 * f.Evaluate(i);
            }
            return sum;
        }
        else
        {
            float sum = initialValue;
            for(float i = 0; i<x; i+=x/1000)
            {
                sum+=x/1000 * f.Evaluate(i);
            }
            return sum;
        }

    }
    public IntegratedFunction(Function f)
    {
        this.f = f;
    }
}