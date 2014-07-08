using System.Web;
using System.Web.Mvc;

namespace MVCClass_SkillTreeORM
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}