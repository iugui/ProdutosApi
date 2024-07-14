﻿using Mapster;
using Microsoft.EntityFrameworkCore;
using ProdutosApi.Context;
using ProdutosApi.DTOs;
using ProdutosApi.Models;

namespace ProdutosApi.Repositories;

public class ProdutoRepository : IProdutoRepository
{
    // Injeção de dependência
    private readonly ProdutosDb _context;
    public ProdutoRepository(ProdutosDb context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProdutoResponseDTO>> GetProdutos()
    {
        var produtos = await _context.Produtos.AsNoTracking().ToListAsync();
        var produtosResponseDTO = produtos.Adapt<List<ProdutoResponseDTO>>();
        return produtosResponseDTO;
    }

    public async Task<ProdutoResponseDTO> GetProduto(int id)
    {
        var produto = await _context.Produtos.FirstOrDefaultAsync(p => p.Id == id);
        var produtoResponseDTO = produto.Adapt<ProdutoResponseDTO>();
        #pragma warning disable
        return produtoResponseDTO;
        // Desabilitando o warning de null pois tratamos esse caso em ProdutosController
    }

    public async Task<ProdutoResponseDTO> CreateProduto(ProdutoRequestDTO produtoDTO)
    {
        var produto = produtoDTO.Adapt<Produto>();
        await _context.Produtos.AddAsync(produto);
        var produtoResponseDTO = produto.Adapt<ProdutoResponseDTO>();
        //await _context.SaveChangesAsync();
        return produtoResponseDTO;
    }

    public async Task<ProdutoResponseDTO> UpdateProduto(int id, ProdutoDTO produtoDTO)
    {
        var produto = produtoDTO.Adapt<Produto>();
        if (id != produto.Id)
        {
            throw new Exception("O id deve ser igual ao id do produto");
        }

        _context.Entry(produto).State = EntityState.Modified;

        //try
        //{
        //    await _context.SaveChangesAsync();
        //}
        //catch (DbUpdateConcurrencyException)
        //{
        //    if (!ProdutoExists(id))
        //    {
        //        throw new ArgumentNullException(nameof(produto));
        //    }
        //}
        var produtoResposeDTO = produto.Adapt<ProdutoResponseDTO>();
        return produtoResposeDTO;
    }

    public async Task<ProdutoResponseDTO> DeleteProduto(int id)
    {
        var produto = await _context.Produtos.FirstOrDefaultAsync(p => p.Id == id);
        if (produto == null)
        {
            #pragma warning disable
            return null;
            // Desabilitando o warning de null pois tratamos esse caso em ProdutosController
        }

        _context.Produtos.Remove(produto);
        //await _context.SaveChangesAsync();
        var produtoResponseDTO = produto.Adapt<ProdutoResponseDTO>();
        return produtoResponseDTO;
    }

    private bool ProdutoExists(int id)
    {
        return _context.Produtos.Any(e => e.Id == id);
    }
}

