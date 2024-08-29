using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Repository_Domain;
using Repository_Pattern;

namespace Inventory
{
    internal class Program
    {
        static void Main(string[] args)
        {

        bool isRun = true; while (isRun)
            {
                Console.Clear();
                Console.WriteLine("\t ________________________________________________");
                Console.WriteLine("\t|------------Select Your Process-----------------|");
                Console.WriteLine("\t|Press 1 :  to Get all Product Information       |");
                Console.WriteLine("\t|Press 2 :  to Create a New Product Information  |");
                Console.WriteLine("\t|Press 3 :  to Update a Product's Information    |");
                Console.WriteLine("\t|Press 4 :  to Delete a Product's Information    |");
                Console.WriteLine("\t|Press 5 :  to Exit The Application              |");
                Console.WriteLine("\t ------------------------------------------------");
                int inputKey = int.Parse(Console.ReadLine());
                Console.Clear();
                var source = RepositoryFactory.Create<IProductRepository>(ContextTypes.XMLSource);
                switch (inputKey)
                {

                    case 1:
                        {                         
                            var items = source.GetAll();
                            Console.WriteLine("\t\t\tProduct Information");
                            foreach (var item in items)
                            {
                                Console.WriteLine($"Product id: {item.ProductId}\nProduct Name : {item.ProductName}\nDescription : {item.Description}\nProduct Category : {item.ProductCatagory}\nBrand Name : {item.BrandName}\nCost per Unit : {item.CostPerUnit}");
                                Console.WriteLine();
                            }
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            break;
                        }
                    case 2:
                        {
                            Product pd = new Product();
                            Console.Write("Product Name : ");
                            pd.ProductName = Console.ReadLine();
                            Console.Write("Product Description : ");
                            pd.Description = Console.ReadLine();
                            Console.Write("Product Category : ");
                            pd.ProductCatagory = Console.ReadLine();
                            Console.Write("Brand Name : ");
                            pd.BrandName = Console.ReadLine();
                            Console.Write("Cost Per Unit : ");
                            pd.CostPerUnit = int.Parse(Console.ReadLine());
                            try
                            {
                                source.Insert(pd);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex);
                                Console.ReadKey();
                                continue;
                            }
                            break;
                        }
                    case 3:
                        {
                            Console.Write("Enter Product ID to Update: ");
                            if (int.TryParse(Console.ReadLine(), out int productIdToUpdate))
                            {
                                var ProductToUpdate = new Product();
                                Console.Write("Updated Product Name : ");
                                ProductToUpdate.ProductName = Console.ReadLine();
                                Console.Write("Updated Product Description : ");
                                ProductToUpdate.Description = Console.ReadLine(); 
                                Console.Write("Updated Product Catagory : ");
                                ProductToUpdate.ProductCatagory = Console.ReadLine();
                                Console.Write("Updated Brand Name : ");
                                ProductToUpdate.BrandName = Console.ReadLine();
                                Console.Write("Updated Cost per Unit : ");
                                ProductToUpdate.CostPerUnit = int.Parse(Console.ReadLine());

                                if (source.Update(productIdToUpdate, ProductToUpdate))
                                {
                                    Console.WriteLine("Course updated successfully!");
                                }
                                else
                                {
                                    Console.WriteLine("Failed to update course. Course not found.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid input for Course ID.");
                            }
                            Console.Write("Press any key to continue...");
                            Console.ReadKey();
                            break;
                        }
                    case 4:
                        {
                             IProductRepository productRepository = RepositoryFactory.Create<IProductRepository>(ContextTypes.XMLSource);
                            Console.WriteLine("Enter the Product ID to delete:");
                            int productIdToDelete = int.Parse(Console.ReadLine());

                            bool isDeleted = productRepository.Delete(productIdToDelete);

                            if (isDeleted)
                            {
                                Console.WriteLine($"Product with ID {productIdToDelete} deleted successfully.");
                            }
                            else
                            {
                                Console.WriteLine($"Product with ID {productIdToDelete} not found or failed to delete.");
                            }
                            Console.WriteLine("Press any Key To Continue...");
                            Console.ReadKey(); 
                            break;
                        }                
                    case 5:
                        {
                            isRun = false;
                            break;
                        }
                    default:
                        {
                            isRun = true;
                            break;
                        }
                }
            }
        }
    }
}
