using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElosztottLabor.DTOs
{
    public class AnswerFormDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<AnswerDTO> Answers { get; set; }

        // Because of the way JSON Deserialization works (first an object is created, then 
        // its properties set), we need a default constructor. 
        public AnswerFormDTO()
        {

        }

        public AnswerFormDTO(AnswerForm AnswerForm)
        {
            this.Id = AnswerForm.Id;
            this.Name = AnswerForm.Name;
            this.Answers = AnswerForm
                                .Answers
                                .Select(q => new AnswerDTO(q))
                                .ToList();
        }

        public AnswerForm ToEntity()
        {
            return new AnswerForm()
            {
                Id = this.Id,
                Name = this.Name,
                Answers = this.Answers.Select(q => q.ToEntity()).ToList()
            };
        }
    }
}
