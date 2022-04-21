using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Notes.Domain;

namespace Notes.Application.Interfaces
{
    public interface INotesDbContext
    {
        public DbSet<Note> Notes { get; set; }
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    
    }
}
