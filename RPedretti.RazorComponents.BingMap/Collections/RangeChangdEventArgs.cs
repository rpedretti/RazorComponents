namespace RPedretti.RazorComponents.BingMap.Collections
{
    public class RangeChangdEventArgs
    {
        #region Properties

        public int Ammount { get; set; }

        public int StartIndex { get; set; }

        public RangeChangeType Type { get; set; }

        #endregion Properties

        #region Constructors

        public RangeChangdEventArgs(int startIndex, int ammount, RangeChangeType type)
        {
            StartIndex = startIndex;
            Ammount = ammount;
            Type = type;
        }

        #endregion Constructors
    }
}
