namespace OtakuSect.BussinessLayer.Helper
{
    public static class ResponseCreater<T>
    {
        public static ApiResponse<T> CreateSuccessResponse(T data, string message = "Successful")
        {
            return new ApiResponse<T>
            {
                StatusCode = 200,
                Success = true,
                Message = message,
                Data = data
            };
        }

        public static ApiResponse<T> CreateErrorResponse(T data, string message = "Error")
        {
            return new ApiResponse<T>
            {
                StatusCode = 500,
                Success = false,
                Message = message,
                Data = data
            };
        }

        public static ApiResponse<T> CreateNotFoundResponse(T data, string message = "Not Found")
        {
            return new ApiResponse<T>
            {
                StatusCode = 404,
                Success = false,
                Message = message,
                Data = data
            };
        }

        public static ApiResponse<T> CreateDuplicateExistResponse(T duplicateKeys, string message = "Already exists")
        {
            return new ApiResponse<T>
            {
                StatusCode = 409,
                Success = false,
                Message = message,
                Data = duplicateKeys
            };
        }
    }
}
