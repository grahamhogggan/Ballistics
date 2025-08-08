public class BoundedFunction : Function
{
    public float startTime;
    public float endTime;
    public Function main;
    public override float Evaluate(float x)
    {
        if(x<startTime||x>endTime)
        {
            return 0;
        }
        else
        {
            return main.Evaluate(x);
        }
    }
    public BoundedFunction(Function f, float s, float e)
    {
        main = f;
        startTime = s;
        endTime = e;
    }
}