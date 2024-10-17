namespace LibraryManagementSystem.Helpers
{
    public class ResponseDetail<T>
    {
        public bool IsSuccessfull {  get; set; }
        public string? Message { get; set; }
        public T Result { get; set; }
        public int? StatusCode { get; set; }


        public ResponseDetail<T> FailedResultData(T res)
        {
            var k = new ResponseDetail<T>
            {
                Message = "Operation was not successful" ,
                IsSuccessfull = false ,
                Result = res,
                StatusCode = 400
            };
            return k;
        }

        public ResponseDetail<T> FailedResultData(T res , string message)
        {
            var k = new ResponseDetail<T>
            {
                Message = message,
                IsSuccessfull = false,
                Result = res,
                StatusCode = 400
            };
            return k;
        }

         public ResponseDetail<T> FailedResultData(T res , string message, int code)
        {
            var k = new ResponseDetail<T>
            {
                Message = message,
                IsSuccessfull = false,
                Result = res,
                StatusCode = code
            };
            return k;
        } 
        
        public ResponseDetail<T> FailedResultData(string message, int code)
        {
            var k = new ResponseDetail<T>
            {
                Message = message,
                IsSuccessfull = false,
                StatusCode = code
            };
            return k;
        }   
        
        public ResponseDetail<T> FailedResultData(string message)
        {
            var k = new ResponseDetail<T>
            {
                Message = message,
                IsSuccessfull = false,
                
            };
            return k;
        }
       
        public ResponseDetail<T> SuccessResultData(T res)
        {
            var k = new ResponseDetail<T>
            {
                Message = "Operation was not successful" ,
                IsSuccessfull = false ,
                Result = res,
                StatusCode = 400
            };
            return k;
        }

        public ResponseDetail<T> SucessResultData(T res , string message)
        {
            var k = new ResponseDetail<T>
            {
                Message = message,
                IsSuccessfull = false,
                Result = res,
                StatusCode = 400
            };
            return k;
        }

         public ResponseDetail<T> SuccessResultData(T res , string message, int code)
        {
            var k = new ResponseDetail<T>
            {
                Message = message,
                IsSuccessfull = false,
                Result = res,
                StatusCode = code
            };
            return k;
        } 
        
        public ResponseDetail<T> SuccessResultData(string message, int code)
        {
            var k = new ResponseDetail<T>
            {
                Message = message,
                IsSuccessfull = false,
                StatusCode = code
            };
            return k;
        }   
        
        public ResponseDetail<T> SuccessResultData(string message)
        {
            var k = new ResponseDetail<T>
            {
                Message = message,
                IsSuccessfull = false,
                
            };
            return k;
        }
       


    }
}
