using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class Result<T>
    {
        public Result(T data)
        {
            Data = data;
            Succeeded = true;
            Error = null;
        }

        public Result(Error error)
        {
            Data = default(T);
            Succeeded = false;
            Error = error;
        }

        public bool Succeeded { get; set; }
        public Error? Error { get; set; }
        public T? Data { get; set; }

        public static implicit operator Result<T>(T data) => new(data);
        public static implicit operator Result<T>(Error error) => new(error);

        public TRes Handle<TRes>(Func<T, TRes> onSuccess, Func<Error, TRes> onError)
        {
            if (Succeeded)
            {
                return onSuccess.Invoke(Data);
            }
            else
            {
                return onError.Invoke(Error);
            }
        }
    }

    public class Result
    {
        public Result()
        {
            Succeeded = true;
            Error = null;
        }

        public Result(Error error)
        {
            Succeeded = false;
            Error = error;
        }

        public bool Succeeded { get; set; }
        public Error? Error { get; set; }

        public static Result SUCCESS => new Result();

        public static implicit operator Result(Error error) => new(error);

        public T Handle<T>(Func<T> onSuccess, Func<Error, T> onError)
        {
            if (Succeeded)
            {
                return onSuccess.Invoke();
            }
            else
            {
                return onError.Invoke(Error);
            }
        }
    }
}
