using Business.Concrete;
using DataAccess.Concrete.EntityFreamwork;
using DataAccess.Concrete.InMemory;

namespace ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ProductManager productManager = new ProductManager(new EfProductDal());
            var result= productManager.GetByUnitPrice(40, 60).OrderBy(p => p.UnitPrice);
            foreach (var product in result )
            {
                Console.WriteLine(product.UnitPrice);
                
            }
            
        }
    }
}