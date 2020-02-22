using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using manlogin_pcaptcha.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace manlogin_pcaptcha.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Form(FormModel model)
        {
            ViewData["message"] = "";
            ViewData["result"] = false;
            if (model.pcaptcha !="")
            {
                var uid 		= "0x4e2e"; // کد یکتای کپچای شما در سایت من لاکین
                var secretKey 	= "e8414ae66a37806bae1166a6297ce302a393e70b8e444b37abe20fe4e277779c"; // کلید خصوصی
                var url        = "https://manlogin.com/captcha/cheack/v1/"+uid+"/"+secretKey+"/"+model.pcaptcha;

                var req = new HttpRequestMessage(HttpMethod.Get,url);
                var client = new HttpClient();
                var res = client.GetAsync(url);
                dynamic response = res.Result.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject(response);
                if (result["success"] != "False")
                {
                    // Do more by Data Posted
                    ViewData["message"] = "با موفقیت کپچای شما تایید شد";
                    ViewData["result"] = true;
                }else{
                    ViewData["message"] = "کپچای من ربات نیستم رو به درستی کامل کنید";
                }
            }else{
                ViewData["message"] = "خطایی پیش اومده";
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
