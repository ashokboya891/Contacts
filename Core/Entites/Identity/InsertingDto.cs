using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.DTOs
{
    public class InsertingDto
    {
    public string BikeName{set;get;} 
     public string BikeCC{set;get;} 
    public string  Model{set;get;} 
    public string Country{set;get;}
    public long Price{set;get;}  
    }
}