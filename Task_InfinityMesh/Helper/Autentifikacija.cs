using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Task_InfinityMesh.EF;
using Task_InfinityMesh.Models;

namespace Task_InfinityMesh.Helper
{
    public class Autentifikacija
    {
        private const string LogiraniKorisnik = "logirani_korisnik";

        public static void PokreniNovuSesiju(string user, HttpContextBase context, bool zapamtiPassword)
        {
            context.Session.Add(LogiraniKorisnik, user);

            if (zapamtiPassword)
            {
                HttpCookie cookie = new HttpCookie("_mvc_session", user != null ? user : "");
                cookie.Expires = DateTime.Now.AddDays(10);
                context.Response.Cookies.Add(cookie);
            }
        }

        public static string GetLogiraniKorisnik(HttpContextBase context)
        {
            string korisnik = (string)context.Session[LogiraniKorisnik];

            if (korisnik != null)
                return korisnik;

            HttpCookie cookie = context.Request.Cookies.Get("_mvc_session");

            if (cookie == null)
                return null;

            string user;
            try
            {
                user = cookie.Value;
            }
            catch
            {
                return null;
            }

            PokreniNovuSesiju(user, context, true);
            return user;

        }
    }
}