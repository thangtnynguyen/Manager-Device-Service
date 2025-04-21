using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager_Device_Service.Domains.Model.Identity.User
{
    public class AssignUserToRoleRequest
    {

        public int UserId { get; set; }
        public List<string>? RoleNames { get; set; }

        //public List<int>? RoleIds { get; set; }



    }
}
