//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

public partial class TransactionDetail
{
    public int Id { get; set; }
    public Nullable<decimal> Rate { get; set; }
    public Nullable<decimal> Qty { get; set; }
    public Nullable<decimal> Total { get; set; }

    public virtual Product Product { get; set; }
    public virtual Transaction Transaction { get; set; }
}
