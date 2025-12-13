using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace netChat
{
    [Table("message")]
    public class Message
    {
        [Column("messageid")]
        public int MessageId { get; set; }   

        [Column("roomid")]
        public string? RoomId { get; set; }  

        [Column("userid")]
        public string UserId { get; set; } = null!;

        [Column("content")]
        public string Content { get; set; } = null!;

        [Column("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}
