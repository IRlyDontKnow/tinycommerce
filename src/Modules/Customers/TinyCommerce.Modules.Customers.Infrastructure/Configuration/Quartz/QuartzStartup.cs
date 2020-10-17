using System.Collections.Specialized;
using Quartz;
using Quartz.Impl;
using Quartz.Logging;
using Serilog;
using TinyCommerce.Modules.Customers.Infrastructure.Configuration.Processing.InternalCommands;
using TinyCommerce.Modules.Customers.Infrastructure.Configuration.Processing.Outbox;
using TinyCommerce.Modules.Customers.Infrastructure.PasswordReminders.CleanupExpiredPasswordReminders;

namespace TinyCommerce.Modules.Customers.Infrastructure.Configuration.Quartz
{
    internal static class QuartzStartup
    {
        private static IScheduler _scheduler;

        public static void Start(ILogger logger)
        {
            logger.Information("Starting quartz...");

            var schedulerConfiguration = new NameValueCollection
            {
                {"quartz.scheduler.instanceName", "Customers"}
            };
            var schedulerFactory = new StdSchedulerFactory(schedulerConfiguration);
            _scheduler = schedulerFactory.GetScheduler().GetAwaiter().GetResult();

            LogProvider.SetCurrentLogProvider(new SerilogLogProvider(logger));

            _scheduler
                .Start()
                .GetAwaiter()
                .GetResult();

            ScheduleOutboxProcessing();
            ScheduleProcessInternalCommands();
            SchedulePasswordRemindersCleanup();
        }

        private static void SchedulePasswordRemindersCleanup()
        {
            var job = JobBuilder.Create<CleanupPasswordRemindersJob>()
                .Build();

            var trigger = TriggerBuilder
                .Create()
                .StartNow()
                .WithCronSchedule("0 0 12 * * ?")
                .Build();

            _scheduler.ScheduleJob(job, trigger)
                .GetAwaiter()
                .GetResult();
        }

        private static void ScheduleOutboxProcessing()
        {
            var processOutboxJob = JobBuilder.Create<ProcessOutboxJob>().Build();
            var trigger = TriggerBuilder
                .Create()
                .StartNow()
                .WithSimpleSchedule(x =>
                    x.WithIntervalInSeconds(1)
                        .RepeatForever()
                )
                .Build();

            _scheduler.ScheduleJob(processOutboxJob, trigger)
                .GetAwaiter()
                .GetResult();
        }

        private static void ScheduleProcessInternalCommands()
        {
            var job = JobBuilder.Create<ProcessInternalCommandsJob>().Build();
            var trigger = TriggerBuilder
                .Create()
                .StartNow()
                .WithSimpleSchedule(x =>
                    x.WithIntervalInSeconds(1)
                        .RepeatForever()
                )
                .Build();

            _scheduler.ScheduleJob(job, trigger).GetAwaiter().GetResult();
        }

        public static void Stop()
        {
            if (_scheduler.IsShutdown)
                return;

            _scheduler.Shutdown();
        }
    }
}