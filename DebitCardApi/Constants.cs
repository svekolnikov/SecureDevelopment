using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace DebitCardApi
{
    public class Constants
    {
        public const string COOKIEKEY = "jwt";
        public const string ISSUER = AUDIENCE;
        public const string AUDIENCE = "http://localhost:5000/";
        public const int LIFETIME = 60; //minutes
        public const string KEY = "JhvUvhuhJBkJBhLjhVytgjvBKJnijgtkDyVgUKGvtyCrtXChgcgCHGCHGcjhvjjhLghfdesgcgvJHVJFJ";

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
