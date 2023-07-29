using WSM.WebApp.MVC.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace WSM.WebApp.MVC.Services
{
    public interface IComunicacaoService
    {
        Task<UsuarioRespostaLogin> Login(UsuarioLogin usuarioLogin);

        Task<UsuarioRespostaLogin> Registro(UsuarioRegistro usuarioRegistro);

        Task<IEnumerable<ContaViewModel>> ObterTodas();
        Task<ContaViewModel> ObterPorId(Guid id);
        Task<ContaViewModel> AdicionarConta(ContaViewModel contaViewModel);
        Task<ContaViewModel> AtualizarConta(ContaViewModel contaViewModel);
        Task<ContaViewModel> RemoverConta(ContaViewModel contaViewModel);
    }
}