
using System.ComponentModel.DataAnnotations.Schema;

namespace CarsharingSystem.Models
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Comment
    {
        private ICollection<Comment> answers;

        public Comment()
        {
            this.answers = new HashSet<Comment>();
        }

        [Column("CommentId")]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public int TravelId { get; set; }

        public virtual Travel Travel { get; set; }

        public string AuthorId { get; set; }

        public User Author { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? AnswerOnId { get; set; }

        public virtual Comment AnswerOn { get; set; }

        public virtual ICollection<Comment> Answers { get { return this.answers; } set { this.answers = value; } }
    }
}
