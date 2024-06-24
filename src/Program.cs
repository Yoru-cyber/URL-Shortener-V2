using System.Text;
using StackExchange.Redis;
var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:5001")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                      });
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}
app.UseCors(MyAllowSpecificOrigins);
var options = ConfigurationOptions.Parse("redis:6379");
ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(options);
IDatabase db = redis.GetDatabase();
string characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
app.UseHttpsRedirection();
string randomString()
{
    StringBuilder shortenedURl = new();
    Random rand = new();
    for (int i = 0; i < 5; i++)
    {
        int number = rand.Next(characters.Length);
        shortenedURl.Append(characters[number]);
    }

    return shortenedURl.ToString();
}

app.MapPost("/shorten", async (HttpRequest request) =>
{
    
    if (request.HasJsonContentType() is false){
        return Results.BadRequest(new {message = "Body is not in json format"});
    }
    var response = await request.ReadFromJsonAsync<ShortenedURL>() ?? throw new Exception("Response is null");
    if (string.IsNullOrEmpty(response.URL) is true)
    {
        return Results.BadRequest("Url is empty");
    }
    string shortenedURl = randomString();
    db.StringSet(shortenedURl, response.URL);
    return Results.Ok(new { message = "Success", shortenedURL = "http://localhost:3001/" + shortenedURl });


});
app.MapGet("/{url}", (HttpContext context, string url) =>
{
    RedisValue url2 = db.StringGet(url);
    if (url2.IsNullOrEmpty is false)
    {   
        return Results.Redirect(url2);
    }
    return Results.BadRequest(new { messsage = "Url does not exist or is empty" });

});
app.Run();
record ShortenedURL(string URL);