using JetBrains.Annotations;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Volo.Abp;
using Volo.Abp.Auditing;
using Volo.Abp.AuditLogging;
using Volo.Abp.Domain.Services;

namespace Acme.BookStore.Authors
{
    public class AuthorManager : DomainService
    {
        public readonly IAuthorRepository _authorRepository;
        
        public AuthorManager(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<Author> CreateAsync(
          [NotNull] string name,
          DateTime birthDate,
          [CanBeNull] string shortBio = null
            )
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            //现有的作者
            var existingAuthor=await _authorRepository.FindByNameAsync(name);
            //检查是否存在同名用户
            if (existingAuthor != null)
            {
                throw new AuthorAlreadyExistsException(name);
            }
            return new Author(
                //GuidGenerator创建实体ID
                GuidGenerator.Create(),
                name,
                birthDate,
                shortBio
                );           
        }
        public async Task ChangeNameAsync(
            [NotNull] Author author,
            [NotNull] string newName
            )
        {
            Check.NotNull(author, nameof(author));
            Check.NotNullOrWhiteSpace(newName, nameof(newName));

            var existingAuthor=await _authorRepository.FindByNameAsync(newName);
            if (existingAuthor !=null && existingAuthor.Id != author.Id)
            {
                //提示已经有这个名字了
                throw new AuthorAlreadyExistsException(newName);
            }
            author.ChangeName(newName);

        }
    }
}
