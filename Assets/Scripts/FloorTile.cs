using UnityEngine.UI;

public class FloorTile : Tile
{
    private bool isOccupied = false;
    private Button tileButton;


    private void MovePlayerHere()
    {
        MoveHere(Player.instance);

        Board.instance.ResetTiles();    

        Player.instance.NextAction();
    }

    protected override bool IsAvailable()
    {
        return !isOccupied;
    }

    public override void Display()
    {
        tileButton.interactable = IsAvailable();
    }

    public override void Hide()
    {
        tileButton.interactable = false;
    }

    public override void MoveHere(Being beingToMove, bool setStartingPosition = false)
    {
        Occupy();
        beingToMove.MoveTo(row, column, tileButton.transform.position, setStartingPosition);
    }

    public override void Occupy()
    {
        isOccupied = true;
    }

    public override void Unoccupy()
    {
        isOccupied = false;
    }

    void Awake()
    {
        tileButton = gameObject.GetComponent<Button>();
    }

    void Start() 
    {
        tileButton.onClick.AddListener(MovePlayerHere);
    }
}
