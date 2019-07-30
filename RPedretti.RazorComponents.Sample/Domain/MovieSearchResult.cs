namespace RPedretti.RazorComponents.Sample.Domain
{
    public class MovieSearchResult
    {
        #region Properties

        public string Response { get; set; }
        public Movie[] Search { get; set; }
        public string totalResults { get; set; }

        #endregion Properties
    }
}
