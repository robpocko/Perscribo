using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Perscribo.EF.Library.Models
{
    public interface IAddressedEntity
    {
        Address Address { get; set; }
    }
}
