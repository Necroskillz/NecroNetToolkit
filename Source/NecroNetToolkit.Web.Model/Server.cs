//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace NecroNetToolkit.Web.Model
{
    public partial class Server
    {
        public Server()
        {
            this.ActualDeals = new HashSet<ActualDeal>();
            this.HistoryDeals = new HashSet<HistoryDeal>();
        }
    
        public int ID { get; set; }
        public string Name { get; set; }
        public Nullable<decimal> Rank { get; set; }
        public Nullable<decimal> APrice { get; set; }
        public Nullable<decimal> CPrice { get; set; }
        public Nullable<decimal> GooglePageRank { get; set; }
        public string IC { get; set; }
        public string AffilID { get; set; }
        public string Web { get; set; }
        public string XmlFeed { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    
        public virtual ICollection<ActualDeal> ActualDeals { get; set; }
        public virtual ICollection<HistoryDeal> HistoryDeals { get; set; }
    }
    
}
