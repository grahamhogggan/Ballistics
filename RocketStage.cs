public class RocketStage
{
    public float mass; //kg
    public float thrust;//N
    public float burnDuration;//s

    public BoundedFunction GetThrustFunction(float startTime)
    {
        ConstantFunction thrustFunction = new ConstantFunction(thrust / mass);
        BoundedFunction boundedFunction = new BoundedFunction(thrustFunction, startTime, startTime + burnDuration);
        return boundedFunction;
    }
    public RocketStage(float mass, float thrust, float duration)
    {
        this.mass = mass;
        this.thrust = thrust;
        this.burnDuration = duration;
    }
}