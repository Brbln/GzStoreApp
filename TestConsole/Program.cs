using System;
using Microsoft.EntityFrameworkCore;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;

EfProductDal productDal = new EfProductDal();

// Ürün adı ile arama testi
Console.WriteLine("Ürün adı 'elma' içeren ürünler:");
var productsByName = productDal.GetByProductName("elma");
foreach (var product in productsByName)
{
    Console.WriteLine($"ID: {product.ProductId}, Adı: {product.PName}, Stok: {product.PStock}");
}

// Stok miktarına göre arama testi
Console.WriteLine("\nStok miktarı 10'dan fazla olan ürünler:");
var productsByStock = productDal.GetByStock(10);
foreach (var product in productsByStock)
{
    Console.WriteLine($"ID: {product.ProductId}, Adı: {product.PName}, Stok: {product.PStock}");
}

// Kategori ID'ye göre ürünler
Console.WriteLine("\nKategori ID = 1 olan ürünler:");
var productsByCategory = productDal.GetCatById(1);
foreach (var product in productsByCategory)
{
    Console.WriteLine($"ID: {product.ProductId}, Adı: {product.PName}, Kategori: {product.Category.CName}");
}

Console.WriteLine("\nTest tamamlandı.");
Console.ReadLine();

//EfUserDal userDal = new EfUserDal();

//// Test GetByEmail
//var userByEmail = userDal.GetByEmail("a.brbln.97@gmail.com");
//if (userByEmail != null)
//    Console.WriteLine($"Kullanıcı bulundu: {userByEmail.UserName}");
//else
//    Console.WriteLine("Kullanıcı bulunamadı.");

//// Test IsEmailExists
//bool emailExists = userDal.IsEmailExists("a.brbln.97@gmail.com");
//Console.WriteLine($"Email var mı? {emailExists}");

//// Test GetByUserName
//var userByName = userDal.GetByUserName("Asli");
//Console.WriteLine(userByName != null
//    ? $"Kullanıcı adı bulundu: {userByName.Email}"
//    : "Kullanıcı adı bulunamadı.");

//// Test IsUNameExists
//bool userNameExists = userDal.IsUNameExists("ASs");
//Console.WriteLine($"Kullanıcı adı var mı? {userNameExists}");

//Console.ReadLine();