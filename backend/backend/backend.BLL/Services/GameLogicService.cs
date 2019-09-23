using backend.BLL.Services.Interfaces;
using Hangfire;
using System;

namespace backend.BLL.Services
{
    public class GameLogicService: IGameLogicService
    {
        public void testMethod()
        {
            RecurringJob.AddOrUpdate("asd-id", () => Console.WriteLine("hello!"), Cron.Minutely);
        }
    }
}
