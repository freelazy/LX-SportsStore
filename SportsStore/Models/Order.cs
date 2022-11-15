using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SportsStore.Models
{
    public class Order
    {
        [BindNever]
        public int OrderID { get; set; }

        [BindNever]
        public ICollection<CartLine> Lines { get; set; }

        [Required(ErrorMessage = "请输入收件人名字")]
        public string Name { get; set; }

        [Required(ErrorMessage = "请输入收件人地址详情")]
        public string Detail { get; set; }

        [Required(ErrorMessage = "请输入收件人城市")]
        public string City { get; set; }

        [Required(ErrorMessage = "请输入收件人省份")]
        public string Province { get; set; }

        // 是否需要礼品包装
        public bool GiftWrap { get; set; }
    }
}
