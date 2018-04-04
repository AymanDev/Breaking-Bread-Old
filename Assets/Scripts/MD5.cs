using System.Linq;
using UnityEngine;

public class MD5 : MonoBehaviour
{
    public static string Md5Sum(string strToEncrypt)
    {
        var ue = new System.Text.UTF8Encoding();
        var bytes = ue.GetBytes(strToEncrypt);

        var md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
        var hashBytes = md5.ComputeHash(bytes);

        var hashString =
            hashBytes.Aggregate("", (current, t) => current + System.Convert.ToString(t, 16).PadLeft(2, '0'));

        return hashString.PadLeft(32, '0');
    }
}