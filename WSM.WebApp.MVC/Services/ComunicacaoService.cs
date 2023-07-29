using WSM.WebApp.MVC.Extensions;
using WSM.WebApp.MVC.Models;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WSM.WebApp.MVC.Services
{
    public class ComunicacaoService : Service, IComunicacaoService
    {
        private readonly HttpClient _httpClient;

        public ComunicacaoService(HttpClient httpClient, 
                                   IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.AutenticacaoUrl);

            _httpClient = httpClient;
        }

        #region Login
        public async Task<UsuarioRespostaLogin> Registro(UsuarioRegistro usuarioRegistro)
        {
            var registroContent = ObterConteudo(usuarioRegistro);

            var response = await _httpClient.PostAsync("/api/v1/identidade/novo-login", registroContent);

            if (!TratarErrosResponse(response))
            {
                return new UsuarioRespostaLogin
                {
                    ResponseResult = await DeserializarObjetoResponse<ResponseResult>(response)
                };
            }

            return await DeserializarObjetoResponse<UsuarioRespostaLogin>(response);
        }

        public async Task<UsuarioRespostaLogin> Login(UsuarioLogin usuarioLogin)
        {
            var loginContent = ObterConteudo(usuarioLogin);

            var response = await _httpClient.PostAsync("/api/v1/identidade/autenticar", loginContent);

            if (!TratarErrosResponse(response))
            {
                return new UsuarioRespostaLogin
                {
                    ResponseResult = await DeserializarObjetoResponse<ResponseResult>(response)
                };
            }

            return await DeserializarObjetoResponse<UsuarioRespostaLogin>(response);
        }
        #endregion


        public async Task<ContaViewModel> AdicionarConta(ContaViewModel contaViewModel)
        {
            var contaAdicionar = ObterConteudo(contaViewModel);
            var response = await _httpClient.PostAsync($"/api/v1/conta/adicionar", contaAdicionar);

            if (!TratarErrosResponse(response))
            {
                return null;
            }

            return await DeserializarObjetoResponse<ContaViewModel>(response);
        }

        public async Task<ContaViewModel> ObterPorId(Guid id)
        {
            var response = await _httpClient.GetAsync($"/api/v1/conta/obter-conta/{id}");

            if (!TratarErrosResponse(response))
            {
                return null;
            }

            return await DeserializarObjetoResponse<ContaViewModel>(response);
        }

        public async Task<IEnumerable<ContaViewModel>> ObterTodas()
        {
            var response = await _httpClient.GetAsync("/api/v1/conta/obter-todas");

            if (!TratarErrosResponse(response))
            {
                return null;
            }

            return await DeserializarObjetoResponse<IEnumerable<ContaViewModel>>(response);
        }

        public async Task<ContaViewModel> AtualizarConta(ContaViewModel contaViewModel)
        {
            var contaAdicionar = ObterConteudo(contaViewModel);
            var response = await _httpClient.PutAsync($"/api/v1/conta/atualizar/{contaViewModel.Id}", contaAdicionar);

            if (!TratarErrosResponse(response))
            {
                return null;
            }

            return await DeserializarObjetoResponse<ContaViewModel>(response);
        }

        public async Task<ContaViewModel> RemoverConta(ContaViewModel contaViewModel)
        {
            var response = await _httpClient.DeleteAsync($"/api/v1/conta/remover/{contaViewModel.Id}");

            if (!TratarErrosResponse(response))
            {
                return null;
            }

            return null;
        }
    }
}