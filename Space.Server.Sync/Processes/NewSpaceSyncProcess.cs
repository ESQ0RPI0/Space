using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space.Server.Sync.Processes
{
    public class NewSpaceSyncProcess
    {
        public NewSpaceSyncProcess()
        {
            
        }

        public Task Sync()
        { 
            return Task.CompletedTask;
        }
    }
}
