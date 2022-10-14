namespace CVBuilder.Application.Models.Pagination
{
    public class PaginationMetaData
    {
        public PaginationMetaData(int totalItems, int currentPage, int pageSize)
        {
            TotalItems = totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages =  (int) Math.Ceiling(TotalItems / (double)pageSize);
            HasMoreData = CurrentPage < TotalPages;
        }

        public int TotalPages { get; set; }
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public bool HasMoreData { get; set; }


    }
}
