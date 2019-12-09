using System;

namespace BuddyBiller.BLL
{
    abstract class CategoriesBll
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime AddedDate { get; set; }
        public int AddedBy { get; set; }

    }
}
