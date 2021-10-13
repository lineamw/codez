using ElosztottLabor.DTOs;
using ElosztottLabor.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElosztottLabor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerFormController : ControllerBase
    {
        private readonly IAnswerFormService _service;

        public AnswerFormController(IAnswerFormService service)
        {
            this._service = service;
        }

        // Fetch all Answer forms 
        [HttpGet]
        public ActionResult<IEnumerable<AnswerFormDTO>> GetAnswerForms()
        {
            return Ok(_service.GetAnswerForms().Select(qf => new AnswerFormDTO(qf)));
        }

        // Find one Answer form by id 
        [HttpGet("{id}")]
        public ActionResult<AnswerFormDTO> GetAnswerForm(long id)
        {
            var result = _service.GetAnswerForm(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(new AnswerFormDTO(result));
        }

        // Create a new Answer form 
        [HttpPost]
        public ActionResult<AnswerFormDTO> CreateNewAnswerForm(AnswerFormDTO AnswerFormDTO)
        {
            // Handle error if no data is sent. 
            if (AnswerFormDTO == null)
            {
                return BadRequest("AnswerForm data must be set!");
            }

            try
            {
                // Map the DTO to entity and save the entity 
                AnswerForm createdEntity = _service.SaveAnswerForm(AnswerFormDTO.ToEntity());

                // According to the conventions, we have to return a HTTP 201 created repsonse, with 
                // field "Location" in the header pointing to the created object 
                return CreatedAtAction(
                    nameof(GetAnswerForm),
                    new { id = createdEntity.Id },
                    new AnswerFormDTO(createdEntity));
            }
            catch (AnswerFormExistsException)
            {
                return Conflict("The desired ID for the AnswerForm is already taken!");
            }
        }

        // Update an existing Answer form 
        [HttpPut("{id}")]
        public ActionResult UpdateAnswerForm(long id, AnswerFormDTO AnswerFormDTO)
        {
            // Handle error if no data is sent. 
            if (AnswerFormDTO == null)
            {
                return BadRequest("AnswerForm data must be set!");
            }

            try
            {
                // Map the DTO to entity and save it 
                _service.UpdateAnswerForm(id, AnswerFormDTO.ToEntity());

                // According to the conventions, we have to return HTTP 204 No Content. 
                return NoContent();
            }
            catch (AnswerFormDoesntExistsException)
            {
                // Handle error if the Answer form to update doesn't exists. 
                return BadRequest("No AnswerForm exists with the given ID!");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteAnswerForm(long id)
        {
            _service.DeleteAnswerForm(id);
            // According to the conventions, we have to return HTTP 204 No Content. 
            return NoContent();
        }
    }
}