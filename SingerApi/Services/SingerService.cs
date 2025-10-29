using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using SingerApi.Repositories;
using SingerApi.Mappers;

namespace SingerApi.Services;

public class SingerService : SingerApi.SingerService.SingerServiceBase
{
    private readonly ISingerRepository _singerRepository;

    public SingerService(ISingerRepository singerRepository)
    {
        
        _singerRepository = singerRepository;
    }
    public override async Task<SingerResponse> GetSingerById(SingerByIdRequest request, ServerCallContext context)
    {
        var singer = await _singerRepository.GetByIdAsync(request.Id, context.CancellationToken);
        if (singer is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"Singer with ID {request.Id} not found."));
        }
        return singer.ToResponse();
    }

    public override async Task<CreateSingerResponse> CreateSingers(IAsyncStreamReader<CreateSingerRequest> requestStream, ServerCallContext context)
    {
        var createdSingers = new List<SingerResponse>();

        while (await requestStream.MoveNext(context.CancellationToken))
        {
            var request = requestStream.Current;
            var singer = request.ToModel();
            var singerExists = await _singerRepository.GetByNameAsync(singer.Name, context.CancellationToken);

            if (singerExists.Any())
                continue;
            var createdSinger = await _singerRepository.CreateAsync(singer, context.CancellationToken);
            createdSingers.Add(createdSinger.ToResponse());
        }

        return new CreateSingerResponse
        {
            SuccessCount = createdSingers.Count,
            Singers = { createdSingers }
        };
    }
}