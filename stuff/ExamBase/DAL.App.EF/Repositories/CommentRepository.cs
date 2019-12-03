using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using Domain;
using ee.itcollege.nikita.DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class CommentRepository : BaseRepository<Comment, Comment, AppDbContext>, ICommentRepository
    {
        public CommentRepository(AppDbContext repositoryDbContext)
            : base(repositoryDbContext, new IdentityFunctionMapper())
        {
        }

        public async Task<List<Comment>> AllAsyncWithSearch(int userId, string search)
        {
            var query = RepositoryDbSet
                .Include(c => c.AppUser)
                .Where(c => c.AppUserId == userId).AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToLower().Trim();
                query = query.Where(c => c.CommentBody.ToLower().Contains(search) ||
                                         c.CommentTitle.ToLower().Contains(search));
            }

            return await query.ToListAsync();
        }

        public async Task<Comment> FindAsyncById(int id, int userId)
        {
            return await RepositoryDbSet
                .Where(s => s.Id == id & s.AppUserId == userId).FirstOrDefaultAsync();
        }

        public async Task<List<DTO.Comment>> AllAsyncWithSearchAPI(int userId, string search)
        {
            var query = RepositoryDbSet
                .Include(c => c.AppUser)
                .Where(c => c.AppUserId == userId).AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToLower().Trim();
                query = query.Where(c => c.CommentBody.ToLower().Contains(search) ||
                                         c.CommentTitle.ToLower().Contains(search));
            }

            var res = (await query.ToListAsync()).Select(e => new DTO.Comment
            {
                Id = e.Id,
                CommentBody = e.CommentBody,
                CommentTitle = e.CommentTitle
            }).ToList();
            
            return res;
        }
        
        public async Task<DTO.Comment> FindAsyncByIdAPI(int id, int userId)
        {
            var temp = await RepositoryDbSet
                .Where(s => s.Id == id & s.AppUserId == userId).FirstOrDefaultAsync();
            
            return new DTO.Comment()
            {
                Id = temp.Id,
                CommentBody = temp.CommentBody,
                CommentTitle = temp.CommentTitle
            };
        }
        
    }
}