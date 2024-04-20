using System.ComponentModel;

namespace FUNFOX.NET5.Application.Enums
{
    public enum UploadType : byte
    {
        [Description(@"Images\Classes")]
        Product,

        [Description(@"Images\ProfilePictures")]
        ProfilePicture,

        [Description(@"Documents")]
        Document
    }
}