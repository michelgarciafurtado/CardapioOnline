using CardapioOnline.Models;

namespace CardapioOnline.Consumer
{
    public class CardapioInitializer:IHostedService
    {
        private readonly Cardapio _cardapio;
        private readonly IServiceProvider _serviceProvider;

        public CardapioInitializer(Cardapio cardapio, IServiceProvider serviceProvider)
        {
            _cardapio = cardapio;
            _serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
