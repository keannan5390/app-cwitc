using System.Threading.Tasks;
using CWITC.DataObjects;
using CWITC.DataStore.Abstractions;

using CWITC.DataStore.Mock;
using System.Collections.Generic;

namespace CWITC.DataStore.Mock
{
    public class CategoryStore : BaseStore<Category>, ICategoryStore
    {
        public override Task<IEnumerable<Category>> GetItemsAsync(bool forceRefresh = false)
        {
           var categories = new []
                {
                    new Category { Name = "Mobile", ShortName="Mobile", Color="#B8E986"},
                    new Category { Name = "DevOps", ShortName="DevOps", Color="#F16EB0"},
                    new Category { Name = ".NET", ShortName=".NET", Color="#7DD5C9" },
                    new Category { Name = "Cloud", ShortName="Cloud", Color="#51C7E3"},
                    new Category { Name = "Cybersecurity", ShortName="Cybersecurity", Color="#F88F73" },
                    new Category { Name = "Front-End", ShortName="Front-End", Color="#4B637E"},
                    new Category { Name = "Database", ShortName="Database", Color="#AC9AD3" },
                };
            return Task.FromResult(categories as IEnumerable<Category>);
        }
    }
}

