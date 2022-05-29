namespace testeWipro.Models
{
    public class ResponseModel
    {
        public ResponseModel(string message, object content)
        {
            Message = message;
            Content = content;
        }
        public ResponseModel(int statusCode, object content)
        {
            StatusCode = statusCode;
            Content = content;
        }

        public ResponseModel(string message, int statusCode)
        {
            Message = message;
            StatusCode = statusCode;
        }

        public ResponseModel(string message)
        {
            Message = message;
        }
        public ResponseModel(object content)
        {
            Content = content;
        }

        public ResponseModel() { }

        public int StatusCode { get; set; }
        public string Message { get; set; }
        public object Content { get; set; }
    }

}
