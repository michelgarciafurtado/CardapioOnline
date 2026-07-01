using CardapioOnline.Consumer;
using CardapioOnline.Models;
using Compartilhado.Eventos;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddMassTransit(busConfigurator => {

    busConfigurator.AddConsumer<ProdutoCriadoConsumer>();

    busConfigurator.UsingRabbitMq((busContext, rabbitCfg) =>
    {
        rabbitCfg.Message<ProdutoCriadoEvento>(x => x.SetEntityName("produto_criado_event"));
        rabbitCfg.Host(builder.Configuration["RabbitMQ:HostName"],"/", h =>
        {
            h.Username(builder.Configuration["RabbitMQ:UserName"]);
            h.Password(builder.Configuration["RabbitMQ:Password"]);
        });

        // Configura a fila para este consumer automaticamente 
        rabbitCfg.ReceiveEndpoint("produto_queue", e =>
        {
            
            e.ConfigureConsumer<ProdutoCriadoConsumer>(busContext);
            
        });
        
    });
    busConfigurator.AddRequestClient<ObterProdutosRequest>(new Uri("queue:fila_obter_produtos"));
});

builder.Services.AddSingleton<Cardapio>();
builder.Services.AddHostedService<CardapioInitializer>();
builder.Services.AddHostedService<CardapioUpdater>();
builder.Services.AddSignalR();

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
