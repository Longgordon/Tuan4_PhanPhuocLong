using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tuan3_PhanPhuocLong.Models;
using PagedList;

namespace Tuan3_PhanPhuocLong.Controllers
{
    public class RubikController : Controller
    {
        // GET: Rubik
        MydataDataContext data = new MydataDataContext();
        public ActionResult Index(int? page, string searching)
        {
            if (page == null) page = 1;
            var all_Rubik = data.Rubiks.Where(p => p.ten.Contains(searching) || searching == null).OrderBy(p => p.id).ToList();
            int pageSize = 3;
            int pageNum = page ?? 1;
            return View(all_Rubik.ToPagedList(pageNum, pageSize));
        }
        public ActionResult Detail(int id)
        {
            var D_sach = data.Rubiks.Where(m => m.id == id).First();
            return View(D_sach);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection, Rubik s)
        {
            var E_maloai = Convert.ToInt32(collection["maloai"]);
            var E_ten = collection["ten"];
            var E_mota = collection["mota"];
            var E_hang = collection["hang"];
            var E_gia = Convert.ToDecimal(collection["gia"]);
            var E_hinh = collection["hinh"];
            var E_ngaycapnhat = Convert.ToDateTime(collection["ngaycapnhat"]);
            var E_soluongton = Convert.ToInt32(collection["soluongton"]);
            if (string.IsNullOrEmpty(E_ten))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                s.maloai = E_maloai; 
                s.ten = E_ten.ToString();
                s.mota = E_mota.ToString();
                s.hang = E_hang.ToString();
                s.hinh = E_hinh.ToString();
                s.ngaycapnhat = E_ngaycapnhat;
                s.gia = E_gia;
                s.soluongton = E_soluongton;
                data.Rubiks.InsertOnSubmit(s);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Create();
        }
        public string ProcessUpload(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return "";
            }
            file.SaveAs(Server.MapPath("~/Content/images/" + file.FileName));
            return "/Content/images/" + file.FileName;
        }
        public ActionResult Edit(int id)
        {
            var E_rubik = data.Rubiks.First(m => m.id == id);
            return View(E_rubik);
        }
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var E_rubik = data.Rubiks.First(m => m.id == id);
            var E_ten = collection["ten"];
            var E_mota = collection["mota"];
            var E_hinh = collection["hinh"];
            var E_hang = collection["hang"];
            var E_giaban = Convert.ToDecimal(collection["giaban"]);
            var E_ngaycapnhat = Convert.ToDateTime(collection["ngaycatnhat"]);
            var E_soluongton = Convert.ToInt32(collection["soluongton"]);
            E_rubik.id = id;
            if (string.IsNullOrEmpty(E_ten))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                E_rubik.ten = E_ten.ToString();
                E_rubik.mota = E_mota.ToString();
                E_rubik.hang = E_hang.ToString();
                E_rubik.hinh = E_hinh;
                E_rubik.gia = E_giaban;
                E_rubik.ngaycapnhat = E_ngaycapnhat;
                E_rubik.soluongton = E_soluongton;
                UpdateModel(E_rubik);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Edit(id);
        }
        public ActionResult Delete(int id)
        {
            var D_rubik = data.Rubiks.First(m => m.id == id);
            return View(D_rubik);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var D_rubik = data.Rubiks.Where(m => m.id == id).First();
            data.Rubiks.DeleteOnSubmit(D_rubik);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }
    }
}