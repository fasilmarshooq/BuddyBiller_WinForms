using BB.System.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyStore.BLL
{
    public class DeaCustBLL
    {
        public int id { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string contact { get; set; }
        public string address { get; set; }
        public DateTime added_date { get; set; }
        public int added_by { get; set; }

        public void SaveOrUpdate(Party entity, BuddyBillerRepository db)
        {
            var sql = @"MERGE INTO Parties
               USING (VALUES (@Id,@Type,@Name,@Email,@PhoneNumber,@Address,@Added_Date,@Added_By_Id,@IsActive)) AS
                            s(Id,Type,Name,Email,PhoneNumber,Address,Added_Date,Added_By_Id,IsActive)
                ON Parties.id = s.id
                WHEN MATCHED THEN
                    UPDATE
                    SET     Type=s.Type
                            ,Name=s.Name
                            ,Email=s.Email
                            ,PhoneNumber=s.PhoneNumber
                            ,Address=s.Address
                            ,Added_Date=s.Added_Date
                            ,Added_By_Id=s.Added_By_Id
                            ,IsActive=s.IsActive

                WHEN NOT MATCHED THEN
                    INSERT (Type,Name,Email,PhoneNumber,Address,Added_Date,Added_By_Id,IsActive)
                    VALUES (s.Type,s.Name,s.Email,s.PhoneNumber,s.Address,s.Added_Date,s.Added_By_Id,s.IsActive);";

            object[] parameters = {
                new SqlParameter("@id", entity.Id),
                new SqlParameter("@Type", entity.Type),
                new SqlParameter("@Name", entity.Name),
                new SqlParameter("@Email", entity.Email),
                new SqlParameter("@PhoneNumber", entity.PhoneNumber),
                new SqlParameter("@Address", entity.Address),
                new SqlParameter("@Added_Date", entity.Added_Date),
                new SqlParameter("@Added_By_Id", '1'), // TO:DO extend to stamp user session id
                new SqlParameter("@IsActive", entity.IsActive)

            };
            db.Database.ExecuteSqlCommand(sql, parameters);
        }

    }
}
