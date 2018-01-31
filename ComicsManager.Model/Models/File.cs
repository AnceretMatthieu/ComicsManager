namespace ComicsManager.Model.Models
{
    public class File : BaseEntity
    {
        public byte[] Path { get; set; }

        public string Type { get; set; }
    }
}
