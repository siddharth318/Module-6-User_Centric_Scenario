using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;
using sadfcsd.Models;
using System.Diagnostics;

namespace sadfcsd.Controllers
{

    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ITokenAcquisition tokenAcquisition;
        public HomeController(ILogger<HomeController> logger, ITokenAcquisition _tokenAcquisition)
        {
            _logger = logger;
            tokenAcquisition = _tokenAcquisition;
        }


        public async Task<string> GetStorageFile()
        {
            string blobContent = "";
            TokenAcquisitionTokenCredential tokenAcquisitionCredential = new TokenAcquisitionTokenCredential(tokenAcquisition);
            Uri blobURL = new Uri("https://quickcartstorage.blob.core.windows.net/data/text.txt");

            BlobClient b = new BlobClient(blobURL, tokenAcquisitionCredential);
            MemoryStream ms = new MemoryStream();
            b.DownloadTo(ms);
            ms.Position = 0;

            StreamReader streamReader = new StreamReader(ms);
            blobContent = streamReader.ReadToEnd();
            return blobContent;
        }

        public IActionResult Index()
        {
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