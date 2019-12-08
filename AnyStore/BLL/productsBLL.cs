using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyStore.BLL
{
    public class productsBLL
    {
        //Getters and Setters for Product Module
        public int id { get; set; }
        public string name { get; set; }
        public string category { get; set; }
        public string description { get; set; }
        public decimal rate { get; set; }
        public decimal qty { get; set; }
        public DateTime added_date { get; set; }
        public int added_by { get; set; }

        public void SaveOrUpdate(Product entity, BuddyBillerRepository db)
        {
            var sql = @"MERGE INTO Products
               USING (VALUES (@Id,@Name,@Description,@Rate,@Qty,@Added_Date,@ProductType_Id,@Added_By_Id,@IsActive)) AS
                            s(Id,Name,Description,Rate,Qty,Added_Date,ProductType_Id,Added_By_Id,IsActive)
                ON Products.id = s.id
                WHEN MATCHED THEN
                    UPDATE
                    SET		 Name=s.Name
                            ,Description=s.Description
                            ,Rate=s.Rate
                            ,Qty=s.Qty
                            ,Added_Date=s.Added_Date
							,ProductType_Id=s.ProductType_Id
                            ,Added_By_Id=s.Added_By_Id
                            ,IsActive=s.IsActive

                WHEN NOT MATCHED THEN
                    INSERT (Name,Description,Rate,Qty,Added_Date,ProductType_Id,Added_By_Id,IsActive)
                    VALUES (s.Name,s.Description,s.Rate,s.Qty,s.Added_Date,s.ProductType_Id,s.Added_By_Id,s.IsActive);";

            object[] parameters = {
                new SqlParameter("@Id", entity.Id),
                new SqlParameter("@Name", entity.Name),
                new SqlParameter("@Description", entity.Description),
                new SqlParameter("@Rate", entity.Rate),
                new SqlParameter("@Qty", entity.Qty),
                new SqlParameter("@Added_Date", entity.Added_Date),
                new SqlParameter("@ProductType_Id", entity.ProductType.Id),
                new SqlParameter("@Added_By_Id", '1'), // TO:DO extend to stamp user session id
                new SqlParameter("@IsActive", entity.IsActive)

            };
            db.Database.ExecuteSqlCommand(sql, parameters);
        }


    }
}
