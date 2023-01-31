using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.IO;

namespace MVCCoreApplication.Logging
{
    public class UserLogger:ActionFilterAttribute
    {
        readonly string _fileName ="user.txt";
        readonly string _filePath;
        DateTime _endTime;
        DateTime _startTime;
        TimeSpan _duration;
       
        public UserLogger(IWebHostEnvironment environment)
        {
           _filePath = environment.ContentRootPath + @$"\\ Logging " + @$"{_fileName}";
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //start time
            _startTime = DateTime.Now;
            var controllerName = ((ControllerBase)context.Controller).ControllerContext.ActionDescriptor.ControllerName;
            var actionName = ((ControllerBase)context.Controller).ControllerContext.ActionDescriptor.ActionName; 
            
            using(StreamWriter writer = new StreamWriter(_filePath))
            {
                writer.Write($"StartTime :: {_startTime}" +
                            $"\tControllerName :: {controllerName}" +
                            $"\t ActionName :: {actionName}");   
                   
            }
           
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
           _endTime= DateTime.Now;
            _duration = _endTime- _startTime;
            File.AppendAllText(_filePath, $"\t TotalTime In Seconds :: {_duration.TotalSeconds}\n");
            // end time
            // total time = endtime-starttime
        }
    }
}
