using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonelKayit
{
    public class DbClass
    {
        public ObjectId _id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public DateTime birthdate { get; set; }
        public string email { get; set; }
        public bool active { get; set; }
        public int[] accounts { get; set; }
        public object tier_and_details { get; set; }
    }
}
