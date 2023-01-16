using UnityEngine;

public class  Platform : MonoBehaviour
{
    public int _x;
    public int _y;
    private bool _placed;

    public void SetCoordinates(int x, int y)
    {
        _x = x;
        _y = y;
        _placed = true;
    }

    public int GetX() 
    {
        return _x;
    }

    public int GetY() 
    {
        return _y;
    }

    // Keep track of placement to avoid clearing the 0,0 spot.
    public bool GetHasBeenPlaced() 
    {
        return _placed;
    }
}
