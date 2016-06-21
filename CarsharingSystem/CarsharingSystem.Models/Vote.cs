using System.ComponentModel.DataAnnotations.Schema;

namespace CarsharingSystem.Models
{
    public class Vote
    {
        [Column("VoteId")]
        public int Id { get; set; }

        public int TravelId { get; set; }

        public virtual Travel Travel { get; set; }

        public int AuthorId { get; set; }

        public virtual User Author { get; set; }

        public int? PassengerId { get; set; }

        public virtual User Passenger { get; set; }

        public int Score { get; set; }
    }
}