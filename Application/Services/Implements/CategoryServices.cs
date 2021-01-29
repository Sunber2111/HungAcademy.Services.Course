using System;
using Application.Categories;
using Application.Services.Interfaces;
using Domain;

namespace Application.Services.Implements
{
    public class CategoryServices : BaseServices<CategoryDto, Category>, ICategoryServices
    {
        public CategoryServices(IServiceProvider services) : base(services)
        {

        }
    }
}