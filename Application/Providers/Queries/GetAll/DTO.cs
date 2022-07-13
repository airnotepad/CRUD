using Domain.Entity;
using System.Collections.Generic;

namespace Application.Providers.Queries.GetAll
{
    public class ProvidersGetAll
    {
        public IEnumerable<Provider> List { get; set; }
    }
}