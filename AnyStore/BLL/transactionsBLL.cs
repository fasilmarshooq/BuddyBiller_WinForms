using System;
using System.Data;

namespace BuddyBiller.BLL
{
    class TransactionsBll
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int DeaCustId { get; set; }
        public decimal GrandTotal { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Tax { get; set; }
        public decimal Discount { get; set; }
        public int AddedBy { get; set; }
        public DataTable TransactionDetails { get; set; }

    }
}
