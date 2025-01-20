namespace PatiliDostlarVTN.Service
{
    public class CookieService : ICookieService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CookieService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void WriteCookie(string key, string value, int? expireTime)
        {
            var context = _httpContextAccessor.HttpContext;
            CookieOptions option = new CookieOptions
            {
                Expires = expireTime.HasValue
                    ? DateTime.Now.AddMinutes(expireTime.Value)
                    : DateTime.Now.AddDays(7)
            };

            context.Response.Cookies.Append(key, value, option);
        }

        public string ReadCookie(string key)
        {
            var context = _httpContextAccessor.HttpContext;
            return context.Request.Cookies[key];
        }

        public void DeleteCookie(string key)
        {
            var context = _httpContextAccessor.HttpContext;
            context.Response.Cookies.Delete(key);
        }

        public void UpdateCookie(string key, string newValue, int? expireTime)
        {
            DeleteCookie(key);
            WriteCookie(key, newValue, expireTime);
        }
    }

}
