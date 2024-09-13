using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ZimoziAssesment.Error
{
    public static class ResponseExtension
    {
        public static IActionResult GetResponse<T>(this Response<T> response , ControllerBase controller)
        {
            return response.StatusCode switch
            {
                200 => controller.Ok(response),
                400 => controller.BadRequest(response),
                404 => controller.NotFound(response),
                500 => controller.StatusCode(500 , response)
            };
        }
    }
}
