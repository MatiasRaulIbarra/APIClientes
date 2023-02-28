using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIClientes.Modelos;
using APIClientes.Modelos.Dto;
using APIClientes.Repositorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIClientes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteRepositorio _clienteRepositorio;
        protected ResponseDto _response;

        public ClientesController(IClienteRepositorio clienteRepositorio)
        {
            _clienteRepositorio = clienteRepositorio;
            _response = new ResponseDto();
        }

        //GET:api/Clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
            try
            {
                var lista = await _clienteRepositorio.GetClientes();
                _response.Result = lista;
                _response.DisplayMessage = "Lista de clientes";
            }
            catch (Exception ex)
            {

                _response.IsSucces = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return Ok(_response);
        }

        //GET:api/Clientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
            var cliente = await _clienteRepositorio.GetClienteById(id);
            if (cliente == null)
            {
                _response.IsSucces = false;
                _response.DisplayMessage = "Cliente no existe";
                return NotFound(_response);
            }
            _response.Result = cliente;
            _response.DisplayMessage = "Información del cliente";
            return Ok(_response);
        }

        [HttpPut("{id")]
        public async Task<IActionResult> PutCliente(int id, ClienteDto clienteDto)
        {
            try
            {
                ClienteDto model = await _clienteRepositorio.CreateUpdate(clienteDto);
                _response.Result = model;
                return Ok(_response);
            }
            catch (Exception ex)
            {

                _response.IsSucces = false;
                _response.DisplayMessage = "Error al actualizar el registro";
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }
        }

        //POST:api/Clientes

        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(ClienteDto clienteDto)
        {
            try
            {
                ClienteDto model = await _clienteRepositorio.CreateUpdate(clienteDto);
                _response.Result = model;
                return CreatedAtAction("GetCliente", new { id = model.Id }, _response);
            }
            catch (Exception ex)
            {

                _response.IsSucces = false;
                _response.DisplayMessage = "Error al actualizar el registro";
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            try
            {
                bool estaEliminado = await _clienteRepositorio.DeleteCliente(id);
                if (estaEliminado)
                {
                    _response.Result = estaEliminado;
                    _response.DisplayMessage = "Cliente eliminado con Exito";
                    return Ok(_response);
                }
                else
                {
                    _response.IsSucces = false;
                    _response.DisplayMessage = "Error al eliminar Cliente";
                    return BadRequest(_response);
                }
            }
            catch (Exception ex )
            {

                _response.IsSucces = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }
        }
    }
}
