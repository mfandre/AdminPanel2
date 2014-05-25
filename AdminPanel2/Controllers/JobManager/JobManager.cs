using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace AdminPanel2.Controllers.JobManager
{
    /// <summary>
    /// Classe reposnsabel por gerenciar UM Job
    /// </summary>
    public class JobManager
    {
        public JobStatus status { get; private set; }

        /// <summary>
        /// De acordo com o ID passado monta o job e o executa
        /// </summary>
        /// <param name="id"></param>
        public async Task ExecuteJob(int id)
        {
            //Todo: Pegar as informacoes do banco para montar o job e executa-lo...
            var progress = new Progress<JobStatus>();
            progress.ProgressChanged += (s, e) =>
            {
                status = e;
            };

            await Task.Run(() =>
            {
                JobAgsService agsService = new JobAgsService(
                "http://ags101.portalinflor.com.br/arcgis/rest/services/Treinamento/Teste/GPServer/Script/submitJob?f=json",
                "http://ags101.portalinflor.com.br/arcgis/rest/services/Treinamento/Teste/GPServer/Script/jobs/{jobId}?f=json",
                "http://ags101.portalinflor.com.br/arcgis/rest/services/Treinamento/Teste/GPServer/Script/jobs/{jobId}/cancel");
                JobWorker jw = new JobWorker(agsService,progress);
                jw.DoWork();
            });
        }

    }
}