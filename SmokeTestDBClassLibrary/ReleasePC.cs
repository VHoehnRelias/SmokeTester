using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokeTestDBClassLibrary
{
    using System;
    using System.Collections.Generic;

    public partial class Release
    {
        public override string ToString()
        {
            return string.Format("{0} ({1:MM/dd/yy})", this.Name, this.Date);
        }
    }
}
