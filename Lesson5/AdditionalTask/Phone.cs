using System;
using System.Collections.Generic;

#nullable disable

namespace AdditionalTask
{
    public partial class Phone
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }
    }
}
