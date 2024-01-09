using WebApi.DbOperations;

namespace WebApi.Application.JobOperations.Command.DeleteJobCommand;

public class DeleteJobCommand
{
    public int JobId { get; set; }
    private readonly UserDbContext _context;
    public DeleteJobCommand(UserDbContext context)
    {
        _context = context;
    }

    public void Handle()
    {
        var job = _context.Jobs.SingleOrDefault(x => x.Id == JobId);
        if (job is null)
            throw new InvalidOperationException("Job is not found.");

        _context.Jobs.Remove(job);
        _context.SaveChanges();
    }
}