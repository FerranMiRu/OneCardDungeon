using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTile : Tile
{
    protected override bool IsAvailable()
    {
        return false;
    }

    public override void Display()
    {
        ;
    }

    public override void Hide()
    {
        ;
    }

    public override void MoveHere(Being beingToMove, bool setStartingPosition = false)
    {
        ;
    }

    public override void Occupy()
    {
        ;
    }

    public override void Unoccupy()
    {
        ;
    }
}
