namespace SongsApi.Exceptions;

public class SongNotFoundException : Exception
{
    public SongNotFoundException(Guid id) : base($"Song {id} not found")
    {}
}