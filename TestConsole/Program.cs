using System;
using Microsoft.EntityFrameworkCore;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;

EfProductDal productDal = new EfProductDal();
Console.Write("Ürün adı giriniz: ");
string name = Console.ReadLine();

List<Product> products = productDal.GetByProductName(name);
if (products != null && products.Count > 0)
{
    foreach (var product in products)
    {
        Console.WriteLine($"{product.PName} - {product.PPrice} TL");
    }
}
else
{
    Console.WriteLine("Ürün bulunamadı.");
}
Console.Write("Min stok belirtiniz: ");
int stock = int.Parse(Console.ReadLine());
List<Product> products2 = productDal.GetByStock(stock);
if (products2 != null && products2.Count > 0)
{
    foreach (var product in products2)
    {
        Console.WriteLine($"{product.PName} için kalan stock miktarı: {product.PStock}");
    }
}
else
{
    Console.WriteLine("yetersiz stok");
}
var prd=productDal.GetCatById(1);

foreach (var product in prd)
{
    Console.WriteLine($"Ürün: {product.PName}, Kategori: {product.Category.CName}");
}
Console.ReadLine();
