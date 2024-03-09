using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutoringRequest.Models.DTO.Http;

public class HttpResponse<T> where T : class
{
    public T? Value { get; set; }
    public List<T>? Values { get; set; }

    public bool IsSuccess { get; set; }

}
