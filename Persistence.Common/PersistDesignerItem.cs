using System;
using System.Linq;

namespace Persistence.Common
{
    public class PersistDesignerItem : DesignerItemBase
    {
        public PersistDesignerItem(int id, double left, double top, string hostUrl) : base(id, left, top)
        {
            this.HostUrl = hostUrl;
        }

        public string HostUrl { get; set; }

    }
}
