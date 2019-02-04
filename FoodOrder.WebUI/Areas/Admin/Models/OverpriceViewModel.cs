using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebUI.Areas.Admin.Models
{
    public class OverpriceViewModel
    {
        [DisplayName("Сумма")] public decimal Price { get; set; }
        [DisplayName("Разница суммы")] public decimal PriceDifference { get; set; }
        [DisplayName("Имя сотрудника")] public string UserName { get; set; }

        [DisplayName("Дата заказа")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [DisplayName("Максимальная сумма")]
        public decimal MaxPrice { get; set; }
    }
}