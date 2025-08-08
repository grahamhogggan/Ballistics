public class ConstantFunction : Function
{
    private float constant;
    public ConstantFunction(float c)
    {
        constant = c;
    }
    public override float Evaluate(float x)
    {
        return constant;
    }
    
}