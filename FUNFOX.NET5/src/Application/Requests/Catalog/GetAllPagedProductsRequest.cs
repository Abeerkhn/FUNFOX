namespace FUNFOX.NET5.Application.Requests.Catalog
{
    public class GetAllPagedClassesRequest : PagedRequest
    {
        public string SearchString { get; set; }
    }
}