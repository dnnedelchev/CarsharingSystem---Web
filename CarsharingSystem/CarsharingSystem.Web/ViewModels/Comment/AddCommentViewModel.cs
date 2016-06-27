
namespace CarsharingSystem.Web.ViewModels.Comment
{
    public class AddCommentViewModel
    {
        public int? AnswerOnId { get; set; }

        public int TravelId { get; set; }
        
        public string Title { get; set; }

        public string Content { get; set; }

        public bool ShowDivTitle { get; set; }
    }
}