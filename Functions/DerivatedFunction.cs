public class DerivatedFunction : Function
{
    Function parent = new Function();
    public DerivatedFunction(Function f)
    {
        parent = f;
    }
    public override float Evaluate(float x)
    {
        return (parent.Evaluate(x+0.01f) - parent.Evaluate(x)) / 0.01f;
    }
}