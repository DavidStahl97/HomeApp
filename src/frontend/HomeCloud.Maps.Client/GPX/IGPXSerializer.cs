using HomeCloud.Maps.Client.GPX.Model;
using System.IO;

namespace HomeCloud.Maps.Client.GPX
{
    public interface IGPXSerializer
    {
        Route Deserialize(Stream stream);
    }
}