using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AdminPanel2.Controllers.JobManager.Behaviors;

namespace AdminPanel2.Controllers.JobManager
{
    /// <summary>
    /// Status de execução de um JOB
    /// </summary>
    public enum JobStatus
    {
        ///<summary>Requisição realizada para o JOB</summary>
        submitted,
        ///<summary>O JOB está esperando recursos da CPU para ser executado</summary>
        waiting,
        ///<summary>O JOB recebeu um sinal de CANCEL e está em processo de cacelamento</summary>
        cancelling,
        ///<summary>O JOB está em execução</summary>
        executing,
        ///<summary>O JOB respondeu com um sinal de falha</summary>
        failed,
        ///<summary>O JOB foi executado</summary>
        succeeded,
        ///<summary>O JOB foi cancelado</summary>
        cancelled,
        ///<summary>Aconteceu algum erro no jobmanager, alguma execeção foi lançada durante a chamada do Job</summary>
        executionError,
        ///<summary>Status vazio</summary>
        none
    }

    public abstract class Job
    {
        public string jobId { get; set; }
        public JobStatus jobStatus { get; set; }
        public bool jobCanceled { get; set; }

        public JobBehavior jobBehave { get; set; }

        public Job(JobBehavior behave)
        {
            this.jobBehave = behave;
        }

        public void execute() 
        {
            try
            {
                jobId = jobBehave.execute();
            }
            catch
            {
                jobStatus = JobStatus.executionError;
                throw;
            }
        }
        
        public JobStatus status() 
        {
            try
            {
                jobStatus = jobBehave.status();
                return jobStatus;
            }
            catch
            {
                jobStatus = JobStatus.executionError;
                throw;
            }
        }

        public void cancel() 
        { 
            try
            {
                jobCanceled = jobBehave.cancel();
            }
            catch
            {
                jobStatus = JobStatus.executionError;
                throw;
            }
        }

        public List<Tuple<string,string>> getMessages()
        {
            try
            {
                return jobBehave.getMessages();
            }
            catch
            {
                throw;
            }
        }
    }
}