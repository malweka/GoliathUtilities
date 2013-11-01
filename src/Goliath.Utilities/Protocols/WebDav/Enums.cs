using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Goliath.Protocols.WebDav
{
    public enum DavWebMethod
    {
        Undefined = 0,
        Get,
        Head,
        Post,
        Delete,
        Put,
        Options,
        PropFind,
        PropPatch,
        MkCol,
        Copy,
        Move,
        Lock,
        Unlock,
    }

    public enum StatusCode
    {
        Ok = 200,
        Created = 201,
        NoContent = 204,
        MultiStatus = 207,
        BadRequest = 400,
        Unauthorized = 401,
        Forbidden = 403,
        NotFound = 404,
        MethodNotAllowed = 405,
        Conflict = 409,
        PreconditionFailed = 412,
        RequestUriTooLong = 414,
        UnsupportedMediaType = 415,
        UnprocessableEntity = 422,
        Locked = 423,
        FailedDependency = 424,
        BadGateway = 502,
        InsufficientStorage = 507,
    }
}
