using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Entities
{
    public class Review : Entity
    {
        public int ReviewId { get; set; }
        public int ItemId { get; set; }
        public string CustomerName { get; set; }
        public string? CustomerEmail { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime ReviewDate { get; set; }
        public string? Picture {  get; set; }
        public int LikeCounts { get; set; }
        public bool IsVerifiedPurchase { get; set; }
        public string? Response {  get; set; }

        [ForeignKey("ItemId")]
        [InverseProperty("Reviews")]
        public virtual MenuItem? Item { get; set; }

        public Review(
            int reviewId,
            int itemId,
            string customerName,
            string? customerEmail,
            int rating,
            string comment,
            DateTime reviewDate,
            string? picture,
            int likeCounts,
            bool isVerifiedPurchase,
            string? response
        ) : base(reviewId)
        {
            ReviewId = reviewId;
            ItemId = itemId;
            CustomerName = customerName;
            CustomerEmail = customerEmail;
            Rating = rating;
            Comment = comment;
            ReviewDate = reviewDate;
            Picture = picture;
            LikeCounts = likeCounts;
            IsVerifiedPurchase = isVerifiedPurchase;
            Response = response;
        }

        public void SetItem(int itemId)
        {
            ItemId = itemId;
        }

        public void SetCustomerName(string customerName)
        {
            CustomerName = customerName;
        }

        public void SetCustomerEmail(string? customerEmail)
        {
            CustomerEmail = customerEmail;
        }

        public void SetRating(int rating) 
        { 
            Rating = rating;
        }

        public void SetComment(string comment)
        {
            Comment = comment;
        }

        public void SetReviewDate(DateTime reviewDate) 
        {
            ReviewDate = reviewDate;
        }

        public void SetPicture(string? picture)
        {
            Picture = picture;
        }

        public void SetLikeCounts(int likeCounts) 
        {
            LikeCounts = likeCounts;
        }

        public void SetIsVerifiedPurchase(bool isVerifiedPurchase) 
        { 
            IsVerifiedPurchase &= isVerifiedPurchase;
        }

        public void SetResponse(string? response) 
        { 
            Response = response;
        }
    }
}
