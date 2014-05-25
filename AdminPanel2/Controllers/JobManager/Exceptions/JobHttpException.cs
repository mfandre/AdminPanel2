using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminPanel2.Controllers.JobManager.Exceptions
{
    public class JobHttpException : Exception
    {
        public JobHttpException(string message)
            : base(message)
        {
        }

        public JobHttpException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}