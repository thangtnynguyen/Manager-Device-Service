using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager_Device_Service.Core.Model
{
    public class EnumrableEntityIdentityRequest<T>
    {
        public IEnumerable<T?>? Ids { get; set; }
    }
}
