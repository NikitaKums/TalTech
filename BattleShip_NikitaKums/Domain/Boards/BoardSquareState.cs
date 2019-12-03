namespace Domain.Boards
{
    public enum BoardSquareState
    {
        Water, //empty square
        Carrier, //numbers will represent ships on the board when printed to the user
        BattleShip,
        Submarine,
        Cruiser,
        Patrol,
        Hit,
        Miss,
        Neighbour,
        Dead //for ship placing
    }
}