namespace DrahtenWeb.Dtos
{
    public class ResponseDto
    {
        public bool IsSuccess { get; set; } = false;
        public object Result { get; set; }
    }
}
