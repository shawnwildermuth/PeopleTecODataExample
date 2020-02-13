using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persontec.Api.Data.Entities
{
  public class Child
  {
    private readonly IMapper _mapper;

    public Child()
    {

    }

    public Child(IMapper mapper)
    {
      _mapper = mapper;
    }

    public string Name { get; internal set; }
    public int Age { get; internal set; }
    public DateTime BirthDate { get; internal set; }
  }
}
