using Riok.Mapperly.Abstractions;

namespace SSTAlumniAssociation.AdminWebApi.Mappers;

[Mapper]
public static partial class GrpcMapper
{
    internal static DateTime ToDateTime(Google.Protobuf.WellKnownTypes.Timestamp timestamp)
    {
        return timestamp.ToDateTime();
    }

    internal static Google.Protobuf.WellKnownTypes.Timestamp ToDateTime(DateTime timestamp)
    {
        return Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(timestamp);
    }
}