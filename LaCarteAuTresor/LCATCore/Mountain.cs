namespace LaCarteAuTresor;

public class Mountain : Case
{
    #region parameters properties
    public static int HorizontalLengthPosition
    {
        get { return 1; }
    }
    public static int VerticalLengthPosition
    {
        get { return 2; }
    }
    #endregion
    // public Mountain(string fileLine)
    // {
    //     var splitedFileLine = fileLine.Split(" - ");
    //     Mountain(splitedFileLine[1], splitedFileLine[2]);
    // }
    public Mountain(int HorizontalOffset, int VerticalOffset) : base(HorizontalOffset, VerticalOffset)
    {
        LetterReference = 'M';
    }
}