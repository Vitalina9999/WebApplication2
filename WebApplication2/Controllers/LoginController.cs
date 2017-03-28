using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using InstagramWrapper.EndPoints;
using InstagramWrapper.Model;
using InstagramWrapper.Service;

namespace WebApplication2.Controllers
{
    public class LoginController : Controller
    {
        InstaConfig instaConfig = new InstaConfig();

        public LoginController()
        {
            instaConfig.redirect_uri = "http://localhost:18381/Login/InstagramReturnUrl";
            instaConfig.client_secret = "33cd8a80a17a43e18878ef87d9fa8466";
            instaConfig.client_id = "518b584b85ed47ffbb6c1f33d621d275";
        }

        [HttpGet]
        public ActionResult Like()
        {
            string basicUrl = "https://api.instagram.com/";


            string authUrl = String.Format("{0}oauth/authorize/?client_id={1}&redirect_uri={2}&response_type=code", basicUrl, instaConfig.client_id, instaConfig.redirect_uri);

            return Redirect(authUrl);
        }

        [HttpGet]
        public ActionResult InstagramReturnUrl(string code)
        {
            InstagramAuth instagramAuth = new InstagramAuth();
            
            OuthUser user = instagramAuth.GetAccessToken(code, instaConfig); // get user who loged in with an access_token
            string access_token = user.access_token;


            Likes likes = new Likes();

            string idImage = "1476852486660100934_2015123882";
            likes.LikeMedia(idImage, access_token);

            return View();
        }
    }
}