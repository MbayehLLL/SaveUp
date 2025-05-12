using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveUp.Models;

public class SavingItem { 
    public string Description { get; set; } 
    public double Price { get; set; }
    public DateTime DateTime { get; set; } 
}