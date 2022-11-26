using API_DB.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_DB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoController : ControllerBase
    {
        public readonly WebApiContext _dbcontext;


        public TipoController(WebApiContext _context)
        {
            _dbcontext = _context;
        }

        [HttpGet]
        [Route("Lista")]

        public IActionResult Lista()
        {

            List<Tipo> lista = new List<Tipo>();

            try
            {
                lista = _dbcontext.Tipos.ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = lista });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = "No se encontraron datos" });
            }

        }

        [HttpGet]
        [Route("Detalle")]
        public IActionResult Detalle(int id)
        {

            Tipo pTipo = _dbcontext.Tipos.Find(id);

            if (pTipo == null)
            {
                return NotFound(new { mensaje = "No existe Datos" });
            }


            try
            {
                pTipo = _dbcontext.Tipos.Where(x => x.IdTipo == id).FirstOrDefault();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = pTipo });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = "No se encontraron datos" });
            }

        }

        ///// /////////
        [HttpPost]
        [Route("Guardar")]
        public IActionResult Guardar([FromBody] Tipo objTipo)
        {
            try
            {

                _dbcontext.Tipos.Add(objTipo);
                var result = _dbcontext.SaveChanges();

                if (result > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = "Datos almacenados correctamente" });
                }
                else
                    return StatusCode(StatusCodes.Status404NotFound, new { mensaje = "No es posible almacenar datos" });



            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = "No es posible almacenar datos" });
            }
        }

        [HttpDelete]
        [Route("Eliminar")]
        public IActionResult Eliminar(int id)
        {
            Tipo objTipo = _dbcontext.Tipos.Find(id);

            if (objTipo == null)
            {
                return NotFound(new { mensaje = "No existe Datos" });
            }


            try
            {

                _dbcontext.Tipos.Remove(objTipo);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = "Objeto Eliminado Correctamente" });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = "No es posible eliminar la información" });
            }
        }

        [HttpPut]
        [Route("Modificar")]
        public IActionResult Modificar([FromBody] Tipo objTipo)
        {
            Tipo _Tipo = _dbcontext.Tipos.Find(objTipo.IdTipo);

            if (objTipo == null)
            {
                return NotFound(new { mensaje = "No existe Datos" });
            }

            try
            {


                _Tipo.IdTipo = objTipo.IdTipo; //is null ? _Tipo.IdTipo : objTipo.IdTipo;
                _Tipo.Descripcion = objTipo.Descripcion is null ? _Tipo.Descripcion : objTipo.Descripcion;




                _dbcontext.Tipos.Update(_Tipo);
                var result = _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = "Datos almacenados correctamente" });



            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = "No es posible almacenar datos" });
            }
        }


    }
}
