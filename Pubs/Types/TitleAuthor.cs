namespace Pubs.Types
{
    public class TitleAuthor
    {
        public TitleAuthor(string authorId, string titleId, int sortOrder, int royaltyPer)
        {
            this.AuthorId = authorId;
            this.TitleId = titleId;
            this.SortOrder = sortOrder;
            this.RoyaltyPer = royaltyPer;
        }

        public string AuthorId { get; set; }
        public string TitleId { get; set; }
        public int SortOrder { get; set; }
        public int RoyaltyPer { get; set; }
    }
}
