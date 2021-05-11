using System;

namespace CluedIn.Crawling.Greenhouse.Core.Models
{
    public class User : EntityWithIdKey
    {
       
        public string name { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string primary_email_address { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime created_at { get; set; }
        public bool disabled { get; set; }
        public bool site_admin { get; set; }
        public string[] emails { get; set; }
        public string employee_id { get; set; }
        public int[] linked_candidate_ids { get; set; }
    }
}
