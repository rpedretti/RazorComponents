namespace RPedretti.RazorComponents.Sample.Shared.Domain
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
