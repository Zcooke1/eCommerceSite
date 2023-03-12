namespace eCommerceSite.Models
{
    public class ProductCatalogViewModel
    {
        public ProductCatalogViewModel(List<Product> products, int lastPage, int currPage)
        {
            Products = products;
            LastPage = lastPage;
            CurrentPage = currPage;
        }
        public List<Product> Products { get; set; } 

        /// <summary>
        /// The last page of the catalog. Calculated by
        /// having a total number of products divided by
        /// products per page
        /// </summary>
        public int LastPage { get; set; }
        /// <summary>
        /// The current page the user is viewing.
        /// </summary>
        public int CurrentPage { get; set; }


    }
}
