using System.IO;

namespace HomeCloud.Maps.Infrastructure.GPX.Model
{
    public interface IGPXSerializer
    {
        Route Deserialize(Stream stream);
    }
}