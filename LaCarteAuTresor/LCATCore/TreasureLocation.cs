namespace LaCarteAuTresor;
public class TreasureLocation : Case
{
    int _remainingTreasure;
    #region Properties
    public int RemainingTreasure
    {
        get { return _remainingTreasure; }
        set
        {
            _remainingTreasure = value;
        }
    }
    public static int HorizontalLengthPosition
    {
        get { return 1; }
    }
    public static int VerticalLengthPosition
    {
        get { return 2; }
    }

    public static int RemainingTreasurePosition
    {
        get { return 3; }
    }
    #endregion
    public TreasureLocation(int horizontalOffset, int verticalOffset, int remainingTreasure) : base(horizontalOffset, verticalOffset)
    {
        _remainingTreasure = remainingTreasure;

        LetterReference = 'T';
    }
}