namespace LifeManager.Domain
{
    public abstract class Notificacao
    {
        public string Mensagem { get; private set; }
        public bool Sucesso { get; private set; }
        public Notificacao(string mensagem, bool sucesso)
        {
            Mensagem = mensagem;
            Sucesso = sucesso;
        }
    }

    public class NotificacaoItens 
    {
        public string Erro { get; private set; }

        public NotificacaoItens(string erro)
        {
            this.Erro = erro;
        }
    }
}
