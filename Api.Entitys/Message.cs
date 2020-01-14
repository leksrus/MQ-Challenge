using System.Net;

namespace Api.Entitys
{
    public class Message
    {
        public string Id { get; set; }

        public HttpStatusCode HttpStatusCode { get; set; }

        public Product Product { get; set; }
    }
}
