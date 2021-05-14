using System.ComponentModel.DataAnnotations;

namespace CluedIn.Crawling.Greenhouse.Core.Models
{
    public class EntityWithIdKey
    {
        [Key]
        public long id { get; set; }

        public string GetKey()
        {
            return id.ToString();
        }
    }
}
