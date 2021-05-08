namespace MarkomPos.DataInterface.Common.Response
{
    public interface IResponseDTO
    {
        bool IsPassed { get; set; }
        string Message { get; set; }
        dynamic Data { get; set; }
    }
}
