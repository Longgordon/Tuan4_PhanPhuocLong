using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tuan3_PhanPhuocLong.Models
{
    public class Giohang
    {
        MydataDataContext data = new MydataDataContext();
        public int idrb { get; set; }
        [Display(Name = "Tên Rubik")]
        public string ten { get; set; }
        [Display(Name= "Ảnh Bìa")]
        public string hinh { get; set; }
        [Display(Name = "Giá Bán")]
        public double gia { get; set; }
        [Display(Name = "Số lượng")]
        public int soluong { get; set; }
        [Display(Name = "Thành tiền")]
        public double thanhtien 
        {
            get { return soluong * gia;}
        }
        public Giohang (int id)
        {
            idrb = id;
            Rubik rb = data.Rubiks.Single(p => p.id == id);
            ten = rb.ten;
            hinh = rb.hinh;
            gia = double.Parse(rb.gia.ToString());
            soluong = 1;
        }
    }
}