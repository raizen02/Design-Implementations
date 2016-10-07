using Microsoft.Diagnostics.Tracing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;

namespace ecrm.Infrastructure.Logging
{
    [EventSource(Name = "Filinvest-ECRM")]
    public sealed class EcrmEventSource : EventSource
    {
        public static EcrmEventSource Log = new EcrmEventSource();

        public class Keywords
        {
            public const EventKeywords Method = (EventKeywords)0x0001;
        }

        public class Tasks
        {
            public const EventTask Method = (EventTask)0x1;
        }

        private const int MethodStartEventId = 1;
        private const int MethodStopEventId = 2;
        private const int ErrorEventId = 3;
        private const int WarningEventId = 4;
        private const int InfoEventId = 5;
        private const int DebugEventId = 6;

        [Event(MethodStartEventId, Keywords = Keywords.Method, Message = "Start - Class: {0}, Method: {1}",
         Channel = EventChannel.Admin, Task = Tasks.Method, Opcode = EventOpcode.Start, Level = EventLevel.Informational)]
        public void MethodStart(string className, [CallerMemberName]string methodName = "")
        {
            WriteEvent(MethodStartEventId, className, methodName);
        }

        [Event(MethodStopEventId, Keywords = Keywords.Method, Message = "Stop - Class: {0}, Method: {1}",
         Channel = EventChannel.Admin, Task = Tasks.Method, Opcode = EventOpcode.Stop, Level = EventLevel.Informational)]
        public void MethodStop(string className, [CallerMemberName]string methodName = "")
        {
            WriteEvent(MethodStopEventId, className, methodName);
        }

        [Event(ErrorEventId, Message = "Exception in Class: {0}, Method: {2}, Message: {1}", Channel = EventChannel.Admin, Level = EventLevel.Error)]
        public void Error(string className, string message, [CallerMemberName]string methodName = "")
        {
            WriteEvent(ErrorEventId, className, methodName, message);
        }

        [Event(WarningEventId, Message = "Warning in Class: {0}, Method: {2}, Message: {1}", Channel = EventChannel.Admin, Level = EventLevel.Warning)]
        public void Warning(string className, string message, [CallerMemberName]string methodName = "")
        {
            WriteEvent(WarningEventId, className, methodName, message);
        }

        [Event(InfoEventId, Message = "Class: {0}, Method: {2}, Message: {1}")]
        public void Info(string className, string message, [CallerMemberName]string methodName = "")
        {
            WriteEvent(InfoEventId, className, methodName, message);
        }

        [Event(DebugEventId, Message = "Class: {0}, Method: {2}, Message: {1}", Level = EventLevel.Verbose)]
        public void Debug(string className, string message, [CallerMemberName]string methodName = "")
        {
            WriteEvent(DebugEventId, className, methodName, message);
        }
    }
}