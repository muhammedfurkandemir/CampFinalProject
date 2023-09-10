using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFreamwork;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        //aşağıdaki gibi bir constroctır ile bağladığımızda losely coupled : zayıf bağlılık denir.
        //bir sınıfa veya bir şeye bağımlılık olacak sa asla sınıf gibi somut olanlarla değil soyut varlıklarla 
        //interface gibi bağlanmalıdır.
        //IoC Container -- Inversion of Control
        //IProductService _productService;

        //public ProductsController(IProductService productService)
        //{
        //    _productService = productService;
        //}
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            //eğer burda direk olarak product service new leyerek çağırsaydım bir bağımlılık olacaktı.
            //bu bağımlılığa dependency chain : bağımlılık zinciri denir
            //swagger : hazır dökümantasyon imkanı sağlar.

            //var result= _productService.GetAll();

            IProductService productService = new ProductManager(new EfProductDal());
            var result=productService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            IProductService productService = new ProductManager(new EfProductDal());
            var result = productService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(Product product)
        {
            IProductService productService = new ProductManager(new EfProductDal());
            var result = productService.Add(product);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result); 
        }
    }
}
