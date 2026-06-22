using CardapioOnline.Consumer;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddMassTransit(busConfigurator => {

    busConfigurator.AddConsumer<ProdutoCriadoConsumer>();

    busConfigurator.UsingRabbitMq((busContext, rabbitCfg) =>
    {
        rabbitCfg.Host(builder.Configuration["RabbitMQ:HostName"], h =>
        {
            h.Username(builder.Configuration["RabbitMQ:UserName"]);
            h.Password(builder.Configuration["RabbitMQ:Password"]);
        });
        var nomeDaFila = builder.Configuration["RabbitMQ:FilaProduto"];

        rabbitCfg.PrefetchCount = 16;
        rabbitCfg.ReceiveEndpoint(nomeDaFila!, endpoint =>
        {
            // Vincula o consumidor a esta fila específica
            endpoint.ConfigureConsumer<ProdutoCriadoConsumer>(busContext);
        });
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
