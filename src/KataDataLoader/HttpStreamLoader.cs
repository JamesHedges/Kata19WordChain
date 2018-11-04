using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace KataDataLoader
{
    public class LoadTestDataHandler : IRequestHandler<LoadTestDataCommand, LoadTestDataResponse>
    {
        private async Task<Stream> GetHttpStreamAsync(string baseUrl, string fileName)
        {
            HttpClient client;
            client = new HttpClient()
            {
                BaseAddress = new Uri(baseUrl)
            };

            var response = await client.GetStreamAsync(fileName);
            return response;
        }

        public async Task<LoadTestDataResponse> Handle(LoadTestDataCommand request, CancellationToken cancellationToken)
        {
            using (var outStream = File.Create($@"{request.FilePath}\{request.FileName}"))
            using (Stream inStream = await GetHttpStreamAsync(request.BaseUrl, request.FileName))
            {
                inStream.CopyTo(outStream);
            }
            return new LoadTestDataResponse();
        }
    }
}
