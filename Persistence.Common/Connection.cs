using System;
using System.Linq;

namespace Persistence.Common
{
    public class Connection : PersistableItemBase
    {
        public Connection(int id, Guid sourceKey, Orientation sourceOrientation, 
            Type sourceType, Guid sinkKey, Orientation sinkOrientation, Type sinkType) : base()
    	{
			this.Id = id;
            this.SourceKey = sourceKey;
            this.SourceOrientation = sourceOrientation;
            this.SourceType = sourceType;
            this.SinkKey = sinkKey;
            this.SinkOrientation = sinkOrientation;
            this.SinkType = sinkType;
        }
		
        public Guid SourceKey { get; set; }
        public Orientation SourceOrientation { get; set; }
        public Type SourceType { get; set; }
        public Guid SinkKey { get; set; }
        public Orientation SinkOrientation { get;  set; }
        public Type SinkType { get; set; }
    }

}
