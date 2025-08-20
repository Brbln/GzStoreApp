using AutoMapper;
using Business.Abstract;
using Business.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GamzeProje.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellerController : ControllerBase
    {
        private readonly ISellerService _sellerService;
        private readonly IMapper _mapper;

        public SellerController(ISellerService sellerService, IMapper mapper)
        {
            _sellerService = sellerService;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetSeller()
        {
            var seller = _sellerService.GetById(1);
            if (seller == null) return NotFound();
            return Ok(_mapper.Map<SellerDto>(seller));
        }
    }
}
