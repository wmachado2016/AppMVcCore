using WSM.ResApi.Models.Interface;

namespace WSM.ResApi.Models
{
    public class Conta: Entity
    {
        public Conta(string nome, string descricao)
        {
            Nome = nome;
            Descricao = descricao;
        }

        public string Nome { get; set; }
        public string Descricao { get; set; }
    }
}
