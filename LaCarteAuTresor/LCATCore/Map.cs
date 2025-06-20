namespace LaCarteAuTresor
{
    public class Map
    {

        int _horizontalLength = 0;
        int _verticalLength = 0;
        List<Case> _mapCases = new List<Case>();

        #region Properties
        public int HorizontalLength
        {
            get { return _horizontalLength; }
            set
            {
                _horizontalLength = value;
            }
        }
        public int VerticalLength
        {
            get { return _verticalLength; }
            set
            {
                _verticalLength = value;
            }
        }
        //parameters properties
        public static char LetterReference
        {
            get { return 'C'; }
        }
        public static int HorizontalLengthPosition
        {
            get { return 1; }
        }
        public static int VerticalLengthPosition
        {
            get { return 2; }
        }
        #endregion
        public List<Case> MapCases
        {
            get { return _mapCases; }
        }

        public List<Mountain> Mountains
        {
            get { return _mapCases.Where(case_ => case_.GetType() == typeof(Mountain)).Cast<Mountain>().ToList(); }
        }

        public List<Adventurer> Adventurers
        {
            get { return [.. _mapCases.Where(case_ => case_.GetType() == typeof(Adventurer)).OrderBy(case_ => (case_ as Adventurer)?.AdventurerOrderPosition).Cast<Adventurer>()]; }
        }

        public List<TreasureLocation> TreasureLocations
        {
            get { return _mapCases.Where(case_ => case_.GetType() == typeof(TreasureLocation)).Cast<TreasureLocation>().ToList(); }
        }
        public Map(int horizontalLength, int verticalLength)
        {
            _horizontalLength = horizontalLength;
            _verticalLength = verticalLength;
        }


    }
}