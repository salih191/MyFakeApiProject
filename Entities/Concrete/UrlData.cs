using Core.Entities;

namespace Entities.Concrete
{
    public class UrlData:IEntity
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Token { get; set; }
        public byte[] DataBytes { get; set; }
        public string Method { get; set; }
        public string Content_Type { get; set; }
    }
}