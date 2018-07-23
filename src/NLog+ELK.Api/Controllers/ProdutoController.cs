using System.Collections.Generic;
using System.Linq;
using Compartilhado;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Exemplo01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {

        ILogger _log;

        public ProdutoController(ILogger<ProdutoController> log)
        {
            this._log = log;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> GetAll()
        {
            this._log.LogInformation("Retornando a lista de produtos.");

            try
            {
                MongoDBContext db = new MongoDBContext();
                return db.Produtos.Find(FilterDefinition<Produto>.Empty).ToList();
            }
            catch (System.Exception ex)
            {
                this._log.LogError(ex, "Erro ao listar produtos.");
                return new BadRequestResult();
            }
        }

        [HttpGet("{id}", Name = "Get")]
        public ActionResult<Produto> Get(string id)
        {
            try
            {

                MongoDBContext db = new MongoDBContext();
                Produto umProduto = db.Produtos.Find(Builders<Produto>.Filter.Eq(p => p.Id, id)).SingleOrDefault();
                if (umProduto == null)
                {
                    return NotFound();
                }

                return umProduto;
            }
            catch (System.Exception ex)
            {
                return new BadRequestObjectResult(ex);
            }
        }

        [HttpPost]
        public ActionResult<Produto> Post([FromBody] Produto produto)
        {
            this._log.LogInformation("Efetuando o cadastro do produto.");

            try
            {
                MongoDBContext db = new MongoDBContext();
                db.Produtos.InsertOne(produto);
                return CreatedAtRoute("Get", new { id = produto.Id }, produto);
            }
            catch (System.Exception ex)
            {
                this._log.LogError(ex, "Erro ao cadastrar o produto.");
                return new BadRequestResult();
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromRoute] string id, [FromBody]Produto produto)
        {
            this._log.LogInformation("Alterando o cadastro do produto {0}.", id);

            try
            {
                MongoDBContext db = new MongoDBContext();
                if (db.Produtos.Find(Builders<Produto>.Filter.Eq(p => p.Id, id)).SingleOrDefault() == null)
                {
                    return NotFound();
                }

                produto.Id = id;
                db.Produtos.FindOneAndReplace(Builders<Produto>.Filter.Eq(p => p.Id, id), produto);
                return new NoContentResult();
            }
            catch (System.Exception ex)
            {
                this._log.LogError("Erro ao alterar o cadastro do produto {0}.", id);
                return new BadRequestObjectResult(ex);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            this._log.LogInformation("Exluindo o cadastro do produto {0}.", id);

            try
            {
                MongoDBContext db = new MongoDBContext();
                if (db.Produtos.Find(Builders<Produto>.Filter.Eq(p => p.Id, id)).SingleOrDefault() == null)
                {
                    return NotFound();
                }

                db.Produtos.FindOneAndDelete(Builders<Produto>.Filter.Eq(p => p.Id, id));
                return new NoContentResult();
            }
            catch (System.Exception ex)
            {
                this._log.LogError("Erro ao excluir o cadastro do produto {0}.", id);
                return new BadRequestObjectResult(ex);
            }
        }
    }
}