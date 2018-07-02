using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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

        public ActionResult<IEnumerable<Produto>> GetAll()
        {
            this._log.LogInformation("Retornando a lista de produtos.");
            return new List<Produto>();
        }
    }

    public class Produto
    {

    }
}