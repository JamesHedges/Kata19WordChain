using MediatR;

namespace KataDataLoader
{
    public class LoadTestDataCommand : IRequest<LoadTestDataResponse>
    {
        public string BaseUrl { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
    }

    public class LoadTestDataResponse
    { }
}
