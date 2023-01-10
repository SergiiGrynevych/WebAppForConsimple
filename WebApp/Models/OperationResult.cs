namespace WebApp.Models
{
    public class OperationResult
    {

        public object Item { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public OperationResult(bool success, object items)
        {
            IsSuccess = success;
            Item = items;
        }

        public OperationResult(bool success, object items, string message)
        {
            IsSuccess = success;
            Item = items;
            Message = message;
        }

        public OperationResult(bool success, string message)
        {
            IsSuccess = success;
            Message = message;
        }

        public OperationResult(bool success)
        {
            IsSuccess = success;
        }
    }
}
