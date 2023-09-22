public class PointCounter
{
    private int points;

    public int Points { get => points; }

    public PointCounter()
    {
        points = 0;
    }


    public void AddPoint()
    {
        points++;
    }
}