using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AdminPanel2.Controllers.JobManager.Behaviors.AgsService;

namespace AdminPanel2.Controllers.JobManager
{
    public class JobAgsService : Job
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="ExecuteUrl">Url de execução do job, ao ser executado um jobId é criado pelo próprio AGS e fica armazenado na classe sendo usado posteriormente para cancelar/pegar status do job.</param>
        /// <param name="StatusUrl">Url para recuperar o status do job. {jobId} é obrigatório na url</param>
        /// <param name="CancelUrl">Url para cancelar o job. {jobId} é obrigatório na url</param>
        public JobAgsService(string ExecuteUrl, string StatusUrl, string CancelUrl)
            : base(new BaseAgsServiceBehavior(ExecuteUrl, CancelUrl, StatusUrl))
        {
        }

    }
}