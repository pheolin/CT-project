namespace LaCarteAuTresor;
public class Case
{
    int _horizontalOffset;
    int _verticalOffset;

    #region Properties
    public char LetterReference
    {
        protected set; get;
    }
    public int HorizontalOffset
    {
        get { return _horizontalOffset; }
        set
        {
            _horizontalOffset = value;
        }
    }
    public int VerticalOffset
    {
        get { return _verticalOffset; }
        set
        {
            _verticalOffset = value;
        }
    }
    #endregion
    public Case(int horizontalOffset, int verticalOffset)
    {
        _horizontalOffset = horizontalOffset;
        _verticalOffset = verticalOffset;
    }
}