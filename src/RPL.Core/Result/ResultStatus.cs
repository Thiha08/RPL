using System.ComponentModel;

namespace RPL.Core.Result
{
    public enum ResultStatus
    {
        [Description("Ok")]
        Ok = 200,

        //[Description("Created")]
        //Created = 201,

        //[Description("Accepted")]
        //Accepted = 202,

        //[Description("NoContent")]
        //NoContent = 204,

        [Description("BadRequest")]
        BadRequest = 400,

        //[Description("Unauthorized")]
        //Unauthorized = 401,

        //[Description("Forbidden")]
        //Forbidden = 403,

        //[Description("NotFound")]
        //NotFound = 404,

        [Description("InternalServerError")]
        InternalServerError = 500,


    }
}
