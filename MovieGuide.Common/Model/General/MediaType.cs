using System.Runtime.Serialization;

namespace MovieGuide.Common.Model.General
{
    public enum MediaType
    {
        Unknown,

        [EnumMember(Value = "movie")]
        Movie = 1,

        [EnumMember(Value = "tv")]
        TvShow = 2,

        [EnumMember(Value = "person")]
        Person = 3
    }
}
