using System;
using System.Collections.Generic;
using Newtonsoft.Json;

// ReSharper disable InconsistentNaming
namespace CluedIn.Crawling.Greenhouse.Core.Models
{
    public class Candidate : EntityWithIdKey
    {
        public Candidate()
        {

        }

     
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string company { get; set; }
        public string title { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime last_activity { get; set; }
        public bool is_private { get; set; }

        [JsonIgnore]
        [AlwaysNull]
        private string photo_url { get; set; }

        public List<Attachment> attachments { get; set; }
        public List<string> application_ids { get; set; }
        public List<Phone_Numbers> phone_numbers { get; set; }
        public List<Address> addresses { get; set; }
        public List<Email_Addresses> email_addresses { get; set; }
        public List<Website_Addresses> website_addresses { get; set; }
        public List<dynamic> social_media_addresses { get; set; }
        public Recruiter recruiter { get; set; }
        public Coordinator coordinator { get; set; }
        public bool can_email { get; set; }
        public List<string> tags { get; set; }
        public List<Application> applications { get; set; }
        public List<Education> educations { get; set; }
        public List<Employment> employments { get; set; }
        public List<string> linked_user_ids { get; set; }
        public Custom_Fields custom_fields { get; set; }
        public Keyed_Custom_Fields keyed_custom_fields { get; set; }
    }

    public class Education : EntityWithIdKey
    {
        public string school_name { get; set; }
        public string degree { get; set; }
        public string discipline { get; set; }
        public DateTime? start_date { get; set; }
        public DateTime? end_date { get; set; }
    }

    public partial class Custom_Fields
    {
        public string desired_salary { get; set; }
        public bool work_remotely { get; set; }
        public string graduation_year { get; set; }
    }

    public partial class Keyed_Custom_Fields
    {
        public Desired_Salary desired_salary { get; set; }
        public Work_Remotely work_remotely { get; set; }
        public Graduation_Year_1 graduation_year_1 { get; set; }
    }

    public class Desired_Salary
    {
        public string name { get; set; }
        public string type { get; set; }
        public string value { get; set; }
    }

    public class Work_Remotely
    {
        public string name { get; set; }
        public string type { get; set; }
        public bool value { get; set; }
    }

    public class Graduation_Year_1
    {
        public string name { get; set; }
        public string type { get; set; }
        public string value { get; set; }
    }

    public class Phone_Numbers
    {
        public string value { get; set; }
        public string type { get; set; }
    }

    public class Address
    {
        public string value { get; set; }
        public string type { get; set; }
    }

    public class Email_Addresses
    {
        public string value { get; set; }
        public string type { get; set; }
    }

    public class Website_Addresses
    {
        public string value { get; set; }
        public string type { get; set; }
    }

    public class Employment : EntityWithIdKey
    {
        public string company_name { get; set; }
        public string title { get; set; }
        public DateTime? start_date { get; set; }
        public DateTime? end_date { get; set; }
    }

    public class Notes
    {
        public string name { get; set; }
        public string type { get; set; }
        public string value { get; set; }
    }
}
