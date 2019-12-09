using System;
using System.Data.SqlClient;

namespace BuddyBiller.BLL
{
    public class DeaCustBll
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public string Address { get; set; }
        public DateTime AddedDate { get; set; }
        public int AddedBy { get; set; }

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
