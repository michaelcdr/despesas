using LifeManager.Domain;

namespace LifeManager.Application.Despesas.Responses
{
    public class AtualizarDespesaResponse : Notificacao
    {
        public AtualizarDespesaResponse(string mensagem, bool sucesso) : base(mensagem, sucesso)
        {

        }
    }
}
