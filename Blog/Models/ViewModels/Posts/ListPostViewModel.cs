namespace Blog.Models.ViewModels.Posts
{
    public class ListPostViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public DateTime LastUpdate { get; set; }
        public string Category { get; set; }
        public string Author { get; set; }
    }
}
