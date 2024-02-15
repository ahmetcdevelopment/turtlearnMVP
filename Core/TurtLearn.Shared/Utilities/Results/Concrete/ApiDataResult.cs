using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtLearn.Shared.Utilities.Results.Abstract;
using TurtLearn.Shared.Utilities.Results.ComplexTypes;

namespace TurtLearn.Shared.Utilities.Results.Concrete;

public class ApiDataResult<T> : IDataResult<T>
{
    public ApiDataResult(string _Key, ResultStatus resultStatus, T data)
    {
        ResultStatus = resultStatus;
        Data = data;
        Key = _Key;
        ResponseDate = DateTime.Now;
    }
    public ApiDataResult(string _Key, ResultStatus resultStatus, T data, IDictionary<string, string[]> validationPairs)
    {
        ResultStatus = resultStatus;
        Data = data;
        Key = _Key;
        ResponseDate = DateTime.Now;
        ValidationPairs = validationPairs;
    }


    public ApiDataResult(string _Key, ResultStatus resultStatus, string message, T data)
    {
        ResultStatus = resultStatus;
        Message = message;
        Key = _Key;
        Data = data;
        ResponseDate = DateTime.Now;
    }
    public ApiDataResult(string _Key, ResultStatus resultStatus, string message, T data, IDictionary<string, string[]> validationPairs)
    {
        ResultStatus = resultStatus;
        Message = message;
        Key = _Key;
        Data = data;
        ResponseDate = DateTime.Now;
        ValidationPairs = validationPairs;
    }
    public ApiDataResult(string _Key, ResultStatus resultStatus, string message, T data, Exception exception)
    {
        ResultStatus = resultStatus;
        Message = message;
        Key = _Key;
        Data = data;
        Exception = exception;
        ResponseDate = DateTime.Now;
    }
    public string Key { get; set; }
    public DateTime ResponseDate { get; set; }
    public IDictionary<string, string[]> ValidationPairs { get; set; }
    public T Data { get; }

    public ResultStatus ResultStatus { get; }

    public string Message { get; }

    public Exception Exception { get; }
}
