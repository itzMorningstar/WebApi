using Entities.Logs;
using Microsoft.EntityFrameworkCore;
using ServicesLibrary.GenericRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLibrary.LogServices;

public class LogService : ILogService
{
    private readonly ApplicationDbContext dbContext;
    private readonly IGenericRepository<Log> logRepo;

    public LogService(ApplicationDbContext dbContext, IGenericRepository<Log> logRepo)
    {
        this.dbContext = dbContext;
        this.logRepo = logRepo;
    }

    public void LogError(string message, Exception exception)
    {
        var logEntry = new Log
        {
            Timestamp = DateTime.Now,
            Level = "Error",
            Message = message,
            Exception = exception.ToString()
        };

        logRepo.Add(logEntry);
    }
}
