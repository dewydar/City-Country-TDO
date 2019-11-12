using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Demo.Models;
using Demo.Models.DTO_Classes;

namespace Demo.Controllers
{
    public class CitiesDtoController : ApiController
    {
       private CitiesEntities db = new CitiesEntities();
        public IQueryable<CityDetailsDTO> GetCities()
        {
            var city = db.Cities.Select(z => new CityDetailsDTO()
            {
                CityId = z.CityId,
                CityName = z.CityName,
                countriesName = z.Country.countriesName
            });
            return city;
        }
        public IHttpActionResult GetCity(int id)
        {
            var city = db.Cities.Select(z => new CityDetailsDTO()
            {
                CityId = z.CityId,
                CityName = z.CityName,
                countriesName = z.Country.countriesName
            }).SingleOrDefault(x => x.CityId == id);
            return Ok(city);
        }
        [HttpPost]
        public IHttpActionResult PostCountry(CityDetailsDTO data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Country country = new Country();
                City city = new City();
                city.CityName = data.CityName;
                country.countriesName = data.countriesName;
                db.Cities.Add(city);
                db.SaveChanges();
                db.Countries.Add(country);
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return Ok(data);
        }
        [HttpDelete]
        public IHttpActionResult DeleteCountry(int id)
        {
            Country country = db.Countries.Find(id);
            if (country == null)
            {
                return NotFound();
            }

            db.Countries.Remove(country);
            db.SaveChanges();

            return Ok(country);
        }

        [HttpPut]
        public IHttpActionResult PutCountry(Country cou)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Country country = new Country();
                country = db.Countries.Find(cou.Id);
                if (country != null)
                {
                    country.countriesName = cou.countriesName;
                }
                int i = this.db.SaveChanges();

            }
            catch (Exception)
            {
                throw;
            }
            return Ok(cou);
        }
        [HttpPut]
        public IHttpActionResult PutCity(City cy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                City city = new City();
                city = db.Cities.Find(cy.CityId);
                if (city != null)
                {
                    city.CityName = cy.CityName;
                    city.FK_countriesId = cy.FK_countriesId;
                }
                int i = this.db.SaveChanges();

            }
            catch (Exception)
            {
                throw;
            }
            return Ok(cy);
        }
    }
}
