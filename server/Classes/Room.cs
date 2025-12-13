using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace netChat
{
    [Table("room")]
    public class Room
    {
        [Key]
        [Column("roomid")]
        public string RoomId { get; set; } = Guid.NewGuid().ToString().Substring(0, 6);

        [Column("roomname")]
        public string RoomName { get; set; } = "";

        [Column("creatorid")]  
        public string CreatorId { get; set; } = "";

        [Column("userlist")]
        public string UserList { get; set; } = "";
    }

}
