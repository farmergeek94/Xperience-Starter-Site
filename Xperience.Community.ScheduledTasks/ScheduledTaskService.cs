using CMS.Scheduler.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Xperience.Community.ScheduledTasks
{
    internal class ScheduledTaskService
    {
        public IEnumerable<Type> AvailableTasks => Assembly.GetExecutingAssembly().GetTypes().Where(x=>x.IsClass && x.IsInstanceOfType(typeof(ITask)));
    }
}
