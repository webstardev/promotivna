using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarkomPos.Entities
{
    public class OrderAndOffer : DbEntity
    {
        public int OfferId { get; set; } // povezana ponuda (connected offer)
        public Offer Offer { get; set; }

        public int OrderId { get; set; } // povezana narudžba (connected order)
        public Order Order { get; set; }
    }
}