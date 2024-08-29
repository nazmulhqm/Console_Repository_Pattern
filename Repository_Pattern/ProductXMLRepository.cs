using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Repository_Domain;
using Repository_Source;

namespace Repository_Pattern
{
    public class ProductXMLRepository : XMLRepositoryBase<XMLSet<Product>, Product, int>, IProductRepository
    {
        public ProductXMLRepository() : base("ProductInformation.xml")
        {

        }
        public Product GetProduct(int productId)
        {
            return GetAll().FirstOrDefault(p=>p.ProductId == productId);
        }
        public bool DeleteAll()
        {
            try
            {
                m_context.Data.Clear();
                m_context.Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Update(int productId, Product updatedProduct)
        {
            try
            {
                var product = m_context.Data.FirstOrDefault(p => p.ProductId == productId);
                if (product != null)
                {
                    product.ProductName = updatedProduct.ProductName;
                    product.Description = updatedProduct.Description;
                    product.ProductCatagory = updatedProduct.ProductCatagory;
                    product.BrandName = updatedProduct.BrandName;
                    product.CostPerUnit = updatedProduct.CostPerUnit;

                    m_context.Save();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        bool IRepository<Product, int>.Delete(int productId)
        {
            try
            {
                var productToRemove = m_context.Data.FirstOrDefault(p => p.ProductId == productId);
                if (productToRemove != null)
                {
                    m_context.Data.Remove(productToRemove);

                    m_context.Save();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
