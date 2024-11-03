using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HangFireTarefa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        [HttpGet]
        public void ListInt()
        {
            for(int i = 0; i < 1; i++) 
            {
                Console.WriteLine(i);
            }
        }

        [HttpPost]
        [Route("CreateBackgroundJob")]
        public ActionResult CreateBackgroundJob()
        {
            BackgroundJob.Enqueue(() => ListInt());
            return Ok();
        }



        [HttpPost]
        [Route("CreateScheduleJob")]
        public ActionResult CreateScheduleJob()
        {
            var scheduleDateTime = DateTime.UtcNow.AddSeconds(5);
            var dateTimeOffSet = new DateTimeOffset(scheduleDateTime);

            BackgroundJob.Schedule(() => Console.WriteLine("agendamento"), dateTimeOffSet);

            return Ok();
        }


        [HttpPost]
        [Route("CreateContinuationJob")]
        public ActionResult CreateContinuationJob()
        {
            var scheduleDateTime = DateTime.UtcNow.AddSeconds(5);
            var dateTimeOffSet = new DateTimeOffset(scheduleDateTime);

            var job1 = BackgroundJob.Schedule(() => Console.WriteLine("schedule"), dateTimeOffSet);
            var job2 = BackgroundJob.ContinueJobWith(job1, () => Console.WriteLine("second job"));
            var job3 = BackgroundJob.ContinueJobWith(job2, () => Console.WriteLine("third job"));
            var job4 = BackgroundJob.ContinueJobWith(job3, () => Console.WriteLine("fourth job"));

            return Ok();
        }



        [HttpPost]
        [Route("CreateRecurringJob")]
        public ActionResult CreateRecurringJob()
        {
            RecurringJob.AddOrUpdate("RecurringJob1", () => Console.WriteLine("recurring job"), "* * * * *");

            return Ok();
        }
    }
}
