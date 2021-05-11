using System;
using System.Collections.Generic;
using System.Text;
// ReSharper disable InconsistentNaming

namespace CluedIn.Crawling.Greenhouse.Core.Models
{
   

    public class Application
    {
        public int id { get; set; }
        public int candidate_id { get; set; }
        public bool prospect { get; set; }
        public DateTime applied_at { get; set; }
        public object rejected_at { get; set; }
        public DateTime last_activity_at { get; set; }
        public Location location { get; set; }
        public Source source { get; set; }
        public Credited_To credited_to { get; set; }
        public object rejection_reason { get; set; }
        public object rejection_details { get; set; }
        public Job[] jobs { get; set; }
        public int? job_post_id { get; set; }
        public string status { get; set; }
        public Current_Stage current_stage { get; set; }
        public Answer[] answers { get; set; }
        public Prospective_Office prospective_office { get; set; }
        public Prospective_Department prospective_department { get; set; }
        public Prospect_Detail prospect_detail { get; set; }
        public Custom_Fields custom_fields { get; set; }
        public Keyed_Custom_Fields keyed_custom_fields { get; set; }
        public Attachment[] attachments { get; set; }
    }



    public class Source
    {
        public int id { get; set; }
        public string public_name { get; set; }
    }

    public partial class Credited_To
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string name { get; set; }
        public string employee_id { get; set; }
    }

    public class Current_Stage
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class Prospective_Office
    {
        public object primary_contact_user_id { get; set; }
        public object parent_id { get; set; }
        public string name { get; set; }
        public Location1 location { get; set; }
        public int id { get; set; }
        public object external_id { get; set; }
        public object[] child_ids { get; set; }
    }

    public class Location1
    {
        public string name { get; set; }
    }

    public class Prospective_Department
    {
        public object parent_id { get; set; }
        public string name { get; set; }
        public int id { get; set; }
        public object external_id { get; set; }
        public object[] child_ids { get; set; }
    }

    public class Prospect_Detail
    {
        public Prospect_Pool prospect_pool { get; set; }
        public Prospect_Stage prospect_stage { get; set; }
        public Prospect_Owner prospect_owner { get; set; }
    }

    public class Prospect_Pool
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class Prospect_Stage
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class Prospect_Owner
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public partial class Custom_Fields
    {
        public string application_custom_test { get; set; }
    }

    public partial class Keyed_Custom_Fields
    {
        public Application_Custom_Test application_custom_test { get; set; }
    }

    public class Application_Custom_Test
    {
        public string name { get; set; }
        public string type { get; set; }
        public string value { get; set; }
    }


    public class Answer
    {
        public string question { get; set; }
        public string answer { get; set; }
    }

    public class Attachment
    {
        public string filename { get; set; }
        public string url { get; set; }
        public string type { get; set; }
    }


}
