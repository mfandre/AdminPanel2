using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace AdminPanel2.Controllers.JobManager
{
    /// <summary>
    /// Classe reposnsabel por gerenciar UM Job.
    /// <para>Ela também armazena as menssagens retornadas pelo JobWorker</para>
    /// </summary>
    public class JobManager
    {
        public JobStatus status { get; private set; }

        /// <summary>
        /// De acordo com o ID passado monta o job e o executa
        /// </summary>
        /// <param name="id"></param>
        public async Task ExecuteJob(Job job)
        {
            try
            {
                var progress = new Progress<JobStatus>();
                JobWorker jw = new JobWorker(job, progress);

                progress.ProgressChanged += (s, e) =>
                {
                    status = e;

                    //armazena msg no banco
                    if (status == JobStatus.cancelled)
                    {
                        List<Tuple<string,string>> msgs = jw.Messages;
                    }
                    else if (status == JobStatus.executionError) 
                    {
                        List<Tuple<string,string>> msgs = jw.Messages;
                    }
                    else if (status == JobStatus.failed) 
                    {
                        List<Tuple<string,string>> msgs = jw.Messages;
                    }
                    else if (status == JobStatus.succeeded)
                    {
                        List<Tuple<string, string>> msgs = jw.Messages;
                    }
                };

                await Task.Run(() =>
                {
                    //Executando o job, perceba que este método é assincrono
                    jw.DoWork();
                });
            }
            catch
            {
                throw;
            }
        }
    }
}