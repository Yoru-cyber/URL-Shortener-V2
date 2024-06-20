using System.Text;
using StackExchange.Redis;
var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:5001");
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
    if (request.HasFormContentType is false){
        return Results.BadRequest(new {message = "Body does not contain form"});
    }
    var form = await request.ReadFormAsync();
    if (string.IsNullOrEmpty(form["url"]) is true)
    {
        return Results.BadRequest("Url is empty");
    }
    string url = form["url"];
    string newUrl = randomString();
    db.StringSet(newUrl, url);
    return Results.Ok(new { message = "Success", newUrl = "http://localhost:3001/" + newUrl });


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