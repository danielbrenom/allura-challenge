using Allura.Challenge.Database.Repositories.Interfaces;

namespace Allura.Challenge.Database.Repositories.Base
{
    public class BasePaginator : IBasePaginator
    {
        public int ItemsPerPage { get; set; }
        public int Page { get; set; }

        public BasePaginator(int itemsPerPage)
        {
            ItemsPerPage = itemsPerPage;
        }
    }

    public static class BasePaginatorExtensions
    {
        public static int FirstItemOnPage(this IBasePaginator paginator)
        {
            return paginator.Page > 0 ? (paginator.Page - 1) * paginator.ItemsPerPage : 0;
        }
    }
}