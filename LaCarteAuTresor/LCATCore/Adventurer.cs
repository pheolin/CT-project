namespace LaCarteAuTresor;
public class Adventurer : Case
{
    #region Fields
    string _name;
    char _direction;
    string _moves;
    int _collectedTreasures;
    int _adventurerOrderPosition;
    #endregion

    #region Properties
    public string Moves
    {
        get { return _moves; }
    }
    public int AdventurerOrderPosition
    {
        get { return _adventurerOrderPosition; }
    }
    public string Name
    {
        get { return _name; }
    }
    public char Direction
    {
        get { return _direction; }
        set
        {
            _direction = value;
        }
    }
    public int CollectedTreasures
    {
        get { return _collectedTreasures; }
        set
        {
            _collectedTreasures = value;
        }
    }
    public static int HorizontalLengthPosition
    {
        get { return 2; }
    }
    public static int VerticalLengthPosition
    {
        get { return 3; }
    }

    public static int NamePosition
    {
        get { return 1; }
    }

    public static int DirectionPosition
    {
        get { return 4; }
    }

    public static int MovesPosition
    {
        get { return 5; }
    }
    #endregion
    public Adventurer(int horizontalOffset, int verticalOffset, string name, char direction, string moves, int adventurerOrderPosition) : base(horizontalOffset, verticalOffset)
    {
        _name = name;
        _direction = direction;
        _moves = moves;
        _adventurerOrderPosition = adventurerOrderPosition;

        LetterReference = 'A';
    }


}