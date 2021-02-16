using System.IO;

namespace HomeCloud.Maps.Infrastructure.GPX.Model
{
    interface IGPXSerializer
    {
        Route Deserialize(Stream stream);
    }
}