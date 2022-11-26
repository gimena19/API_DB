using API_DB.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_DB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactoController : ControllerBase
    {

        public readonly WebApiContext _dbcontext;


        public ContactoController(WebApiContext _context)
        {
            _dbcontext = _context;
        }

        [HttpGet]
        [Route("Lista")]

        public IActionResult Lista()
        {

            List<Contacto> lista = new List<Contacto>();

            try
            {
                lista = _dbcontext.Contactos.Include(x => x.ObjTipo).ToList();
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

            Contacto objContacto = _dbcontext.Contactos.Find(id);

            if(objContacto == null)
            {
                return NotFound(new {mensaje = "No existe Datos"});
            }
            
            
            try
            {
                objContacto = _dbcontext.Contactos.Include(c => c.ObjTipo).Where(x=> x.IdContacto ==id).FirstOrDefault() ;
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = objContacto });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = "No se encontraron datos" });
            }

        }

        /// /////////
        [HttpPost]
        [Route("Guardar")]
        public IActionResult Guardar([FromBody] Contacto objcontacto)
        {
            try
            {
                
                _dbcontext.Contactos.Add(objcontacto);
                var result = _dbcontext.SaveChanges();

                if(result> 0)
                {
                    return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = "Datos almacenados correctamente" });
                }else
                    return StatusCode(StatusCodes.Status404NotFound, new { mensaje = "No es posible almacenar datos" });



            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = "No es posible almacenar datos" });
            }
        }

        [HttpDelete]
        [Route("Eliminar")]
        public IActionResult Eliminar(int id)
        {
            Contacto objContacto = _dbcontext.Contactos.Find(id);

            if (objContacto == null)
            {
                return NotFound(new { mensaje = "No existe Datos" });
            }


            try
            {
                
                _dbcontext.Contactos.Remove(objContacto);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "OK", response = "Objeto Eliminado Correctamente"});
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status404NotFound, new { mensaje = "No es posible eliminar la información" });
            }
        }

        [HttpPut]
        [Route("Modificar")]
        public IActionResult Modificar([FromBody] Contacto objContacto)
        {
            Contacto _Contacto = _dbcontext.Contactos.Find(objContacto.IdContacto);

            if (objContacto == null)
            {
                return NotFound(new { mensaje = "No existe Datos" });
            }

            try
            {


                _Contacto.Nombre = objContacto.Nombre is null ? _Contacto.Nombre : objContacto.Nombre;
                _Contacto.Descripcion = objContacto.Descripcion is null ? _Contacto.Descripcion : objContacto.Descripcion;
                _Contacto.Telefono = objContacto.Telefono is null ? _Contacto.Telefono : objContacto.Telefono;
                _Contacto.IdTipo = objContacto.IdTipo is null ? _Contacto.IdTipo : objContacto.IdTipo;




                _dbcontext.Contactos.Update(_Contacto);
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
