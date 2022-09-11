using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Framework :Entity
    {
        public int ProgrammingLanguageId { get; set; }
        public string Name { get; set; }
        public virtual ProgrammingLanguage? ProgrammingLanguage { get; set; }

        public Framework(int id,int programmingLanguageId, string name )
        {
            ProgrammingLanguageId = programmingLanguageId;
            Name = name;
            Id = id;
        }

        public Framework()
        {

        }
    }
}
