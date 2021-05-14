using System;
using System.ComponentModel.DataAnnotations;

namespace CluedIn.Crawling.Greenhouse.Core.Models
{
    public class Offer : EntityWithIdKey
    {
      
        public string version { get; set; }
        public string application_id { get; set; }
        public string job_id { get; set; }
        public string candidate_id { get; set; }
        public Opening opening { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string sent_at { get; set; }
        public DateTime resolved_at { get; set; }
        public string starts_at { get; set; }
        public string status { get; set; }
        public Custom_Fields custom_fields { get; set; }
        public Keyed_Custom_Fields keyed_custom_fields { get; set; }
    }

    public partial class Custom_Fields
    {

        public string favorite_station { get; set; }
        public string best_seasons { get; set; }
        public string start_date { get; set; }
        public string willing_to_negotiate { get; set; }
        public string salary { get; set; }
        public string notes { get; set; }
    }

    public partial class Keyed_Custom_Fields
    {
        public Keyed_Custom_Field time_type { get; set; }
        public Keyed_Custom_Field favorite_station { get; set; }
        public Keyed_Custom_Field best_seasons { get; set; }
        public Keyed_Custom_Field start_date { get; set; }
        public Keyed_Custom_Field will_negotiate { get; set; }
        public Keyed_Custom_Field salary { get; set; }
        public Keyed_Custom_Field notes { get; set; }
    }

    public class Keyed_Custom_Field
    {
        public string name { get; set; }
        public string type { get; set; }
        public string value { get; set; }
    }
    

   
}
