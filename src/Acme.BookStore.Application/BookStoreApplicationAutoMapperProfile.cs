using Acme.BookStore.APITestValue;
using Acme.BookStore.Authors;
using Acme.BookStore.Books;

using AutoMapper;

namespace Acme.BookStore;

public class BookStoreApplicationAutoMapperProfile : Profile
{
    public BookStoreApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<APITest, APITestDto>();
        CreateMap<Book, BookDto>();//使用IObjectMaooer将Book对象转为BookDto对象
        CreateMap<CreateUpdateBookDto, Book>();//将CreateUpdateBookDto对象转为Book对象
        CreateMap<Author, AuthorDto>();
        CreateMap<Author, AuthorLookupDto>();
    }
}
