namespace money_tracker.Application.Dtos.Response
{
    public abstract class PaginatedResponse
    {
        public PaginatedResponse(long total, int perPage, int page)
        {
            Page = page;
            PerPage = perPage;
            TotalItems = total;
            TotalPages = (int)Math.Ceiling((double)total / perPage);
        }

        public long TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int Page { get; set; }
        public int PerPage { get; set; }
    }
}
