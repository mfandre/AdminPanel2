using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web.Helpers;
using AdminPanel2.Controllers.JobManager.Exceptions;

namespace AdminPanel2.Controllers.JobManager.Behaviors.AgsService
{

    public class BaseAgsServiceBehavior : JobBehavior
    {
        public string jobExecuteUrl { get; set; }
        public string jobCancelUrl { get; set; }
        public string jobStatusUrl { get; set; }
        private List<Tuple<string,string>> messages;

        public BaseAgsServiceBehavior(string ExecuteUrl, string CancelUrl, string StatusUrl)
        {
            this.jobCancelUrl = CancelUrl;
            this.jobExecuteUrl = ExecuteUrl;
            this.jobStatusUrl = StatusUrl;
            messages = new List<Tuple<string, string>>();
        }

        private string RequestUrl(string url)
        {
            try
            {
                // Create a request for the URL. 
                WebRequest request = WebRequest.Create(url);
                // If required by the server, set the credentials.
                request.Credentials = CredentialCache.DefaultCredentials;
                // Get the response.
                WebResponse response = request.GetResponse();
                // Display the status.
                Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                if (((HttpWebResponse)response).StatusCode == HttpStatusCode.BadRequest)
                    throw new JobHttpException("Servidor respondeu com código 400.");
                else if (((HttpWebResponse)response).StatusCode == HttpStatusCode.BadGateway)
                    throw new JobHttpException("Servidor respondeu com código 502.");
                else if (((HttpWebResponse)response).StatusCode == HttpStatusCode.Forbidden)
                    throw new JobHttpException("Servidor respondeu com código 403.");
                // Get the stream containing content returned by the server.
                Stream dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
                // Display the content.
                Console.WriteLine(responseFromServer);
                // Clean up the streams and the response.
                reader.Close();
                response.Close();
                return responseFromServer;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Executa a GPTool
        /// </summary>
        /// <returns>Retorna o JOBID do GPTool</returns>
        public string execute()
        {
            try
            {
                string responseFromServer = RequestUrl(this.jobExecuteUrl);

                var dynamicObject = Json.Decode(responseFromServer);

                //{"error":{"code":400,"message":"Invalid or missing input parameters.","details":[]}}
                if (dynamicObject.error != null)
                {
                    throw new JobHttpException(dynamicObject.error.code + ":" + dynamicObject.error.message);
                }

                jobStatusUrl = jobStatusUrl.Replace("{jobId}", dynamicObject.jobId);

                return dynamicObject.jobId;
            }
            catch
            {
                throw;
            }
        }
        
        public bool cancel()
        {
            return false;
        }

        public JobStatus status()
        {
            try
            {
                string responseFromServer = RequestUrl(this.jobStatusUrl);

                var dynamicObject = Json.Decode(responseFromServer);

                //{"error":{"code":400,"message":"Invalid or missing input parameters.","details":[]}}
                if (dynamicObject.error != null)
                {
                    throw new JobHttpException(dynamicObject.error.code + ":" + dynamicObject.error.message);
                }

                if (dynamicObject.jobStatus == "esriJobWaiting")
                    return JobStatus.waiting;
                else if (dynamicObject.jobStatus == "esriJobSubmitted")
                    return JobStatus.submitted;
                else if (dynamicObject.jobStatus == "esriJobExecuting")
                    return JobStatus.executing;
                else if (dynamicObject.jobStatus == "esriJobCancelling")
                    return JobStatus.cancelling;
                else if (dynamicObject.jobStatus == "esriJobSucceeded")
                    return JobStatus.succeeded;
                else if (dynamicObject.jobStatus == "esriJobCancelled")
                    return JobStatus.cancelled;
                else if (dynamicObject.jobStatus == "esriJobFailed")
                    return JobStatus.failed;

                return JobStatus.none;
            }
            catch
            {
                throw;
            }
        }

        public List<Tuple<string,string>> getMessages()
        {
            try
            {
                string responseFromServer = RequestUrl(this.jobStatusUrl);

                var dynamicObject = Json.Decode(responseFromServer);

                if (dynamicObject.messages != null)
                {
                    foreach (var msg in dynamicObject.messages)
                    {
                        messages.Add(new Tuple<string, string>(msg.type, msg.description));
                    }
                }

                return messages;
            }
            catch
            {
                throw;
            }
        }
    }
}