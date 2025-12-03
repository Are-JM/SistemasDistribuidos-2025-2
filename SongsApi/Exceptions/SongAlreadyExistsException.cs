namespace SongsApi.Exceptions;

public class SongAlreadyExistsException : Exception
{
    public SongAlreadyExistsException(string songName) : base($"Song {songName} already exists")
    {
        
    }
}