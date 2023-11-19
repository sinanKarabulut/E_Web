using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EWebDataApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EWebDataApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KullaniciController : ControllerBase
    {
        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<Kullanici>> GetAll()
        {
            return Ok(new List<Kullanici>
            {
                new Kullanici{KullaniciID = 1,Ad = "sinan", Soyad = "Karabulut", KullaniciAdi = "sinan", Sifre = "12345"},
                new Kullanici{KullaniciID = 2,Ad = "sinan1", Soyad = "Karabulut", KullaniciAdi = "sinan1", Sifre = "12345"},
                new Kullanici{KullaniciID = 3,Ad = "sinan2", Soyad = "Karabulut", KullaniciAdi = "sinan2", Sifre = "12345"}
            });
        }
    }
}
