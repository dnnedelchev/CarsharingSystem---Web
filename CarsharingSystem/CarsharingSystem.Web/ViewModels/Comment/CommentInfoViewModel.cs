
namespace CarsharingSystem.Web.ViewModels.Comment
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class CommentInfoViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? AnswerOnId { get; set; }

        public int TravelId { get; set; }

        public IList<CommentInfoViewModel> Answers { get; set; }
    }
}