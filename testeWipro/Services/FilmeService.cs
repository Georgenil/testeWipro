using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;
using System.Threading.Tasks;
using testeWipro.Data;
using testeWipro.Domain;
using testeWipro.Models;
using static testeWipro.Models.FilmeModels;

namespace testeWipro.Services
{
    public class FilmeService
    {
        private readonly ApplicationDbContext db;

        public FilmeService(ApplicationDbContext _db)
        {
            db = _db;
        }
        public async Task<ResponseModel> CadastrarFilme(Filme filme)
        {
            try
            {
                Filme filmeEncontrado = db.Filmes.FirstOrDefault(x => x.Nome.ToLower().Trim().Equals(filme.Nome.ToLower().Trim())
                && x.Ativo.Equals(true));

                if (filmeEncontrado == null)
                {
                    filme.DataCriacao = DateTime.Now;
                    filme.DataAtualizacao = DateTime.Now;
                    filme.Status = false;

                    db.Filmes.Add(filme);
                    await db.SaveChangesAsync();
                    return new ResponseModel(200, "Filme cadastrado!");
                }

                return new ResponseModel(500, "Filme já está cadastrado");
            }
            catch
            {
                return new ResponseModel(500, "Erro ao cadastrar filme");
            }
        }

        public async Task<ResponseModel> LocarFilme(FilmeModelSomenteIds model)
        {
            try
            {
                Filme filmeEncontrado = db.Filmes.FirstOrDefault(x => x.Id.Equals(model.FilmeId) && x.Ativo.Equals(true));
                Cliente clienteEncontrado = db.Clientes.FirstOrDefault(x => x.Id.Equals(model.ClienteId) && x.Ativo.Equals(true));
                Locacao locacaoEncontrada = db.Locacoes.FirstOrDefault(x => x.ClienteId.Equals(model.ClienteId) && x.Status.Equals(true));

                if (locacaoEncontrada == null)
                {
                    if (filmeEncontrado != null && clienteEncontrado != null)
                    {
                        if (filmeEncontrado.Status == false)
                        {
                            filmeEncontrado.DataAtualizacao = DateTime.Now;
                            filmeEncontrado.Status = true;

                            EntityEntry<Filme> resultado = db.Update(filmeEncontrado);
                            await db.SaveChangesAsync();

                            if (resultado.Entity.Status == true)
                            {
                                Locacao locacao = new();

                                locacao.FilmeId = resultado.Entity.Id;
                                locacao.ClienteId = clienteEncontrado.Id;
                                locacao.DataLocacao = DateTime.Now;
                                locacao.DataFim = locacao.DataLocacao.AddDays(10);
                                locacao.Descricao = "ALOCADO";
                                locacao.Status = true;

                                db.Locacoes.Add(locacao);
                                await db.SaveChangesAsync();
                                return new ResponseModel(200, "Filme alocado");
                            }
                        }
                    }
                }
                return new ResponseModel(500, "Cliente está com um filme no momento");
            }
            catch
            {
                return new ResponseModel(500, "Erro ao locar filme");
            }
        }

        public async Task<ResponseModel> DevolverFilme(FilmeModelSomenteIds model)
        {
            try
            {
                Locacao locacaoEncontrada = db.Locacoes.Include(x => x.Filme).Include(x => x.Cliente)
                    .FirstOrDefault(x => x.FilmeId.Equals(model.FilmeId) && x.ClienteId.Equals(model.ClienteId));

                if (locacaoEncontrada != null)
                {
                    locacaoEncontrada.Filme.DataAtualizacao = DateTime.Now;
                    locacaoEncontrada.DataDevolucao = DateTime.Now;
                    locacaoEncontrada.Filme.Status = false;
                    locacaoEncontrada.Status = false;

                    if (locacaoEncontrada.DataFim < locacaoEncontrada.DataDevolucao)
                    {
                        locacaoEncontrada.Descricao = "ATRASADO";
                    }

                    locacaoEncontrada.Descricao = "SEM ATRASO";

                    db.Locacoes.Update(locacaoEncontrada);
                    await db.SaveChangesAsync();

                    return new ResponseModel(200, "Filme devolvido");
                }
                return new ResponseModel(404, "Filme não encontrado");
            }
            catch
            {
                return new ResponseModel(500, "Erro ao devolver filme");
            }
        }

        public async Task<ResponseModel> AtivarOuDesativarFilme(int filmeId)
        {
            try
            {
                Filme filmeEncontrado = db.Filmes.FirstOrDefault(x => x.Id.Equals(filmeId));

                if (filmeEncontrado != null)
                {
                    filmeEncontrado.Ativo = !filmeEncontrado.Ativo;
                    db.Update(filmeEncontrado);
                    await db.SaveChangesAsync();

                    return new ResponseModel(200, "Status foi alterado");
                }
                return new ResponseModel(404, "Filme não encontrado");
            }
            catch
            {
                return new ResponseModel(500, "Erro mudar status do filme");
            }
        }
    }
}

