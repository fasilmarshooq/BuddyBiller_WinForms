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
    public int id { get; set; }
    public string type { get; set; }
    public Nullable<int> dea_cust_id { get; set; }
    public Nullable<decimal> grandTotal { get; set; }
    public Nullable<System.DateTime> transaction_date { get; set; }
    public Nullable<decimal> tax { get; set; }
    public Nullable<decimal> discount { get; set; }
    public Nullable<int> added_by { get; set; }
}