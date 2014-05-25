using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace AdminPanel2.Controllers.JobManager
{
    public class JobWorker
    {
        private Job jobToExecute;
        private IProgress<JobStatus> executionProgress;

        public JobStatus Status { get; private set; }

        public JobWorker(Job job, IProgress<JobStatus> ExecutionCallback)
        {
            this.jobToExecute = job;
            executionProgress = ExecutionCallback;
        }

        public List<Tuple<string, string>> Messages
        {
            get { return jobToExecute.getMessages(); }
        }

        //public JobStatus Status()
        //{
        //    if (String.IsNullOrEmpty(jobToExecute.jobId))
        //        return JobStatus.none;

        //    JobStatus status = JobStatus.none;
        //    try
        //    {
        //        status = jobToExecute.status();
        //        return status;
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        public void DoWork()
        {
            try
            {
                jobToExecute.execute();
                JobStatus status = jobToExecute.status();
                while (status == JobStatus.waiting || status == JobStatus.executing || status == JobStatus.cancelling || status == JobStatus.submitted)
                {
                    status = jobToExecute.status();
                    Status = status;
                    executionProgress.Report(status);
                }
            }
            catch
            {
                throw;
            }
        }
    }

}