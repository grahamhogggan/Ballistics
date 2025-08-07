using System.Collections.Generic;

public class AccelerationFunction : Function
{
    public float mass = 1; // kg
    public List<ThrustProfile> profiles;
    public override float Evaluate(float x)
    {
        if (x < 0) return 0;
        float a = -9.8f; // m/s
        foreach (ThrustProfile p in profiles)
        {
            if (x > p.start && x < p.start + p.duration)
            {
                a += p.thrust / mass;
            }
        }
        return a;
    }
    public AccelerationFunction()
    {
        profiles = new List<ThrustProfile>();
    }
}