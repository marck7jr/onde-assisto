namespace OndeAssisto.Common.Models
{
    public class Usuario : Entidade
    {
        private string nome;
        private string email;
        private bool administrador;

        public string Nome { get => nome; set => Set(ref nome, value); }
        public string Email { get => email; set => Set(ref email, value); }
        public bool Administrador { get => administrador; set => Set(ref administrador, value); }
    }
}
