public class ThrustProfile
{
    public float thrust; // N
    public float start;
    public float duration;
    public ThrustProfile(float s, float d, float t)
    {
        start = s;
        duration = d;
        thrust = t;
    }
}