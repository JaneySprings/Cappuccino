using System.Collections.Generic;
namespace Cappuccino.Core.Network.Methods.Users {

    /* 
     * Mark: documentation [https://vk.com/dev/users.get]
     */
    public class Get : ApiRequest<Models.Users.GetResponse> {
        public Get(IEnumerable<int>? user_ids = null,
                   IEnumerable<string>? fields = null,
                   NameCases? name_case = null) : base("users.get") {

            AddParam(nameof(user_ids), user_ids);
            AddParam(nameof(fields), fields);
            AddParam(nameof(name_case), name_case.ToString());
        }
    }

    /* 
     * Mark: documentation [https://vk.com/dev/users.search]
     */
    public class Search : ApiRequest<Models.Users.SearchResponse> {
        public Search(string q,
                      int? sort = null,
                      int? offset = null,
                      int? count = null,
                      IEnumerable<string>? fields = null,
                      IEnumerable<string>? from_list = null,
                      int? group_id = null,
                      int? city = null,
                      int? country = null,
                      string? hometown = null,
                      int? university_country = null,
                      int? university = null,
                      int? university_year = null,
                      int? university_faculty = null,
                      int? university_chair = null,
                      int? sex = null,
                      int? status = null,
                      int? age_from = null,
                      int? age_to = null,
                      int? birth_day = null,
                      int? birth_month = null,
                      int? birth_year = null,
                      bool? online = null,
                      bool? has_photo = null,
                      int? school_country = null,
                      int? school_city = null,
                      int? school_class = null,
                      int? school = null,
                      int? school_year= null,
                      int? religion = null,
                      int? company = null,
                      string? position = null) : base("users.search") {

            AddParam(nameof(q), q);
            AddParam(nameof(sort), sort);
            AddParam(nameof(offset), offset);
            AddParam(nameof(count), count);
            AddParam(nameof(fields), fields);
            AddParam(nameof(from_list), from_list);
            AddParam(nameof(group_id), group_id);
            AddParam(nameof(city), city);
            AddParam(nameof(country), country);
            AddParam(nameof(hometown), hometown);
            AddParam(nameof(university_country), university_country);
            AddParam(nameof(university), university);
            AddParam(nameof(university_year), university_year);
            AddParam(nameof(university_faculty), university_faculty);
            AddParam(nameof(university_chair), university_chair);
            AddParam(nameof(sex), sex);
            AddParam(nameof(status), status);
            AddParam(nameof(age_from), age_from);
            AddParam(nameof(age_to), age_to);
            AddParam(nameof(birth_day), birth_day);
            AddParam(nameof(birth_month), birth_month);
            AddParam(nameof(birth_year), birth_year);
            AddParam(nameof(online), online);
            AddParam(nameof(has_photo), has_photo);
            AddParam(nameof(school_country), school_country);
            AddParam(nameof(school_city), school_city);
            AddParam(nameof(school_class), school_class);
            AddParam(nameof(school), school);
            AddParam(nameof(school_year), school_year);
            AddParam(nameof(religion), religion);
            AddParam(nameof(company), company);
            AddParam(nameof(position), position);
        }
    }

}