using UnityEngine;

public abstract class Tile : MonoBehaviour
{
    // protected int row;
    // protected int column;
    public int row;
    public int column;


    protected abstract bool IsAvailable();

    public abstract void Display();

    public abstract void MoveHere(Being beingToMove, bool setStartingPosition = false);

    public abstract void Hide();

    public abstract void Occupy();

    public abstract void Unoccupy();

    public void SetCoordinates(int row, int column)
    {
        this.row = row;
        this.column = column;
    }
}
