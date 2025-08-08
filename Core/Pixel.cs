public struct Pixel
{
    public int x;
    public int y;
    public Pixel(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    public Pixel()
    {
        x = 0;
        y = 0;
    }
    public Pixel(int x)
    {
        this.x = x;
        this.y = x;
    }
    public Pixel(Pixel other)
    {
        this.x = other.x;
        this.y = other.y;
    }
}