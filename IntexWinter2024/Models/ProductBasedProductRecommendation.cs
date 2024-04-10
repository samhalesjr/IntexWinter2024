using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace IntexWinter2024.Models
{
    public class ProductBasedProductRecommendation
    {
        [Key]
        public int ProductBasedProductRecommendationId { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        [ForeignKey("RecommendationOne")]
        public int RecommendationOneProductId { get; set; }
        public virtual Product RecommendationOne { get; set; }

        [ForeignKey("RecommendationTwo")]
        public int RecommendationTwoProductId { get; set; }
        public virtual Product RecommendationTwo { get; set; }

        [ForeignKey("RecommendationThree")]
        public int RecommendationThreeProductId { get; set; }
        public virtual Product RecommendationThree { get; set; }

        [ForeignKey("RecommendationFour")]
        public int RecommendationFourProductId { get; set; }
        public virtual Product RecommendationFour { get; set; }

        [ForeignKey("RecommendationFive")]
        public int RecommendationFiveProductId { get; set; }
        public virtual Product RecommendationFive { get; set; }

        [ForeignKey("RecommendationSix")]
        public int RecommendationSixProductId { get; set; }
        public virtual Product RecommendationSix { get; set; }
    }
}
