using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tuan3_PhanPhuocLong.Models;

namespace Tuan3_PhanPhuocLong.Controllers
{
    public class GiohangController : Controller
    {
        // GET: Giohang
        MydataDataContext data = new MydataDataContext();
        public List<Giohang> Laygiohang()
        {
            List<Giohang> lstGiohang= Session ["GioHang"] as List<Giohang>;
            if (lstGiohang == null)
            {
                lstGiohang = new List<Giohang>();
                Session["GioHang"] = lstGiohang;
            }
            return lstGiohang;
        }
        public ActionResult Themgiohang(int id, string strURL)
        {
            List<Giohang> lstGiohang = Laygiohang();
            Giohang sanpham = lstGiohang.Find(p => p.idrb == id);
            if(sanpham == null)
            {
                sanpham = new Giohang(id);
                lstGiohang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.soluong++;
                return Redirect(strURL);
            }
        }
        private int Tongsoluongsp()
        {
            int tsl = 0;
            List<Giohang> lstGiohang = Session["GioHang"] as List<Giohang>;
            if(lstGiohang != null)
            {
                tsl = lstGiohang.Count;
            }
            return tsl;
        }
        private int TongSoLuong()
        {
            int tsl = 0;
            List<Giohang> lstGiohang = Session["GioHang"] as List<Giohang>;
            if (lstGiohang != null)
            {
                tsl = lstGiohang.Sum(n => n.soluong);
            }
            return tsl;
        }
        private double Tongtien()
        {
            double tt = 0;
            List<Giohang> lstGiohang = Session["GioHang"] as List<Giohang>;
            if (lstGiohang != null)
            {
                tt= lstGiohang.Sum(n=>n.thanhtien);
            }
            return tt;
        }
        public ActionResult Giohang()
        {
            List <Giohang> lstGiohang = Laygiohang();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = Tongtien();
            ViewBag.Tongsoluongsanpham = Tongsoluongsp();
            return View(lstGiohang);
        }
        public ActionResult GiohangPartial()
        {
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = Tongtien();
            ViewBag.Tongsoluongsanpham = Tongsoluongsp();
            return PartialView();
        }
        public ActionResult Xoagiohang(int id)
        {
            List < Giohang > lstGiohang = Laygiohang();
            Giohang sanpham = lstGiohang.SingleOrDefault(n => n.idrb == id);
            if(sanpham != null)
            {
                lstGiohang.RemoveAll(n => n.idrb == id);
                return RedirectToAction("Giohang");
            }
            return RedirectToAction("Giohang");
        }
        public ActionResult Capnhatgiohang(int id, FormCollection collection)
        {
            List<Giohang> lstGiohang = Laygiohang();
            Giohang sanpham = lstGiohang.SingleOrDefault(n => n.idrb == id);
            if (sanpham != null)
            {
                sanpham.soluong = int.Parse(collection["txtSoLg"].ToString());
            }
            return RedirectToAction("Giohang");
        }
        public ActionResult Xoatatcagiohang()
        {
            List<Giohang> lstGiohang = Laygiohang();
            lstGiohang.Clear();
            return RedirectToAction("Giohang");
        }
    }
}