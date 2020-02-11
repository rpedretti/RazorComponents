namespace RPedretti.RazorComponents.Sample.Shared.Domain
{
    public class MovieSearchResult
    {
        #region Properties

        public string? Response { get; set; }
        public Movie[] Search { get; set; } = new Movie[0];
        public string totalResults { get; set; } = "0";

        #endregion Properties
    }
}
