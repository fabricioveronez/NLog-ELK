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
            try
            {
                MongoDBContext db = new MongoDBContext();
                db.Produtos.InsertOne(produto);
                this._log.LogInformation("Produto {idProduto}, {nomeProduto}, {descricaoProduto} cadastrado com sucesso.", produto.Id, produto.Nome, produto.Descricao);
                return CreatedAtRoute("Get", new { id = produto.Id }, produto);
            }
            catch (System.Exception ex)
            {
                this._log.LogError(ex, "Erro ao cadastrar o produto {nomeProduto} e {descricaoProduto}.", produto.Nome, produto.Descricao);
                return new BadRequestResult();
            }
        }
    }
}