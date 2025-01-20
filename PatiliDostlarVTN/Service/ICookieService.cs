namespace PatiliDostlarVTN.Service
{
    public interface ICookieService
    {
        void WriteCookie(string key, string value, int? expireTime);
        string ReadCookie(string key);
        void DeleteCookie(string key);
        void UpdateCookie(string key, string newValue, int? expireTime);
    }

}
