using System.Linq;
using System.Threading.Tasks;
using testeWipro.Data;
using testeWipro.Domain;
using testeWipro.Models;

namespace testeWipro.Services
{
    public class ClienteService
    {
        private readonly ApplicationDbContext db;

        public ClienteService(ApplicationDbContext _db)
        {
            db = _db;
        }
        public async Task<ResponseModel> CadastrarLocador(Cliente locador)
        {
            try
            {
                Cliente locadorEncontrado = db.Clientes.FirstOrDefault(x => x.Nome.ToLower().Trim().Equals(locador.Nome.ToLower().Trim()));

                if (locadorEncontrado == null)
                {
                    db.Clientes.Add(locador);
                    await db.SaveChangesAsync();
                    return new ResponseModel(200, "Locador cadastrado!");
                }

                return new ResponseModel(500, "Locador já está cadastrado");
            }
            catch
            {
                return new ResponseModel(500, "Erro ao cadastrar locador");
            }
        }

        public async Task<ResponseModel> AtivarOuDesativarCliente(int clienteId)
        {
            try
            {
                Cliente clienteEncontrado = db.Clientes.FirstOrDefault(x => x.Id.Equals(clienteId));

                if (clienteEncontrado != null)
                {
                    clienteEncontrado.Ativo = !clienteEncontrado.Ativo;
                    db.Update(clienteEncontrado);
                    await db.SaveChangesAsync();

                    return new ResponseModel(200, "Status foi alterado");
                }
                return new ResponseModel(404, "Cliente não encontrado");
            }
            catch
            {
                return new ResponseModel(500, "Erro mudar status do cliente");
            }
        }
    }
}
