using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Models
{
    public class Users
    {
        [Key]
        public int UserID { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }
        [Required]
        [StringLength(50)]
        public string Realname { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public bool EmailValidated { get; set; }

        [DataType(DataType.EmailAddress)]
        public string PublicEmail { get; set; }

        [StringLength(200)]
        public string SpaceTheme { get; set; }

        [StringLength(200)]
        public string AvataSrc { get; set; }

        public bool Gender { get; set; }

        [StringLength(50)]
        public string CreateIP { get; set; }

        [StringLength(50)]
        public string LastVisitIP { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime LastVisitDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime LastPostDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreateDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime UpadateDate { get; set; }

        public int SpaceViews { get; set; }

        public int LoginCount { get; set; }

        public bool IsActive { get; set; }

        public int TotalInvite { get; set; }

        public int TotalTopics { get; set; }

        public int TotalPosts { get; set; }

        public int WeekPosts { get; set; }

        public int DayPosts { get; set; }

        public int MonthPosts { get; set; }

        public int TotalComments { get; set; }

        public int TotalShares { get; set; }

        public int TotalCollection { get; set; }

        public int ValuedTopic { get; set; }

        public int DeletedTopics { get; set; }

        public int DeletedReplies { get; set; }

        public int TotalBlogArticles { get; set; }

        public int TotalAlbums { get; set; }

        public int TotalPhotos { get; set; }

        public int TotalOnlineTime { get; set; }

        public int MonthOnlineTime { get; set; }

        public int WeekOnlineTime { get; set; }

        public int DayOnlineTime { get; set; }

        [DataType(DataType.MultilineText)]
        public string UserInfo { get; set; }

        [StringLength(20)]
        public string MobilePhone { get; set; }
    }
}
