using System;
using System.Collections.Generic;
namespace AdminPanel2.Controllers.JobManager.Behaviors
{
    public interface JobBehavior
    {
        string execute();
        bool cancel();
        JobStatus status();
        List<Tuple<string,string>> getMessages();
    }
}
