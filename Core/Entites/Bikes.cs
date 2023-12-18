using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entites
{
    public class Bikes
    {
    public int Id{set;get;}
     public string BikeName{set;get;} 
     public string BikeCC{set;get;} 
    public string  Model{set;get;} 
    public string Country{set;get;}
    public long Price{set;get;}  
    // public DateOnly Yod{set;get;}=new DateOnly();

    }
}