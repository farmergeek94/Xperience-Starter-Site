using HBS.TransformableViews;

namespace HBS.Xperience.TransformableViews.Services
{
    public interface IEncryptionService
    {
        string DecryptString(string cipherText);
        TransformableViewInfo DecryptView(TransformableViewInfo view);
        string EncryptString(string plainText);
        TransformableViewInfo EncryptView(TransformableViewInfo view);
    }
}