using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace netChat
{
    [Table("users")]
    public class User
    {
        [Key]
        [Column("userid")]
        public string UserId { get; set; } = "";

        [Column("username")]
        public string UserName { get; set; } = "";

        [Column("password")]
        public string Password { get; set; } = "";

        [Column("rkey")]
        public string RKey { get; set; } = "";
    }
}
