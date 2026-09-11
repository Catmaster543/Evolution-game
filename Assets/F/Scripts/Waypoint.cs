using UnityEngine;

public class Waypoint
{
    public Vector2 Position {  get; private set; }

    public Waypoint (Vector2 position)
    {
        Position = position;
    }
    public override string ToString()
    {
        return $"Waypoint(pos={Position})";
    }
}
