using System.Collections.Generic;
public class StagedRocket
{
    public Function positionFunction;
    public Function velocityFunction;
    public PiecewiseFunction accelerationFunction;

    public StagedRocket()
    {
        accelerationFunction = new PiecewiseFunction();
        accelerationFunction.AddPiece(new ConstantFunction(-9.8f), 0, float.MaxValue);
        velocityFunction = new IntegratedFunction(accelerationFunction);
        positionFunction = new IntegratedFunction(velocityFunction);
    }
    public void AddStage(RocketStage stage, float ignition)
    {
        accelerationFunction.AddPiece(stage.GetThrustFunction(ignition));
        velocityFunction = new IntegratedFunction(accelerationFunction);
        positionFunction = new IntegratedFunction(velocityFunction);
    }
}