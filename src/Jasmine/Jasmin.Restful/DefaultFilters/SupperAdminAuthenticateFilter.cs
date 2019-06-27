using System;
using System.Collections.Generic;
using System.Text;
using Jasmine.Restful.DefaultFilters;

namespace Jasmine.Restful.DefaultServices
{
    public class SupperAdminAuthenticateFilter : AuthenticateFilter
    {
        public SupperAdminAuthenticateFilter() : base(AuthenticateLevel.SupperAdmin)
        {
        }
    }
}
