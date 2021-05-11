using System;
using System.Collections.Generic;

// ReSharper disable InconsistentNaming
namespace CluedIn.Crawling.Greenhouse.Core.Models
{
    public class Job : EntityWithIdKey
    {
        public string name { get; set; }
        public string requisition_id { get; set; }
        public string notes { get; set; }
        public bool confidential { get; set; }
        public string status { get; set; }
        public DateTime created_at { get; set; }
        public DateTime opened_at { get; set; }
        public DateTime closed_at { get; set; }
        public DateTime updated_at { get; set; }
        public bool is_template { get; set; }
        public int copied_from_id { get; set; }
        public List<Department> departments { get; set; }
        public List<Office> offices { get; set; }
        public Custom_Fields custom_fields { get; set; }
        public Keyed_Custom_Fields keyed_custom_fields { get; set; }
        public Hiring_Team hiring_team { get; set; }
        public List<Opening> openings { get; set; }
    }

    public partial class Custom_Fields
    {
        public string employment_type { get; set; }
        public string maximum_budget { get; set; }
        public Salary_Range salary_range { get; set; }
    }

    public class Salary_Range
    {
        public int min_value { get; set; }
        public int max_value { get; set; }
        public string unit { get; set; }
    }

    public partial class Keyed_Custom_Fields
    {
        public Employment_Type employment_type { get; set; }
        public Budget budget { get; set; }
        public Salary_Range1 salary_range { get; set; }
    }

    public class Employment_Type
    {
        public string name { get; set; }
        public string type { get; set; }
        public string value { get; set; }
    }

    public class Budget
    {
        public string name { get; set; }
        public string type { get; set; }
        public string value { get; set; }
    }

    public class Salary_Range1
    {
        public string name { get; set; }
        public string type { get; set; }
        public Value value { get; set; }
    }

    public class Value
    {
        public int min_value { get; set; }
        public int max_value { get; set; }
        public string unit { get; set; }
    }

    public class Hiring_Team
    {
        public List<Hiring_Managers> hiring_managers { get; set; }
        public List<Recruiter> recruiters { get; set; }
        public List<Coordinator> coordinators { get; set; }
        public List<Sourcer> sourcers { get; set; }
    }

    public class Hiring_Managers : EntityWithIdKey
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string name { get; set; }
        public string employee_id { get; set; }
    }

    public class Recruiter : EntityWithIdKey
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string name { get; set; }
        public string employee_id { get; set; }
        public bool responsible { get; set; }
    }

    public class Coordinator : EntityWithIdKey
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string name { get; set; }
        public string employee_id { get; set; }
        public bool responsible { get; set; }
    }

    public class Sourcer : EntityWithIdKey
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string name { get; set; }
        public string employee_id { get; set; }
    }

    public class Department : EntityWithIdKey
    {
        public string name { get; set; }
        public int parent_id { get; set; }
        public List<int> child_ids { get; set; }
        public string external_id { get; set; }
    }

    public class Office : EntityWithIdKey
    {
        public string name { get; set; }
        public Location location { get; set; }
        public int primary_contact_user_id { get; set; }
        public int parent_id { get; set; }
        public List<int> child_ids { get; set; }
        public string external_id { get; set; }
    }

    public class Location
    {
        public string name { get; set; }
    }

    public class Opening : EntityWithIdKey
    {
        public string opening_id { get; set; }
        public string status { get; set; }
        public DateTime opened_at { get; set; }
        public DateTime? closed_at { get; set; }
        public int? application_id { get; set; }
        public Close_Reason close_reason { get; set; }
    }

    public class Close_Reason : EntityWithIdKey
    {
        public string name { get; set; }
    }
}
