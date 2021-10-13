using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElosztottLabor.Interfaces
{
    public class IAnswerFormService
    {
        IEnumerable<AnswerForm> GetAnswerForms();

        AnswerForm GetAnswerForm(long id);

        AnswerForm SaveAnswerForm(AnswerForm AnswerForm);

        void UpdateAnswerForm(long id, AnswerForm AnswerForm);

        void DeleteAnswerForm(long id);

        bool AnswerFormExistsById(long id);
    }
}
