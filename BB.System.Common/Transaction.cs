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

public partial class Transaction
{
    public int Id { get; set; }
    public string Type { get; set; }
    public Nullable<System.DateTime> Transaction_date { get; set; }
    public Nullable<decimal> Tax { get; set; }
    public Nullable<decimal> Discount { get; set; }
    public Nullable<decimal> GrandTotal { get; set; }

    public virtual User Added_By { get; set; }
    public virtual Party Party { get; set; }
}
