using System;

public class PointCounter
{
    private int points;

    public int Points { get => points; }

    public event Action <int> onPointCountChanged;

    public PointCounter()
    {
        points = 0;
    }


    public void AddPoint()
    {
        points++;
        onPointCountChanged?.Invoke(points);
    }
}