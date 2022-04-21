using System;

namespace Notes.Domain
{
    class Note
    {
        public Guid UserID { get; set; }
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public DateTime DateCreation { get; set; }
        public DateTime? EditDate { get; set; }
    }
}
