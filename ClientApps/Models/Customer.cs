using ClientApps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientApps.Entities
{
public class Customer : BaseEntity
{
    public string Name { get; set; }
    public string Contact { get; set; }
    public string City { get; set; }
    public string Email { get; set; }
}
}
