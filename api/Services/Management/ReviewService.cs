using api.Data;
using api.DTOs;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Services
{
    public class ReviewService
    {
        private readonly DataContext _context;

        public ReviewService(DataContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Review>> GetAll()
    {
      return await _context.Reviews.ToListAsync();
    }

    public async Task<Review?> GetByID(int id)
    {
      return await _context.Reviews.FindAsync(id);
    }

    public async Task<Review> Create(ReviewDTO newReviewDTO)
    {
      var newReview = new Review();
      newReview.ReviewID = await GetCount() + 1;
      newReview.CustomerID = newReviewDTO.CustomerID;
      newReview.ProductID = newReviewDTO.ProductID;
      newReview.ReviewMessage = newReviewDTO.ReviewMessage;

      _context.Reviews.Add(newReview);
      await _context.SaveChangesAsync();

      return newReview;
    }

    public async Task Update(int id, ReviewDTO reviewDTO)
    {
      var existingReview = await GetByID(id);

      if (existingReview is not null)
      {
      existingReview.CustomerID = reviewDTO.CustomerID;
      existingReview.ProductID = reviewDTO.ProductID;
      existingReview.ReviewMessage = reviewDTO.ReviewMessage;

      await _context.SaveChangesAsync();
     }
    }

    public async Task Delete(int id)
    {
      var reviewToDelete = await GetByID(id);

      if(reviewToDelete is not null)
      {
        _context.Reviews.Remove(reviewToDelete);
        await _context.SaveChangesAsync();
      }
    }

    public async Task<int> GetCount()
    {
      return await _context.Reviews.CountAsync();
    }
    }
}
