using System.ComponentModel.DataAnnotations;

namespace CluedIn.Crawling.Greenhouse.Core.Models
{
    public class EntityWithIdKey
    {
        [Key]
        public int id { get; set; }

        public string GetKey()
        {
            return id.ToString();
        }
    }
}
