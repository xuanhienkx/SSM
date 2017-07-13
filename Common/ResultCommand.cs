using System;
using System.Collections.Generic;

namespace SSM.Common
{
    public class ResultCommand
    {
        public bool IsFinished { get; set; }
        public string Message { get; set; }
    }
    public interface IResult
    {
        bool IsSuccess { get; }
        IList<string> ErrorResults { get; }
    }
    public class CommandResult : IResult
    {
        public CommandResult(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        public bool IsSuccess { get; set; }

        public Exception Exception { get; set; }

        public IList<string> ErrorResults { get; set; }

        public static IResult ErrorResult(params string[] messages)
        {
            return new CommandResult(false)
            {
                ErrorResults = messages
            };
        }
    }
}