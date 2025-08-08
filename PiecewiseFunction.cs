using System.Collections.Generic;
public class PiecewiseFunction : Function
{
    private List<BoundedFunction> functions = new List<BoundedFunction>();
    public void AddPiece(Function f, float startTime, float endTime)
    {
        BoundedFunction b = new BoundedFunction(f, startTime, endTime);
        functions.Add(b);
    }
    public void AddPiece(BoundedFunction f)
    {
        functions.Add(f);
    }
    public override float Evaluate(float x)
    {
        float total = 0;
        foreach (BoundedFunction b in functions)
        {
            total += b.Evaluate(x);
        }
        return total;
    }
}