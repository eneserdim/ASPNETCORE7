using Core_Web_Project.Models;
using Core_Web_Project.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Core_Web_Project.Controllers
{
    [Authorize(Roles =UserRoles.Role_Admin)]
    public class KitapController : Controller
    {

        private readonly IKitapRepository _kitapRepository;
        private readonly IKitapTuruRepository _kitapTuruRepository;
        public readonly IWebHostEnvironment _webHostEnvironment; //Dosya Yolu Tutmak İçin

        public KitapController(IKitapRepository kitapRepository, IKitapTuruRepository kitapTuruRepository, IWebHostEnvironment webHostEnvironment)
        {
            _kitapRepository = kitapRepository;
            _kitapTuruRepository = kitapTuruRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            //List<Kitap> objKitapList = _kitapRepository.GetAll().ToList();
            List<Kitap> objKitapList = _kitapRepository.GetAll(IncludeProps:"KitapTuru").ToList();
            return View(objKitapList);
        }


        public IActionResult EkleGuncelle(int? id)
        {
            IEnumerable<SelectListItem> KitapTuruList = _kitapTuruRepository.GetAll().Select(k => new SelectListItem
            {
                Text = k.Ad,
                Value = k.Id.ToString()
            });
            ViewBag.KitapTuruList = KitapTuruList;
            if (id == null || id == 0)
            {
                return View();
            }
            else
            {
                Kitap? kitapVt = _kitapRepository.Get(u => u.Id == id);
                if (kitapVt == null)
                {
                    return NotFound();
                }
                return View(kitapVt);
            }
        }

        [HttpPost]
        public IActionResult EkleGuncelle(Kitap kitap, IFormFile? file)
        {

            if (ModelState.IsValid)
            {
                //Fotoğraf Yolu ve Adı Belirleme
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string kitapPath = Path.Combine(wwwRootPath, @"img");

                if (file != null)
                {
                    using (var fileStream = new FileStream(Path.Combine(kitapPath, file.FileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    kitap.ResimUrl = @"\img\" + file.FileName;
                }


                if (kitap.Id == 0) 
                {
                    _kitapRepository.Ekle(kitap);
                    TempData["basarili"] = "Yeni Kitap Başarıyla Oluşturuldu.";
                }
                else
                {
                    _kitapRepository.Guncelle(kitap);
                    TempData["basarili"] = "Kitap Güncelleme Başarılı.";
                }
                _kitapRepository.Kaydet();
               
                return RedirectToAction("Index");
            }
            return View();

        }



        //public IActionResult Guncelle(int? id)
        //{
        //    if (id==null || id==0)
        //    {
        //        return NotFound();
        //    }
        //    Kitap? kitapVt = _kitapRepository.Get(u=>u.Id==id);
        //    if (kitapVt == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(kitapVt);
        //}

        //[HttpPost]
        //public IActionResult Guncelle(Kitap kitap)
        //{


        //    if (ModelState.IsValid)
        //    {
        //        _kitapRepository.Guncelle(kitap);
        //        _kitapRepository.Kaydet();
        //        TempData["basarili"] = "Yeni Kitap Başarıyla Güncellendi.";
        //        return RedirectToAction("Index");
        //    }
        //    return View();

        //}


        public IActionResult Sil(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Kitap? kitapVt = _kitapRepository.Get(u => u.Id == id);
            if (kitapVt == null)
            {
                return NotFound();
            }
            return View(kitapVt);
        }

        [HttpPost, ActionName("Sil")]
        public IActionResult SilPOST(int? id)
        {
            Kitap? kitap = _kitapRepository.Get(u => u.Id == id);
            if (kitap == null)
            {
                return NotFound();
            }
            _kitapRepository.Sil(kitap);
            _kitapRepository.Kaydet();
            TempData["basarili"] = "Yeni Kitap Başarıyla Silindi.";
            return RedirectToAction("Index","Kitap");

        }

    }
}
