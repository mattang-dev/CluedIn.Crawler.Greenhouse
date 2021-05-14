using System;
using System.Collections.Generic;
using System.Text;
// ReSharper disable InconsistentNaming

namespace CluedIn.Crawling.Greenhouse.Core.Models
{
   

    public class Application : EntityWithIdKey
    {
        public Application()
        {

        }
       
        public string candidate_id { get; set; }
        public bool prospect { get; set; }
        public DateTime applied_at { get; set; }
        public string rejected_at { get; set; }
        public DateTime last_activity_at { get; set; }
        public Location location { get; set; }
        public Source source { get; set; }
        public Credited_To credited_to { get; set; }
        public dynamic rejection_reason { get; set; }
        public dynamic rejection_details { get; set; }
        public List<Job> jobs { get; set; }
        public string job_post_id { get; set; }
        public string status { get; set; }
        public Current_Stage current_stage { get; set; }
        public List<Answer> answers { get; set; }
        public Prospective_Office prospective_office { get; set; }
        public Prospective_Department prospective_department { get; set; }
        public Prospect_Detail prospect_detail { get; set; }
        public Custom_Fields custom_fields { get; set; }
        public Keyed_Custom_Fields keyed_custom_fields { get; set; }
        public List<Attachment> attachments { get; set; }
    }



    public class Source
    { public Source() { }
    public string id { get; set; }
        public string public_name { get; set; }
    }

    public partial class Credited_To
    { public Credited_To() { }
    public string id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string name { get; set; }
        public string employee_id { get; set; }
    }

    public class Current_Stage
    { public Current_Stage() { }
    public string id { get; set; }
        public string name { get; set; }
    }

    public class Prospective_Office
    { public Prospective_Office() { }
    public string primary_contact_user_id { get; set; }
        public string parent_id { get; set; }
        public string name { get; set; }
        public Location1 location { get; set; }
        public string id { get; set; }
        public string external_id { get; set; }
        public List<string> child_ids { get; set; }
    }

    public class Location1
    { public Location1() { }
    public string name { get; set; }
    }

    public class Prospective_Department
    { public Prospective_Department() { }
    public string parent_id { get; set; }
        public string name { get; set; }
        public string id { get; set; }
        public string external_id { get; set; }
        public List<string> child_ids { get; set; }
    }

    public class Prospect_Detail
    { public Prospect_Detail() { }
    public Prospect_Pool prospect_pool { get; set; }
        public Prospect_Stage prospect_stage { get; set; }
        public Prospect_Owner prospect_owner { get; set; }
    }

    public class Prospect_Pool
    { public Prospect_Pool() { }
    public string id { get; set; }
        public string name { get; set; }
    }

    public class Prospect_Stage
    {
    public Prospect_Stage() { }
    public string id { get; set; }
        public string name { get; set; }
    }

    public class Prospect_Owner
    { public Prospect_Owner() { }
    public string id { get; set; }
        public string name { get; set; }
    }

    public partial class Custom_Fields
    { public Custom_Fields() { }
    public string application_custom_test { get; set; }
    }

    public partial class Keyed_Custom_Fields
    { public Keyed_Custom_Fields() { }
    public Application_Custom_Test application_custom_test { get; set; }
    }

    public class Application_Custom_Test
    { public Application_Custom_Test() { }
    public string name { get; set; }
        public string type { get; set; }
        public string value { get; set; }
    }


    public class Answer
    {
        public Answer() { }
        public string question { get; set; }
        public string answer { get; set; }
    }

    public class Attachment
    {
        public Attachment(){}
        public string filename { get; set; }
        public string url { get; set; }
        public string type { get; set; }
    }


}
