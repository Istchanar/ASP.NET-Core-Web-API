using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;


//Температуру в запросе для краткости лучше указывать с точностью для минут 2021-09-06T13:00:11.
//Метод get вернёт значения в диапазоне температуры, метод post вставит новое значение,
//Метод put заменет значение из указанного в query диапазона, метод delete удалит температуру в указанном в query диапазоне.

namespace MetricsManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TemperatureDataController : ControllerBase
    {
        private readonly List<TemperatureData> _temperaturedata;

        public TemperatureDataController(List<TemperatureData> temperaturedata)
        {
            _temperaturedata = temperaturedata;
        }
        
        [HttpGet]
        public IEnumerable<TemperatureData> Get([FromQuery] DateTime dateOlder, [FromQuery] DateTime dateNewer)
        {
            return _temperaturedata.Where(temp => temp.Date >= dateOlder && temp.Date <= dateNewer).OrderBy(temp => temp.Date);
        }

        [HttpPost]
        public IActionResult Post([FromBody] TemperatureData temperaturedata)
        {
            if (_temperaturedata.TrueForAll(temp => temp.Date != temperaturedata.Date))
                _temperaturedata.Add(temperaturedata);

            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete([FromQuery] DateTime dateOlder, [FromQuery] DateTime dateNewer)
        {
            _temperaturedata.RemoveAll(temp => temp.Date >= dateOlder && temp.Date <= dateNewer);
            return Ok();
        }

        [HttpPut]
        public IActionResult Put([FromBody] TemperatureData temperaturedata)
        {
            for(int i = 0; i < _temperaturedata.Count; i++) 
            { 
                if (_temperaturedata[i].Date == temperaturedata.Date)
                {
                    _temperaturedata[i].TemperatureC = temperaturedata.TemperatureC;
                }
            }
            return Ok();
        }
    }
}
