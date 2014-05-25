using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using AdminPanel2.Controllers.JobManager;
using AdminPanel2.Controllers.JobManager.Behaviors.AgsService;
using AdminPanel2.Controllers.JobManager.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.JobManager
{
    [TestClass]
    public class TestJobManager
    {
        [TestMethod]
        public void TestExecuteAgsService()
        {
            JobAgsService agsService = new JobAgsService(
                "http://srv508.portalinflor.com.br/arcgis/rest/services/Treinamento/Teste/GPServer/Script/submitJob?f=json",
                "http://srv508.portalinflor.com.br/arcgis/rest/services/Treinamento/Teste/GPServer/Script/jobs/{jobId}?f=json",
                "http://srv508.portalinflor.com.br/arcgis/rest/services/Treinamento/Teste/GPServer/Script/jobs/{jobId}/cancel");

            Assert.IsInstanceOfType(agsService.jobBehave, typeof(BaseAgsServiceBehavior));

            agsService.execute();
            Assert.AreNotEqual("", agsService.jobId);

            JobStatus status = agsService.status();
            while (status == JobStatus.waiting || status == JobStatus.executing || status == JobStatus.cancelling || status == JobStatus.submitted)
            {
                status = agsService.status();
                Assert.AreNotEqual(JobStatus.none, status);
            }
            Assert.IsTrue(agsService.getMessages().Count > 0);
        }

        [TestMethod]
        public async Task TestJobWorkerDoWork()
        {
            JobAgsService agsService = new JobAgsService(
                "http://ags101.portalinflor.com.br/arcgis/rest/services/Treinamento/Teste/GPServer/Script/submitJob?f=json",
                "http://ags101.portalinflor.com.br/arcgis/rest/services/Treinamento/Teste/GPServer/Script/jobs/{jobId}?f=json",
                "http://ags101.portalinflor.com.br/arcgis/rest/services/Treinamento/Teste/GPServer/Script/jobs/{jobId}/cancel");

            var progress = new Progress<JobStatus>();
            progress.ProgressChanged += (s, e) =>
            {
                Assert.AreNotEqual(e, JobStatus.none);
                Assert.AreNotEqual(e, JobStatus.executionError);
            };

            JobWorker jw = new JobWorker(agsService, progress);
            await Task.Run(() => jw.DoWork());

            JobStatus status = jw.Status;
            Assert.AreNotEqual(status, JobStatus.none);
        }

        [TestMethod]
        [ExpectedException(typeof(JobHttpException))]
        public async Task TestJobWorkerWithWrongUrlExecute()
        {
            JobAgsService agsService = new JobAgsService(
                "http://ags101.portalinflor.com.br/arcgis/rest/services/TreinamentoTeste/GPServer/Script/submitJob?f=json",
                "http://ags101.portalinflor.com.br/arcgis/rest/services/TreinamentoTeste/GPServer/Script/jobs/{jobId}?f=json",
                "http://ags101.portalinflor.com.br/arcgis/rest/services/TreinamentoTeste/GPServer/Script/jobs/{jobId}/cancel");

            var progress = new Progress<JobStatus>();
            progress.ProgressChanged += (s, e) =>
            {
            };

            JobWorker jw = new JobWorker(agsService, progress);
            await Task.Run(() => jw.DoWork());

            JobStatus status = jw.Status;
            Assert.AreNotEqual(status, JobStatus.none);
        }

        [TestMethod]
        public void TestExecuteJobByJobManager()
        {
            JobAgsService agsService = new JobAgsService(
                    "http://ags101.portalinflor.com.br/arcgis/rest/services/Treinamento/Teste/GPServer/Script/submitJob?f=json",
                    "http://ags101.portalinflor.com.br/arcgis/rest/services/Treinamento/Teste/GPServer/Script/jobs/{jobId}?f=json",
                    "http://ags101.portalinflor.com.br/arcgis/rest/services/Treinamento/Teste/GPServer/Script/jobs/{jobId}/cancel");

            AdminPanel2.Controllers.JobManager.JobManager jm = new AdminPanel2.Controllers.JobManager.JobManager();
            jm.ExecuteJob(agsService);
            Thread.Sleep(1000);
            Assert.AreNotEqual(JobStatus.none, jm.status);
            Thread.Sleep(1000);
            Assert.AreNotEqual(JobStatus.executionError, jm.status);
            Thread.Sleep(1000);
            Assert.AreNotEqual(JobStatus.cancelled, jm.status);
            Thread.Sleep(1000);
            Assert.AreNotEqual(JobStatus.cancelling, jm.status);
            Thread.Sleep(1000);
            Assert.AreNotEqual(JobStatus.failed, jm.status);
        }
    }
}
