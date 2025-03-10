﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantApp.Models;
using RestaurantApp.Services;

namespace RestaurantApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IContex _context;


        public RestaurantsController(IMapper mapper, IContex context)
        {
            _mapper = mapper;
            _context = context;
        }

        // GET: api/Restaurants
        //[Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Restaurant>>> GetRestaurant()
        {
            return Ok(await _context.GetAllObjects());
        }

        // GET: api/Restaurants/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Restaurant>> GetRestaurant(int id)
        {
            var restaurant = await _context.GetObjectById(id);

            if (restaurant == null)
            {
                return NotFound();
            }

            return (ActionResult<Restaurant>)restaurant;
        }

        // PUT: api/Restaurants/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutRestaurant(int id, Restaurant restaurant)
        //{
        //    if (id != restaurant.RestId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(restaurant).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!RestaurantExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Restaurants
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPost]
        //public async Task<ActionResult<Restaurant>> PostRestaurant(Restaurant restaurant)
        //{
        //    _context.Restaurant.Add(restaurant);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetRestaurant", new { id = restaurant.RestId }, restaurant);
        //}

        // DELETE: api/Restaurants/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Restaurant>> DeleteRestaurant(int id)
        //{
        //    var restaurant = await _context.Restaurant.FindAsync(id);
        //    if (restaurant == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Restaurant.Remove(restaurant);
        //    await _context.SaveChangesAsync();

        //    return restaurant;
        //}
    }
}
