using System.Collections.Generic;
namespace Cappuccino.Core.Network.Methods.Users {

    /* 
     * Mark: documentation [https://vk.com/dev/users.get]
     */
    public class Get : ApiRequest<Models.Users.GetResponse> {
        public IEnumerable<string> Fields { set => AddParam("fields", value); }
        public string NameCase { set => AddParam("name_case", value); }
        public IEnumerable<int> UserIds { set => AddParam("user_ids", value); }

        public Get() : base("users.get") {}
    }

    /* 
     * Mark: documentation [https://vk.com/dev/users.search]
     */
    public class Search : ApiRequest<Models.Users.SearchResponse> {
        public string Query { set => AddParam("q", value); }
        public int Count { set => AddParam("count", value); }
        public int Offset { set => AddParam("offset", value); }
        public int sort { set => AddParam("sort", value); }
        public IEnumerable<string> Fields { set => AddParam("fields", value); }
        public IEnumerable<string> FromList { set => AddParam("from_list", value); }
        public int GroupId { set => AddParam("group_id", value); }
        public int City { set => AddParam("city", value); }
        public int Country { set => AddParam("country", value); }
        public string Hometown { set => AddParam("hometown", value); }
        public int UniversityCountry { set => AddParam("university_country", value); }
        public int University { set => AddParam("university", value); }
        public int UniversityYear { set => AddParam("university_year", value); }
        public int UniversityFaculty { set => AddParam("university_faculty", value); }
        public int UniversityChair { set => AddParam("university_chair", value); }
        public int Sex { set => AddParam("sex", value); }
        public int Status { set => AddParam("status", value); }
        public int AgeFrom { set => AddParam("age_from", value); }
        public int AgeTo { set => AddParam("age_to", value); }
        public int BirthDay { set => AddParam("birth_day", value); }
        public int BirthMonth { set => AddParam("birth_month", value); }
        public int BirthYear { set => AddParam("birth_year", value); }
        public int Online { set => AddParam("online", value); }
        public int HasPhoto { set => AddParam("has_photo", value); }
        public int SchoolCountry { set => AddParam("school_country", value); }
        public int SchoolCity { set => AddParam("school_city", value); }
        public int SchoolClass { set => AddParam("school_class", value); }
        public int School { set => AddParam("school", value); }
        public int SchoolYear { set => AddParam("school_year", value); }
        public int Religion { set => AddParam("religion", value); }
        public int Company { set => AddParam("company", value); }
        public int Position { set => AddParam("position", value); }

        public Search() : base("users.search") {}
    }

}