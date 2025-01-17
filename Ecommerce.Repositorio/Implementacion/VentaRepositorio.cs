using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Modelo;
using Ecommerce.Repositorio.Contrato;
using Ecommerce.Repositorio.DBContext;
using Microsoft.EntityFrameworkCore;


namespace Ecommerce.Repositorio.Implementacion
{
    public class VentaRepositorio : GenericoRepositorio<Venta>, IVentaRepositorio
    {
        private readonly DbecommerceContext _dbContext;
        public VentaRepositorio(DbecommerceContext dbContext):base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Venta> Registrar(Venta modelo)
        {
            Venta ventaGenerada = new Venta();

            var strategy = _dbContext.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using (var Transaction = await _dbContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        foreach (DetalleVenta dv in modelo.DetalleVenta)
                        {
                            Producto producto_encontrado = _dbContext.Productos
                                .Where(p => p.IdProducto == dv.IdProducto)
                                .First();

                            producto_encontrado.Cantidad -= dv.Cantidad;

                            _dbContext.Productos.Update(producto_encontrado);
                        }
                        await _dbContext.SaveChangesAsync();
                        await _dbContext.Venta.AddAsync(modelo);
                        await _dbContext.SaveChangesAsync();

                        ventaGenerada = modelo;
                        await Transaction.CommitAsync();
                    }
                    catch (Exception)
                    {
                        await Transaction.RollbackAsync();
                        throw;
                    }
                }
            });

            return ventaGenerada;
        }
    }
}
