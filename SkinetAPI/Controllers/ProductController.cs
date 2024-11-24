﻿using Microsoft.AspNetCore.Mvc;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using SkinetAPI.Dtos;
using AutoMapper;

namespace SkinetAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IGenericRepository<Product> _productsRepo;

    private readonly IGenericRepository<ProductBrand> _productBrandRepo;

    private readonly IGenericRepository<ProductType> _productTypeRepo;

    private readonly IMapper _mapper;

    public ProductController(IGenericRepository<Product> productsRepo,
        IGenericRepository<ProductBrand> productBrandRepo, 
        IGenericRepository<ProductType> productTypeRepo,
        IMapper mapper) 
    {
        _productBrandRepo = productBrandRepo;
        _productTypeRepo = productTypeRepo;
        _productsRepo = productsRepo;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts()
    {
        var spec = new ProductsWithTypesAndBrandSpecification();

        var products = await _productsRepo.ListAsync(spec);


        return Ok(
            _mapper.Map<IReadOnlyList<Product>, 
            IReadOnlyList<ProductToReturnDto>>(products)
            );
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
    {
        var spec = new ProductsWithTypesAndBrandSpecification(id);

        var product = await _productsRepo.GetEntityWithSpec(spec);

        return _mapper.Map<Product, ProductToReturnDto>(product);
    }

    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
    {
        return Ok(await _productBrandRepo.ListAllAsync());
    }

    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
    {
        return Ok(await _productTypeRepo.ListAllAsync());
    }
}
