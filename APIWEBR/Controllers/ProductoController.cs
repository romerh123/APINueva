using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using APIWEBR.Models;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System;

namespace APIWEBR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        public DbapiContext _dbContext;

        public ProductoController(DbapiContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        [Route("Lista")]

        public IActionResult Lista()
        {
            List<Producto> lista = new List<Producto>();

            try
            {
                lista = _dbContext.Productos.Include(c=> c.oCategoria).ToList();

                return StatusCode(StatusCodes.Status200OK ,new { mensaje = "Ok", Response = lista});


            }catch  (Exception ex) {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message, Response = lista });
            
            }
        }
        [HttpGet]
        [Route("Obtener/{idProducto:int}")]

        public IActionResult Obtener(int idProducto)
        {
            Producto oProducto = _dbContext.Productos.Find(idProducto);

            if(oProducto == null) {

                return BadRequest("Producto no encontrado");
            
            }

            try
            {
                oProducto = _dbContext.Productos.Include(c => c.oCategoria).Where(p => p.IdProducto == idProducto).FirstOrDefault();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Ok", Response = oProducto }); 
            }
            catch  (Exception ex) {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message, Response = oProducto });

            }
        }
        [HttpPost]
        [Route("Guardar")]

        public IActionResult Guardar([FromBody] Producto objeto)
        {
            try
            {
                _dbContext.Productos.Add(objeto);
                _dbContext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Ok"});

            }
            catch (Exception ex) {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });

            }     

        }
        [HttpPut]
        [Route("Editar/")]

        public IActionResult Editar([FromBody] Producto objeto) 
        {
            Producto oProductos = _dbContext.Productos.Find(objeto.IdProducto);

            if(oProductos == null)
            {
                return BadRequest("Producto No encontradop");
            }
            try
            {
                oProductos.CodigoBarra = objeto.CodigoBarra is null ? oProductos.CodigoBarra : objeto.CodigoBarra;
                oProductos.Descripcion = objeto.Descripcion is null ? oProductos.Descripcion : objeto.Descripcion;
                oProductos.Marca = objeto.Marca is null ? oProductos.Marca : objeto.Marca;
                oProductos.Cantidad= objeto.Cantidad is null? oProductos.Cantidad: objeto.Cantidad;
                oProductos.IdCategoria = objeto.IdCategoria is null? oProductos.IdCategoria : objeto.IdCategoria;   
                oProductos.Precio = objeto.Precio is null? oProductos.Precio : objeto.Precio;


                _dbContext.Productos.Update(oProductos);
                _dbContext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Ok" });

            }
            catch  (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        
        }
        [HttpDelete]
        [Route("Eliminar/{idProducto:int}")]

        public IActionResult Eliminar( int idProducto)
        {
            Producto Oproducto = _dbContext.Productos.Find(idProducto);
            if( Oproducto == null )
            {
                return BadRequest("Producto no encontrado");
            }
            try
            {
                _dbContext.Productos.Remove(Oproducto);
                _dbContext.SaveChanges();


                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Ok" });

            }
            catch  (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }

          


    }
}
