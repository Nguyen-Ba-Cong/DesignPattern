
using DesignPattern.Database.Entity;
using DesignPattern.Service.IRepositories;
using DesignPattern.Service.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DesignPattern.Test.RepositoryTest
{
    public class CategoryRepositoryTest
    {
        ICategoryRepository categoryRepository;
        List<Category> categories;
        DbContextOptions<DesignPatternDBContext> options;
        public CategoryRepositoryTest()
        {
            categories = new List<Category>()
            {
                new Category()
                {
                    Id=1,
                    Name="Test 1",
                    News = new Collection<New>()
                    {
                        new New()
                        {
                            Id = 1,
                            Title = "Title 1"
                        },
                         new New()
                        {
                            Id = 2,
                            Title = "Title 2"
                        },
                          new New()
                        {
                            Id = 3,
                            Title = "Title 3"
                        }
                    }
                },
                 new Category()
                {
                    Id=2,
                    Name="Test 2",
                },
                  new Category()
                {
                    Id=3,
                    Name="Test 3",
                },

            };
            options = new DbContextOptionsBuilder<DesignPatternDBContext>()
               .UseInMemoryDatabase(databaseName: "DesignPattern")
               .Options;
            using (var context = new DesignPatternDBContext(options))
            {
                context.Categories.AddRange(categories);
                context.SaveChanges();
            }

        }
        [Fact]
        public void GetAll_Category_Test()
        {
            using (var context = new DesignPatternDBContext(options))
            {
                categoryRepository = new CategoryRepository(context);
                var list = categoryRepository.All(0, 10).ToList();
                Assert.Equal(3, list.Count);
            }

        }
        [Fact]
        public void Get_Category_By_Id_Test()
        {
            using (var context = new DesignPatternDBContext(options))
            {
                categoryRepository = new CategoryRepository(context);
                var category = categoryRepository.FindByCondition(x => x.Id == 1).FirstOrDefault();
                Assert.NotNull(category);
                Assert.Equal("Test 1", category.Name.ToString());
            }
        }
        [Fact]
        public void Add_Category_Test()
        {
            using (var context = new DesignPatternDBContext(options))
            {
                categoryRepository = new CategoryRepository(context);
                Category category = new Category()
                {
                    Id = 3,
                    Name = "Test 3"
                };
                var result = categoryRepository.Create(category);
                var list = categoryRepository.All(0, 10).ToList();
                Assert.Equal(4, list.Count);
                Assert.Equal(3, category.Id);
            }
        }
        [Fact]
        public void Update_Category_Test()
        {
            using (var context = new DesignPatternDBContext(options))
            {
                categoryRepository = new CategoryRepository(context);
                Category category = new Category()
                {
                    Id = 1,
                    Name = "Test 1 Update"
                };
                var result = categoryRepository.Update(category);
                Assert.Equal("Test 1 Update", result.Name);
            }
        }
        [Fact]
        public void Delete_Category_Test()
        {
            using (var context = new DesignPatternDBContext(options))
            {
                categoryRepository = new CategoryRepository(context);
                Category category = new Category()
                {
                    Id = 1,
                    Name = "Test 1"
                };
                var result = categoryRepository.Delete(category);
                var list = categoryRepository.All(0, 10).ToList();
                Assert.Equal(2, list.Count);
                Assert.Equal("Test 1", result.Name);
            }
        }
        [Fact]
        public void Get_New_By_CategoryId_Test()
        {
            using (var context = new DesignPatternDBContext(options))
            {
                categoryRepository = new CategoryRepository(context);
                var result = categoryRepository.GetNewByCategoryId(1);
                Assert.Equal(3, result.Count);
            }
        }
    }
}
