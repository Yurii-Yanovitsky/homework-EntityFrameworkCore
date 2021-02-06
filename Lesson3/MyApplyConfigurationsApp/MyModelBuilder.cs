using System;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace MyApplyConfigurationsApp
{
    public class MyModelBuilder
    {
        private ModelBuilder _modelBuilder;
        public MyModelBuilder(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
        }

        // Реализация метода применения всех конфигураций сущностей, что есть в сборке.
        public void ApplyConfigurationsFromAssembly(Assembly assambly)
        {
            // Получаем все типы, которые реализуют интерфейс IEntityTypeConfiguration<>
            var types = assambly.GetTypes()
                .Where(t => !t.IsAbstract)
                .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>))).ToList();

            foreach (var t in types)
            {
                // Получаем объект класса Type, который содержит в себе инфо о типе, которым закрыт наш интерфейс
                var entity = t.GetInterfaces().Select(i => i.GetGenericArguments()[0]).First();

                var modelBuilderType = typeof(ModelBuilder);
                // Получаем generic method класса ModelBuilder и подставляем туда entity
                MethodInfo entityMethod = modelBuilderType.GetMethod("Entity", 1, new Type[] { }).MakeGenericMethod(entity);

                //Получаем объект класса EntityTypeBuilder<TEntity>, где TEntity - это тип о котором содержит информацию entity 
                var entityBuilder = entityMethod.Invoke(_modelBuilder, null);

                // Предаем entityBuilder в метод Configure() и выполняемся
                t.GetMethod("Configure").Invoke(Activator.CreateInstance(t), new[] { entityBuilder });
            }
        }
    }
}
