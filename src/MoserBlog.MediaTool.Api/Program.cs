using Azure.Storage.Blobs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/image", (string imageName) => {
    var blobServiceClient = new BlobServiceClient(builder.Configuration.GetConnectionString("BlobConnection"));
    var containerClient = blobServiceClient.GetBlobContainerClient(builder.Configuration["BlobConfig:ContainerName"]);

    var blobClient = containerClient.GetBlobClient(imageName);

    return $"{containerClient.Uri}/{blobClient.Name}";
});

app.UseHttpsRedirection();

app.Run();