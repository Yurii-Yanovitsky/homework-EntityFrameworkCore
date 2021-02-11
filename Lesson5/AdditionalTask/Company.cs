using System;
using System.Collections.Generic;

#nullable disable

namespace AdditionalTask
{
    public partial class Company
    {
        public Company()
        {
            Phones = new HashSet<Phone>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Phone> Phones { get; set; }
    }
}
