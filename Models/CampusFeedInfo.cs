using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CampusFeedApi.Models
{
    public class CampusFeedInfo
    {
        public CampusFeedInfo(string campusFeedId)
        {
            CampusFeedId = campusFeedId;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string CampusFeedId { get; set; }


        [Required]
        public string Content { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public int Like { get; set; }
        public int Dislike { get; set; }
    }
}
