using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using WSM.ResApi.Data.Repository;
using WSM.ResApi.Models;

namespace WSM.ResApi.Controllers.V1
{
    [Route("api/v1/conta")]
    public class ContaComunicacaoController : MainController
    {
        private readonly IContaRepository _contaRepository;
        private readonly IMapper _mapper;

        public ContaComunicacaoController(IContaRepository repository, IMapper mapper)
        {
            _contaRepository = repository;
            _mapper = mapper;
        }

        [HttpGet("buscar-cep")]
        public ActionResult<Endereco> BuscarEndereco(string cep)
        {
            if(cep.Length < 7)
            {
                AdicionarErroProcessamento("Cep inválido");
                return CustomResponse();
            }

            var client = new RestClient("http://viacep.com.br/ws/");
            var request = new RestRequest(cep + "/json/");
            var response = client.ExecuteGet(request);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var data = JsonSerializer.Deserialize<Endereco>(response.Content!, options)!;

            return CustomResponse(data);
        }

        [HttpGet("obter-todas")]
        public async Task<ActionResult<IEnumerable<ContaViewModel>>> ObterTodos()
        {
            return CustomResponse( _mapper.Map<IEnumerable<ContaViewModel>>(await _contaRepository.ObterTodos()));
        }

        [HttpGet("obter-conta/{id}")]
        public async Task<ActionResult<ContaViewModel>> ObterConta(Guid id)
        {
            var contaViewModel = _mapper.Map<ContaViewModel>(await _contaRepository.ObterPorId(id));
            if (contaViewModel == null)
            {
                AdicionarErroProcessamento("Conta não existe");
                return CustomResponse();
            }

            return CustomResponse(contaViewModel);
        }


        [HttpPut("atualizar/{id}")]
        public async Task<IActionResult> PutContaViewModel(Guid id, ContaViewModel contaViewModel)
        {
            if (id != contaViewModel.Id)
            {
                AdicionarErroProcessamento("Conta não existe");
                return CustomResponse();
            }

            if (!ModelState.IsValid) return CustomResponse();

            try
            {
                var conta = await _contaRepository.ObterPorId(id);

                conta.Nome = contaViewModel.Nome;
                conta.Descricao = contaViewModel.Descricao;
                _contaRepository.Atualizar(conta);

                return CustomResponse(conta);

            }
            catch (DbUpdateConcurrencyException)
            {
                return CustomResponse();
            }
        }

        [HttpPost("adicionar")]
        public ActionResult<Conta> Adicionar(Conta conta)
        {
            if (conta == null)
            {
                AdicionarErroProcessamento("Objeto com dados vazios");
                return CustomResponse();
            }

            _contaRepository.Adicionar(conta);

            return CreatedAtAction("ObterConta", new { id = conta.Id }, conta);
        }

        [HttpDelete("remover/{id}")]
        public async Task<ActionResult> DeleteContaViewModel(Guid id)
        {
            var conta = await _contaRepository.ObterPorId(id);
            if (conta == null)
            {
                AdicionarErroProcessamento("Conta não existe");
                return CustomResponse();
            }

            try
            {
                _contaRepository.Remover(conta);
                return CustomResponse();
            }
            catch (Exception)
            {
                return CustomResponse();
            }
        }

    }
}