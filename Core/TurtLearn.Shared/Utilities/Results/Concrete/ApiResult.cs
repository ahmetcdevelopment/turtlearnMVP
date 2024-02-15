using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Utilities.Results.Abstract;
using TurtLearn.Shared.Utilities.Results.ComplexTypes;

namespace TurtLearn.Shared.Utilities.Results.Concrete
{
    public class ApiResult : IResult
    {
        public ApiResult(string _Key, ResultStatus resultStatus, string message)
        {
            Key = _Key;
            ResultStatus = resultStatus;
            Message = message;
            ResponseDate = DateTime.Now;
        }
        public ApiResult(string _Key, ResultStatus resultStatus, string message, IDictionary<string, string[]> validationPairs)
        {
            Key = _Key;
            ResultStatus = resultStatus;
            Message = message;
            ResponseDate = DateTime.Now;
            ValidationPairs = validationPairs;
        }
        public ApiResult(string _Key, ResultStatus resultStatus, Exception exception)
        {
            ResultStatus = resultStatus;
            Key = _Key;
            Exception = exception;
            ResponseDate = DateTime.Now;
        }
        public ApiResult(string _Key, ResultStatus resultStatus, string message, Exception exception)
        {
            ResultStatus = resultStatus;
            Message = message;
            Key = _Key;
            ResponseDate = DateTime.Now;
            Exception = exception;
        }
        public string Key { get; set; }
        public DateTime ResponseDate { get; set; }
        public IDictionary<string,string[]> ValidationPairs { get; set; }
        public ResultStatus ResultStatus { get; }

        public string Message { get; }

        public Exception Exception { get; }
    }
}
