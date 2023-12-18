using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.DTOs;
using Api.Extensions;
using AutoMapper;

namespace Api.services
{
    public class PaginationService<T>
    {
        private readonly IMapper _mapper;

        public PaginationService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public PagedResult<TDto> Paginate<TDto>(List<T> items, int page, int pageSize)
        {
            var totalItems = items.Count;
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var itemsPage = items.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            var itemsDto = _mapper.Map<List<TDto>>(itemsPage);

            var result = new PagedResult<TDto>
            {
                CurrentPage = page,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = totalPages,
                Items = itemsDto
            };

            return result;
        }
        public PaginationHeader GetPaginationHeader(PagedResult<BikeDTo> result)
        {
            return new PaginationHeader
            {
                CurrentPage = result.CurrentPage,
                PageSize = result.PageSize,
                TotalItems = result.TotalItems,
                TotalPages = result.TotalPages,
            };
        }

    }
}