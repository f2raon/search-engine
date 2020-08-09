using SearchEngine.Utils;

namespace SearchEngine.Models
{
    public class ResponseModel<T>
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public string Comment { get; set; }
        public T Data { get; set; }

        public ResponseModel(int inCode, string inMessage, string inComment, T inData)
        {
            Code = inCode;
            Message = inMessage.ToStr();
            Comment = inComment.ToStr();
            Data = inData;
        }

        public ResponseModel(int inCode, string inMessage, string inComment)
        {
            Code = inCode;
            Message = inMessage.ToStr();
            Comment = inComment.ToStr();
        }
    }
}
