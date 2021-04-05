using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApplyConfigurationsApp
{
    public class Message
    {
        public int ID { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public int UserFromID { get; set; }
        public virtual User UserFrom { get; set; }
        public int ToUserID { get; set; }
        public virtual User ToUser { get; set; }
    }
}
