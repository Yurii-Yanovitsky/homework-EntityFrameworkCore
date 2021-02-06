using System.Collections.Generic;

namespace MyApplyConfigurationsApp
{
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public virtual List<Message> SentMessages { get; set; } = new List<Message>();
        public virtual List<Message> RecivedMessages { get; set; } = new List<Message>();
    }
}
