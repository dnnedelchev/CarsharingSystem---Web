
using System;
using System.Collections.Generic;
using System.Data.Entity;
using CarsharingSystem.Data;
using CarsharingSystem.Models;
using CarsharingSystem.Web.ViewModels.Comment;

namespace CarsharingSystem.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    public class CommentController : BaseController
    {
        public CommentController(ICarsharingData data)
            : base (data)
        {
        }
        
        public ActionResult GetComments(int id)
        {
            var comments = this.Data.Travels
                .All()
                .First(travel => travel.Id == id)
                .Comments
                .Where(comment => comment.AnswerOnId == null)
                .Select(comment => new CommentInfoViewModel
                {
                    Id = comment.Id,
                    Title = comment.Title,
                    Content = comment.Content,
                    CreatedOn = DateTime.Now,//comment.CreatedOn, TODO
                    Answers = comment.Answers.Select(answer => new CommentInfoViewModel
                    {
                        Id = answer.Id,
                        Title = answer.Title,
                        Content = answer.Content,
                        CreatedOn = answer.CreatedOn,
                        Answers = null,
                        TravelId = id
                    }).ToList(),//GetAnswers(this.Data.Comments.All(), comment.Id),
                    TravelId = id
                })
                .ToList();
            return PartialView("_CommentInfoPartial", comments);
        }

        [NonAction]
        private IList<CommentInfoViewModel> GetAnswers(IQueryable<Comment> comments, int answerOnId)
        {
            return comments
                    .Where(comment => comment.AnswerOnId == answerOnId)
                    .Select(comment => new CommentInfoViewModel
                    {
                        Id = comment.Id,
                        Title = comment.Title,
                        Content = comment.Content,
                        AnswerOnId = comment.AnswerOnId,
                        Answers = GetAnswers(comments, comment.Id)
                    })
                    .ToList();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AddCommentViewModel addComment)
        {
            var comment = new Comment
            {
                Title = addComment.Title,
                Content = addComment.Content,
                TravelId = addComment.TravelId,
                AuthorId = this.UserProfile.Id,
                CreatedOn = DateTime.Now,
                AnswerOnId = addComment.AnswerOnId
            };
            this.Data.Comments.Add(comment);
            this.Data.SaveChanges();

            return RedirectToAction("GetComments", new { id = addComment.TravelId });
        }
    }
}