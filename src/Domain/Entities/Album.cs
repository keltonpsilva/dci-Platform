using System.Collections.Generic;

namespace Domain.Entities
{
    public class Album
    {
        public Album()
        {
            Photos = new List<Photo>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public List<Photo> Photos { get; set; }
    }
}
