namespace MoserBlog.MediaTool.Api.Clients.Dtos;

public record MediaResultDto {
    public byte[] FileArray { get; set; }
    public string ContentType { get; set; }


    public MediaResultDto()
    {
        FileArray = new byte[0];
        ContentType = string.Empty;
    }

    public MediaResultDto(byte[] fileArray, string contentType)
    {
        FileArray = fileArray;
        ContentType = contentType;
    }
}
